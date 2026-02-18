using UnityEngine;

public class EntitySpawner : MonoBehaviour 
{

    // no class variables 
    public T Spawn<T>(T prefab, Vector3 position) where T : Component
    {
        T gObj = Instantiate(prefab, position, Quaternion.identity);
        
        return gObj;
    }


}
