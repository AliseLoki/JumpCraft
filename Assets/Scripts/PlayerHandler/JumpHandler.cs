using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class JumpHandler : MonoBehaviour
{
    private const int NumJumps = 1;

    [SerializeField] private int _jumpPower = 3;
    [SerializeField] private int _minJumpPower = 3;
    [SerializeField] private int _maxJumpPower = 13;
    [SerializeField] private float _pauseBetweenIncreasingJumpPower = 0.2f;
    [SerializeField] private float _duration = 1;
    [SerializeField] private float _jumpHeight = 3;

    [SerializeField] private Player _player;

    private Coroutine _coroutine;

    public event Action<float> JumpPowerChanged;

    public Action OnPrepareJump;
    public Action OnJump;
    public Action OnLand;
    public Action OnDrop;

    public float GetCurrentPower => _jumpPower;

    public float GetMaxJumpPower => _maxJumpPower;

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

        transform.DOJump(new Vector3(transform.position.x + jumpPowerX, transform.position.y,
            transform.position.z + jumpPowerZ), _jumpHeight, NumJumps, _duration);
        if (_coroutine != null) StopCoroutine(_coroutine);
        SetDefaultNumberOfJumpPower();
    }

    private IEnumerator JumpPowerIncreaser()
    {
        while (!Input.GetMouseButtonUp(0))
        {
            _player.SoundController.PlaySound(SoundName.JumpPowerUp.ToString());
            OnPrepareJump?.Invoke();

            for (int i = _minJumpPower; i <= _maxJumpPower; i++)
            {
                SetJumpPower(i);
                yield return new WaitForSeconds(_pauseBetweenIncreasingJumpPower);
            }

            _player.SoundController.PlaySound(SoundName.JumpPowerDown.ToString());

            for (int i = _maxJumpPower; i >= _minJumpPower; i--)
            {
                SetJumpPower(i);
                yield return new WaitForSeconds(_pauseBetweenIncreasingJumpPower);
            }
        }
    }

    private void SetJumpPower(int jumpPower)
    {
        _jumpPower = jumpPower;
        JumpPowerChanged?.Invoke(_jumpPower);
    }

    private void SetDefaultNumberOfJumpPower()
    {
        _jumpPower = _minJumpPower;
        JumpPowerChanged.Invoke(_jumpPower);
    }
}
