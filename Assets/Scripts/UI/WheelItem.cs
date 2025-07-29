using UnityEngine;
using UnityEngine.UI;

public class WheelItem : MonoBehaviour
{
    [SerializeField] private int _index;
    [SerializeField][Range(0, 1)] private float _weight;
    [SerializeField] private string _name;

    [SerializeField] private Image _image;

    [SerializeField] private WheelItemSO _wheelItemSO;

    [SerializeField] private PrizeName _prizeName;

    public int Index => _index;
    public float Weight => _weight;
    public string Name => _name;

    public PrizeName PrizeName => _prizeName;

    public void Init(WheelItemSO wheelItemSO)
    {
        _name = wheelItemSO.Name;
        _weight = wheelItemSO.Weight;
        _image.sprite = wheelItemSO.Sprite;
        _prizeName = wheelItemSO.PrizeName;
    }
}
