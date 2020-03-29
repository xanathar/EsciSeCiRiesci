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

    public GameObject inventoryPlaceholderRoot;

    List<InventoryObject> inventorySlots;

    InventoryObject activeObject = null;

    Dictionary<EntityType, Sprite> m_SpriteDatabase;


    public void Awake()
    {
        inventorySlots = this.gameObject.GetComponentsInChildren<InventoryObject>().ToList();

        if (inventorySlots.Count != EXPECTED_INVENTORY_SLOTS)
            Debug.LogErrorFormat("Found {0} inventory slots instead of {1}", inventorySlots.Count, 10);
    }

    public void Start()
    {
        m_SpriteDatabase = inventoryPlaceholderRoot
            .GetComponentsInChildren<InventoryPlaceholder>()
            .Where(p => p.Item != EntityType.Unknown)
            .ToDictionary(p => p.Item, p => p.GetComponent<Image>().sprite);

        Restore();
    }


    public void AddItem(EntityType et)
    {
        FindFreeSlot().Assign(m_SpriteDatabase[et], et);
    }

    public void Backup()
    {
        GameState.InventoryItems.Clear();

        for (int i = 0; i < inventorySlots.Count; i++)
            GameState.InventoryItems.Add(inventorySlots[i].GetEntityType());

        Debug.LogFormat("Backup'd {0} inventory items", GameState.InventoryItems.Count);
        GameState.Dump();
    }

    private void Restore()
    {
        for (int i = 0; i < inventorySlots.Count; i++)
            inventorySlots[i].Clear();

        foreach (var ii in GameState.InventoryItems)
        {
            if (ii != EntityType.Unknown)
                AddItem(ii);
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
        if (GameState.CurrentRoom == RoomType.Corridoio || GameState.CurrentRoom == RoomType.Computer)
        {
            InteractionText.text = "Non ha molto senso usare un oggetto qui";
            return;
        }


        switch (inventoryObject.GetEntityType())
        {
            case EntityType.Insalata:
                InteractionText.text = "Usa vaschetta di insalata preconfezionata, marca 'Dammi Del Tu'.";
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
        if (GameState.CurrentRoom == RoomType.Corridoio || GameState.CurrentRoom == RoomType.Computer)
        {
            InteractionText.text = "Non ha molto senso usare un oggetto qui";
            return;
        }

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
