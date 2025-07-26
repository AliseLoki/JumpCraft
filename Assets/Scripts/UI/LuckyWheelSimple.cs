using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LuckyWheelSimple : MonoBehaviour
{
    private const float FullCircle = 360;
    private const float SectoreAngle = 45;

    [SerializeField] Button _wheelButton;
    [SerializeField] TMP_Text _prizeName;

    [SerializeField] private float _wheelSpeed = 800;
    [SerializeField] private float _timeBeforeCheating = 1;
    [SerializeField] private int _fullSpinningAmount = 3;

    [SerializeField] private List<WheelItem> _wheelItems;
    [SerializeField] private List<WheelItemSO> _wheelItemsSO;
    
    private void OnEnable()
    {
        FillInWheelImagesData();
    }

    public void OnLuckyWheelButtonDown()
    {
        StartCoroutine(SpinningRoutine(_wheelButton.transform, _wheelSpeed));
    }

    private IEnumerator SpinningRoutine(Transform wheel, float defaultSpeed)
    {
        int index = GetCheatPrize();
       
        SetInteractable(false);

        yield return DefaultRotationRoutine(_timeBeforeCheating, 0, defaultSpeed, wheel);

        float distance = FullCircle * _fullSpinningAmount - wheel.transform.rotation.eulerAngles.z + index * SectoreAngle;
        float timeBeforeStop = (2 * distance) / defaultSpeed;

        yield return DefaultRotationRoutine(timeBeforeStop, defaultSpeed, 0, wheel);

        SetInteractable(true);
    }

    private IEnumerator DefaultRotationRoutine(float stopTime, float a, float b, Transform wheel)
    {
        float passedTime = 0;

        while (passedTime < stopTime)
        {
            float speed = Mathf.Lerp(a, b, passedTime / stopTime);
            wheel.rotation *= Quaternion.Euler(0, 0, speed * Time.deltaTime);
            passedTime += Time.deltaTime;
            yield return null;
        }
    }

    private void SetInteractable(bool isInteractable)
    {
        _wheelButton.interactable = isInteractable;
    }

    private void FillInWheelImagesData()
    {
        for (int i = 0; i < _wheelItems.Count; i++)
        {
            var wheelItemSO = GetWheelItemSO();
            _wheelItems[i].Init(wheelItemSO.Name, wheelItemSO.Weight, wheelItemSO.Sprite);
        }
    }

    private WheelItemSO GetWheelItemSO()
    {
        int randomIndex = Random.Range(0, _wheelItemsSO.Count);

        if (_wheelItemsSO[randomIndex].Weight <= Random.Range(0, 1))
        {
           return GetWheelItemSO();
        }

        return _wheelItemsSO[randomIndex];
    }

    private int GetCheatPrize()
    {
        int randomSector = Random.Range(0, _wheelItems.Count);

        if (_wheelItems[randomSector].Weight <= Random.Range(0f, 1f))
        {
            return GetCheatPrize();
        }

        _prizeName.text = _wheelItems[randomSector].Name;

        return randomSector;
    }
}
