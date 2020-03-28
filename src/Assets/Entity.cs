using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Entity : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IEntity
{
    public EntityType EntityType;
    public bool IsPickable;
    public Image highlight;

    private Room m_Room;
    private RoomManager m_RoomManager;

    private void Start()
    {
        if (highlight == null)
            highlight = this.GetComponent<Image>();

        this.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        highlight.color = new Color(0, 0, 0, 0);
    }

    public void Init(RoomManager roomManager, Room room)
    {
        m_RoomManager = roomManager;
        m_Room = room;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        highlight.color = new Color(1, 1, 1, 1);

        if (m_RoomManager.Inventory.ActiveItem != EntityType.Unknown)
        {
            m_Room.StartInventoryInteraction(m_RoomManager.Inventory.ActiveItem, this);
        }
        else
        {
            m_Room.StartInteraction(this);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        highlight.color = new Color(0, 0, 0, 0);
        m_Room.StopInteraction(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (m_RoomManager.Inventory.ActiveItem != EntityType.Unknown)
        {
            bool usedUp = m_Room.ConfirmInventoryInteraction(m_RoomManager.Inventory.ActiveItem, this);
            m_RoomManager.Inventory.ClearActiveObject(usedUp);
            m_RoomManager.InteractionText.text = m_RoomManager.Inventory.GetDefaultActionInteractionText();
        }
        else
        {
            m_Room.ConfirmInteraction(this);
        }
    }

    public EntityType GetEntityType()
    {
        return EntityType;
    }

    public Sprite Pick()
    {
        if (IsPickable)
        {
            Sprite img = this.gameObject.transform.parent.GetComponent<Image>().sprite;
            this.gameObject.transform.parent.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
            return img;
        }
        else
        {
            Debug.LogError("PICKED NOT PICKABLE!!");
            return null;
        }
    }
}
