using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForagerTier1.Shared
{
    public class RefreshService : IRefreshService
    {
        public event Action RefreshRequested;
        public void CallRequestRefresh()
        {
            RefreshRequested?.Invoke();
        }
    }
}
