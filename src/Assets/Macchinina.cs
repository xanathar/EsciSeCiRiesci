using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Macchinina : MonoBehaviour
{
    public Vector3 PosizioneFinale;
    private Vector3 PosizioneIniziale;

    enum Stato
    {
        FermaInizio,
        FermaFine,
        VersoInizio,
        VersoFine
    }

    Stato stato;
    float tempo = 0;
    const float DURATA = 2;

    void Start()
    {
        PosizioneIniziale = this.gameObject.transform.localPosition;
        stato = Stato.FermaInizio;
    }

    void Update()
    {
        if (stato == Stato.FermaInizio || stato == Stato.FermaFine)
            return;

        bool finito = false;

        Vector3 pos;

        tempo += Time.deltaTime / DURATA;

        if (tempo >= 1)
            finito = true;

        float tt = 3 * (tempo * tempo) - 2 * (tempo * tempo * tempo);

        if (stato == Stato.VersoInizio)
        {
            pos = (PosizioneIniziale * tt) + (PosizioneFinale * (1f - tt));

            if (finito)
                stato = Stato.FermaInizio;
        }
        else
        {
            pos = (PosizioneFinale * tt) + (PosizioneIniziale * (1f - tt));

            if (finito)
                stato = Stato.FermaFine;
        }

        this.gameObject.transform.localPosition = pos;
    }

    public void AccendiMacchinina()
    {
        if (stato == Stato.FermaFine)
        {
            tempo = 0;
            stato = Stato.VersoInizio;
            this.GetComponent<AudioSource>().Play();
        }
        else if (stato == Stato.FermaInizio)
        {
            tempo = 0;
            stato = Stato.VersoFine;
            this.GetComponent<AudioSource>().Play();
        }
    }

}
