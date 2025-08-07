using System.Collections.Generic;

namespace YG
{
    public partial class SavesYG
    {
        public int Heart = 3;
        public int Diamond = 0;
        public int Coin = 0;
        public int Score = 0;
        public PlayerViewSO CurrentPlayerViewSO = null;
        public List<PlayerViewSO> AvailableViews = new();
    }
}