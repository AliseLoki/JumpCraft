using System.Collections.Generic;
using UnityEngine;

public class BootsTrap : MonoBehaviour
{
    [SerializeField] private CameraController _mainCamera;
    [SerializeField] private ShopView _shopView;
    [SerializeField] private SoundController _soundController;
    [SerializeField] private List<PlatformView> _platformViews;
    [SerializeField] private UIHandler _ui;

    private Player _player;
    private Fabrica _fabrica;
    private PlatformsController _platformsController;
    private CollectablesController _collectablesController;
    private ObjectsPool _objectsPool;

    private void Awake()
    {
        CreateControllers();
        InitReferences();
        StartgameProcess();
    }

    private void CreateControllers()
    {
        _fabrica = CreateEmptyObjectWithScript<Fabrica>(nameof(Fabrica));
        _objectsPool = CreateEmptyObjectWithScript<ObjectsPool>(nameof(ObjectsPool));
        _platformsController = CreateEmptyObjectWithScript<PlatformsController>(nameof(PlatformsController));
        _collectablesController = CreateEmptyObjectWithScript<CollectablesController>(nameof(CollectablesController));
        _player = _fabrica.CreatePrefab(_fabrica.GetPrefabLinkFromFolder<Player>(nameof(Player)));
        _fabrica.CreatePrefab(_fabrica.GetPrefabLinkFromFolder<Floor>(nameof(Floor)));
    }

    private void InitReferences()
    {
        _mainCamera.Init(_player);
        _objectsPool.Init(_fabrica, _platformViews);
        _platformsController.Init(_objectsPool);
        _collectablesController.Init(_platformsController, _objectsPool);
        _player.Init(_platformsController, _fabrica, _shopView, _soundController);
        _ui.Init(_player, _fabrica, _platformsController);
    }

    private void StartgameProcess()
    {
        _player.StartGame();
        _ui.StartGame();
        _collectablesController.StartGame();
    }

    private T CreateEmptyObjectWithScript<T>(string name) where T : MonoBehaviour
    {
        return new GameObject(name).AddComponent<T>();
    }
}