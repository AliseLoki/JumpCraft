using System.Collections.Generic;
using UnityEngine;

namespace YG
{
    public partial class SavesYG
    {
        public int Heart = 2;
        public int Diamond = 0;
        public int Coin = 0;
        public PlayerViewSO CurrentPlayerViewSO = null;
        public List<PlayerViewSO> AvailableViews = new();
    }
}