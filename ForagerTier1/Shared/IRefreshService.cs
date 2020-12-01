using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForagerTier1.Shared
{
    public interface IRefreshService
    {
        event Action RefreshRequested;
        void CallRequestRefresh();
    }
}
