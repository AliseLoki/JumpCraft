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
        if (collision.collider.TryGetComponent(out Platform platform))
        {
            _isGrounded = true;
            _player.SoundController.PlaySound(SoundName.Landing.ToString());
            _player.JumpHandler.OnPlayerIsLanded();
            PlayerJumpedOnPlatform?.Invoke(platform);
        }

        if(collision.collider.TryGetComponent(out Trampoline trampoline))
        {
            _player.SoundController.PlaySound(SoundName.Landing.ToString());
            _player.JumpHandler.OnPlayerIsLanded();
            PlayerJumpedOnPlatform?.Invoke(trampoline.Platform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Platform platform)) _isGrounded = false;
        _player.SoundController.PlaySound(SoundName.Jump.ToString());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Pig pig))
        {
            _player.SoundController.PlaySound(SoundName.Rabbit.ToString());
            pig.gameObject.SetActive(false);
            _player.UIHandler.OpenLuckyWheelPanel();
        }

        if (other.TryGetComponent(out Collectable collectable))
        {
            _player.UIHandler.ChangeCollectablesAmount(collectable.Name);
            collectable.gameObject.SetActive(false);
        }
    }
}