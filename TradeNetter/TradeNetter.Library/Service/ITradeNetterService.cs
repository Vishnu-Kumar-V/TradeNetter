using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeNetter.Library.Models;

namespace TradeNetter.Library.Service
{
    public interface ITradeNetterService
    {
        void AddTrade(Trade trade);
        double GetNettedPNL();
    }
}
