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
        yield return EntityType.Corridoio_WC;
    }

    public override void ConfirmInteraction(EntityType e)
    {
        switch (e)
        {
            case EntityType.Corridoio_Soggiorno:
                DisableInteractions();
                GetCamminate().FirstOrDefault(c => c.Destinazione == RoomType.Soggiorno).IniziaCamminata();
                break;
            case EntityType.Corridoio_Cucina:
                DisableInteractions();
                GetCamminate().FirstOrDefault(c => c.Destinazione == RoomType.Cucina).IniziaCamminata();
                break;
            case EntityType.Corridoio_Balcone:
                DisableInteractions();
                GetCamminate().FirstOrDefault(c => c.Destinazione == RoomType.Balcone).IniziaCamminata();
                break;
            case EntityType.Corridoio_Camera:
                DisableInteractions();
                GetCamminate().FirstOrDefault(c => c.Destinazione == RoomType.Camera).IniziaCamminata();
                break;
            case EntityType.Corridoio_WC:
                GetCamminate().FirstOrDefault(c => c.Destinazione == RoomType.WC).IniziaCamminata();
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
        LogRoom("welcome");

        if (GameState.HasState(SpecialState.GameWon))
        {
            DisableInteractions();
            GetCamminate().FirstOrDefault(c => c.Destinazione == RoomType.FUOOOORIIIII).IniziaCamminata();
        }
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
