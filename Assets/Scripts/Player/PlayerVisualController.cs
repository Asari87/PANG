using PANG.Input;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handle player animations
/// </summary>
public class PlayerVisualController : MonoBehaviour
{
    private SpriteRenderer SpriteRenderer;
    private Animator animator;
    private PlayerController controller;

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        controller = GetComponentInParent<PlayerController>();
        controller.OnDeathStateChanged += HandleDeathAnimation;
    }
    private void OnDestroy()
    {
        controller.OnDeathStateChanged -= HandleDeathAnimation;
    }

    private void HandleDeathAnimation(bool state)
    {
        animator.SetBool("IsDead", state);
        if (state)
            animator.SetTrigger("Die");
    }

    public void TriggerShoot()
    {
        animator.SetTrigger("Shoot");
    }
    private void Update()
    {
        float currentspeed = controller.moveSpeed;
        SpriteRenderer.flipX = currentspeed > 0;
        animator.SetFloat("Speed", Mathf.Abs(currentspeed));
    }
}
