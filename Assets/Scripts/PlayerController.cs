using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputHandler), typeof(Rigidbody2D))]
public class PlayerController : Character
{
    [Header("Movement Settings")]
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;

    private Rigidbody2D rBody;
    private PlayerInputHandler input;
    private bool isGrounded;

    public bool IsGrounded
    {
        get { return isGrounded; }
    }

    protected override void Awake()
    {
        base.Awake();
        rBody = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInputHandler>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        Debug.Log(isGrounded);
    }

    private void FixedUpdate()
    {
        if (IsDead) return;

        HandleMovement();
        HandleJump();
        HandleBetterFalling();
    }

    private void HandleMovement()
    {
        // We get MoveInput from the modular InputHandler sibling
        // We get MoveSpeed from the Character parent class
        float horizontalVelocity = input.MoveInput.x * MoveSpeed;

        rBody.linearVelocity = new Vector2(horizontalVelocity, rBody.linearVelocity.y);
    }

    private void HandleJump()
    {
        // Only jump if the input handler's property is currently true
        if (input.JumpTriggered && isGrounded)
        {
            ApplyJumpForce();

            // We must "consume" the jump so we don't apply force every frame
            input.UseJump();
        }
    }

    private void HandleBetterFalling()
    {
        if(rBody.linearVelocity.y < 0)
        {
            rBody.linearVelocity += Vector2.up * Physics2D.gravity.y * 2f * Time.deltaTime;
        }
    }

    private void ApplyJumpForce()
    {
        // Reset vertical velocity first to ensure consistent jump height
        rBody.linearVelocity = new Vector2(rBody.linearVelocity.x, 0);

        rBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        // Debug.Log("Player Jumped!");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character victim = collision.GetComponent<Character>();

        if (victim != null && !victim.IsDead)
        {
            // Check if player is above the enemy
            if (transform.position.y > collision.transform.position.y + 0.2f)
            {
                victim.TakeDamage(100);
                ApplyJumpForce();
            }
            else
            {
                this.TakeDamage(10);
            }
        }
    }
}
