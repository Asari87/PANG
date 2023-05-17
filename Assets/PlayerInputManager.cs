using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PANG.Input
{
    public class PlayerInputManager : MonoBehaviour
    {
        private GameControls gameControls;
        private PlayerController controller;

        private void Awake()
        {
            controller = GetComponent<PlayerController>();
            gameControls = new();

            gameControls.Player1.Movement.performed += HandleControls;
            gameControls.Player1.Movement.canceled += HandleControls;
            gameControls.Player1.Shoot.performed += HandlerShoot;

        }

        private void HandleControls(InputAction.CallbackContext context)
        {
            controller.Move(context.ReadValue<float>());
        }

        private void HandlerShoot(InputAction.CallbackContext context)
        {
            if(context.performed)
                controller.Shoot();
        }

        private void OnEnable()
        {
            gameControls.Player1.Enable();
        }
        private void OnDisable()
        {
            gameControls.Player1.Disable();
        }
    }
}