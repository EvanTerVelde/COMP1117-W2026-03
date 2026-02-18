using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private EntitySpawner spawner;

    public Gem gemPrefab;
    public Cherry cherryPrefab;

    // two actions 
    // spawn cherry
    public void OnSpawnCherry(InputAction.CallbackContext context)
    {
        spawner.Spawn<Cherry>(cherryPrefab, GetRandomPosition());
    }

    public void OnSpawnGem(InputAction.CallbackContext context)
    {
        Gem newGem = spawner.Spawn<Gem>(gemPrefab, GetRandomPosition());
        newGem.DoGemBehaviour();
    }
    private Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(-5, 5), Random.Range(-2, 2), 0);
    }
}
