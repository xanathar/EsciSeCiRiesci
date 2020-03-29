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
        yield return EntityType.Chiave;
    }

    public override void EnterRoom()
    {
        Log("Sei in cucina");

        if (!GameState.HasState(SpecialState.RimossaMelma))
            GetRoomOverlay(OverlayType.Melma).OverlayOn();
    }

    public override void ConfirmInteraction(EntityType e)
    {
        switch (e)
        {
            case EntityType.Insalata:
                Log("Ho preso l'insalata, ma non ho nessuna intenzione di mangiarla.");
                PickEntity(EntityType.Insalata);
                break;
            case EntityType.Porta:
                Travel(RoomType.Corridoio);
                break;
            case EntityType.Frigorifero:
                GetRoomOverlay(OverlayType.Frigorifero).OverlayOn();
                Success();
                Log("Ho aperto il frigorifero");
                break;
            case EntityType.Bicarbonato:
                Log("Ho preso il bicarbonato. Ha un sacco di usi, potrebbe tornare utile.");
                PickEntity(EntityType.Bicarbonato);
                break;
            case EntityType.Calamaro:
                Log("Ho preso una cosa molliccia che credo sia un calamaro.");
                PickEntity(EntityType.Calamaro);
                break;
            case EntityType.Rubinetti:
                if (!GameState.HasState(SpecialState.RimossaMelma))
                    Log("Ci manca solo aggiungere altra acqua!");
                else
                    GetRoomOverlay(OverlayType.AcquaRubinetto).OverlayToggle();
                break;
            case EntityType.Guanti:
                Log("La confezione è sigillata e non riesco ad aprirla. Servirebbero delle forbici.");
                break;
            case EntityType.Lavandino:
                if (!GameState.HasState(SpecialState.RimossaMelma))
                    Log("Non metterei le mani lì dentro per nessun motivo!!!");
                break;
            case EntityType.Chiave:
                Log("Ho preso la chiave.");
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
            Log("E' già abbastanza melmoso così");
            return false;
        }

        if (inventory == EntityType.Aceto && e == EntityType.Lavandino)
        {
            GameState.SetState(SpecialState.MessoAceto);
            if (GameState.HasState(SpecialState.MessoBicarbonato))
            {
                Log("Il lavandino si è liberato! E c'è qualcosa sul fondo.");
                GameState.SetState(SpecialState.RimossaMelma);
                GetRoomOverlay(OverlayType.Melma).OverlayOff();
                Success();
                return true;
            }
            else
            {
                Log("Ho versato l'aceto nel lavandino");
                Success();
                return true;
            }
        }
        if (inventory == EntityType.Bicarbonato && e == EntityType.Lavandino)
        {
            GameState.SetState(SpecialState.MessoBicarbonato);
            if (GameState.HasState(SpecialState.MessoAceto))
            {
                Log("Il lavandino si è liberato! E c'è qualcosa sul fondo.");
                GameState.SetState(SpecialState.RimossaMelma);
                GetRoomOverlay(OverlayType.Melma).OverlayOff();
                Success();
                return true;
            }
            else
            {
                Log("Ho versato il bicarbonato nel lavandino");
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
            case EntityType.Insalata:
                Prompt("Prendi l'insalata");
                break;
            case EntityType.Porta:
                Prompt("Esci da questa stanza");
                break;
            case EntityType.Frigorifero:
                Prompt("Apri il frigorifero");
                break;
            case EntityType.Bicarbonato:
                Prompt("Prendi il bicarbonato di sodio");
                break;
            case EntityType.Calamaro:
                Prompt("Prendi il calamaro");
                break;
            case EntityType.Rubinetti:
                Prompt("Apri i rubinetti");
                break;
            case EntityType.Guanti:
                Prompt("Prendi i guanti");
                break;
            case EntityType.Lavandino:
                if (GameState.HasState(SpecialState.MessoBicarbonato))
                    Prompt("Libera lavandino pieno di acqua sporca e bicarbonato");
                else if (GameState.HasState(SpecialState.MessoAceto))
                    Prompt("Libera lavandino pieno di acqua sporca e aceto");
                else
                    Prompt("Libera lavandino otturato");
                break;
            case EntityType.Chiave:
                Prompt("Prendi la chiave");
                break;
            default:
                break;
        }
    }

    public override void StartInventoryInteraction(EntityType inventory, EntityType e)
    {
        string prompt = "";
        switch (e)
        {
            case EntityType.Insalata:
                prompt = ("l'insalata");
                break;
            case EntityType.Porta:
                prompt = ("la porta");
                break;
            case EntityType.Frigorifero:
                prompt = ("il frigorifero");
                break;
            case EntityType.Bicarbonato:
                prompt = ("il bicarbonato di sodio");
                break;
            case EntityType.Calamaro:
                prompt = ("il calamaro");
                break;
            case EntityType.Rubinetti:
                prompt = ("i rubinetti");
                break;
            case EntityType.Guanti:
                prompt = ("i guanti");
                break;
            case EntityType.Lavandino:
                prompt = ("il lavandino otturato");
                break;
            case EntityType.Chiave:
                prompt = ("la chiave");
                break;
            default:
                break;
        }

        Prompt(GetDefaultPrompt() + " " + prompt);
    }
}
