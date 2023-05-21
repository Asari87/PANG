using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFitWhole : MonoBehaviour
{
    public SpriteRenderer sprite;

    // Use this for initialization
    void Start()
    {
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
}
