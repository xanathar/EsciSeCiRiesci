using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Cucina : Room
{
    public override IEnumerable<EntityType> AcceptedEntities()
    {
        yield return EntityType.Porta;
        yield return EntityType.Guanti;
        yield return EntityType.Bicarbonato;
        yield return EntityType.Lavandino;
        yield return EntityType.Rubinetti;
        yield return EntityType.Insalata;
        yield return EntityType.Calamaro;
        yield return EntityType.Frigorifero;
    }

    public override void EnterRoom()
    {
        Log("Sei in cucina");
    }

    public override void ConfirmInteraction(EntityType e)
    {
        if (e == EntityType.Insalata)
        {
            PickEntity(EntityType.Insalata);
        }

        if (e == EntityType.Porta)
            Travel(RoomType.Corridoio);

        if (e == EntityType.Frigorifero)
        {
            GetRoomOverlay(OverlayType.Frigorifero).OverlayOn();
            GetEntityObject(EntityType.Insalata).EnableEntity();
        }
    }

    public override bool ConfirmInventoryInteraction(EntityType inventory, EntityType e)
    {
        return false;
    }

    public override RoomType GetRoomType()
    {
        return RoomType.Cucina;
    }

    public override void StartInteraction(EntityType e)
    {
    }

    public override void StartInventoryInteraction(EntityType inventory, EntityType e)
    {
    }
}
