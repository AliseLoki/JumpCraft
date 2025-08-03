using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning
{
    private float FullCircle = 360;
    private float _wheelSpeed = 800;
    private float _timeBeforeCheating = 1;
    private int _fullSpinningAmount = 3;

    public event Action<PrizeName> PrizeRecieved;

    private SoundController _soundController;

    public Spinning(SoundController soundController)
    {
        _soundController = soundController;
    }

    public IEnumerator SpinningRoutine(Transform wheel, List<WheelItem> wheelItems)
    {
        _soundController.PlaySound(SoundName.LuckyWheel.ToString());

        int index = GetCheatPrize(wheelItems);

        yield return DefaultRotationRoutine(_timeBeforeCheating, 0, _wheelSpeed, wheel);

        float distance = FullCircle * _fullSpinningAmount - wheel.transform.rotation.eulerAngles.z + index * (FullCircle / wheelItems.Count);
        float timeBeforeStop = (2 * distance) / _wheelSpeed;

        yield return DefaultRotationRoutine(timeBeforeStop, _wheelSpeed, 0, wheel);

        _soundController.StopSound();
        _soundController.PlaySound(SoundName.Win.ToString());

        PrizeRecieved?.Invoke(wheelItems[index].PrizeName);
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

    private int GetCheatPrize(List<WheelItem> wheelItems)
    {
        int randomSector = UnityEngine.Random.Range(0, wheelItems.Count);

        if (wheelItems[randomSector].Weight <= UnityEngine.Random.Range(0f, 1f))
        {
            return GetCheatPrize(wheelItems);
        }

        return randomSector;
    }
}