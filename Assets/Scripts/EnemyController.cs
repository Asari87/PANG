using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyController : MonoBehaviour
{
    public EnemySO enemyStats;

    private Rigidbody2D rb;
    [SerializeField] private bool isPaused = false;
    private Vector3 currentPos;

    public static Action<EnemyController> OnDeath;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetStats(EnemySO enemyStats)
    {
        this.enemyStats = enemyStats;
    }
    public void SetDirection(Direction dir)
    {
            rb.velocity = new Vector2((int)dir * enemyStats.axisForces.x, 0);
    }

    private void Update()
    {
        
        if (isPaused)
            transform.position = currentPos;
    }
    private void LateUpdate()
    {
        currentPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string layer = LayerMask.LayerToName(collision.gameObject.layer);
        switch (layer)
        {
            case "Ground":
                //Bounce
                rb.velocity = new Vector2(rb.velocity.x, enemyStats.axisForces.y); 
                break;
            case "Wall":
                //Change direction
                rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
                break;
            case "Bullet":
                //Die
                Debug.Log("Pop");
                OnDeath?.Invoke(this);
               
                break;
        }
        

    }
}
