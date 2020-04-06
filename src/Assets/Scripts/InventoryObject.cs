using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IAdventureDropHandler
{
    EntityType entity = EntityType.Unknown;
    public Image InventoryHighlight;
    public InventoryManager InventoryManager;
    float m_AlphaTime = float.NaN;
    bool lastCachedPositionInside = false;
    bool mouseStatusDownInside = false;

    void Start()
    {
        InventoryHighlight.Shade(0, 1);
        this.Shade(1, 0);
    }

    void Update()
    {
        if (entity == EntityType.Unknown)
        {
            this.Shade(1, 0);
        }
        else if (InventoryManager.ActiveItem == entity)
        {
            if (float.IsNaN(m_AlphaTime))
            {
                m_AlphaTime = Time.time;
            }

            float alpha = Mathf.Cos(10f*(Time.time - m_AlphaTime)) * 0.5f + 0.5f;
            this.Shade(1, alpha);
        }
        else
        {
            this.Shade(1);
            InventoryHighlight.Shade(lastCachedPositionInside ? 1 : 0, 1);
        }
    }

    internal void Assign(Sprite s, EntityType et)
    {
        this.GetComponent<Image>().overrideSprite = s;
        entity = et;
        this.GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }

    internal bool IsFree()
    {
        return entity == EntityType.Unknown;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        lastCachedPositionInside = true;
        if (entity != EntityType.Unknown && InventoryManager.ActiveItem != entity)
        {
            InventoryHighlight.Shade(1, 1);
            InventoryManager.StartInteraction(this);

            if (InventoryManager.ActiveInteractionMode == InteractionMode.DragAndDrop)
            {
                InventoryManager.CurrentDropHandler = this;
            }
        }
    }

    public void Clear()
    {
        InventoryHighlight.Shade(0, 1);
        this.Shade(1, 0);
        this.GetComponent<Image>().overrideSprite = null;
        entity = EntityType.Unknown;
    }

    public EntityType GetEntityType()
    {
        return this.entity;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        lastCachedPositionInside = false;

        if (entity != EntityType.Unknown && InventoryManager.ActiveItem != entity)
        {
            InventoryHighlight.Shade(0, 1);
            InventoryManager.StopInteraction(this);
        }

        if (entity != EntityType.Unknown && mouseStatusDownInside)
        {
            InventoryManager.CommitInteraction(this, InteractionMode.DragAndDrop);
        }

        if (object.ReferenceEquals(InventoryManager.CurrentDropHandler, this))
        {
            InventoryManager.CurrentDropHandler = null;
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (entity != EntityType.Unknown)
            InventoryManager.CommitInteraction(this);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        mouseStatusDownInside = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        mouseStatusDownInside = false;

        if (InventoryManager != null &&
            InventoryManager.ActiveInteractionMode == InteractionMode.DragAndDrop &&
            InventoryManager.ActiveItem != EntityType.Unknown &&
            InventoryManager.ActiveItem == this.GetEntityType() &&
            InventoryManager.CurrentDropHandler != null)
        {
            InventoryManager.CurrentDropHandler.ConfirmDragDropInventory();
        }

    }

    public void ConfirmDragDropInventory()
    {
        if (InventoryManager.ActiveInteractionMode == InteractionMode.DragAndDrop &&
            InventoryManager.ActiveItem != GetEntityType() &&
            InventoryManager.ActiveItem != EntityType.Unknown)
        {
            InventoryManager.CommitInteraction(this);
            InventoryManager.CurrentDropHandler = null;
        }
    }
}
