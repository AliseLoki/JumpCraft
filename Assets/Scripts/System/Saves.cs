using System.Collections.Generic;
using UnityEngine;

namespace YG
{
    public partial class SavesYG
    {
        public int DiamondsAmount = 0;
        public PlayerViewSO CurrentPlayerViewSO = Resources.Load("SteveDefaultViewSO") as PlayerViewSO;
        public List<PlayerViewSO> AvailableViews = new List<PlayerViewSO>() { Resources.Load("SteveDefaultViewSO") as PlayerViewSO };
    }
}