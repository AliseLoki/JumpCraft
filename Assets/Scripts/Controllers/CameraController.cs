using UnityEngine;

public class CameraController : MonoBehaviour
{
    private const float MobileFOV = 40f;
    private const float DesktopFOV = 60f;

    [SerializeField] private ShopView _shopView;

    private float _currentFOV;

    private Player _player;

    private Vector3 _defaultPosition = new Vector3(-4.2f, 13.2f, -10.6f);
    private Vector3 _defaultRotation = new Vector3(33.7f, 37.8f, 6.4f);
    private Vector3 _offset = new Vector3(-4.2f, 9.2f, -10.6f);
    private Vector3 _offsetForOpenShop = new Vector3(4.45f, 2.37f, -6.5f);
    private Vector3 _rotationForOpenShop = new Vector3(15.4f, -3, 0);

    private void LateUpdate()
    {
        if (_shopView.gameObject.activeSelf)
        {
            SetPosition(_offsetForOpenShop, _rotationForOpenShop, DesktopFOV);
        }
        else
        {
            SetPosition(_offset, _defaultRotation,_currentFOV);
        }
    }

    public void Init(Player player, bool isMobile)
    {
        _player = player;

        if (isMobile) _currentFOV = MobileFOV;
        else _currentFOV = DesktopFOV;

        SetPosition(_defaultPosition, _defaultRotation, _currentFOV);
    }

    private void SetPosition(Vector3 offset, Vector3 rotation, float fov)
    {
        transform.position = _player.transform.position + offset;
        transform.eulerAngles = rotation;
        Camera.main.fieldOfView = fov;
    }
}