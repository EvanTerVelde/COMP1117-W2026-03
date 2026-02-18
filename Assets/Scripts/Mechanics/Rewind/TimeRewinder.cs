using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class TimeRewinder : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private int maxFrames = 300;
    [SerializeField] private bool isRewinding = false;

    private CircularBuffer<Vector3> positionHistory;
    private CircularBuffer<Quaternion> rotationHistory;
    private CircularBuffer<Vector3> scaleHistory;
    

    private void Awake()
    {
        positionHistory = new CircularBuffer<Vector3>(maxFrames);
        rotationHistory = new CircularBuffer<Quaternion>(maxFrames);
        scaleHistory = new CircularBuffer<Vector3>(maxFrames);
    }

    public void OnRewind(InputAction.CallbackContext context)
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
        positionHistory.Push(transform.position);
        rotationHistory.Push(transform.rotation);
        scaleHistory.Push(transform.localScale);
    }
    // rewind
    private void Rewind()
    {
        if (positionHistory.Count > 0)      // makes sure buffer has something in it
        {
            transform.position = positionHistory.Pop();
            transform.rotation = rotationHistory.Pop();
            Vector3 tempLocalScale = scaleHistory.Pop();

            transform.localScale = new Vector3(tempLocalScale.x * -1, tempLocalScale.y, tempLocalScale.z);
        }
        else
        {
            isRewinding = false;
        }
    }
}
