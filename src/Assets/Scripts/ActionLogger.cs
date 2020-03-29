using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionLogger : MonoBehaviour
{
    string last = "";
    float lastUpdateTime = 0;

    public void Log(string txt)
    {
        this.GetComponent<Text>().text = txt;
        lastUpdateTime = Time.time;
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
