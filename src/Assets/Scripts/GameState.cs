using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public static class GameState
{
    public const RoomType START_ROOM = RoomType.Soggiorno;

    private static RoomType m_CurrentRoom = START_ROOM;
    private static HashSet<EntityType> m_PickedEntities = new HashSet<EntityType>();
    public static List<EntityType> InventoryItems = new List<EntityType>();
    private static HashSet<SpecialState> m_SpecialStates = new HashSet<SpecialState>();

    public static RoomType CurrentRoom
    {
        get { return m_CurrentRoom; }
        set
        {
            if (value != m_CurrentRoom)
            {
                m_CurrentRoom = value;
                Save();
            }
        }
    }

    public static bool HasState(SpecialState s)
    {
        return m_SpecialStates.Contains(s);
    }

    public static void SetState(SpecialState s)
    {
        m_SpecialStates.Add(s);
    }

    public static bool HasPickedEntity(EntityType e)
    {
        return m_PickedEntities.Contains(e);
    }

    public static void AddPickedEntity(EntityType e)
    {
        m_PickedEntities.Add(e);
    }
     
    public static void ResetNew()
    {
        m_CurrentRoom = START_ROOM;
        m_PickedEntities.Clear();
        InventoryItems.Clear();
        m_SpecialStates.Clear();
        Save();
    }

    public static void Save()
    {
        I64StreamEncoder enc = new I64StreamEncoder();

        enc.Append((long)m_CurrentRoom);
        enc.AppendChunk(m_PickedEntities.Select(e => (long)e));
        enc.AppendChunk(InventoryItems.Select(e => (long)e));
        enc.AppendChunk(m_SpecialStates.Select(e => (long)e));

        string save = enc.ToString();

        PlayerPrefs.SetString("savedata", save);
        Debug.Log("Loaded data : " + save);
    }

    public static bool Load()
    {
        string load = PlayerPrefs.GetString("savedata", null);

        Debug.Log("Loaded data : " + load);

        if (load == null)
            return false;

        m_PickedEntities.Clear();
        InventoryItems.Clear();
        m_SpecialStates.Clear();

        I64StreamDecoder dec = new I64StreamDecoder(load);

        m_CurrentRoom = (RoomType)dec.DecodeNext().Value;
        m_PickedEntities.UnionWith(dec.DecodeChunk().Select(v => (EntityType)v));
        InventoryItems.AddRange(dec.DecodeChunk().Select(v => (EntityType)v));
        m_SpecialStates.UnionWith(dec.DecodeChunk().Select(v => (SpecialState)v));

        return true;
    }

    public static bool HasSave()
    {
        return PlayerPrefs.HasKey("savedata");
    }


    public static void Dump()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("Current room = " + m_CurrentRoom.ToString());

        sb.AppendLine("Picked entities: " + string.Join(",", m_PickedEntities.Select(e => e.ToString()).ToArray()));
        sb.AppendLine("Inventory items: " + string.Join(",", InventoryItems.Select(e => e.ToString()).ToArray()));
        sb.AppendLine("Special states: " + string.Join(",", m_SpecialStates.Select(e => e.ToString()).ToArray()));

        Debug.Log(sb.ToString());
    }
}
