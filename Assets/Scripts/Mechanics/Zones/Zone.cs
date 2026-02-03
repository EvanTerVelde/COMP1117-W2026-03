using UnityEngine;

// The 'abstract' keyword prevents this script from being attached to a GameObject
// It can only be used as a base for other scripts.
[RequireComponent(typeof(BoxCollider2D))]
public abstract class Zone : MonoBehaviour
{
    [Header("Zone Settings")]
    [SerializeField] protected Color debugColor = new Color(0, 1, 0, 0.3f);

    // The "Contract"
    // Every child (wwater, lava, slow-mo) MUST define what happens in this method
    protected abstract void ApplyZoneEffect(Player player);

    // We use a Trigger to detect the player
    private void OnTriggerStay2D(Collider2D collision)
    {
        // Look for the Player component on the objhect that entered the trigger
        if(collision.TryGetComponent(out Player player))
        {
            ApplyZoneEffect(player);
        }
    }

    // Debugging Purpose Only!
    // Visual aid for the editor so you can see zones.
    private void OnDrawGizmos()
    {
        Gizmos.color = debugColor;
        BoxCollider2D box = GetComponent<BoxCollider2D>();

        // Because of RequireComponent, we can safely assume 'box' is not null
        // but it's still good practice to check!
        if(box != null)
        {
            Gizmos.DrawCube(transform.position + (Vector3)box.offset, box.size);
        }
    }
}
