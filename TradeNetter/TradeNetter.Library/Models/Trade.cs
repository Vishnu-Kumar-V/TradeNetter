using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeNetter.Library.Types;

namespace TradeNetter.Library.Models
{
    public class Trade
    {
        public int TradeID { get; set; }

        public DirectionType Direction { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public UnderLyingType UnderLying { get; set; }
    }
}
