using System;
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

    private float targetAlphaColor = 0f;
    private float currentAlphaColor = 0f;

    private bool m_Enabled = true;
    private bool m_StateChecked = false;

    private void Awake()
    {
        //this.Shade(1);
        targetAlphaColor = 0f;
        currentAlphaColor = 0f;
    }

    public bool IsEnabledEntity()
    {
        return m_Enabled && (m_RoomManager == null || m_RoomManager.InteractionsEnabled);
    }

    public void DisableEntity()
    {
        this.Shade(0);

        if (highlight != null)
            highlight.Shade(0);

        m_Enabled = false;
    }

    public void EnableEntity()
    {
        this.Shade(1);
        highlight.Shade(1, 0);
        m_Enabled = true;
    }

    private void Start()
    {
        if (highlight == null)
            highlight = this.GetComponent<Image>();

        highlight.Shade(1, 0);
    }

    private void Update()
    {
        if (!m_StateChecked)
        {
            m_StateChecked = true;

            Utils.Assert(m_RoomManager != null, "Room manager is null in entity {0}", this.GetEntityType());
        }

        if (IsEnabledEntity())
        {
            if (currentAlphaColor < targetAlphaColor)
            {
                currentAlphaColor = Math.Min(targetAlphaColor, currentAlphaColor + Time.deltaTime * 4f);
                highlight.Shade(1, currentAlphaColor);
            }
            else if (currentAlphaColor > targetAlphaColor)
            {
                currentAlphaColor = Math.Max(targetAlphaColor, currentAlphaColor - Time.deltaTime * 4f);
                highlight.Shade(1, currentAlphaColor);
            }
        }
    }




    public void Init(RoomManager roomManager, Room room)
    {
        Debug.LogFormat("Entity {0} Initialized!!", this.EntityType);

        m_RoomManager = roomManager;
        m_Room = room;
 
        Utils.Assert(m_RoomManager != null, "Entity {0} has null room manager", this.EntityType);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetAlphaColor = 1f;

        if (IsEnabledEntity())
        {
            if (m_RoomManager.Inventory.ActiveItem != EntityType.Unknown)
            {
                m_Room.StartInventoryInteraction(m_RoomManager.Inventory.ActiveItem, this.GetEntityType());
            }
            else
            {
                m_Room.StartInteraction(this.GetEntityType());
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetAlphaColor = 0f;

        if (IsEnabledEntity())
            m_Room.StopInteraction(this.GetEntityType());
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (IsEnabledEntity())
        {
            if (m_RoomManager.Inventory.ActiveItem != EntityType.Unknown)
            {
                bool usedUp = m_Room.ConfirmInventoryInteraction(m_RoomManager.Inventory.ActiveItem, this.GetEntityType());
                m_RoomManager.Inventory.ClearActiveObject(usedUp);
                m_RoomManager.InteractionText.text = m_RoomManager.Inventory.GetDefaultActionInteractionText();
            }
            else
            {
                m_Room.ConfirmInteraction(this.GetEntityType());
            }
        }
    }

    public EntityType GetEntityType()
    {
        return EntityType;
    }

    public Sprite Pick()
    {
        if (IsEnabledEntity())
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
            }
        }
        return null;
    }
}
