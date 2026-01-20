using UnityEngine;
using UnityEngine.Windows;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rBody;
    private PlayerInputHandler input;
    private PlayerController controller;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rBody = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInputHandler>();
        controller = GetComponent<PlayerController>();
    }

    private void Update()
    {
        // 1. Handle Walking vs Idle
        // We use the absolute value of horizontal input
        float moveSpeed = Mathf.Abs(input.MoveInput.x);
        anim.SetFloat("moveSpeed", moveSpeed);

        anim.SetBool("isGrounded", controller.IsGrounded);

        // 2. Handle Vertical Movement (Jump Up vs Jump Down)
        // We look at the actual Y velocity of the Rigidbody
        anim.SetFloat("verticalVelocity", rBody.linearVelocity.y);

        // 3. Optional: Flip the sprite based on movement direction
        if (input.MoveInput.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(input.MoveInput.x), 1, 1);
        }
    }
}
