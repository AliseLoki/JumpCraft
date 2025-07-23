using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool : MonoBehaviour
{
    [SerializeField] private int _amountToPool = 10;

    private Interactable _coinToPool;
    private Interactable _platformToPool;

    private List<Interactable> _coins = new();
    private List<Interactable> _platforms = new();

    private Fabrica _fabrica;

    public Interactable CoinToPool => _coinToPool;
    public Interactable PlatformToPool => _platformToPool;

    public List<Interactable> Coins => _coins;
    public List<Interactable> Platforms => _platforms;

    public void Init(Fabrica fabrica)
    {
        _fabrica = fabrica;
        _coinToPool = _fabrica.GetPrefabLinkFromFolder<Diamond>(nameof(Diamond));
        _platformToPool = _fabrica.GetPrefabLinkFromFolder<Platform>(nameof(Platform));
        InitContainers();
    }

    public Interactable GetPooledObject(List<Interactable> pool, Interactable prefab, Vector3 position)
    {
        for (int i = 0; i < _amountToPool; i++)
        {
            if (!pool[i].gameObject.activeInHierarchy)
            {
                Interactable obj = pool[i];
                SetPosition(obj, position);
                return obj;
            }
        }

        var newInteractable = Create(pool, prefab);
        SetPosition(newInteractable, position);
        pool.Add(newInteractable);
        return newInteractable;
    }

    private void SetPosition(Interactable interactable, Vector3 pos)
    {
        interactable.gameObject.SetActive(true);
        interactable.transform.position = pos;
    }

    private void InitContainers()
    {
        CreatePrefabsInPool(_coins, _coinToPool);
        CreatePrefabsInPool(_platforms, _platformToPool);
    }

    private void CreatePrefabsInPool(List<Interactable> pool, Interactable prefab)
    {
        Interactable template;

        for (int i = 0; i < _amountToPool; i++)
        {
            template = Create(pool, prefab);
        }
    }

    private Interactable Create(List<Interactable> pool, Interactable prefab)
    {
        Interactable template = _fabrica.CreatePrefab(prefab, Quaternion.identity, transform);
        template.gameObject.SetActive(false);
        pool.Add(template);
        return template;
    }
}