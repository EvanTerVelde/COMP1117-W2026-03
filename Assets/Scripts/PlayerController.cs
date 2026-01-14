using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Initial Player Stats")]
    // Initial Player Stats
    [SerializeField] private float initialSpeed = 5;
    [SerializeField] private int initialHealth = 100;

    // Private variables
    private PlayerStats stats;
    private Vector2 moveInput;

    // Components
    private Rigidbody2D rBody;

    void Awake()
    {
        // Initialize
        rBody = GetComponent<Rigidbody2D>();

        stats = new PlayerStats(initialSpeed, initialHealth);
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        float velocityX = moveInput.x * stats.MoveSpeed;

        rBody.linearVelocity = new Vector2(velocityX, rBody.linearVelocity.y);
    }

    public void TakeDamage(int damageAmount)
    {
        stats.CurrentHealth -= damageAmount;
        // stats.CurrentHealth = stats.CurrentHealth - damageAmount;

        Debug.Log("Player took damage");
    }
}
