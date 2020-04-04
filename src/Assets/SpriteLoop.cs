using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteLoop : MonoBehaviour
{
    public List<Sprite> AnimationSprites;
    public float frameInterval = 0.2f;

    private float timeCounter = 0f;
    private int currentFrame = 0;

    private void Start()
    {
        UpdateSprite();
    }

    void Update()
    {
        timeCounter += Time.deltaTime;

        if (timeCounter > frameInterval)
        {
            timeCounter = 0f;
            currentFrame = (currentFrame + 1) % AnimationSprites.Count;
            UpdateSprite();
        }
    }

    private void UpdateSprite()
    {
        this.GetComponent<Image>().overrideSprite = AnimationSprites[currentFrame];
    }
}
