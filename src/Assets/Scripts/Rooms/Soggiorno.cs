
using System;
using System.Collections.Generic;
using UnityEngine;

public class Soggiorno : Room
{
    public Soggiorno()
    {
    }
    public override void EnterRoom()
    {
        Log("Scegli dove andare");
    }


    public override IEnumerable<EntityType> AcceptedEntities()
    {
        yield return EntityType.Giornali;
        yield return EntityType.Telefono;
        yield return EntityType.Scrigno;
        yield return EntityType.Lampadina;
        yield return EntityType.Porta;
    }

    public override void ConfirmInteraction(EntityType e)
    {
        switch (e)
        {
            case EntityType.Giornali:
                Log("Non è il momento di leggere!");
                break;
            case EntityType.Telefono:
                Log("Non sapresti a chi telefonare adesso.");
                break;
            case EntityType.Scrigno:
                Log("E' chiuso con un lucchetto.");
                break;
            case EntityType.Lampadina:
                Log("Hai preso la lampadina.");
                PickEntity(e);
                break;
            case EntityType.Porta:
                Travel(RoomType.Soggiorno);
                break;
            default:
                Debug.LogErrorFormat("Unknown entity : {0}", e);
                break;
        }
    }

    public override bool ConfirmInventoryInteraction(EntityType inventory, EntityType e)
    {
        LogRandomFailure();
        return false;
    }

    public override RoomType GetRoomType()
    {
        return RoomType.Soggiorno;
    }

    public override void StartInteraction(EntityType e)
    {
        switch (e)
        {
            case EntityType.Giornali:
                Prompt("Vedi un mucchio di giornali");
                break;
            case EntityType.Telefono:
                Prompt("Vedi un telefono");
                break;
            case EntityType.Scrigno:
                Prompt("Vedi uno strano scrigno");
                break;
            case EntityType.Lampadina:
                Prompt("Vedi una lampadina di ricambio");
                break;
            case EntityType.Porta:
                Prompt("Vedi una porta", Inventory.InventoryShortName);
                break;
            default:
                Debug.LogErrorFormat("Unknown entity : {0}", e);
                break;
        }
    }

    public override void StartInventoryInteraction(EntityType inventory, EntityType e)
    {
        switch (e)
        {
            case EntityType.Giornali:
                Prompt("Usa {0} con i giornali", Inventory.InventoryShortName);
                break;
            case EntityType.Telefono:
                Prompt("Usa {0} con il telefono", Inventory.InventoryShortName);
                break;
            case EntityType.Scrigno:
                Prompt("Usa {0} con lo strano scrigno", Inventory.InventoryShortName);
                break;
            case EntityType.Porta:
                Prompt("Usa {0} con la porta", Inventory.InventoryShortName);
                break;
            case EntityType.Lampadina:
                Debug.LogErrorFormat("Unknown entity : {0}", e);
                break;
            default:
                Debug.LogErrorFormat("Unknown entity : {0}", e);
                break;
        }
    }
}
