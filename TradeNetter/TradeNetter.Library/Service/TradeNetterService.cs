using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeNetter.Library.Models;

namespace TradeNetter.Library.Service
{
    public class TradeNetterService : ITradeNetterService
    {
        private List<Trade> TradesList = new List<Trade>();

        /// <summary>
        /// Add trades
        /// </summary>
        /// <param name="trade"></param>
        public void AddTrade(Trade trade)
        {
            TradesList.Add(trade);
        }

        /// <summary>
        /// Calculate PNL
        /// </summary>
        /// <returns></returns>
        public double GetNettedPNL()
        {
            double nettedPNL = 0;
            TradesList = TradesList.OrderBy(t => t.UnderLying).OrderBy(t => t.TradeID).ToList();
            List<Trade> processedTrades = new List<Trade>();
            foreach (Trade trade in TradesList)
            {
                int quantity = trade.Quantity;
                while (quantity > 0)
                {
                    // Find trades under same underlying in opposite direction
                    Trade oppositeTrade = processedTrades.Find(x => x.UnderLying.Equals(trade.UnderLying) && x.Direction != trade.Direction);
                    if (null == oppositeTrade)
                        break;
                    //Get minimum quantity to calculate PNL
                    int minimumQuantity = (quantity > oppositeTrade.Quantity) ? oppositeTrade.Quantity : quantity;
                    nettedPNL += (minimumQuantity * trade.Price * Convert.ToDouble(trade.Direction)) + (minimumQuantity * oppositeTrade.Price * Convert.ToDouble(oppositeTrade.Direction));
                    Console.WriteLine("====================================================\n\t\t Netted PNL\n====================================================");
                    Console.WriteLine("(" + Convert.ToDouble(trade.Direction) * minimumQuantity + "*" + trade.Price + " + " + Convert.ToDouble(oppositeTrade.Direction) * minimumQuantity + "*" + oppositeTrade.Price + ")");
                    quantity = quantity - minimumQuantity;
                    if (oppositeTrade.Quantity == minimumQuantity)
                    {
                        processedTrades.Remove(oppositeTrade);
                    }
                    else
                    {
                        oppositeTrade.Quantity -= minimumQuantity;
                    }
                }
                //IF a trade is not filled in current iteration save it for next iterations
                if (quantity > 0)
                {
                    trade.Quantity = quantity;
                    processedTrades.Add(trade);
                }
            }
            return nettedPNL;
        }
    }
}
