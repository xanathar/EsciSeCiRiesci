using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    EntityType entity = EntityType.Unknown;
    public Image InventoryHighlight;
    public InventoryManager InventoryManager;
    float m_AlphaTime = float.NaN;

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
        if (entity != EntityType.Unknown)
        {
            InventoryHighlight.Shade(1, 1);
            InventoryManager.StartInteraction(this);
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
        if (entity != EntityType.Unknown)
        {
            InventoryHighlight.Shade(0, 1);
            InventoryManager.StopInteraction(this);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (entity != EntityType.Unknown)
            InventoryManager.CommitInteraction(this);
    }
}
