using System.Collections;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private Vector3 _startPos;
    private Vector3 _endPos;

    private float _offset = 5;
    private float _speed = 3f;

    void Start()
    {
        _startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - _offset);
        _endPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + _offset);
        StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        while (true)
        {
            while (transform.position != _endPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, _endPos, _speed * Time.deltaTime);
                yield return null;
            }

            while (transform.position != _startPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, _startPos, _speed * Time.deltaTime);
                yield return null;
            }
        }
    }
}