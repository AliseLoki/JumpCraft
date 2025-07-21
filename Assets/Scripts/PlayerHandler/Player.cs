using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private ShopView _shopView;
    private SoundController _soundController;

    [SerializeField] private CollisionHandler _collisionHandler;
    [SerializeField] private JumpHandler _jumpHandler;
    [SerializeField] private ViewHandler _viewHandler;

    public event Action CollectablesAmountChanged;

    public CollisionHandler CollisionHandler => _collisionHandler;
    public JumpHandler JumpHandler => _jumpHandler;

    public SoundController SoundController => _soundController;

    public ShopView ShopView => _shopView;

    private void OnDisable()
    { 
        _shopView.PlayerViewChanged -= OnPlayerViewChanged;
    }

    public void Init( ShopView shopView, SoundController contr)
    {
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