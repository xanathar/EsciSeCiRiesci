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
        yield return EntityType.Lampadario;
        yield return EntityType.Chiave;
    }

    public override void EnterRoom()
    {
        LogRoom("welcome");

        if (!GameState.HasState(SpecialState.RimossaMelma))
            GetRoomOverlay(OverlayType.Melma).OverlayOn();
    }

    public override void ConfirmInteraction(EntityType e)
    {
        switch (e)
        {
            case EntityType.Insalata:
                PickEntity(EntityType.Insalata);
                break;
            case EntityType.Porta:
                Travel(RoomType.Corridoio);
                break;
            case EntityType.Frigorifero:
                GetRoomOverlay(OverlayType.Frigorifero).OverlayOn();
                Success();
                LogRoom("action_frigorifero");
                break;
            case EntityType.Bicarbonato:
                PickEntity(EntityType.Bicarbonato);
                break;
            case EntityType.Calamaro:
                PickEntity(EntityType.Calamaro);
                break;
            case EntityType.Rubinetti:
                if (!GameState.HasState(SpecialState.RimossaMelma))
                    LogRoom("stop_rubinetti");
                else
                    GetRoomOverlay(OverlayType.AcquaRubinetto).OverlayToggle();
                break;
            case EntityType.Guanti:
                LogRoom("stop_guanti");
                break;
            case EntityType.Lampadario:
                LogRoom("interagisci_commit_lampadario");
                break;
            case EntityType.Lavandino:
                if (!GameState.HasState(SpecialState.RimossaMelma))
                    LogRoom("stop_lavandino");
                break;
            case EntityType.Chiave:
                PickEntity(EntityType.Chiave);
                break;
            default:
                break;
        }
    }

    public override bool ConfirmInventoryInteraction(EntityType inventory, EntityType e)
    {
        if (inventory == EntityType.Olio && e == EntityType.Lavandino)
        {
            LogRoom("stop_melmoso");
            return false;
        }

        if (inventory == EntityType.Aceto && e == EntityType.Lavandino)
        {
            GameState.SetState(SpecialState.MessoAceto);
            if (GameState.HasState(SpecialState.MessoBicarbonato))
            {
                LogRoom("action_sturato");
                GameState.SetState(SpecialState.RimossaMelma);
                GetRoomOverlay(OverlayType.Melma).OverlayOff();
                Success();
                return true;
            }
            else
            {
                LogRoom("action_aceto");
                Success();
                return true;
            }
        }
        if (inventory == EntityType.Bicarbonato && e == EntityType.Lavandino)
        {
            GameState.SetState(SpecialState.MessoBicarbonato);
            if (GameState.HasState(SpecialState.MessoAceto))
            {
                LogRoom("action_sturato");
                GameState.SetState(SpecialState.RimossaMelma);
                GetRoomOverlay(OverlayType.Melma).OverlayOff();
                Success();
                return true;
            }
            else
            {
                LogRoom("action_bicarbonato");
                Success();
                return true;
            }
        }

        LogRandomFailure();
        return false;
    }

    public override RoomType GetRoomType()
    {
        return RoomType.Cucina;
    }

    public override void StartInteraction(EntityType e)
    {
        switch (e)
        {
            case EntityType.Lavandino:
                if (GameState.HasState(SpecialState.MessoBicarbonato))
                    PromptRoom("lavandino_bicarbonato");
                else if (GameState.HasState(SpecialState.MessoAceto))
                    PromptRoom("lavandino_aceto");
                else
                    PromptRoom("lavandino_base");
                break;
            default:
                Prompt(Texts.GetEntityText(EntityTextType.Interagisci, e));
                break;
        }
    }

    public override void StartInventoryInteraction(EntityType inventory, EntityType e)
    {
        Prompt(GetDefaultPrompt() + " " + Texts.GetEntityText(EntityTextType.Inventario_Complemento, e));
    }
}
