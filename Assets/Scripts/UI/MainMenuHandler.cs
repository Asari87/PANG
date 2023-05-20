using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField] private Button playBtn;
    [SerializeField] private Button optionsBtn;
    [SerializeField] private Button quitBtn;


    private void Awake()
    {
        playBtn.onClick.AddListener(() => SceneHandler.Instance.LoadLevel(1));
        quitBtn.onClick.AddListener(() => Application.Quit());  
    }

    private void OnDestroy()
    {
        playBtn.onClick.RemoveAllListeners();
        quitBtn.onClick.RemoveAllListeners();
    }
}
