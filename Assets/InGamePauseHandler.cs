using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGamePauseHandler : MonoBehaviour
{

    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        SceneHandler.Instance.LoadScene(0);
    }
}
