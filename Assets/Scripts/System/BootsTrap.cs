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
    private GameController _gameController;
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
        _fabrica = new Fabrica();
        _collectablesController = new CollectablesController();

        CreateEmptyObjectWithScript<Semen>(nameof(Semen));
        _objectsPool = CreateEmptyObjectWithScript<ObjectsPool>(nameof(ObjectsPool));
        _gameController = CreateEmptyObjectWithScript<GameController>(nameof(GameController));
        _player = _fabrica.CreatePrefab(_fabrica.GetPrefabLinkFromFolder<Player>(nameof(Player)));
    }

    private void InitReferences()
    {
        _mainCamera.Init(_player, CheckDevice());
        _objectsPool.Init(_fabrica);
        _gameController.Init(_objectsPool, _player, _collectablesController);
        _collectablesController.Init( _objectsPool);
        _player.Init(_shopView, _soundController, _gameController, _ui);
        _ui.Init(_player, _fabrica, _gameController);
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
    }

    private T CreateEmptyObjectWithScript<T>(string name) where T : MonoBehaviour
    {
        return new GameObject(name).AddComponent<T>();
    }
}