using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private UIHandler _ui;
    private ShopView _shopView;
    private SoundController _soundController;
    private GameController _gameController;

    [SerializeField] private CollisionHandler _collisionHandler;
    [SerializeField] private JumpHandler _jumpHandler;
    [SerializeField] private ViewHandler _viewHandler;
    [SerializeField] private Health _health;

    public CollisionHandler CollisionHandler => _collisionHandler;
    public JumpHandler JumpHandler => _jumpHandler;
    public SoundController SoundController => _soundController;
    public GameController GameController => _gameController;
    public ShopView ShopView => _shopView;

    public UIHandler UIHandler => _ui;

    public Health Health => _health;

    private void OnDisable()
    {
        _shopView.PlayerViewChanged -= OnPlayerViewChanged;
    }

    public void Init(ShopView shopView, SoundController contr, GameController plContr, UIHandler ui)
    {
        _shopView = shopView;
        _soundController = contr;
        _gameController = plContr;
        _ui = ui;
    }

    public void StartGame()
    {
        _viewHandler.InitDefaultView();
        transform.rotation = Quaternion.Euler(0, 90, 0);
        _shopView.PlayerViewChanged += OnPlayerViewChanged;
    }

    private void OnPlayerViewChanged(PlayerViewSO playerViewSO)
    {
        _viewHandler.InitNewView(playerViewSO);
    }
}