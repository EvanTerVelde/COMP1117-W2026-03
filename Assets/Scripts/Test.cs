using UnityEngine;
using System.Collections.Generic;

public class Test : MonoBehaviour
{
    public List<GameObject> interactables;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (GameObject gObj in interactables)
        {
            gObj.GetComponent<IInteractable>().Interact();
        }
        
    }
}
