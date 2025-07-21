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

    public event Action CollectablesAmountChanged;

    public Fabrica Fabrica => _fabrica;

    public CollisionHandler CollisionHandler => _collisionHandler;
    public JumpHandler JumpHandler => _jumpHandler;

    public SoundController SoundController => _soundController;

    public ShopView ShopView => _shopView;

    public PlatformsController PlatformsController => _platformsController;

    private void OnDisable()
    { 
        _shopView.PlayerViewChanged -= OnPlayerViewChanged;
    }

    public void Init(PlatformsController platformsController, Fabrica fabrica, ShopView shopView, SoundController contr)
    {
        _platformsController = platformsController;
        _fabrica = fabrica;
        _shopView = shopView;
        _soundController = contr;
    }

    public void StartGame()
    {
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
}