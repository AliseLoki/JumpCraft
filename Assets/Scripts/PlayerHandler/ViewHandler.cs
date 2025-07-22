using UnityEngine;
using YG;

public class ViewHandler : MonoBehaviour
{
    private const string Piglin = "Piglin";

    [SerializeField] private Transform _crown;
    [SerializeField] private Material _material;

    public void InitDefaultView()
    {
        _material.mainTexture = YG2.saves.CurrentPlayerViewSO.ViewImage;
        _crown.gameObject.SetActive(CheckIfPig(YG2.saves.CurrentPlayerViewSO));
    }

    public void InitNewView(PlayerViewSO playerViewSO)
    {
        if (YG2.saves.CurrentPlayerViewSO != playerViewSO)
        {
            if (!CheckIfPlayerViewSOExist(playerViewSO))
            {
                YG2.saves.AvailableViews.Add(playerViewSO);             
            }

            YG2.saves.CurrentPlayerViewSO = playerViewSO;
            YG2.SaveProgress();

            _material.mainTexture = playerViewSO.ViewImage;
            _crown.gameObject.SetActive(CheckIfPig(playerViewSO));
        }
    }

    private bool CheckIfPig(PlayerViewSO playerViewSO)
    {
        if (playerViewSO.Name == Piglin) return true;
        return false;
    }

    private bool CheckIfPlayerViewSOExist(PlayerViewSO playerViewSO)
    {
        foreach (var item in YG2.saves.AvailableViews) if (item == playerViewSO) return true;
        return false;
    }
}