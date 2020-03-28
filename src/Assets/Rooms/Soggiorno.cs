
using System;
using System.Collections.Generic;
using UnityEngine;

public class Soggiorno : Room
{
    public override IEnumerable<EntityType> AcceptedEntities()
    {
        yield return EntityType.Giornali;
        yield return EntityType.Telefono;
        yield return EntityType.Scrigno;
        yield return EntityType.Lampadina;
        yield return EntityType.PortaStanza;
    }

    public override void ConfirmInteraction(IEntity e)
    {
        switch (e.GetEntityType())
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
            case EntityType.PortaStanza:
                Travel(RoomType.Soggiorno);
                break;
            default:
                Debug.LogErrorFormat("Unknown entity : {0}", e.GetEntityType());
                break;
        }
    }

    public override bool ConfirmInventoryInteraction(EntityType inventory, IEntity e)
    {
        LogRandomFailure();
        return false;
    }

    public override RoomType GetRoomType()
    {
        return RoomType.Soggiorno;
    }

    public override void StartInteraction(IEntity e)
    {
        switch (e.GetEntityType())
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
            case EntityType.PortaStanza:
                Prompt("Vedi una porta", Inventory.InventoryShortName);
                break;
            default:
                Debug.LogErrorFormat("Unknown entity : {0}", e.GetEntityType());
                break;
        }
    }

    public override void StartInventoryInteraction(EntityType inventory, IEntity e)
    {
        switch (e.GetEntityType())
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
            case EntityType.PortaStanza:
                Prompt("Usa {0} con la porta", Inventory.InventoryShortName);
                break;
            case EntityType.Lampadina:
                Debug.LogErrorFormat("Unknown entity : {0}", e.GetEntityType());
                break;
            default:
                Debug.LogErrorFormat("Unknown entity : {0}", e.GetEntityType());
                break;
        }
    }
}
