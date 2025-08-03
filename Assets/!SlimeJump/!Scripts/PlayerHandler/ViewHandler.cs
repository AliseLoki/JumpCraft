using UnityEngine;
using YG;

public class ViewHandler : MonoBehaviour
{
    private const string King = "King";
    private const string Helmet = "Helmet";
    private const string Viking = "Viking";

    [SerializeField] private Transform _crown;
    [SerializeField] private Transform _helmet;
    [SerializeField] private Transform _viking;

    [SerializeField] private PlayerViewSO _defaultViewSO;

    public void InitDefaultView()
    {
        if (YG2.saves.CurrentPlayerViewSO == null)
        {
            YG2.saves.CurrentPlayerViewSO = _defaultViewSO;
        }

        Check(YG2.saves.CurrentPlayerViewSO);
        YG2.saves.AvailableViews.Add(YG2.saves.CurrentPlayerViewSO);
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

            Check(playerViewSO);
        }
    }

    private void Check(PlayerViewSO playerViewSO)
    {
        if (playerViewSO.Name == King)
        {
            _crown.gameObject.SetActive(true);
            _helmet.gameObject.SetActive(false);
            _viking.gameObject.SetActive(false);
        }
        else if (playerViewSO.Name == Helmet)
        {
            _crown.gameObject.SetActive(false);
            _helmet.gameObject.SetActive(true);
            _viking.gameObject.SetActive(false);
        }
        else if (playerViewSO.Name == Viking)
        {
            _crown.gameObject.SetActive(false);
            _helmet.gameObject.SetActive(false);
            _viking.gameObject.SetActive(true);
        }
        else
        {
            _crown.gameObject.SetActive(false);
            _helmet.gameObject.SetActive(false);
            _viking.gameObject.SetActive(false);
        }
    }

    private bool CheckIfPlayerViewSOExist(PlayerViewSO playerViewSO)
    {
        foreach (var item in YG2.saves.AvailableViews) if (item == playerViewSO) return true;
        return false;
    }
}