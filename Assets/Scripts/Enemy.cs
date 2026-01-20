using UnityEngine;

// Enemy IS-A Character
public class Enemy : Character
{
    [Header("Enemy Settings")]
    [SerializeField] private float patrolDistance = 5.0f;

    private float startX;
    private int direction = 1; // 1 for right, -1 for left.

    protected override void Awake()
    {
        // Help initialize internal stats
        base.Awake();

        startX = transform.position.x;
    }

    private void Update()
    {
        if(IsDead)
            return;

        HandleMovement();
    }

    private void HandleMovement()
    {
        float speed = MoveSpeed; // MoveSpeed from the Character parent class

        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

        float distanceFromStart = transform.position.x - startX;

        if(Mathf.Abs(distanceFromStart) >= patrolDistance)
        {
            // Turn around
            direction *= -1;

            transform.localScale = new Vector3(direction, 1, 1);
        }
    }

    protected override void Die()
    {
        // 1. Prevent multiple calls to Die
        if (IsDead) return;

        // 2. Call the base to set IsDead = true
        base.Die();

        // 3. Stop Physics Jitter
        Rigidbody2D rBody = GetComponent<Rigidbody2D>();
        if (rBody != null)
        {
            rBody.linearVelocity = Vector2.zero;
            rBody.bodyType = RigidbodyType2D.Kinematic; // Stops all physics forces
        }

        // 4. Stop Logic Jitter
        // This stops the Update() loop from running HandleMovement/Flipping
        this.enabled = false;

        // 5. Play Animation
        if (anim != null)
        {
            anim.SetTrigger("Death");
        }

        // 6. Cleanup
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 0.5f);
    }
}
