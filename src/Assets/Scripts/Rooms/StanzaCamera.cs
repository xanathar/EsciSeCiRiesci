using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StanzaCamera : Room
{
    public override void EnterRoom()
    {
        LogRoom("welcome");

        if (GameState.HasState(SpecialState.AutocertificazioneStampata) &&
            (!GameState.HasPickedEntity(EntityType.Autocertificazione)))
        {
            GetRoomOverlay(OverlayType.AutocertificazioneNellaStampante).OverlayOn();
        }
    }

    public override IEnumerable<EntityType> AcceptedEntities()
    {
        yield return EntityType.Porta;
        yield return EntityType.Computer;
        yield return EntityType.Mascherina;
        yield return EntityType.Stampante;
        yield return EntityType.Ciabatte;
        yield return EntityType.Orsacchiotto;
        yield return EntityType.Pallone1;
        yield return EntityType.Pallone2;
        yield return EntityType.TelecomandoMacchinina;
        yield return EntityType.Libri_Studio;
        yield return EntityType.Foto;
        yield return EntityType.Tazza;
        yield return EntityType.Fiori;
    }

    public override void ConfirmInteraction(EntityType e)
    {
        switch (e)
        {
            case EntityType.Porta:
                Travel(RoomType.Corridoio);
                break;
            case EntityType.Stampante:
                if (GameState.HasState(SpecialState.AutocertificazioneStampata))
                {
                    if (!GameState.HasPickedEntity(EntityType.Autocertificazione))
                    {
                        Inventory.AddItem(EntityType.Autocertificazione);
                        GameState.AddPickedEntity(EntityType.Autocertificazione);
                        GetRoomOverlay(OverlayType.AutocertificazioneNellaStampante).OverlayOff();
                        LogRoom("preso_autocertificazione");
                    }
                    else
                    {
                        LogRoom("stampante_inutile");
                    }
                }
                else if (GameState.HasState(SpecialState.MessoInchiostro))
                {
                    LogRoom("stampante_pronta");
                }
                else
                {
                    LogRoom("stampante_inchiostro");
                }
                break;
            case EntityType.TelecomandoMacchinina:
                GetMacchinina().AccendiMacchinina();
                break;
            case EntityType.Computer:
                Travel(RoomType.Computer_Password);
                break;
            default:
                LogEntity(EntityTextType.ConfermaInterazione, e);
                break;
        }
    }

    public override bool ConfirmInventoryInteraction(EntityType inventory, EntityType e)
    {
        if (inventory == EntityType.CalamaroInchiostro && e == EntityType.Stampante)
        {
            GameState.SetState(SpecialState.MessoInchiostro);
            LogRoom("messo_inchiostro");
            return true;
        }

        if (inventory == EntityType.Scala && e == EntityType.Mascherina)
        {
            PickEntity(e);
            return true;
        }

        if (inventory == EntityType.Password && e == EntityType.Computer)
        {
            Travel(RoomType.Computer);
            return false;
        }

        LogRandomFailure();
        return false;
    }

    public override RoomType GetRoomType()
    {
        return RoomType.Camera;
    }

    public override void StartInteraction(EntityType e)
    {
        switch (e)
        {
            case EntityType.Stampante:
                if (GameState.HasState(SpecialState.AutocertificazioneStampata))
                {
                    if (!GameState.HasPickedEntity(EntityType.Autocertificazione))
                    {
                        PromptEntity(EntityTextType.Interagisci, EntityType.Autocertificazione);
                    }
                    else
                    {
                        PromptEntity(EntityTextType.Interagisci, EntityType.Stampante);
                    }
                }
                else
                {
                    PromptEntity(EntityTextType.Interagisci, EntityType.Stampante);
                }
                break;
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
