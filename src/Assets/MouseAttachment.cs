using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseAttachment : MonoBehaviour
{
    public InventoryManager inventoryManager;

    void Update()
    {
        if (inventoryManager.ActiveItem != EntityType.Unknown)
        {
            this.GetComponent<Image>().overrideSprite = inventoryManager.GetSpriteForActiveItem();
            this.Shade(1);
        }
        else
        {
            this.GetComponent<Image>().overrideSprite = null;
            this.Shade(0);
        }

        float w = Screen.currentResolution.width;
        transform.position = Input.mousePosition;
    }
}
