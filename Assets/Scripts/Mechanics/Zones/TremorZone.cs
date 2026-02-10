using Unity.Cinemachine;
using UnityEngine;

public class TremorZone : Zone
{
    private CinemachineImpulseSource impulseSource;

    private void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    protected override void ApplyZoneEffect(Player player)
    {
        impulseSource.GenerateImpulseWithVelocity(Random.insideUnitSphere);
    }
}
