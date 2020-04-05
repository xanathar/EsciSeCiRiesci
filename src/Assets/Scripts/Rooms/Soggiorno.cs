using System;
using System.Collections.Generic;
using UnityEngine;

public class Soggiorno : Room
{
    public override void EnterRoom()
    {
        LogRoom("welcome");
    }

    public override IEnumerable<EntityType> AcceptedEntities()
    {
        yield return EntityType.Porta;
        yield return EntityType.Uscita;
        yield return EntityType.Quadro_1;
        yield return EntityType.Quadro_2;
        yield return EntityType.Quadro_3;
        yield return EntityType.Lampadina;
        yield return EntityType.Finestra;
        yield return EntityType.Telefono;
        yield return EntityType.Fiori;
        yield return EntityType.Libri;
        yield return EntityType.CestinoCaramelle;
        yield return EntityType.Penne;
        yield return EntityType.Password;
        yield return EntityType.PupazzoPrincipe;
        yield return EntityType.PupazzoSirenetta;
    }

    public override void ConfirmInteraction(EntityType e)
    {
        switch (e)
        {
            case EntityType.Lampadina:
                PickEntity(EntityType.Lampadina);
                break;
            case EntityType.Password:
                PickEntity(EntityType.Password);
                break;
            case EntityType.Porta:
                Travel(RoomType.Corridoio);
                break;
            case EntityType.Uscita:
                {
                    string str = string.Format("{0}{1}{2}",
                        GameState.HasPickedEntity(EntityType.Autocertificazione) ? "a" : "",
                        GameState.HasPickedEntity(EntityType.Guanti) ? "g" : "",
                        GameState.HasPickedEntity(EntityType.Mascherina) ? "m" : "");

                    if (str == "agm")
                    {
                        GameState.SetState(SpecialState.GameWon);
                        Travel(RoomType.Corridoio);
                    }
                    else
                    {
                        LogRoom("uscita" + str);
                    }
                }
                break;
            case EntityType.Quadro_3:
                if (GameState.HasPickedEntity(EntityType.Password))
                    LogEntity(EntityTextType.ConfermaInterazione, e);
                else
                    GetRoomOverlay(OverlayType.Quadro).OverlayOn();
                break;
            default:
                LogEntity(EntityTextType.ConfermaInterazione, e);
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
            default:
                PromptEntity(EntityTextType.Interagisci, e);
                break;
        }
    }

    public override void StartInventoryInteraction(EntityType inventory, EntityType e)
    {
        Prompt(GetDefaultPrompt() + " " + Texts.GetEntityText(EntityTextType.Inventario_Complemento, e));
    }
}
