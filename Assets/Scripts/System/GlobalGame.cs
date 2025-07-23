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

   
    // 1) сначала проверяем какое устройство, в зависимости от этого устанавливаем:
    // - настройки камеры
    // - язык
    // 2) создаем все объекты
    // 3) инициализируем
    // 4) запускаем во всех метод старт гейм

    // События
    // игрок приземлился на платформу
    // игрок умер
    // игрок открыл магазин или стартовое обучение
    // изменился счет
    // изменилось количество аламазов
    // изменился скин

}
