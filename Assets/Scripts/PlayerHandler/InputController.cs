using Unity.VisualScripting;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void OnMouseDown()
    {
        if (_player.UIHandler.IsGamePlaying)
        {
            _player.JumpHandler.IncreaseJumpPower();
        }
    }

    private void OnMouseUp()
    {
        if (_player.UIHandler.IsGamePlaying)
        {
            _player.SoundController.StopSound();
            _player.JumpHandler.Jump();
        }
    }
}