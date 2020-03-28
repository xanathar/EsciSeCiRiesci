using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    public Text InteractionText;
    public ActionLogger ActionLog;
    public InventoryManager Inventory;
    public List<RoomMarker> RoomMarkers;

    Room m_Room;
    RoomMarker m_RoomMarker;
    Dictionary<EntityType, Entity> m_Entities = new Dictionary<EntityType, Entity>();

    private void Start()
    {
        Dictionary<RoomType, RoomMarker> roomMarkersByType = RoomMarkers.ToDictionary(rm => rm.WhichRoom);
        Dictionary<RoomType, Room> roomsByType = Room.RoomLogicFactory().ToDictionary(r => r.GetRoomType());

        if (!(new HashSet<RoomType>(roomMarkersByType.Keys)).SetEquals(new HashSet<RoomType>(roomsByType.Keys)))
            Debug.LogError("Room markers and logics do not match!");

        foreach (var room in roomMarkersByType.Values.Where(rr => rr.WhichRoom != GameState.CurrentRoom))
            room.enabled = false;

        m_Room = roomsByType[GameState.CurrentRoom];
        m_RoomMarker = roomMarkersByType[GameState.CurrentRoom];

        Inventory.Restore();

        m_Room.Init(this);

        InitEntities();
    }

    private void InitEntities()
    {
        HashSet<EntityType> acceptedEntities = new HashSet<EntityType>(m_Room.AcceptedEntities());

        foreach (Entity e in m_RoomMarker.GetComponentsInChildren<Entity>())
        {
            EntityType et = e.EntityType;

            if (!acceptedEntities.Contains(et))
                Debug.LogErrorFormat("Entity {0} not supported by room {1}", et, m_Room.GetRoomType());

            if (GameState.PickedEntities.Contains(et))
            {
                e.Pick();
            }
            else
            {
                m_Entities.Add(et, e);
                e.Init(this, m_Room);
            }
        }
    }

    public static RoomManager FindInstance()
    {
        return GameObject.FindObjectOfType<RoomManager>();
    }
}
