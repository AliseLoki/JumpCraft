using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool : MonoBehaviour
{
    [SerializeField] private int _amountToPool = 10;

    private Collectable _diamond;
    private Collectable _platform;
    private Collectable _heart;
    private Collectable _pig;
    private Collectable _coin;

    private List<Collectable> _diamonds = new();
    private List<Collectable> _platforms = new();
    private List<Collectable> _hearts = new();
    private List<Collectable> _pigs = new();
    private List<Collectable> _coins = new();

    private Fabrica _fabrica;

    public Collectable Diamond => _diamond;
    public Collectable Platform => _platform;
    public Collectable Heart => _heart;
    public Collectable Pig => _pig;
    public Collectable Coin => _coin;

    public List<Collectable> Diamonds => _diamonds;
    public List<Collectable> Platforms => _platforms;
    public List<Collectable> Hearts => _hearts;
    public List<Collectable> Pigs => _pigs;

    public List<Collectable > Coins => _coins;

    public void Init(Fabrica fabrica)
    {
        _fabrica = fabrica;
        _diamond = _fabrica.GetPrefabLinkFromFolder<Collectable>(nameof(Diamond));
        _coin = _fabrica.GetPrefabLinkFromFolder<Collectable>(nameof(Coin));
        _heart = _fabrica.GetPrefabLinkFromFolder<Collectable>(nameof(Heart));

        _platform = _fabrica.GetPrefabLinkFromFolder<Platform>(nameof(Platform));
        _pig = _fabrica.GetPrefabLinkFromFolder<Pig>(nameof(Pig));

        InitContainers();
    }

    public Collectable GetPooledObject(List<Collectable> pool, Collectable prefab, Vector3 position)
    {
        for (int i = 0; i < _amountToPool; i++)
        {
            if (!pool[i].gameObject.activeInHierarchy)
            {
                Collectable obj = pool[i];
                SetPosition(obj, position);
                return obj;
            }
        }

        var newInteractable = Create(pool, prefab);
        SetPosition(newInteractable, position);
        pool.Add(newInteractable);
        return newInteractable;
    }

    private void SetPosition(Collectable interactable, Vector3 pos)
    {
        interactable.gameObject.SetActive(true);
        interactable.transform.position = pos;
    }

    private void InitContainers()
    {
        CreatePrefabsInPool(_diamonds, _diamond);
        CreatePrefabsInPool(_platforms, _platform);
        CreatePrefabsInPool(_hearts, _heart);
        CreatePrefabsInPool(_pigs, _pig);
        CreatePrefabsInPool(_coins,_coin);
    }

    private void CreatePrefabsInPool(List<Collectable> pool, Collectable prefab)
    {
        Collectable template;

        for (int i = 0; i < _amountToPool; i++)
        {
            template = Create(pool, prefab);
        }
    }

    private Collectable Create(List<Collectable> pool, Collectable prefab)
    {
        Collectable template = _fabrica.CreatePrefab(prefab, Quaternion.identity, transform);
        template.gameObject.SetActive(false);
        pool.Add(template);
        return template;
    }
}