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

        GetRoomOverlay(OverlayType.Frigorifero).Shade(0);
        GetEntityObject(EntityType.Insalata).DisableEntity();
        GetEntityObject(EntityType.Calamaro).DisableEntity();
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
            GetEntityObject(EntityType.Frigorifero).DisableEntity();
            GetEntityObject(EntityType.Frigorifero).gameObject.SetActive(false);
            GetRoomOverlay(OverlayType.Frigorifero).Shade(1);
            GetEntityObject(EntityType.Insalata).EnableEntity();
            GetEntityObject(EntityType.Calamaro).EnableEntity();
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
