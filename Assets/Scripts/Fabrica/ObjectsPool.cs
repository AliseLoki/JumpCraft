using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool : MonoBehaviour
{
    [SerializeField] private int _amountToPool = 10;

    private Interactable _coinToPool;
    private Interactable _platformToPool;
    private Interactable _heartToPool;
    private Interactable _pig;

    private List<Interactable> _coins = new();
    private List<Interactable> _platforms = new();
    private List<Interactable> _hearts = new();
    private List<Interactable> _pigs = new();

    private Fabrica _fabrica;

    public Interactable CoinToPool => _coinToPool;
    public Interactable PlatformToPool => _platformToPool;
    public Interactable Heart => _heartToPool;
    public Interactable Pig => _pig;

    public List<Interactable> Coins => _coins;
    public List<Interactable> Platforms => _platforms;
    public List<Interactable> Hearts => _hearts;

    public List<Interactable> Pigs => _pigs;

    public void Init(Fabrica fabrica)
    {
        _fabrica = fabrica;
        _coinToPool = _fabrica.GetPrefabLinkFromFolder<Diamond>(nameof(Diamond));
        _platformToPool = _fabrica.GetPrefabLinkFromFolder<Platform>(nameof(Platform));
        _heartToPool = _fabrica.GetPrefabLinkFromFolder<Heart>(nameof(Heart));
        _pig = _fabrica.GetPrefabLinkFromFolder<Pig>(nameof(Pig));

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
        CreatePrefabsInPool(_hearts, _heartToPool);
        CreatePrefabsInPool(_pigs, _pig);
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