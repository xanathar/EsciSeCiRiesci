using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OverlayType
{
    Frigorifero,
    Computer_Documento,
    Computer_Errore_Inchiostro,
    Computer_Errore_Cestino,
    Computer_BSOD,
    Melma,
}

public class RoomOverlay : MonoBehaviour
{
    public OverlayType WhichOverlay;

    public List<Entity> EntitiesToEnable;
    public List<Entity> EntitiesToDisable;
    public List<GameObject> GameObjectsToDeactivate;

    public void OverlayInit()
    {
        this.Shade(0);
        EntitiesToEnable.ForEach(e => e.DisableEntity());
    }

    public void OverlayOff()
    {
        this.Shade(0);
        EntitiesToEnable.ForEach(e => e.DisableEntity());
        EntitiesToDisable.ForEach(e => e.EnableEntity());
    }

    public void OverlayOn()
    {
        this.Shade(1);
        EntitiesToDisable.ForEach(e => e.DisableEntity());
        EntitiesToEnable.ForEach(e => e.EnableEntity());
        GameObjectsToDeactivate.ForEach(e => e.SetActive(false));
    }
}
