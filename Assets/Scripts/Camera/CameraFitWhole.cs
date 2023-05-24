using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CameraFitWhole : MonoBehaviour
{
    public SpriteRenderer sprite;



    void Start()
    {
        SetCameraOrthoSize();
    }

    private void SetCameraOrthoSize()
    {
        if (sprite == null) return;
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = sprite.bounds.size.x / sprite.bounds.size.y;

        if (screenRatio >= targetRatio)
        {
            Camera.main.orthographicSize = sprite.bounds.size.y / 2;
        }
        else
        {
            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = sprite.bounds.size.y / 2 * differenceInSize;
        }
    }

    [ContextMenu("Manual Set")]
    private void ManualSet()
    {
        SetCameraOrthoSize();
    }
}
