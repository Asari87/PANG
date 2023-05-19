using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenes { IntroScene, Level, GameOverScene }
public class SceneHandler : MonoBehaviour
{
    public static SceneHandler Instance;
    [SerializeField] private float fadeOutTime;
    [SerializeField] private float fadeInTime;
    private string[] sceneNames;
    private Fader fader;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        InitalizeSceneList();
        fader = GetComponentInChildren<Fader>();

        //local
        void InitalizeSceneList()
        {
            int sceneCount = SceneManager.sceneCountInBuildSettings;
            sceneNames = new string[sceneCount];
            for (int i = 0; i < sceneCount; i++)
            {
                sceneNames[i] = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
            }
        }
    }


    public void LoadScene(Scenes nextScene)
    {
        StartCoroutine(SwtichScene(nextScene.ToString()));
    }
    public void LoadLevel(int levelIndex)
    {
        string requestedScene = $"Level_{levelIndex}";
        if (sceneNames.Contains(requestedScene))
        {
            StartCoroutine(SwtichScene(requestedScene));
        }
        else
        {
            StartCoroutine(SwtichScene(Scenes.GameOverScene.ToString()));
        }
    }

    private IEnumerator SwtichScene(string nextScene)
    {
        yield return fader.FadeOut(fadeOutTime);
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        while (op.progress < .9f)
        {
            yield return null;
        }

        yield return fader.FadeIn(fadeInTime);
    }

    internal void RestartLevel()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }
}
