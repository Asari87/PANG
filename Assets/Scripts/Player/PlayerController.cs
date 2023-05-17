using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PANG.Input
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float maxSpeed = 5;
        [SerializeField] private int smooth = 20;
        public float moveSpeed = 0;
        private float moveDirection;
        [SerializeField] PlayerWeaponController weaponController;

        public void Move(float direction)
        {
            moveDirection = direction;
        }

        public void Shoot()
        {
            weaponController.Shoot();
        }


        private void Update()
        {
            moveSpeed = Mathf.MoveTowards(moveSpeed, maxSpeed * moveDirection, smooth * Time.deltaTime);
            Vector3 step = new Vector3(moveSpeed, 0, 0) * Time.deltaTime;
            transform.position += step;

        }

    }
}