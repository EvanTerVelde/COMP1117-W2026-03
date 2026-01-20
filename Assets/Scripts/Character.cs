using UnityEngine;

public class Character : MonoBehaviour
{
    [Header(" Character Data")]
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private int maxHealth = 100;

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
        currentHealth = maxHealth;
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

    protected virtual void Die()
    {
        isDead = true;
        Debug.Log($"{gameObject.name} has died.");
    }
}
