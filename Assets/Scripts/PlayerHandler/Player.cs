using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlatformsController _platformsController;
    private Fabrica _fabrica;

    private ShopView _shopView;
    private SoundController _soundController;

    [SerializeField] private CollisionHandler _collisionHandler;
    [SerializeField] private JumpHandler _jumpHandler;
    [SerializeField] private ViewHandler _viewHandler;

    private bool _isJumpingOnAxisX = true;
   
    public event Action CollectablesAmountChanged;

    public bool IsJumpingOnAxisX => _isJumpingOnAxisX;

    public Fabrica Fabrica => _fabrica;

    public CollisionHandler CollisionHandler => _collisionHandler;
    public JumpHandler JumpHandler => _jumpHandler;

    public SoundController SoundController => _soundController;

    public ShopView ShopView => _shopView;

    private void OnDisable()
    {      
        _platformsController.PlatformHasSpawnedOnAxisX -= OnPlatformSpawned;
        _shopView.PlayerViewChanged -= OnPlayerViewChanged;
    }

    public void Init(PlatformsController platformsController, Fabrica fabrica, ShopView shopView, SoundController contr)
    {
        _platformsController = platformsController;
       // _platformsController.PlatformHasSpawnedOnAxisX += OnPlatformSpawned;
        _fabrica = fabrica;
        _shopView = shopView;
        _soundController = contr;
      //  _viewHandler.InitDefaultView();
       // _shopView.PlayerViewChanged += OnPlayerViewChanged;
    }

    public void StartGame()
    {
        _platformsController.PlatformHasSpawnedOnAxisX += OnPlatformSpawned;
        _viewHandler.InitDefaultView();
        transform.rotation = Quaternion.Euler(0, 90, 0);
        _shopView.PlayerViewChanged += OnPlayerViewChanged;
    }

    public void ChangeCollectablesAmount()
    {
        CollectablesAmountChanged?.Invoke();
    }

    private void OnPlayerViewChanged(PlayerViewSO playerViewSO)
    {
        _viewHandler.InitNewView(playerViewSO);
    }

    private void OnPlatformSpawned(bool isTrue)
    {
        _isJumpingOnAxisX = isTrue;
        RotatePlayer(isTrue);
    }

    private void RotatePlayer(bool isTrue)
    {
        if (isTrue) transform.rotation = Quaternion.Euler(0, 90, 0);
        else transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}