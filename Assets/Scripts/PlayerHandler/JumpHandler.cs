using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class JumpHandler : MonoBehaviour
{
    private const int NumJumps = 1;

    [SerializeField] private int _jumpPower = 3;
   // [SerializeField] private int _minJumpPower = 3;
    //[SerializeField] private int _maxJumpPower = 13;
   // [SerializeField] private float _pauseBetweenIncreasingJumpPower = 0.2f;
   // [SerializeField] private float _duration = 1;
    //[SerializeField] private float _jumpHeight = 0;

    [SerializeField] private Player _player;

    private Coroutine _coroutine;

    public event Action<float> JumpPowerChanged;

    public Action OnPrepareJump;
    public Action OnJump;
    public Action OnLand;
    public Action OnDrop;

    public float GetCurrentPower => _jumpPower;

    public float GetMaxJumpPower => Semen.Instance.MaxJumpPower;

    public void IncreaseJumpPower()
    {
        if (_player.CollisionHandler.IsGrounded)
        {
            _coroutine = StartCoroutine(JumpPowerIncreaser());
        }
    }

    public void Jump()
    {
        if (_player.CollisionHandler.IsGrounded)
        {
            if (_player.IsJumpingOnAxisX) JumpDefault(_jumpPower, 0);
            else JumpDefault(0, _jumpPower);
        }
    }

    public void OnPlayerIsLanded()
    {
        OnLand?.Invoke();
    }

    private void JumpDefault(float jumpPowerX, float jumpPowerZ)
    {
        OnJump?.Invoke();

        transform.DOJump(new Vector3(transform.position.x + jumpPowerX, transform.position.y + Semen.Instance.MinJumpPower,
            transform.position.z + jumpPowerZ), Semen.Instance.JumpHeight, NumJumps, Semen.Instance.JumpDuration);

        //transform.DOJump(new Vector3(transform.position.x + jumpPowerX, transform.position.y,
        //    transform.position.z + jumpPowerZ), _jumpHeight, NumJumps, _duration);
        if (_coroutine != null) StopCoroutine(_coroutine);
        SetJumpPower(Semen.Instance.MinJumpPower);
    }

    private IEnumerator JumpPowerIncreaser()
    {
        while (!Input.GetMouseButtonUp(0))
        {
            _player.SoundController.PlaySound(SoundName.JumpPowerUp.ToString());
            OnPrepareJump?.Invoke();

            for (int i = Semen.Instance.MinJumpPower; i <= Semen.Instance.MaxJumpPower; i++)
            {
                SetJumpPower(i);
                yield return new WaitForSeconds(Semen.Instance._pauseBetweenIncreasingJumpPower);
            }

            _player.SoundController.PlaySound(SoundName.JumpPowerDown.ToString());

            for (int i = Semen.Instance.MaxJumpPower; i >= Semen.Instance.MinJumpPower; i--)
            {
                SetJumpPower(i);
                yield return new WaitForSeconds(Semen.Instance._pauseBetweenIncreasingJumpPower);
            }
        }
    }

    private void SetJumpPower(int jumpPower)
    {
        _jumpPower = jumpPower;
        JumpPowerChanged?.Invoke(_jumpPower);
        Debug.Log(_jumpPower);
    }
}
