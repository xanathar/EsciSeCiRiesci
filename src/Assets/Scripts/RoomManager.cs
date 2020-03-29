using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    public Text InteractionText;
    public ActionLogger ActionLog;
    public InventoryManager Inventory;
    public Image TravelFader;
    public SoundFX SoundFX;

    Room m_Room;
    RoomMarker m_RoomMarker;
    Dictionary<EntityType, Entity> m_Entities = new Dictionary<EntityType, Entity>();
    Dictionary<OverlayType, RoomOverlay> m_Overlays = null;

    private void Start()
    {
        TravelFader.transform.localPosition = new Vector3(-124, TravelFader.transform.localPosition.y, TravelFader.transform.localPosition.z);

        Dictionary<RoomType, RoomMarker> roomMarkersByType = this.gameObject.GetComponentsInChildren<RoomMarker>().ToDictionary(rm => rm.WhichRoom);
        Dictionary<RoomType, Room> roomsByType = Room.RoomLogicFactory().ToDictionary(r => r.GetRoomType());

        if (!(new HashSet<RoomType>(roomMarkersByType.Keys)).SetEquals(new HashSet<RoomType>(roomsByType.Keys)))
            Debug.LogError("Room markers and logics do not match!");

        foreach (var room in roomMarkersByType.Values.Where(rr => rr.WhichRoom != GameState.CurrentRoom))
            room.gameObject.SetActive(false);

        m_Room = roomsByType[GameState.CurrentRoom];
        m_RoomMarker = roomMarkersByType[GameState.CurrentRoom];

        m_Room.Init(this);

        InitEntities();

        InteractionText.text = "";

        foreach (var ovl in m_Overlays.Values)
            ovl.OverlayInit();

        m_Room.EnterRoom();

        StartCoroutine(FadeEnter());
    }

    public bool InteractionsEnabled = true;

    IEnumerator FadeEnter()
    {
        for (float alpha = 1f; alpha > 0f; alpha -= Time.deltaTime * 2f)
        {
            TravelFader.Shade(alpha);
            yield return null;
        }
        TravelFader.Shade(0f);
    }

    IEnumerator FadeExit()
    {
        InteractionsEnabled = false;

        for (float alpha = 0f; alpha < 1f; alpha += Time.deltaTime * 2f)
        {
            TravelFader.Shade(alpha);
            yield return null;
        }
        TravelFader.Shade(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    private void InitEntities()
    {
        HashSet<EntityType> acceptedEntities = new HashSet<EntityType>(m_Room.AcceptedEntities());

        m_Overlays = m_RoomMarker.GetComponentsInChildren<RoomOverlay>().ToDictionary(ro => ro.WhichOverlay);

        foreach (Entity e in m_RoomMarker.GetComponentsInChildren<Entity>())
        {
            EntityType et = e.EntityType;

            if (!acceptedEntities.Contains(et))
                Debug.LogErrorFormat("Entity {0} not supported by room {1}", et, m_Room.GetRoomType());

            if (GameState.PickedEntities.Contains(et))
            {
                e.DisableEntityPermanently();
            }

            if (m_Entities.ContainsKey(et))
            {
                Utils.Error("Duplicated entity {0} in room {1}", et, m_Room.GetRoomType());
            }
            else
            {
                m_Entities.Add(et, e);
            }
            e.Init(this, m_Room);
        }
    }

    internal void StartTravel()
    {
        StartCoroutine(FadeExit());
    }

    public static RoomManager FindInstance()
    {
        return GameObject.FindObjectOfType<RoomManager>();
    }

    public Entity GetEntityObject(EntityType entityType)
    {
        Entity value;
        if (m_Entities.TryGetValue(entityType, out value))
        {
            return value;
        }

        Utils.Error("Cannot find {0} in entity map", entityType);

        return null;
    }

    public RoomOverlay GetRoomOverlay(OverlayType overlayType)
    {
        RoomOverlay value;
        if (m_Overlays.TryGetValue(overlayType, out value))
        {
            return value;
        }

        Utils.Error("Cannot find {0} in overlay map", overlayType);

        return null;
    }
}
