using UnityEngine;

public class TreasureChest : MonoBehaviour, IInteractable
{
    [Header("Loot Settings")]
    [SerializeField] private GameObject gemPrefab;
    [SerializeField] private int gemCount = 3;
    [SerializeField] private float launchForce = 5f;

    [Header("Visuals")]
    [SerializeField] private Sprite openChestSprite;

    private SpriteRenderer sRend;
    private bool isOpened = false;

    private void Awake()
    {
        sRend = GetComponent<SpriteRenderer>();
    }

    public void Interact()
    {
        if (isOpened) return;   // Prevent multiple openings

        isOpened = true;
        OpenChest();
    }

    private void OpenChest()
    {
        // 1. Change visual to open state.
        if (sRend != null && openChestSprite != null)
        {
            sRend.sprite = openChestSprite;
        }

        // 2. Spew gems
        for(int i = 0; i < gemCount; i++)
        {
            GameObject gem = Instantiate(gemPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = gem.GetComponent<Rigidbody2D>();

            if(rb != null)
            {
                // Create a "fountain" effect
                Vector2 force = new Vector2(Random.Range(-1f, 1f), 1.5f).normalized * launchForce;
                rb.AddForce(force, ForceMode2D.Impulse);
            }
        }
    }
}
