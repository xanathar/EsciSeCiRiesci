using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Computer : Room
{
    public override IEnumerable<EntityType> AcceptedEntities()
    {
        yield return EntityType.Computer_Cestino;
        yield return EntityType.Computer_Folder;
        yield return EntityType.Computer_Documento;
        yield return EntityType.Computer_Esci;
        yield return EntityType.Computer_MsgBox_Ink_Ok;
        yield return EntityType.Computer_MsgBox_Cestino_Ok;
        yield return EntityType.Computer_Documento_Stampa;
        yield return EntityType.Computer_Documento_Chiudi;
    }

    public override void ConfirmInteraction(EntityType e) 
    {
        switch (e) 
        {
            case EntityType.Computer_Cestino:
                GetRoomOverlay(OverlayType.Computer_Errore_Cestino).OverlayOn();
                break;
            case EntityType.Computer_MsgBox_Cestino_Ok:
                GetRoomOverlay(OverlayType.Computer_Errore_Cestino).OverlayOff();
                break;
            case EntityType.Computer_Documento:
                GetRoomOverlay(OverlayType.Computer_Documento).OverlayOn();
                break;
            case EntityType.Computer_Documento_Stampa:
                GetRoomOverlay(OverlayType.Computer_Documento).OverlayOff();
                GetRoomOverlay(OverlayType.Computer_Errore_Inchiostro).OverlayOn();
                break;
            case EntityType.Computer_Documento_Chiudi:
                GetRoomOverlay(OverlayType.Computer_Documento).OverlayOff();
                break;
            case EntityType.Computer_MsgBox_Ink_Ok:
                GetRoomOverlay(OverlayType.Computer_Errore_Inchiostro).OverlayOff();
                break;
            case EntityType.Computer_Folder:
                GetRoomOverlay(OverlayType.Computer_BSOD).OverlayOn();
                StartCoroutine(BSOD());
                break;
        }
    }

    System.Collections.IEnumerator BSOD()
    {
        yield return new WaitForSeconds(5f);
        Travel(RoomType.Computer_Password);
    }

    public override bool ConfirmInventoryInteraction(EntityType inventory, EntityType e)
    {
        return false;
    }

    public override void EnterRoom()
    {
    }

    public override RoomType GetRoomType()
    {
        return RoomType.Computer;
    }

    public override void StartInteraction(EntityType e)
    {
    }

    public override void StartInventoryInteraction(EntityType inventory, EntityType e)
    {
    }
}
