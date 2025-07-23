using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class JumpHandler : MonoBehaviour
{
    private const int NumJumps = 1;

    private float _jumpPower = 3;
    private int _minJumpPower = 3;
    private int _maxJumpPower = 13;
    private float _pauseBetweenIncreasingJumpPower = 0.2f;
    private float _duration = 0.5f;
    private float _jumpHeight = 3;
    private float _platformsOffset = 3f;

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
            JumpDefault(_jumpPower, 0);
        }
    }

    public void OnPlayerIsLanded()
    {
        OnLand?.Invoke();
    }

    public void RedTrampolineJump(Platform firstPlatform)
    {
        float random = UnityEngine.Random.Range(firstPlatform.transform.position.x, firstPlatform.transform.position.x + Semen.Instance.RedTrampolineSuccessfullJump);

        transform.DOJump(new Vector3(random, firstPlatform.transform.position.y + _platformsOffset,
          firstPlatform.transform.position.z), _jumpHeight, NumJumps, _duration);
    }


    public void TrampolineJump(Platform firstPlatform)
    {
        transform.DOJump(new Vector3(firstPlatform.transform.position.x, firstPlatform.transform.position.y + _platformsOffset,
            firstPlatform.transform.position.z), _jumpHeight, NumJumps, _duration);
    }

    private void JumpDefault(float jumpPowerX, float jumpPowerZ)
    {
        OnJump?.Invoke();

        transform.DOJump(new Vector3(transform.position.x + jumpPowerX, transform.position.y + _platformsOffset,
            transform.position.z + jumpPowerZ), _jumpHeight, NumJumps, _duration);

        if (_coroutine != null) StopCoroutine(_coroutine);
        SetJumpPower(_minJumpPower);
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
}
