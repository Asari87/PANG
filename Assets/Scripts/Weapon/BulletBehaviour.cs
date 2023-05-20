using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private float speed;
    [SerializeField] private float bulletLifetime = 5;
    private float timeSpawned;
    public void SetSpeed(float speed)
    { this.speed = speed; }

    private void Awake()
    {
        timeSpawned= Time.time;
    }
    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;

        if(Time.time > timeSpawned + bulletLifetime)
            Destroy(gameObject);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

}
