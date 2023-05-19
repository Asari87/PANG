using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SoundsDatabase : ScriptableObject
{

    [Header("Ambiance")]
    public AudioClip[] menuAmbience;
    public AudioClip[] gameAmbience;

    [Header("UI Buttons")]
    public AudioClip buttonHover;
    public AudioClip buttonPress;

}

