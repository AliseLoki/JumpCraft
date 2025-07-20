using System.Collections.Generic;
using UnityEngine;

public class ViewHandler : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Transform _viewContainer;
    [SerializeField] private PlayerView _currentPlayerView;
    [SerializeField] private PlayerViewSO _currentPlayerViewSO;

    [SerializeField] private List<PlayerViewSO> _allAccessablePlayerViewsSO;

    public void InitDefaultView()
    {
        AddNewViewPrefab(_currentPlayerViewSO);
    }

    public void InitNewView(PlayerViewSO playerViewSO)
    {
        if (_currentPlayerViewSO != playerViewSO)
        {
            if (!CheckIfPlayerViewSOExist(playerViewSO))
            {
                _allAccessablePlayerViewsSO.Add(playerViewSO);
                AddNewViewPrefab(playerViewSO);
            }

            _currentPlayerViewSO = playerViewSO;
            SetExistingPrefabActive(playerViewSO);
        }
    }

    private bool CheckIfPlayerViewSOExist(PlayerViewSO playerViewSO)
    {
        foreach (var item in _allAccessablePlayerViewsSO)
        {
            if (item == playerViewSO) return true;
        }

        return false;
    }

    private void AddNewViewPrefab(PlayerViewSO playerViewSO)
    {
        if (_currentPlayerView != null) _currentPlayerView.gameObject.SetActive(false);
        _currentPlayerView = _player.Fabrica.CreatePrefab(playerViewSO.Prefab, Quaternion.identity, _viewContainer);
        RotateViewAccordingToPlayerRotation(_currentPlayerView);
        _currentPlayerView.gameObject.SetActive(true);
    }

    private void SetExistingPrefabActive(PlayerViewSO playerViewSO)
    {
        foreach (Transform child in _viewContainer)
        {
            PlayerView existingView = child.GetComponent<PlayerView>();

            if (existingView.PlayerViewSO == playerViewSO)
            {
                RotateViewAccordingToPlayerRotation(existingView);
                existingView.gameObject.SetActive(true);              
            }
            else
            {
                existingView.gameObject.SetActive(false);
            }
        }
    }

    private void RotateViewAccordingToPlayerRotation(PlayerView view)
    {
        view.transform.rotation = transform.rotation;
    }
}