using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void OnMouseDown()
    {
        if (!_player.ShopView.isActiveAndEnabled)
        {
            _player.JumpHandler.IncreaseJumpPower();
        }
    }

    private void OnMouseUp()
    {
        if (!_player.ShopView.isActiveAndEnabled)
        {
            _player.SoundController.StopSound();
            _player.JumpHandler.Jump();
        }
    }
}