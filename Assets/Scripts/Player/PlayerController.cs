using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

namespace PANG.Input
{
    /// <summary>
    /// Handle player inputs Move and Shoot.
    /// Handle collision with enemies.
    /// </summary>
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
            if (isDead) return;
            weaponController.Shoot();
        }


        private void Update()
        {
            if (isDead) return;

            //ease in and out of movement
            moveSpeed = Mathf.MoveTowards(moveSpeed, maxSpeed * moveDirection, smooth * Time.deltaTime);

            //calculate step
            Vector3 step = new Vector3(moveSpeed, 0, 0) * Time.deltaTime;

            //check for wall collision
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

            //Die if collided with enemy
            if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                //TODO: swap with health system and die if run out of health
                StartCoroutine(Die());
            }
        }

        private IEnumerator Die()
        {
            isDead = true;
            //Notify whoever cares (currently, visual controller cares)
            OnDeathStateChanged?.Invoke(true);
            SoundManager.Instance.OnPlayerDied();
            yield return new WaitForSeconds(2);
            SceneHandler.Instance.RestartLevel();
        }
    }
}