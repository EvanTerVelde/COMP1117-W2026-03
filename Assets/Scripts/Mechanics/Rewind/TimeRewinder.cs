using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class TimeRewinder : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private int maxFrames = 300;
    [SerializeField] private bool isRewinding = false;

    private CircularBuffer buffer;

    private void Awake()
    {
        buffer = new CircularBuffer(maxFrames);
    }

    public void OnaRewind(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isRewinding = true;
            Debug.Log("Rewind Preformed");
        }
        else if (context.canceled)
        {
            isRewinding = false;
            Debug.Log("Rewinding Canceled");
        }
    }
    private void FixedUpdate()
    {
        if (isRewinding)
        {
            Rewind();
        }
        else
        {
            Record();
        }
    }

    // record
    private void Record()
    {
        buffer.Push(Random.Range(0, 1000));
    }
    // rewind
    private void Rewind()
    {
        if (buffer.Count > 0)      // makes sure buffer has something in it
        {
            int tempInt = buffer.Pop();
            Debug.Log("Item Popped from circular buffer: " + tempInt);
        }
        else
        {
            isRewinding = false;
        }
    }
}
