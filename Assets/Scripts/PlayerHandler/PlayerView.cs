using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private PlayerViewSO _playerViewSO;

    public PlayerViewSO PlayerViewSO => _playerViewSO;  
}
