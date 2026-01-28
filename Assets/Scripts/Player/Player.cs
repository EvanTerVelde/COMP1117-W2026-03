using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputHandler), typeof(Rigidbody2D))]
public class Player : Character
{
    [Header("Player Stats")]
    [SerializeField] private PlayerStats stats;

    [Header("iFrame Settings")]
    [SerializeField] private float iFrameDuration = 2.0f;
    private bool isInvisible = false;

    // Jumping logic
    [Header("Movement Settings")]
    [SerializeField] private float jumpForce = 12f;             // The force of my jump
    [SerializeField] private LayerMask groundLayer;             // Checking to see if I'm standing on the ground layer
    [SerializeField] private Transform groundCheck;             // Position of my ground check
    [SerializeField] private float groundCheckRadius = 0.2f;    // Size of my ground check

    // Private variables
    private Rigidbody2D rBody;          // Used to apply a force to move or jump
    private PlayerInputHandler input;   // Reads the input
    private bool isGrounded;            // Holds the result of the ground check operation

    /*
     * TO-DO: Add isGrounded property to help trigger animation
    */

    protected override void Awake()
    {
        base.Awake();

        if(stats == null)
        {
            stats = new PlayerStats(3, 3);
        }

        maxHealth = stats.MaxHP;
        CurrentHealth = stats.MaxHP;

        // Initialize
        rBody = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInputHandler>();
    }

    private void Update()
    {
        if (IsDead) return;

        // Perform my ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        Debug.Log(isGrounded);

        // Animation parameters
        if (!isInvisible && !IsDead)
        {
            anim.SetFloat("xVelocity", Mathf.Abs(rBody.linearVelocity.x));
            anim.SetBool("isGrounded", isGrounded);
            anim.SetFloat("yVelocity", rBody.linearVelocity.y);
        }
        else if (IsDead)
        {
            anim.SetFloat("xVelocity", 0);
        }

        // Handle Sprite Flipping
        if (input.MoveInput.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(input.MoveInput.x), 1, 1);
        }

    }

    void FixedUpdate()
    {
        if(IsDead)
        {
            return;
        }

        // Handle movement
        HandleMovement();
        // Handle jumping
        HandleJump();
        // Optional: Handle mario-like falling
    }

    private void HandleMovement()
    {
        // We get MoveInput from InputHandler
        // We get MoveSpeed from our Parent class (Character)
        float horizontalVelocity = input.MoveInput.x * MoveSpeed;

        rBody.linearVelocity = new Vector2(horizontalVelocity, rBody.linearVelocity.y);
    }

    private void HandleJump()
    {
        // Only jump if the input handle's jump property is true
        if(input.JumpTriggered && isGrounded)
        {
            // Apply Jump Force
            ApplyJumpForce();

            // "Consume the jump"
        }
    }

    private void ApplyJumpForce()
    {
        // Reset vertical velocity first to ensure consistent jump height.
        rBody.linearVelocity = new Vector2(rBody.linearVelocity.x, 0);

        rBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public override void TakeDamage(int amount)
    {
        // If invisible, we ignore this completely.
        if (isInvisible || IsDead) return;

        base.TakeDamage(amount);

        // If still alive after hit, start iFrames
        if(CurrentHealth > 0)
        {
            StartCoroutine(IFrameRoutine());
        }
    }

    private IEnumerator IFrameRoutine()
    {
        isInvisible = true;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color originalColor = sr.color;

        float timer = 0f;

        while(timer < iFrameDuration)
        {
            sr.enabled = !sr.enabled;

            sr.color = sr.enabled ? new Color(1f, 0.4f, 0.4f) : originalColor;

            // Blink every 0.1seconds
            yield return new WaitForSeconds(0.1f);
            timer += 0.1f;
        }

        sr.enabled = true;
        sr.color = originalColor;
        isInvisible = false;
    }

    protected override void Die()
    {
        StopAllCoroutines();
        isInvisible = false;

        // Trigger the animation to die here
        anim.SetTrigger("Hurt");

        base.Die();

        stats.LoseLife();

        // 3. Logic for what happens next
        if (stats.Lives > 0)
        {
            // For now, we just log it. 
            // Later, you can trigger a level reload or a "Respawn" method.
            Debug.Log("Respawning player...");
        }
        else
        {
            Debug.Log("No lives left. Game Over!");
            // Here you would trigger the Game Over screen.
        }

        rBody.linearVelocity = Vector2.zero;
        rBody.simulated = false;
    }
}
