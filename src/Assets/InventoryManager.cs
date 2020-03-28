using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public InventoryObject Slot1;
    public InventoryObject Slot2;
    public InventoryObject Slot3;
    public Text InteractionText;

    InventoryObject activeObject = null;

    public void AddItem(Sprite s, EntityType et)
    {
        FindFreeSlot().Assign(s, et);
    }

    public void Backup()
    {
        GameState.InventoryItems.Clear();

        if (!Slot1.IsFree())
            GameState.InventoryItems.Add(Slot1.AsInventoryItem());
        if (!Slot2.IsFree())
            GameState.InventoryItems.Add(Slot2.AsInventoryItem());
        if (!Slot3.IsFree())
            GameState.InventoryItems.Add(Slot3.AsInventoryItem());

        Debug.LogFormat("Backup'd {0} inventory items", GameState.InventoryItems.Count);
        GameState.Dump();
    }

    public void Restore()
    {
        Slot1.Clear();
        Slot2.Clear();
        Slot3.Clear();

        foreach (var ii in GameState.InventoryItems)
        {
            AddItem(ii.Sprite, ii.EntityType);
        }

        Debug.LogFormat("Restored {0} inventory items", GameState.InventoryItems.Count);
        GameState.Dump();
    }


    private InventoryObject FindFreeSlot()
    {
        if (Slot1.IsFree())
            return Slot1;
        if (Slot2.IsFree())
            return Slot2;
        if (Slot3.IsFree())
            return Slot3;

        Debug.Log("INVENTORY IS FULL!!");
        return null;
    }

    internal void StartInteraction(InventoryObject inventoryObject)
    {
        switch (inventoryObject.GetEntityType())
        {
            case EntityType.Lampadina:
                InteractionText.text = "Usa lampadina che hai preso dal soggiorno...";
                break;
            default:
                Debug.LogError("Unknown entity!!");
                break;
        }
    }

    internal void StopInteraction(InventoryObject inventoryObject)
    {
        InteractionText.text = GetDefaultActionInteractionText();
    }

    internal void CommitInteraction(InventoryObject inventoryObject)
    {
        activeObject = inventoryObject;
        InteractionText.text = GetDefaultActionInteractionText();
    }

    public string GetDefaultActionInteractionText()
    {
        if (activeObject == null)
            return "";

        switch (activeObject.GetEntityType())
        {
            case EntityType.Lampadina:
                return "Usa lampadina con...";
            default:
                return "";
        }
    }

    public void ClearActiveObject(bool useUp)
    {
        if (activeObject != null)
        {
            if (useUp)
            {
                activeObject.Clear();
            }

            activeObject = null;
        }
    }

    public EntityType ActiveItem
    {
        get
        {
            if (activeObject != null)
            {
                return activeObject.GetEntityType();
            }
            return EntityType.Unknown;
        }
    }

    public string InventoryShortName
    {
        get
        {
            if (activeObject == null)
                return "";

            switch (activeObject.GetEntityType())
            {
                case EntityType.Lampadina:
                    return "la lampadina";
                default:
                    return "";
            }
        }
    }
}
