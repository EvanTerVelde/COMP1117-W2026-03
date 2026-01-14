using UnityEngine;
using UnityEngine.InputSystem;

public class TestEnemy : MonoBehaviour
{
    // public const string PLAYER_TAG = "Player";

    // Unity's recommendation
    [SerializeField] private PlayerController playerController;
    [SerializeField] private int damageToDeal = 15;

    /*
    private void Awake()
    {
        // Not recommended, but an option
        // GameObject thePlayer = GameObject.FindGameObjectWithTag(PLAYER_TAG);

        // Better, but still not the best.
        // PlayerController thePlayerControllerScript = GameObject.FindFirstObjectByType<PlayerController>();
        // thePlayerControllerScript.TakeDamage(10);
    }
    */

    /*
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Spacebar pressed");
        }
    }
    */

    public void OnAttack(InputValue value)
    {
        if(value.isPressed)
        {
            if (playerController != null)
            {
                playerController.TakeDamage(damageToDeal);
            }
            else
            {
                Debug.LogWarning("TESTENEMY.CS: PlayerController is null");
            }
        }
    }
}
