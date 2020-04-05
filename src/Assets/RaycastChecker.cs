using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RaycastChecker : MonoBehaviour
{
    void Start()
    {
#if UNITY_EDITOR
        GameObject[] raycastTargets = this.gameObject
            .GetComponentsInChildren<ICanvasRaycastFilter>()
            .Cast<Graphic>()
            .Where(rt => rt.raycastTarget)
            .Select(gfx => gfx.gameObject)
            .Where(go => go.GetComponent<Entity>() == null)
            .Where(go => go.GetComponent<InventoryPlaceholder>() == null)
            .Where(go => go.GetComponent<InventoryObject>() == null)
            .Where(go => go.GetComponent<InGameUiButton>() == null)
            .ToArray();

        if (raycastTargets.Length == 0)
        {
            Debug.Log("All raycast targets are ok");
        }
        else
        {
            Debug.LogErrorFormat("Invalid raycast targets found : {0}", raycastTargets.Length);

            foreach (var rt in raycastTargets)
            {
                Debug.LogWarningFormat("Invalid raycast target: {0}", Utils.GetObjectPath(rt));
            }
        }
#endif
    }
}
