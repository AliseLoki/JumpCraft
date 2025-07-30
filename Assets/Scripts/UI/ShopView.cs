using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class ShopView : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private List<PlayerViewSO> _playerViewsSO;
    [SerializeField] private SoundController _soundController;
    [SerializeField] private UIHandler _ui;

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
        if (CheckIfBought(playerViewSO))
        {
            _soundController.PlaySound(SoundName.ButtonPressed.ToString());
            PlayerViewChanged?.Invoke(playerViewSO);
        }
        else
        {
            if (_ui.Pay(ref YG2.saves.Diamond, -playerViewSO.Price, _ui.DiamondsAmountText))
            {
                _soundController.PlaySound(SoundName.ButtonPressed.ToString());
                PlayerViewChanged?.Invoke(playerViewSO);
            }
            else
            {
                _soundController.PlaySound(SoundName.No.ToString());
            }
        }
    }

    private bool CheckIfBought(PlayerViewSO playerViewSO)
    {
        foreach (var item in YG2.saves.AvailableViews)
        {
            if (item == playerViewSO) return true;
        }

        return false;
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