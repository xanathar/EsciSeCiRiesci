using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomOverlay : MonoBehaviour
{
    bool m_State = false;
    public OverlayType WhichOverlay;

    public List<Entity> EntitiesToEnable;
    public List<Entity> EntitiesToDisable;
    public List<GameObject> GameObjectsToDeactivate;

    private void TrySyncSound()
    {
        AudioSource sfx = this.GetComponent<AudioSource>();

        if (sfx != null)
        {
            if (m_State)
                sfx.Play();
            else
                sfx.Stop();
        }
    }

    public void OverlayInit()
    {
        m_State = false;
        this.Shade(0);
        EntitiesToEnable.ForEach(e => e.DisableEntity());
    }

    public void OverlayOff()
    {
        m_State = false;
        this.Shade(0);
        EntitiesToEnable.ForEach(e => e.DisableEntity());
        EntitiesToDisable.ForEach(e => e.EnableEntity());
        TrySyncSound();
    }

    public void OverlayOn()
    {
        m_State = true;
        this.Shade(1);
        EntitiesToDisable.ForEach(e => e.DisableEntity());
        EntitiesToEnable.ForEach(e => e.EnableEntity());
        GameObjectsToDeactivate.ForEach(e => e.SetActive(false));
        TrySyncSound();
    }

    public void OverlayToggle()
    {
        if (m_State)
            OverlayOff();
        else
            OverlayOn();
    }
}
