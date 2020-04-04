using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public static class Utils
{
    public static void ShadeText(this Text go, float shade)
    {
        go.GetComponent<Text>().color = new Color(1, 1, 1, shade);
    }

    public static void Shade(this Image go, float shade)
    {
        Shade(go, shade, shade, shade, shade);
    }

    public static void Shade(this Image go, float color, float alpha)
    {
        Shade(go, color, color, color, alpha);
    }

    public static void Shade(this Image go, float r, float g, float b, float a)
    {
        go.GetComponent<Image>().color = new Color(r, g, b, a);
    }

    public static string GetObjectPath(this GameObject rt)
    {
        string name = "";

        while (rt != null)
        {
            name = "/" + rt.name + name;
            rt = (rt.transform.parent != null) ? rt.transform.parent.gameObject : null;
        }
        return name;
    }

    public static void Shade(this MonoBehaviour go, float shade)
    {
        Shade(go, shade, shade, shade, shade);
    }

    public static void Shade(this MonoBehaviour go, float color, float alpha)
    {
        Shade(go, color, color, color, alpha);
    }

    public static void Shade(this MonoBehaviour go, float r, float g, float b, float a)
    {
        go.GetComponent<Image>().color = new Color(r, g, b, a);
    }

    public static void OnClick(this Button btn, Action callback)
    {
        btn.onClick = new Button.ButtonClickedEvent();
        btn.onClick.AddListener(() => callback());
    }

    public static void Fail()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else

#endif
    }

    public static void Error(string format, params object[] args)
    {
        Debug.LogErrorFormat(format, args);
        Fail();
    }

    public static void Assert(bool condition, string format, params object[] args)
    {
        if (!condition)
        {
            Debug.LogErrorFormat(format, args);
            Fail();
        }
    }

    public static void Assert(bool condition)
    {
        if (!condition)
        {
            Debug.LogErrorFormat("Assert failed!");
            Fail();
        }
    }


}
