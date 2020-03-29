﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject MenuGroup;
    public Button ButtonNewGame;
    public Button ButtonLoadGame;
    public Text ButtonLoadGameText;
    public Button ButtonCredits;

    public GameObject AlertGroup;
    public Button ButtonAlertYes;
    public Button ButtonAlertNo;

    public GameObject CreditsGroup;
    public Button ButtonCloseCredits;


    public void Start()
    {
        AlertGroup.SetActive(false);
        CreditsGroup.SetActive(false);

        if (!GameState.HasSave())
        {
            ButtonLoadGame.interactable = false;
            ButtonLoadGameText.color = new Color(0.4f, 0.4f, 0.4f, 0.4f);
        }

        ButtonNewGame.OnClick(() =>
        {
            if (GameState.HasSave())
            {
                MenuGroup.SetActive(false);
                AlertGroup.SetActive(true);
            }
            else
            {
                GameState.ResetNew();
                SceneManager.LoadScene("Adventure");
            }
        });

        ButtonLoadGame.OnClick(() =>
        {
            GameState.Load();
            SceneManager.LoadScene("Adventure");
        });

        ButtonCredits.OnClick(() =>
        {
            MenuGroup.SetActive(false);
            CreditsGroup.SetActive(true);
        });

        ButtonAlertYes.OnClick(() =>
        {
            GameState.ResetNew();
            SceneManager.LoadScene("Adventure");
        });

        ButtonAlertNo.OnClick(() =>
        {
            MenuGroup.SetActive(true);
            AlertGroup.SetActive(false);
        });

        ButtonCloseCredits.OnClick(() =>
        {
            MenuGroup.SetActive(true);
            CreditsGroup.SetActive(false);
        });

    }
}