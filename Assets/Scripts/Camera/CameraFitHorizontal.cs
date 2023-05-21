using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFitHorizontal : MonoBehaviour
{
    public SpriteRenderer sprite;

    // Use this for initialization
    void Start()
    {
        Camera.main.orthographicSize = sprite.bounds.size.y / 2;
    }
}
