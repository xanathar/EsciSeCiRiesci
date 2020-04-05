using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour, IPointerClickHandler
{
    float time = 0f;

    public string ScenaTarget;
    public float Timeout = 10;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameState.ResetNew();
        SceneManager.LoadScene(ScenaTarget);
    }

    void Start()
    {
        time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Time.time - time) > Timeout)
        {
            GameState.ResetNew();
            SceneManager.LoadScene(ScenaTarget);
        }
    }
}
