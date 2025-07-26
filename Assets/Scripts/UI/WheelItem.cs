using UnityEngine;
using UnityEngine.UI;

public class WheelItem : MonoBehaviour
{
    [SerializeField] private int _index;
    [SerializeField][Range(0, 1)] private float _weight;
    [SerializeField] private string _name;

    [SerializeField] private Image _image;

    public int Index => _index;
    public float Weight => _weight;
    public string Name => _name;

    public void Init(string name, float weight, Sprite sprite)
    {
        _name = name;
        _weight = weight;
        _image.sprite = sprite;
    }
}
