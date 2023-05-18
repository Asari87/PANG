using System;

using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyType type;
    private Vector2 axisForces;
    private Rigidbody2D rb;
    public bool IsPaused { get; set; } = false;
    private Vector3 currentPos;

    public static Action<EnemyType, Vector3> OnDeath;
    public static Action<EnemyType, Vector3> OnBounce;
    public static Action<EnemyType, Vector3> OnSpawn;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetStats(EnemyType type, Vector2 axisForces)
    {
        this.type = type;
        this.axisForces = axisForces;
        OnSpawn?.Invoke(type, transform.position);
    }
    public void SetDirection(Direction dir)
    {
            rb.velocity = new Vector2((int)dir * axisForces.x, 0);
    }

    private void Update()
    {
        if (IsPaused)
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
                rb.velocity = new Vector2(rb.velocity.x, axisForces.y);
                OnBounce?.Invoke(type,transform.position);
                break;
            case "Wall":
                //Change direction
                rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
                OnBounce?.Invoke(type, transform.position);
                break;
            case "Bullet":
                //Die
                OnDeath?.Invoke(type, transform.position);
                Destroy(gameObject);
                break;
        }
        

    }
}
