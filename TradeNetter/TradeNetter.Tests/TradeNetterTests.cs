using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeNetter.Library.Models;
using TradeNetter.Library.Service;
using TradeNetter.Library.Types;
using TradeNetter.Library.Utilities;

namespace TradeNetter.Tests
{
    [TestClass]
    public class TradeNetterTests
    {
        [TestMethod]
        public void Test_GetNettedPNL_ThreeTrades_With_Same_Direction()
        {
            // Arrange
            InputParser inputParser = new InputParser();
            TradeNetterService tradeNetterService = new TradeNetterService();
            Trade Trade1 = new Trade()
            {
                Direction = inputParser.ParseEnum<DirectionType>("Buy"),
                Quantity = 2,
                Price = 100,
                UnderLying = inputParser.ParseEnum<UnderLyingType>("Oil"),
                TradeID = 1
            };
            tradeNetterService.AddTrade(Trade1);
            Trade Trade2 = new Trade()
            {
                Direction = inputParser.ParseEnum<DirectionType>("Buy"),
                Quantity = 2,
                Price = 110,
                UnderLying = inputParser.ParseEnum<UnderLyingType>("Oil"),
                TradeID = 2
            };
            tradeNetterService.AddTrade(Trade2);
            Trade Trade3 = new Trade()
            {
                Direction = inputParser.ParseEnum<DirectionType>("Buy"),
                Quantity = 3,
                Price = 102,
                UnderLying = inputParser.ParseEnum<UnderLyingType>("Oil"),
                TradeID = 3
            };
            tradeNetterService.AddTrade(Trade3);
            // Act
            double actual = tradeNetterService.GetNettedPNL();
            // Assert
            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void Test_GetNettedPNL_TowTrades_With_Opposite_Direction_Same_Value()
        {
            // Arrange
            InputParser inputParser = new InputParser();
            TradeNetterService tradeNetterService = new TradeNetterService();
            Trade Trade1 = new Trade()
            {
                Direction = inputParser.ParseEnum<DirectionType>("Buy"),
                Quantity = 2,
                Price = 100,
                UnderLying = inputParser.ParseEnum<UnderLyingType>("Oil"),
                TradeID = 1
            };
            tradeNetterService.AddTrade(Trade1);
            Trade Trade2 = new Trade()
            {
                Direction = inputParser.ParseEnum<DirectionType>("Sell"),
                Quantity = 2,
                Price = 110,
                UnderLying = inputParser.ParseEnum<UnderLyingType>("Oil"),
                TradeID = 2
            };
            tradeNetterService.AddTrade(Trade2);
            // Act
            double actual = tradeNetterService.GetNettedPNL();
            // Assert
            Assert.AreEqual(20, actual);
        }

        [TestMethod]
        public void Test_GetNettedPNL_TowTrades_With_Opposite_Direction_Different_Value()
        {
            // Arrange
            InputParser inputParser = new InputParser();
            TradeNetterService tradeNetterService = new TradeNetterService();
            Trade Trade1 = new Trade()
            {
                Direction = inputParser.ParseEnum<DirectionType>("Buy"),
                Quantity = 1,
                Price = 100,
                UnderLying = inputParser.ParseEnum<UnderLyingType>("Oil"),
                TradeID = 1
            };
            tradeNetterService.AddTrade(Trade1);
            Trade Trade2 = new Trade()
            {
                Direction = inputParser.ParseEnum<DirectionType>("Sell"),
                Quantity = 4,
                Price = 110,
                UnderLying = inputParser.ParseEnum<UnderLyingType>("Oil"),
                TradeID = 2
            };
            tradeNetterService.AddTrade(Trade2);
            // Act
            double actual = tradeNetterService.GetNettedPNL();
            // Assert
            Assert.AreEqual(10, actual);
        }

        [TestMethod]
        public void Test_GetNettedPNL_DifferentPNL()
        {
            // Arrange
            InputParser inputParser = new InputParser();
            TradeNetterService tradeNetterService = new TradeNetterService();
            Trade Trade1 = new Trade()
            {
                Direction = inputParser.ParseEnum<DirectionType>("Buy"),
                Quantity = 1,
                Price = 100,
                UnderLying = inputParser.ParseEnum<UnderLyingType>("Oil"),
                TradeID = 1
            };
            tradeNetterService.AddTrade(Trade1);
            Trade Trade2 = new Trade()
            {
                Direction = inputParser.ParseEnum<DirectionType>("Sell"),
                Quantity = 4,
                Price = 110,
                UnderLying = inputParser.ParseEnum<UnderLyingType>("Gas"),
                TradeID = 2
            };
            tradeNetterService.AddTrade(Trade2);
            Trade Trade3 = new Trade()
            {
                Direction = inputParser.ParseEnum<DirectionType>("Buy"),
                Quantity = 2,
                Price = 120,
                UnderLying = inputParser.ParseEnum<UnderLyingType>("Gas"),
                TradeID = 3
            };
            tradeNetterService.AddTrade(Trade3);
            Trade Trade4 = new Trade()
            {
                Direction = inputParser.ParseEnum<DirectionType>("Sell"),
                Quantity = 5,
                Price = 115,
                UnderLying = inputParser.ParseEnum<UnderLyingType>("Oil"),
                TradeID = 4
            };
            tradeNetterService.AddTrade(Trade4);
            // Act
            double actual = tradeNetterService.GetNettedPNL();
            // Assert
            Assert.AreEqual(-5, actual);
        }
    }
}
