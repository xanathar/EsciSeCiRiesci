using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionLogger : MonoBehaviour
{
    float lastUpdateTime = 0;
    readonly System.Random rand = new System.Random();

    public void Log(string txt)
    {
        this.GetComponent<Text>().text = txt;
        lastUpdateTime = Time.time;
    }

    public void LogRandomFailure()
    {
        int r = this.rand.Next(4);

        switch (r)
        {
            case 0:
                this.Log("Non è una buona idea.");
                break;
            case 1:
                this.Log("Uhm... no.");
                break;
            case 2:
                this.Log("Non credo proprio.");
                break;
            default:
                this.Log("Non ha molto senso.");
                break;

        }
    }

    private void Update()
    {
        float diff = Time.time - lastUpdateTime;

        if (diff < 3f)
            this.GetComponent<Text>().ShadeText(1f);
        else if (diff > 4f)
            this.GetComponent<Text>().ShadeText(0f);
        else
            this.GetComponent<Text>().ShadeText(4f - diff);

        //Debug.LogFormat("LOG time : {0}", diff);
    }
}
