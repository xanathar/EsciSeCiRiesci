using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoronaVirus : MonoBehaviour
{
    public bool Stizzito = false;

    private void Start()
    {
        if (Stizzito)
            this.Shade(1f, 0f);
    }

    public void FadeOff()
    {
        StartCoroutine(CorFadeOff());
    }

    public void FadeIn()
    {
        StartCoroutine(CorFadeIn());
    }

    IEnumerator CorFadeOff()
    {
        for (float t = 0f; t < 1f; t += Time.deltaTime * 3f)
        {
            float k = Mathf.Clamp01(t);
            this.Shade(1f, 1f - k);
            yield return null;
        }
        this.Shade(1f, 0f);
    }


    IEnumerator CorFadeIn()
    {
        for (float t = 0f; t < 1f; t += Time.deltaTime * 3f)
        {
            float k = Mathf.Clamp01(t);
            this.Shade(1f, k);
            yield return null;
        }

        this.Shade(1f, 1f);
    }
}
