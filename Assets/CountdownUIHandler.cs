using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using Unity.VisualScripting;

using UnityEngine;

public class CountdownUIHandler : MonoBehaviour
{
    [SerializeField] private int secondsToCount = 3;
    [SerializeField] private AudioClip tickSound;
    private Animator animator;
    private TMP_Text countdownText;
    private CanvasGroup canvasGroup;

    WaitForSeconds waitDelay;
    private Coroutine countRoutine;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        canvasGroup = GetComponent<CanvasGroup>();
        countdownText = GetComponentInChildren<TMP_Text>();
        waitDelay = new WaitForSeconds(1);
    }
    

    public void Countdown(Action callback)
    {
        if(countRoutine != null)
            StopCoroutine(countRoutine);
        countRoutine = StartCoroutine(Count(callback));
    }

    private IEnumerator Count(Action callback)
    {
        yield return waitDelay;
        for (int counter = secondsToCount; counter > 0; counter--)
        {
            countdownText.text = counter.ToString();
            animator.SetTrigger("Popup");
            yield return waitDelay;
        }
        canvasGroup.alpha = 0;

             callback?.Invoke();
    }

    public void CountTickSound()
    {
        SoundManager.Instance.PlayEffect(tickSound);
    }

}
