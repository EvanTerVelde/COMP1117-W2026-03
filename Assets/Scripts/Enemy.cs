using UnityEngine;
using UnityEngine.Profiling;

public class Enemy : Character
{
    [Header("Enemy Stats")]
    [SerializeField] private EnemyStats stats;

    [Header("Patrol Settings")]
    [SerializeField] private float patrolDistance = 5f;
    private Vector2 startPos;
    private int direction = 1; // 1 for right, -1 for left.

    protected override void Awake()
    {
        // 1. Call parent Awake first
        base.Awake();

        // 2. Safety check (encapsulation)
        if (stats == null)
        {
            stats = new EnemyStats(1, 100, 2);
            Debug.Log($"Profile missing on {gameObject.name}. Default values assigned.");
        }

        // 3.
        currentHealth = stats.MaxHP;
        Debug.Log($"{gameObject.name} initialized with {stats.MaxHP} HP.");

        // anim.SetFloat("Speed", 1f);
    }

    private void Update()
    {
        Debug.Log("Enemy dead: " + IsDead);
        if (IsDead) return;

        // Calculate movement
        float leftBoundary = startPos.x - patrolDistance;
        float rightBoundary = startPos.x + patrolDistance;

        transform.Translate(Vector2.right * direction * MoveSpeed * Time.deltaTime);

        // Flip direction if wwe hit a boundary
        if(transform.position.x >= rightBoundary)
        {
            direction = -1;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if(transform.position.x <= leftBoundary)
        {
            direction = 1;
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    protected override void Die()
    {
        // 1. Use the base to run logic
        base.Die();

        // Destroy gameobject
        Destroy(gameObject, 1.5f);

    }
}
