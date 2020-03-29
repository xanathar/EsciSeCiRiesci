using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public enum InGameUiButtonType
{
    ReturnToMenu,
    HowToPlay
}

public class InGameUiButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    float currentAlphaColor = 0f;
    float targetAlphaColor = 0f;

    public InGameUiButtonType ButtonType;

    void Start()
    {
        this.Shade(1, currentAlphaColor);
    }

    private void Update()
    {
        if (currentAlphaColor < targetAlphaColor)
        {
            currentAlphaColor = Math.Min(targetAlphaColor, currentAlphaColor + Time.deltaTime * 4f);
            this.Shade(1, currentAlphaColor);
        }
        else if (currentAlphaColor > targetAlphaColor)
        {
            currentAlphaColor = Math.Max(targetAlphaColor, currentAlphaColor - Time.deltaTime * 4f);
            this.Shade(1, currentAlphaColor);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetAlphaColor = 1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetAlphaColor = 0f;
    }

    private void TakeScreenshot()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
        string file = string.Format("EsciSeCiRiesci{0}.png",
            DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss"));

        Application.CaptureScreenshot(System.IO.Path.Combine(path, file));
#endif
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameState.Save();

        switch (ButtonType)
        {
            case InGameUiButtonType.ReturnToMenu:
                SceneManager.LoadScene("Menu");
                break;
            case InGameUiButtonType.HowToPlay:
                SceneManager.LoadScene("Tutorial");
                break;
            default:
                break;
        }
    }
}
