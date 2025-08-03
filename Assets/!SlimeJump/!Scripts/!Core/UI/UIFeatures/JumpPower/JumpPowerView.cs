using UnityEngine;
using UnityEngine.UI;

public class JumpPowerView : MonoBehaviour
{
    [SerializeField] private Image _jumpPowerScaleImage;

    float _minValue = 3;
    float _divider = 10;

    public void ChangeJumpPowerImage(float jumpPower)
    {
        SetJumpPowerScaleAmount((jumpPower - _minValue) / _divider);
    }

    public void SetJumpPowerScaleAmount(float jumpPower)
    {
        _jumpPowerScaleImage.fillAmount = jumpPower;
    }
}