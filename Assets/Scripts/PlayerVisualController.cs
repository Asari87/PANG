using PANG.Input;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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