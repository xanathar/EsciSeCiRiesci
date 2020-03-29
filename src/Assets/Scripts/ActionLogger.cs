using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionLogger : MonoBehaviour
{
    string last = "";

    public void Log(string txt)
    {
        this.GetComponent<Text>().text = txt;
    }
}
