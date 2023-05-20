using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SoundsDatabase : ScriptableObject
{

    [Header("Ambiance")]
    public AudioClip[] menuAmbience;
    public LevelAudio[] levelsAmbiance;
    public AudioClip[] gameOverAmbience;

    [Header("UI Buttons")]
    public AudioClip buttonHover;
    public AudioClip buttonPress;

    [Header("Player")]
    public AudioClip playerDeath;

}

[Serializable]
public class LevelAudio
{
    public string sceneName;
    public AudioClip audio;
}
