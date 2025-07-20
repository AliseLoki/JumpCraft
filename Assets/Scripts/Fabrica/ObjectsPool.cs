using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool : MonoBehaviour
{
    // если при передаче объекта из пула делать его активным, то включать в других скриптах не придется
    // можно внутри него создавать пустые контейнеры для платформ и даймондов
    [SerializeField] private int _amountToPool = 5;

    private List<Interactable> _coins = new();
    private List<Interactable> _platforms = new();
    private List<PlatformView> _platformsViews = new();

    private Interactable _coinToPool;
    private Interactable _platformToPool;

    private Fabrica _fabrica;

    public Interactable CoinToPool => _coinToPool;
    public Interactable PlatformToPool => _platformToPool;

    public List<Interactable> Coins => _coins;
    public List<Interactable> Platforms => _platforms;

    public void Init(Fabrica fabrica, List<PlatformView> platformViews)
    {
        _fabrica = fabrica;
        _coinToPool = _fabrica.GetPrefabLinkFromFolder<Diamond>(nameof(Diamond));
        _platformToPool = _fabrica.GetPrefabLinkFromFolder<Platform>(nameof(Platform));

        InitList(platformViews);
        InitContainers();
    }

    public Interactable GetPooledObject(List<Interactable> pool, Interactable prefab)
    {
        for (int i = 0; i < _amountToPool; i++)
        {
            if (!pool[i].gameObject.activeInHierarchy) return pool[i];
        }

        if (prefab as Platform)
        {
            var newPlatform = Create(pool, prefab);
           // CreatePlatformView(newPlatform.transform);
            return newPlatform;
        }
        else
        {
            return Create(pool, prefab);
        }
    }

    private void InitContainers()
    {
        CreatePrefabsInPool(_coins, _coinToPool);
        CreatePlatformWithView(_platforms, _platformToPool);
    }

    private void InitList(List<PlatformView> platformViews)
    {
        foreach (var platformView in platformViews)
        {
            _platformsViews.Add(platformView);
        }
    }

    private void CreatePlatformWithView(List<Interactable> pool, Interactable prefab)
    {
        Interactable platform;

        for (int i = 0; i < _amountToPool; i++)
        {
            platform = Create(pool, prefab);

           // CreatePlatformView(platform.transform);
        }
    }

    private PlatformView CreatePlatformView(Transform transform)
    {
        int index = Random.Range(0, _platformsViews.Count);
        PlatformView view = _fabrica.CreatePrefab(_platformsViews[index], Quaternion.identity, transform);
        return view;
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