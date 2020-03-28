using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    const int EXPECTED_INVENTORY_SLOTS = 10;

    public Text InteractionText;

    List<InventoryObject> inventorySlots;

    InventoryObject activeObject = null;

    public void Awake()
    {
        inventorySlots = this.gameObject.GetComponentsInChildren<InventoryObject>().ToList();

        if (inventorySlots.Count != EXPECTED_INVENTORY_SLOTS)
            Debug.LogErrorFormat("Found {0} inventory slots instead of {1}", inventorySlots.Count, 10);
    }



    public void AddItem(Sprite s, EntityType et)
    {
        FindFreeSlot().Assign(s, et);
    }

    public void Backup()
    {
        GameState.InventoryItems.Clear();

        for (int i = 0; i < inventorySlots.Count; i++)
            GameState.InventoryItems.Add(inventorySlots[i].AsInventoryItem());

        Debug.LogFormat("Backup'd {0} inventory items", GameState.InventoryItems.Count);
        GameState.Dump();
    }

    public void Restore()
    {
        for (int i = 0; i < inventorySlots.Count; i++)
            inventorySlots[i].Clear();

        foreach (var ii in GameState.InventoryItems)
        {
            AddItem(ii.Sprite, ii.EntityType);
        }

        Debug.LogFormat("Restored {0} inventory items", GameState.InventoryItems.Count);
        GameState.Dump();
    }


    private InventoryObject FindFreeSlot()
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i].IsFree())
                return inventorySlots[i];
        }

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
