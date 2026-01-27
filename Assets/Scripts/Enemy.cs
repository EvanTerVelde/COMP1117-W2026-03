using UnityEngine;
using UnityEngine.Profiling;

public class Enemy : Character
{
    [Header("Enemy Stats")]
    [SerializeField] private EnemyStats stats;

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

        anim.SetFloat("Speed", 1f);
    }

    protected override void Die()
    {
        // 1. Use the base to run logic
        base.Die();

        // Destroy gameobject
        Destroy(gameObject, 1.5f);

    }
}
