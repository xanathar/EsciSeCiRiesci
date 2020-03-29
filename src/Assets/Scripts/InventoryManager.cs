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

    internal Sprite GetSpriteForActiveItem()
    {
        if (ActiveItem != EntityType.Unknown)
            return m_SpriteDatabase[ActiveItem];

        return null;
    }

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
            InteractionText.text = "Non ha molto senso usare un oggetto qui";
            return;
        }

        if (this.ActiveItem == EntityType.Unknown)
        {
            switch (inventoryObject.GetEntityType())
            {
                case EntityType.Insalata:
                    InteractionText.text = "Usa vaschetta di insalata preconfezionata, marca 'Dammi Del Tu'.";
                    break;
                case EntityType.Olio:
                    InteractionText.text = "Usa la bustina di olio extravergine di oliva.";
                    break;
                case EntityType.Lattuga:
                    InteractionText.text = "Usa la lattuga.";
                    break;
                case EntityType.Aceto:
                    InteractionText.text = "Usa la bustina di aceto.";
                    break;
                case EntityType.Bicarbonato:
                    InteractionText.text = "Usa il bicarbonato di sodio.";
                    break;
                case EntityType.Chiave:
                    InteractionText.text = "Usa la chiave.";
                    break;
                case EntityType.Calamaro:
                    InteractionText.text = "Usa il... calamaro ?";
                    break;
                default:
                    Utils.Error("Oggetto sconosciuto in inventario! {0}", inventoryObject.GetEntityType());
                    break;
            }
        }
        else
        {
            switch (inventoryObject.GetEntityType())
            {
                case EntityType.Insalata:
                    InteractionText.text = this.GetDefaultActionInteractionText() + " la vaschetta di insalata";
                    break;
                case EntityType.Olio:
                    InteractionText.text = this.GetDefaultActionInteractionText() + " la bustina di olio";
                    break;
                case EntityType.Lattuga:
                    InteractionText.text = this.GetDefaultActionInteractionText() + " la lattuga";
                    break;
                case EntityType.Aceto:
                    InteractionText.text = this.GetDefaultActionInteractionText() + " la bustina di aceto";
                    break;
                case EntityType.Bicarbonato:
                    InteractionText.text = this.GetDefaultActionInteractionText() + " il bicarbonato";
                    break;
                case EntityType.Chiave:
                    InteractionText.text = this.GetDefaultActionInteractionText() + " la chiave";
                    break;
                case EntityType.Calamaro:
                    InteractionText.text = this.GetDefaultActionInteractionText() + " il calamaro";
                    break;
                default:
                    Utils.Error("Oggetto sconosciuto in inventario! {0}", inventoryObject.GetEntityType());
                    break;
            }
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

        if (this.ActiveItem != EntityType.Unknown)
        {
            if ((this.ActiveItem == EntityType.Olio || this.ActiveItem == EntityType.Aceto) && (inventoryObject.GetEntityType() == EntityType.Lattuga))
            {
                ActionLog.Log("Non voglio mangiarla, quindi non ha senso condirla.");
            }
            else
            {
                ActionLog.LogRandomFailure();
            }

            activeObject = null;
            return;
        }
        else
        {
            switch (inventoryObject.GetEntityType())
            {
                case EntityType.Insalata:
                    ActionLog.Log("Hai aperto la vaschetta di insalata.");
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
        }

        StartInteraction(inventoryObject);

        activeObject = inventoryObject;
    }


    public string GetDefaultActionInteractionText()
    {
        if (activeObject == null)
            return "";

        switch (activeObject.GetEntityType())
        {
            case EntityType.Olio:
                return "Usa la bustina di olio con";
            case EntityType.Lattuga:
                return "Usa la lattuga con";
            case EntityType.Aceto:
                return "Usa la bustina di aceto con";
            case EntityType.Bicarbonato:
                return "Usa il bicarbonato di sodio con";
            case EntityType.Chiave:
                return "Usa la chiave con";
            case EntityType.Calamaro:
                return "Usa il calamaro con";
            default:
                Utils.Error("<Manca il testo per {0}", activeObject.GetEntityType());
                return string.Format("<Manca il testo per {0}", activeObject.GetEntityType());
        }
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
