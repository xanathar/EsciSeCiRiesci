using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CucinaBuia : Room
{
    public override IEnumerable<EntityType> AcceptedEntities()
    {
        yield return EntityType.Porta;
        yield return EntityType.Guanti;
        yield return EntityType.Bicarbonato;
        yield return EntityType.Lavandino;
        yield return EntityType.Frigorifero;
        yield return EntityType.Lampadario;
    }

    public override void EnterRoom()
    {
        LogRoom("welcome");
    }

    public override void ConfirmInteraction(EntityType e)
    {
        if (e == EntityType.Porta)
        {
            Travel(RoomType.Corridoio);
        }
        else
        {
            LogRoom("confirm_" + e.ToString().ToLower());
        }
    }

    public override bool ConfirmInventoryInteraction(EntityType inventory, EntityType e)
    {
        if (inventory == EntityType.Lampadina && e == EntityType.Lampadario)
        {
            Success();
            GameState.SetState(SpecialState.CucinaIlluminata);
            Travel(RoomType.Cucina);
            return true;
        }

        LogRandomFailure();
        return false;
    }

    public override RoomType GetRoomType()
    {
        return RoomType.CucinaBuia;
    }

    public override void StartInteraction(EntityType e)
    {
        Texts.GetRoomText(RoomType.CucinaBuia, "interagisci_" + e.ToString().ToLower());
    }

    public override void StartInventoryInteraction(EntityType inventory, EntityType e)
    {
        Prompt(GetDefaultPrompt() + " " + Texts.GetRoomText(RoomType.CucinaBuia, "complemento_" + e.ToString().ToLower()));
    }
}
