using UnityEngine;

public class Gem : MonoBehaviour
{
    public void DoGemBehaviour()
    {
        Debug.Log("<color=cyan> SPARKLE SPARKLE SPARKLE </color>");
        GetComponent<SpriteRenderer>().color = Color.cyan;
    }
}
