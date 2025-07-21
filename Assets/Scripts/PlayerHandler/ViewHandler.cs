using System.Collections.Generic;
using UnityEngine;

public class ViewHandler : MonoBehaviour
{
    private const string Piglin = "Piglin";

    [SerializeField] private Player _player;
    [SerializeField] private PlayerViewSO _currentPlayerViewSO;
    [SerializeField] private Transform _crown;
    [SerializeField] private Material _material;

    [SerializeField] private List<PlayerViewSO> _allAccessablePlayerViewsSO;

    public void InitDefaultView()
    {
        _material.mainTexture = _currentPlayerViewSO.ViewImage;
    }

    public void InitNewView(PlayerViewSO playerViewSO)
    {
        if (_currentPlayerViewSO != playerViewSO)
        {
            if (!CheckIfPlayerViewSOExist(playerViewSO))
            {
                _allAccessablePlayerViewsSO.Add(playerViewSO);
            }

            _currentPlayerViewSO = playerViewSO;
            _material.mainTexture = playerViewSO.ViewImage;
            _crown.gameObject.SetActive(CheckIfPig(playerViewSO));
        }
    }

    private bool CheckIfPig(PlayerViewSO playerViewSO)
    {
        if (playerViewSO.Name == Piglin) return true;
        return false;
    }

    private bool CheckIfPlayerViewSOExist(PlayerViewSO playerViewSO)
    {
        foreach (var item in _allAccessablePlayerViewsSO) if (item == playerViewSO) return true;
        return false;
    }
}