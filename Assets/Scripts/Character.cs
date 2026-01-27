using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Character Data")]
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] protected int maxHealth = 100;

    protected int currentHealth;
    protected Animator anim;

    private bool isDead = false;

    public float MoveSpeed
    {
        get { return moveSpeed; }
    }

    public bool IsDead
    {
        get { return isDead; }
    }

    protected int CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = Mathf.Clamp(value, 0, maxHealth); }
    }

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public virtual void TakeDamage(int amount)
    {
        if(IsDead) return;

        CurrentHealth -= amount;
        Debug.Log($"{gameObject.name} HP is now: {CurrentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Version 2: OVERLOADED methond (new)
    public virtual void TakeDamage(int amount, float knockbackForce, Vector2 hitDirection)
    {
        // 1. Call the original version to handle the health math.

        // 2. Handle the physics
        Rigidbody2D rBody = GetComponent<Rigidbody2D>();
        if (rBody != null)
        {
            // Apply an impulse force away from the hit source
            rBody.AddForce(hitDirection * knockbackForce, ForceMode2D.Impulse);
            Debug.Log($"{gameObject.name} was knocked back with {knockbackForce} force");
        }
    }

    protected virtual void Die()
    {
        isDead = true;
        anim.SetTrigger("Death");
        Debug.Log($"{gameObject.name} has died.");
    }
}
