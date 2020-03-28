using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionLogger : MonoBehaviour
{
    string[] lines = new string[3];

    void Start()
    {
        this.GetComponent<Text>().text = "\nBenvenuti!";
    }

    public void Log(string txt)
    {
        for (int i = 0; i < 3; i++)
        {
            if (lines[i] == null)
            {
                lines[i] = txt;
                RefreshText();
                return;
            }
        }

        lines[0] = lines[1];
        lines[1] = lines[2];
        lines[2] = txt;
        RefreshText();
    }

    private void RefreshText()
    {
        string text = (lines[0] ?? "") + "\n" + (lines[1] ?? "") + "\n" + (lines[2] ?? "");
        this.GetComponent<Text>().text = text;
    }

}
