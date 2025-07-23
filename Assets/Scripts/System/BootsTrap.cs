using UnityEngine;
using YG;

public class BootsTrap : MonoBehaviour
{
    [SerializeField] private CameraController _mainCamera;
    [SerializeField] private UIHandler _ui;

    [SerializeField] private ShopView _shopView;
    [SerializeField] private SoundController _soundController;

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
        CreateEmptyObjectWithScript<Semen>(nameof(Semen));
        _fabrica = CreateEmptyObjectWithScript<Fabrica>(nameof(Fabrica));
        _objectsPool = CreateEmptyObjectWithScript<ObjectsPool>(nameof(ObjectsPool));
        _platformsController = CreateEmptyObjectWithScript<PlatformsController>(nameof(PlatformsController));
        _collectablesController = CreateEmptyObjectWithScript<CollectablesController>(nameof(CollectablesController));
        _player = _fabrica.CreatePrefab(_fabrica.GetPrefabLinkFromFolder<Player>(nameof(Player)));
    }

    private void InitReferences()
    {
        _mainCamera.Init(_player, CheckDevice());
        _objectsPool.Init(_fabrica);
        _platformsController.Init(_objectsPool, _player);
        _collectablesController.Init(_platformsController, _objectsPool);
        _player.Init(_shopView, _soundController, _platformsController);
        _ui.Init(_player, _fabrica, _platformsController);
    }

    private bool CheckDevice()
    {
        if (YG2.envir.isMobile || YG2.envir.isTablet) return true;
        return false;
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