using UnityEngine;

[CreateAssetMenu]
public class WheelItemSO : ScriptableObject
{
    [SerializeField][Range(0, 1f)] private float _weight;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _name;

    public float Weight => _weight;
    public Sprite Sprite => _sprite;
    public string Name => _name;
}