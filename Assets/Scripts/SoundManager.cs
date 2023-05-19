using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SoundType { Effect, Music}
public enum SoundPrefsKeys { EffectMute, EffectVolume, MusicMute, MusicVolume}  
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeReference] private AudioSource effectsSource;
    [SerializeReference] private AudioSource musicSource;
    private bool inGameScene;
    private SoundsDatabase soundsDB;
    private void Awake()
    {
        if(Instance== null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        soundsDB = Resources.Load<SoundsDatabase>("Sounds/SoundsDB");
        Initialize();

        SceneManager.sceneLoaded+= HandleLoadedScene;


        //local
        void Initialize()
        {
            if (PlayerPrefs.HasKey(SoundPrefsKeys.EffectMute.ToString()))
            {
                bool mute = PlayerPrefs.GetInt(SoundPrefsKeys.EffectMute.ToString()) == 1;
                effectsSource.mute = mute;
            }
            if (PlayerPrefs.HasKey(SoundPrefsKeys.MusicMute.ToString()))
            {
                bool mute = PlayerPrefs.GetInt(SoundPrefsKeys.MusicMute.ToString()) == 1;
                musicSource.mute = mute;
            }

            if (PlayerPrefs.HasKey(SoundPrefsKeys.EffectVolume.ToString()))
            {
                effectsSource.volume = PlayerPrefs.GetFloat(SoundPrefsKeys.EffectVolume.ToString());
            }
            if (PlayerPrefs.HasKey(SoundPrefsKeys.MusicVolume.ToString()))
            {
                musicSource.volume = PlayerPrefs.GetFloat(SoundPrefsKeys.MusicVolume.ToString());
            }
        }
    }



    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= HandleLoadedScene;
    }

    private void HandleLoadedScene(Scene arg0, LoadSceneMode arg1)
    {
        //Get scene theme
    }

    public void HandleButtonEvent(ButtonEvent buttonEvent)
    {
        switch (buttonEvent)
        {
            case ButtonEvent.Hover:
                PlayEffect(soundsDB.buttonHover);
                break;
            case ButtonEvent.Pressed:
                PlayEffect(soundsDB.buttonPress);
                break;
        }
    }

    private void Update()
    {
        if(!musicSource.isPlaying)
        {
            if(inGameScene)
                musicSource.clip = soundsDB.gameAmbience[Random.Range(0, soundsDB.gameAmbience.Length)];
            else
                musicSource.clip = soundsDB.menuAmbience[Random.Range(0, soundsDB.menuAmbience.Length)];
            musicSource.Play();
        }
    }

    public void PlayEffect(AudioClip clip)
    {
        effectsSource.PlayOneShot(clip);
    }

    public void PlayClipAtPoint(AudioClip clip, Vector3 worldPosition, float volume = 1)
    {
        AudioSource.PlayClipAtPoint(clip, worldPosition, volume);
    }


    public void ToggleEffects()
    {
        effectsSource.mute = !effectsSource.mute;
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void SetEffectsVolume(float value)
    {
        effectsSource.volume = value;
    }

    public void SetMusicVolume(float value)
    {
        musicSource.volume = value;
    }

    public void SaveChanges()
    {
        PlayerPrefs.SetFloat(SoundPrefsKeys.EffectVolume.ToString(), effectsSource.volume);
        PlayerPrefs.SetFloat(SoundPrefsKeys.MusicVolume.ToString(), musicSource.volume);
        PlayerPrefs.SetInt(SoundPrefsKeys.EffectMute.ToString(), effectsSource.mute ? 1 : 0);
        PlayerPrefs.SetInt(SoundPrefsKeys.MusicMute.ToString(), musicSource.mute ? 1 : 0);
    }
}
