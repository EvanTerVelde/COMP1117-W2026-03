using UnityEngine;

public class WaterZone : Zone
{
    [SerializeField] private float speedMultiplier = 0.5f;

    protected override void ApplyZoneEffect(Player player)
    {
        player.ApplySpeedModifier(speedMultiplier);

        Debug.Log("Player is swimming...");
    }
}
