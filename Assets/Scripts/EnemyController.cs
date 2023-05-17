using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemySO enemyStats;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(enemyStats.axisForces.x,0);
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
                if (enemyStats.childEnemyPrefab != null)
                {
                    Instantiate(enemyStats.childEnemyPrefab.enemyPrefab,transform.position, Quaternion.identity);   
                }
                Destroy(gameObject);
                break;
        }
        

    }
}
