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
        if(anim != null)
        {
            anim.SetTrigger("Death");
        }

        patrolDistance = 0;
        GetComponent<Collider2D>().enabled = false;

        Debug.Log($"{gameObject.name} has died");
        Destroy(gameObject, 0.5f);
    }
}
