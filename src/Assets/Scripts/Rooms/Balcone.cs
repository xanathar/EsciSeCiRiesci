using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balcone : Room
{
    public override void EnterRoom()
    {
        if (GameState.HasState(SpecialState.ArmadiettoAperto))
        {
            GetRoomOverlay(OverlayType.CapraRuminante).OverlayOn();
            GetRoomOverlay(OverlayType.ArmadiettoBalcone).OverlayOn();
            LogRoom("welcome");
        }
        else if (GameState.HasState(SpecialState.CapraRuminante))
        {
            GetRoomOverlay(OverlayType.CapraRuminante).OverlayOn();
            LogRoom("welcome");
        }
        else
        {
            GetRoomOverlay(OverlayType.Capra).OverlayOn();
            LogRoom("welcome_capra");
            StartCoroutine(GoatComplain());
        }
    }

    private IEnumerator GoatComplain()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        PlaySound("goat");
    }

    public override IEnumerable<EntityType> AcceptedEntities()
    {
        yield return EntityType.Porta;
        yield return EntityType.Scala;
        yield return EntityType.Forbici;
        yield return EntityType.Capra;
        yield return EntityType.CapraRuminante;
        yield return EntityType.Serratura;
    }

    public override void ConfirmInteraction(EntityType e)
    {
        switch (e)
        {
            case EntityType.Capra:
            case EntityType.CapraRuminante:
                PlaySound("goat");
                LogEntity(EntityTextType.ConfermaInterazione, e);
                break;
            case EntityType.Forbici:
                PickEntity(EntityType.Forbici);
                break;
            case EntityType.Porta:
                Travel(RoomType.Corridoio);
                break;
            case EntityType.Scala:
                if (GameState.HasState(SpecialState.CapraRuminante))
                {
                    if (GameState.HasState(SpecialState.ScalaOliata))
                        PickEntity(EntityType.Scala);
                    else
                        LogRoom("scala_ruggine");
                }
                else
                {
                    LogEntity(EntityTextType.ConfermaInterazione, e);
                    PlaySound("goat");
                }
                break;
            case EntityType.Serratura:
                if (GameState.HasState(SpecialState.CapraRuminante))
                {
                    LogEntity(EntityTextType.ConfermaInterazione, EntityType.Serratura);
                }
                else
                {
                    LogEntity(EntityTextType.ConfermaInterazione, EntityType.Capra);
                    PlaySound("goat");
                }
                break;
            default:
                LogEntity(EntityTextType.ConfermaInterazione, e);
                break;
        }
    }

    public override bool ConfirmInventoryInteraction(EntityType inventory, EntityType e)
    {
        if (inventory == EntityType.Lattuga && e == EntityType.Capra)
        {
            GameState.SetState(SpecialState.CapraRuminante);
            GetRoomOverlay(OverlayType.Capra).OverlayOff();
            GetRoomOverlay(OverlayType.CapraRuminante).OverlayOn();
            Success();
            return true;
        }

        if (e == EntityType.Capra)
        {
            LogRandomFailure();
            return false;
        }

        if (!GameState.HasState(SpecialState.CapraRuminante))
        {
            LogRoom("fail_capra");
            PlaySound("goat");
            return false;
        }

        if (inventory == EntityType.Chiave && e == EntityType.Serratura)
        {
            GameState.SetState(SpecialState.ArmadiettoAperto);
            GetRoomOverlay(OverlayType.ArmadiettoBalcone).OverlayOn();
            Success();
            return true;
        }

        if (inventory == EntityType.Olio && e == EntityType.Scala)
        {
            GameState.SetState(SpecialState.ScalaOliata);
            LogRoom("scala_olio");
            Success();
            return true;
        }

        if (inventory == EntityType.Forbici && e == EntityType.CapraRuminante)
        {
            LogRoom("kill_capra");
            return false;
        }

        LogRandomFailure();
        return false;
    }

    public override RoomType GetRoomType()
    {
        return RoomType.Balcone;
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
