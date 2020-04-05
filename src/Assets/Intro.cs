using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Intro : MonoBehaviour, IPointerClickHandler
{
    float time = 0f;

    public string ScenaTarget;
    public float Timeout = 10;
    public Text[] FadeTexts;

    public void OnPointerClick(PointerEventData eventData)
    {
        FadeOff();
    }

    void Start()
    {
        time = Time.time;
        foreach (var txt in FadeTexts)
            txt.ShadeText(0f);
        FadeIn();
    }

    void FadeOff()
    {
        StartCoroutine(CorFadeOff());
    }

    void FadeIn()
    {
        StartCoroutine(CorFadeIn());
    }

    IEnumerator CorFadeOff()
    {
        for (float t = 0f; t < 1f; t += Time.deltaTime * 1.5f)
        {
            float k = Mathf.Clamp01(t);
            foreach (var txt in FadeTexts)
                txt.ShadeText(1f - k);
            yield return null;
        }
        this.Shade(1f, 0f);
        GameState.ResetNew();
        SceneManager.LoadScene(ScenaTarget);
    }


    IEnumerator CorFadeIn()
    {
        for (float t = 0f; t < 1f; t += Time.deltaTime * 1.5f)
        {
            float k = Mathf.Clamp01(t);
            foreach (var txt in FadeTexts)
                txt.ShadeText(k);
            yield return null;
        }

        this.Shade(1f, 1f);

    }


    // Update is called once per frame
    void Update()
    {
        if ((Time.time - time) > Timeout)
        {
            FadeOff();
        }
    }
}
