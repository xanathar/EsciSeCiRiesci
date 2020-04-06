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
    public ActionLogger ActionLog;

    public GameObject inventoryPlaceholderRoot;

    List<InventoryObject> inventorySlots;

    public IAdventureDropHandler CurrentDropHandler;

    internal Sprite GetSpriteForActiveItem()
    {
        if (ActiveItem != EntityType.Unknown)
            return m_SpriteDatabase[ActiveItem];

        return null;
    }

    InventoryObject activeObject = null;
    private InteractionMode activeInteractionMode = InteractionMode.Classic;
    Dictionary<EntityType, Sprite> m_SpriteDatabase;

    public InteractionMode ActiveInteractionMode { get { return activeInteractionMode; } }


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
            InteractionText.text = Texts.Get("inventario_disabilitato");
            return;
        }

        if (this.ActiveItem == EntityType.Unknown)
        {
            InteractionText.text = Texts.GetEntityText(EntityTextType.Inventario_Usa, inventoryObject.GetEntityType());
        }
        else
        {
            InteractionText.text = this.GetDefaultActionInteractionText() + Texts.GetEntityText(EntityTextType.Inventario_Complemento, inventoryObject.GetEntityType());
        }
    }

    internal void StopInteraction(InventoryObject inventoryObject)
    {
        InteractionText.text = GetDefaultActionInteractionText();
    }

    internal void CommitInteraction(InventoryObject inventoryObject, InteractionMode interactionMode = InteractionMode.Classic)
    {
        if (GameState.CurrentRoom == RoomType.Corridoio || GameState.CurrentRoom == RoomType.Computer)
        {
            InteractionText.text = Texts.Get("inventario_disabilitato");
            return;
        }

        if (this.ActiveItem != EntityType.Unknown)
        {
            if ((this.ActiveItem == EntityType.Forbici && inventoryObject.GetEntityType() == EntityType.Calamaro)) // ||
                //(this.ActiveItem == EntityType.Calamaro && inventoryObject.GetEntityType() == EntityType.Forbici))
            {
                ActionLog.Log(Texts.Get("calamaro_tagliato"));
                inventoryObject.Clear();
                this.AddItem(EntityType.CalamaroInchiostro);
                this.Backup();
                GameState.Save();
                InteractionText.text = "";
            }

            if ((this.ActiveItem == EntityType.Olio || this.ActiveItem == EntityType.Aceto) && (inventoryObject.GetEntityType() == EntityType.Lattuga))
            {
                ActionLog.Log(Texts.Get("insalata_nocondimento"));
            }
            else
            {
                ActionLog.LogRandomFailure();
            }

            activeObject = null;
        }
        else
        {
            switch (inventoryObject.GetEntityType())
            {
                case EntityType.Insalata:
                    ActionLog.Log(Texts.Get("insalata_aperta"));
                    inventoryObject.Clear();
                    this.AddItem(EntityType.Olio);
                    this.AddItem(EntityType.Aceto);
                    this.AddItem(EntityType.Lattuga);
                    this.Backup();
                    GameState.Save();
                    InteractionText.text = "";
                    return;
                default:
                    break;
            }

            StartInteraction(inventoryObject);
            activeObject = inventoryObject;
            this.activeInteractionMode = interactionMode;
        }
    }


    public string GetDefaultActionInteractionText()
    {
        if (activeObject == null)
            return "";

        return Texts.GetEntityText(EntityTextType.Inventario_Soggetto, activeObject.GetEntityType());
    }

    public void ClearActiveObject(bool useUp)
    {
        if (activeObject != null)
        {
            if (useUp)
            {
                activeObject.Clear();
                Backup();
                GameState.Save();
            }

            activeObject = null;
            activeInteractionMode = InteractionMode.Classic;
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

}
