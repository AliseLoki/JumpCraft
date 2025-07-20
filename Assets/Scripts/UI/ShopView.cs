using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopView : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private List<PlayerViewSO> _playerViewsSO;
    [SerializeField] private SoundController _soundController;

    private Fabrica _fabrica;

    public event Action<PlayerViewSO> PlayerViewChanged;

    public void Init(Fabrica fabrica)
    {
        _fabrica = fabrica;
        //עמזו ג סעאנעדוויל
        InitAllShopItems();
    }

    public void ItemViewButtonClicked(PlayerViewSO playerViewSO)
    {
        _soundController.PlaySound(SoundName.ButtonPressed.ToString());
        PlayerViewChanged?.Invoke(playerViewSO);
    }

    private void InitAllShopItems()
    {
        foreach (var view in _playerViewsSO)
        {
            ItemView newView = _fabrica.CreatePrefab(_fabrica.GetPrefabLinkFromFolder<ItemView>(nameof(ItemView)),
                Quaternion.identity, _container);
            newView.InitItemViewData(view);
            newView.InitShopView(this);
        }
    }
}