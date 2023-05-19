using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenuHandler : MonoBehaviour
{
    [SerializeField] private Button menuBtn;

    private void Awake()
    {
        menuBtn.onClick.AddListener(() => SceneHandler.Instance.LoadScene(0));
    }

    private void OnDestroy()
    {
        menuBtn.onClick.RemoveAllListeners();
    }
}
