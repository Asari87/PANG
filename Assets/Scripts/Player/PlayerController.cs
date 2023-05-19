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
        public Action<bool> OnDeathStateChanged;
        private bool isDead = false;

        private void Start()
        {
            OnDeathStateChanged?.Invoke(false);
            isDead = false;
        }
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
            if (isDead) return;
            moveSpeed = Mathf.MoveTowards(moveSpeed, maxSpeed * moveDirection, smooth * Time.deltaTime);
            Vector3 step = new Vector3(moveSpeed, 0, 0) * Time.deltaTime;
            RaycastHit2D hit = Physics2D.Raycast(transform.position+Vector3.up,step.normalized,1);
            if (hit.transform != null)
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Wall")) return;
            }
            transform.position += step;

        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(isDead) return;
            if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                StartCoroutine(Die());
            }
        }

        private IEnumerator Die()
        {
            isDead = true;
            OnDeathStateChanged?.Invoke(true);
            yield return new WaitForSeconds(2);
            SceneHandler.Instance.RestartLevel();
        }
    }
}