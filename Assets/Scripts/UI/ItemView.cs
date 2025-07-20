using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _skinView;
    [SerializeField] private Image _background;
    [SerializeField] private Button _button;

    private PlayerViewSO _playerViewSO;
    private ShopView _shopView;

    private void OnEnable()
    {
        _button.onClick?.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick?.RemoveListener(OnButtonClick);
    }

    public void InitShopView(ShopView shopView)
    {
        _shopView = shopView;
    }

    public void InitItemViewData(PlayerViewSO playerViewSO)
    {
        _skinView.sprite = playerViewSO.View;
        _background.sprite = playerViewSO.Background;
        _text.text = playerViewSO.Price.ToString();
        _playerViewSO = playerViewSO;
    }

    private void OnButtonClick()
    {
        _shopView.ItemViewButtonClicked(_playerViewSO);
    }
}
