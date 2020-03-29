using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    static bool MusicLoaded = false;

    void Start()
    {
        if (MusicLoaded)
            this.gameObject.SetActive(false);
        else
            DontDestroyOnLoad(this.gameObject);

        MusicLoaded = true;
    }
}
