using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Corridoio : Room
{
    public override IEnumerable<EntityType> AcceptedEntities()
    {
        yield return EntityType.Corridoio_Balcone;
        yield return EntityType.Corridoio_Camera;
        yield return EntityType.Corridoio_Cucina;
        yield return EntityType.Corridoio_Soggiorno;
    }

    public override void ConfirmInteraction(EntityType e)
    {
        switch (e)
        {
            case EntityType.Corridoio_Soggiorno:
                Travel(RoomType.Soggiorno);
                break;
            case EntityType.Corridoio_Cucina:
                Travel(RoomType.Cucina);
                break;
            case EntityType.Corridoio_Balcone:
                Travel(RoomType.Balcone);
                break;
            case EntityType.Corridoio_Camera:
                Travel(RoomType.Camera);
                break;
            default:
                break;
        }
    }

    public override bool ConfirmInventoryInteraction(EntityType inventory, EntityType e)
    {
        return false;
    }

    public override void EnterRoom()
    {
        Log("Scegli dove andare");
    }

    public override RoomType GetRoomType()
    {
        return RoomType.Corridoio;
    }

    public override void StartInteraction(EntityType e)
    {
        
    }

    public override void StartInventoryInteraction(EntityType inventory, EntityType e)
    {
    }
}
