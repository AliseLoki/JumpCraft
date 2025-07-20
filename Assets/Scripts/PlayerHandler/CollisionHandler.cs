using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private Player _player;

    private bool _isGrounded = true;

    public bool IsGrounded => _isGrounded;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Platform platform)) _isGrounded = true;
        _player.SoundController.PlaySound(SoundName.Landing.ToString());
        _player.JumpHandler.OnPlayerIsLanded();

        if (collision.collider.TryGetComponent(out Floor floor))
            _player.SoundController.PlaySound(SoundName.FallingOnTheFloor.ToString());
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
    }
}