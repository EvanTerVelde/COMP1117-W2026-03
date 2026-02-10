using UnityEngine;
using UnityEngine.Events;

public class WorldSwitch : MonoBehaviour, IInteractable
{
    [SerializeField] private Sprite offSprite;
    [SerializeField] private Sprite onSprite;
    [SerializeField] private SpriteRenderer sRend;

    [SerializeField] private UnityEvent onActivated;
    private bool isFlipped = false;

    public void Interact()
    {
        isFlipped = !isFlipped;
        sRend.sprite = isFlipped ? onSprite : offSprite;

        if(isFlipped)
        {
            onActivated.Invoke();
        }
    }
}
