﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class GameState
{
    public const RoomType START_ROOM = RoomType.Cucina;

    private static RoomType m_CurrentRoom = START_ROOM;
    private static HashSet<EntityType> m_PickedEntities = new HashSet<EntityType>();
    public static List<EntityType> InventoryItems = new List<EntityType>();

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
    }

    public static void Save()
    {
        string save = string.Format("{0}|{1}|{2}",
            (int)m_CurrentRoom,
            string.Join(";", m_PickedEntities.Select(e => ((int)e).ToString()).ToArray()),
            string.Join(";", InventoryItems.Select(e => ((int)e).ToString()).ToArray()));

        PlayerPrefs.SetString("savedata", save);
    }

    public static bool Load()
    {
        string load = PlayerPrefs.GetString("savedata", null);

        if (load == null)
            return false;

        m_PickedEntities.Clear();
        InventoryItems.Clear();

        string[] tokens = load.Split('|');
        m_CurrentRoom = (RoomType)(int.Parse(tokens[0]));
        m_PickedEntities.UnionWith(tokens[1].Split(';').Where(t => !string.IsNullOrEmpty(t)).Select(t => (EntityType)(int.Parse(t))));
        InventoryItems.AddRange(tokens[2].Split(';').Where(t => !string.IsNullOrEmpty(t)).Select(t => (EntityType)(int.Parse(t))));

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

        sb.AppendLine("Picked entities:");

        foreach (var ee in m_PickedEntities)
            sb.AppendLine("\t" + ee.ToString());

        sb.AppendLine("Inventory items:");

        foreach (var ee in InventoryItems)
            sb.AppendLine("\t" + ee.ToString());

        Debug.Log(sb.ToString());
    }
}
