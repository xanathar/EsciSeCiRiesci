using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Camminata : MonoBehaviour
{
    public RoomType Destinazione;
    public Passetto PassettoStatico0;
    public Passetto PassettoStatico1;


    Passetto[] m_passetti;
    RoomManager roomManager;

    bool started = false;
    int index = 0;

    void Start()
    {
        m_passetti = GetComponentsInChildren<Passetto>().OrderBy(p => p.IndicePassetto).ToArray();
    }

    public void Init(RoomManager rm)
    {
        roomManager = rm;
    }

    public void IniziaCamminata()
    {
        GameState.CurrentRoom = Destinazione;
        roomManager.Inventory.Backup();

        StartCoroutine(CamminataAnim());
    }

    private IEnumerator CamminataAnim()
    {
        var sfx = this.GetComponent<AudioSource>();
        var rnd = new System.Random();

        for (int i = 0; i < m_passetti.Length; i++)
        {
            if (i > m_passetti.Length - 2)
                roomManager.StartTravel();

            if (i >= 2)
                m_passetti[i - 2].FadeOff();
            else if (i == 1)
                PassettoStatico1.FadeOff();
            else if (i == 0)
                PassettoStatico0.FadeOff();

            m_passetti[i].FadeIn();

            sfx.pitch = ((float)(rnd.NextDouble() * 0.4 + 0.8));
            yield return new WaitForSecondsRealtime(0.1f);
            sfx.Play();
            yield return new WaitForSecondsRealtime(0.4f);
        }
    }
}
