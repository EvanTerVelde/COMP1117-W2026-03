using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private Vector2 moveInput;
    private bool jumpTriggered = false;

    public Vector2 MoveInput
    {
        get { return moveInput; }
    }

    public bool JumpTriggered
    {
        get { return jumpTriggered; }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context) 
    {
        if(context.started)
        {
            jumpTriggered = true;
        }
        else if(context.canceled)
        {
            jumpTriggered = false;
        }
    }

    public void UseJump()
    {
        jumpTriggered = false;
    }
}
