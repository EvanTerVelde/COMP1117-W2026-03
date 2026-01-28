using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the thing we hit has a Player
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            // Call the TakeDamage function you just wrote!
            player.TakeDamage(damageAmount);
        }
    }
}
