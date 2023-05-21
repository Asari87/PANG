using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFitVertical : MonoBehaviour
{
    public SpriteRenderer sprite;

    // Use this for initialization
    void Start()
    {
        Camera.main.orthographicSize = sprite.bounds.size.x * Screen.height / Screen.width * 0.5f;
    }
}
