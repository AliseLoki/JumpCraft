using UnityEngine;

public class Fabrica
{
    public T CreatePrefab<T>(T prefab, Quaternion rotation, Transform parent) where T : MonoBehaviour
    {
        return (T)MonoBehaviour.Instantiate(prefab, parent.transform.position, rotation, parent);
    }

    public T CreatePrefab<T>(T prefab) where T : MonoBehaviour
    {
        return (T)MonoBehaviour.Instantiate(prefab);
    }

    public T GetPrefabLinkFromFolder<T>(string path) where T : MonoBehaviour
    {
        return Resources.Load(path, typeof(T)) as T;
    }
}