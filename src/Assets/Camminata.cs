using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Camminata : MonoBehaviour
{
    public RoomType Destinazione;
    public Passetto PassettoStatico0;
    public Passetto PassettoStatico1;
    public CoronaVirus CovidFelice;
    public CoronaVirus CovidStizzito;
    public Passetto[] PassettiDaNascondere;

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
        if (Destinazione == RoomType.Cucina)
        {
            Destinazione = GameState.HasState(SpecialState.CucinaIlluminata) ? RoomType.Cucina : RoomType.CucinaBuia;
            GameState.CurrentRoom = Destinazione;
            roomManager.Inventory.Backup();
            StartCoroutine(CamminataAnim());
        }
        else if (Destinazione == RoomType.WC)
        {
            StartCoroutine(CamminataAnimWC());
        }
        else if (Destinazione == RoomType.FUOOOORIIIII)
        {
            foreach (var p in PassettiDaNascondere)
                p.Shade(1f, 0f);

            PassettoStatico0.Shade(1f);
            PassettoStatico1.Shade(1f);
            StartCoroutine(CamminataFuori());
        }
        else
        {
            GameState.CurrentRoom = Destinazione;
            roomManager.Inventory.Backup();
            StartCoroutine(CamminataAnim());
        }
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

    private IEnumerator CamminataAnimWC()
    {
        var sfx = this.GetComponent<AudioSource>();
        var rnd = new System.Random();

        for (int i = 0; i <= 7; i++)
        {
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

        m_passetti[6].FadeOff();
        m_passetti[7].FadeOff();
        m_passetti[8].FadeIn();
        m_passetti[9].FadeIn();

        yield return new WaitForSecondsRealtime(1);
        roomManager.SoundFX.Play("wc");
        yield return new WaitForSecondsRealtime(7);

        for (int i = 10; i <= 11; i++)
        {
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
        roomManager.SoundFX.Play("tap2");
        yield return new WaitForSecondsRealtime(3.4f);

        for (int i = 12; i < m_passetti.Length; i++)
        {
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

        m_passetti[m_passetti.Length - 1].FadeOff();
        m_passetti[m_passetti.Length - 2].FadeOff();
        PassettoStatico1.FadeIn();
        PassettoStatico0.FadeIn();
    }


    private IEnumerator CamminataFuori()
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

            if (i == 6)
            {
                CovidFelice.FadeOff();
                CovidStizzito.FadeIn();
            }

            m_passetti[i].FadeIn();

            sfx.pitch = ((float)(rnd.NextDouble() * 0.4 + 0.8));
            yield return new WaitForSecondsRealtime(0.1f);
            sfx.Play();
            yield return new WaitForSecondsRealtime(0.4f);
        }

        SceneManager.LoadScene("Win");
    }

}
