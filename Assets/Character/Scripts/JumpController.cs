using System;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform mainObj;

    [Header("Jump configuration")] 
    [SerializeField] private AnimationCurve jumpCurve;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float length = 1f;
    [SerializeField] private float height = 1f;

    private Vector3 _initPos;
    private float _jumpPower;
    private float _jumpProgress;
    private bool _prepareJump;
    private bool _onAir;

    public float GetPower => _jumpPower;
    public float GetLength => length;
    
    public Action OnPrepareJump;
    public Action OnJump;
    public Action OnLand;
    public Action OnDrop;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PrepareJump();
        }
        
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Jump();
        }

        if (_prepareJump)
        {
            HandleJumpPower();
        }

        if (_onAir)
        {
            Fly();
        }
    }

    private void HandleJumpPower()
    {
        _jumpPower = Mathf.PingPong(Time.time, 1f);
    }

    private void Fly()
    {
        if(!_onAir) return;
        _jumpProgress = Mathf.MoveTowards(_jumpProgress, 1f, speed * Time.deltaTime);
        mainObj.transform.position = new Vector3(_initPos.x + _jumpProgress * (length * _jumpPower), jumpCurve.Evaluate(_jumpProgress) * (height * _jumpPower), _initPos.z);
        
        if (_jumpProgress >= 1f)
        {
            Land();
        }
    }

    private void PrepareJump()
    {
        if(_prepareJump) return;
        
        _prepareJump = true;
        _jumpProgress = 0f;
        OnPrepareJump?.Invoke();
    }

    private void Jump()
    {
        if(!_prepareJump) return;
        
        _prepareJump = false;
        _onAir = true;
        _initPos = mainObj.transform.position;
        OnJump?.Invoke();
    }

    private void Land()
    {
        _jumpProgress = 0f;
        _jumpPower = 0f;
        _onAir = false;
        if (OnGround())
        {
            OnLand?.Invoke();
        }
        else
        {
            Drop();
        }
    }

    private void Drop()
    {
        OnDrop?.Invoke();
    }

    private bool OnGround()
    {
        RaycastHit hit;
        Physics.Raycast(mainObj.transform.position, Vector3.down, out hit);
        if (hit.collider)
        {
            return true;
        }
        return false;
    }
    
}
