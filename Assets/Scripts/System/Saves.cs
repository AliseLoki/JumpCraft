using System.Collections.Generic;
using UnityEngine;

namespace YG
{
    public partial class SavesYG
    {
        public int DiamondsAmount = 0;
        public PlayerViewSO CurrentPlayerViewSO = null;
        public List<PlayerViewSO> AvailableViews = new();
    }
}