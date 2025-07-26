using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private Player _player;

    private bool _isGrounded = true;

    public event Action<Platform> PlayerJumpedOnPlatform;

    public bool IsGrounded => _isGrounded;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent(out Pig pig))
        {
            _player.UIHandler.OpenLuckyWheelPanel();
        }


        if (collision.collider.TryGetComponent(out Platform platform))
        {
            _isGrounded = true;
            _player.SoundController.PlaySound(SoundName.Landing.ToString());
            _player.JumpHandler.OnPlayerIsLanded();
            PlayerJumpedOnPlatform?.Invoke(platform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Platform platform)) _isGrounded = false;
        _player.SoundController.PlaySound(SoundName.Jump.ToString());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Diamond diamond))
        {
            _player.SoundController.PlaySound(SoundName.Diamond.ToString());
            _player.ChangeCollectablesAmount();
            diamond.gameObject.SetActive(false);
        }

        if (other.TryGetComponent(out Heart heart))
        {
            _player.Health.ChangeHealth(1);
            heart.gameObject.SetActive(false);
        }
    }
}