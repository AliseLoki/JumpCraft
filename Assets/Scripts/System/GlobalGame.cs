using UnityEngine;
using UnityEngine.UI;
using YG;

public class GlobalGame : MonoBehaviour
{
    //    [SerializeField] private CameraController _mainCamera;
    //    [SerializeField] private ShopView _shopView;
    //    [SerializeField] private SoundController _soundController;
    //    [SerializeField] private UIHandler _ui;
    [SerializeField] private Image _image;

    private void Awake()
    {
        //CheckDevice();
    }

    public void CheckDevice()
    {
        if (YG2.envir.isMobile || YG2.envir.isTablet)
        {
            _image.gameObject.SetActive(true);
        }
    }

   
    // 1) ������� ��������� ����� ����������, � ����������� �� ����� �������������:
    // - ��������� ������
    // - ����
    // 2) ������� ��� �������
    // 3) ��������������
    // 4) ��������� �� ���� ����� ����� ����

    // �������
    // ����� ����������� �� ���������
    // ����� ����
    // ����� ������ ������� ��� ��������� ��������
    // ��������� ����
    // ���������� ���������� ��������
    // ��������� ����

}
