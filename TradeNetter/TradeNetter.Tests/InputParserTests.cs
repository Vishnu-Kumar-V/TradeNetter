using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TradeNetter.Library.Types;
using TradeNetter.Library.Utilities;

namespace TradeNetter.Tests
{
    [TestClass]
    public class InputParserTests
    {
        [DataTestMethod]
        [DataRow("Buy")]
        [DataRow("Sell")]
        public void Test_ParseEnum_DirectionType_HappyPaths(string input)
        {
            // Arrrage
            InputParser inputParser = new InputParser();
            // Act
            DirectionType direction = inputParser.ParseEnum<DirectionType>(input);
            // Assert
            Assert.IsInstanceOfType(direction, typeof(DirectionType));
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow(null)]
        [DataRow("Blah")]
        public void Test_ParseEnum_DirectionType_SadPaths(string input)
        {
            // Arrrage
            InputParser inputParser = new InputParser();
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => inputParser.ParseEnum<DirectionType>(input));
        }

        [DataTestMethod]
        [DataRow("Oil")]
        [DataRow("Gas")]
        public void Test_ParseEnum_UnderLyingType_HappyPaths(string input)
        {
            // Arrrage
            InputParser inputParser = new InputParser();
            // Act
            UnderLyingType direction = inputParser.ParseEnum<UnderLyingType>(input);
            // Assert
            Assert.IsInstanceOfType(direction, typeof(UnderLyingType));
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow(null)]
        [DataRow("Blah")]
        public void Test_ParseEnum_UnderLyingType_SadPaths(string input)
        {
            // Arrrage
            InputParser inputParser = new InputParser();
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => inputParser.ParseEnum<UnderLyingType>(input));
        }

        [DataTestMethod]
        [DataRow("buy 1 100 oil")]
        [DataRow("BUY 1 100 OIL")]
        [DataRow("sell 2 200 oil")]
        [DataRow("SELL 2 200 OIL")]
        [DataRow("buy 1 100 gas")]
        [DataRow("BUY 1 100 GAS")]
        [DataRow("sell 2 200 gas")]
        [DataRow("SELL 2 200 GAS")]

        public void Test_ValidateInputTrade_HappyPaths(string input)
        {
            // Arrrage
            InputParser inputParser = new InputParser();
            // Act
            string returnErrorMessage = inputParser.ValidateInputTrade(input);
            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(returnErrorMessage));
        }

        [DataTestMethod]
        [DataRow("INVALID 1 100 Oil")]
        [DataRow("Buy INVALID 100 Oil")]
        [DataRow("Sell 2 INVALID Oil")]
        [DataRow("Sell 2 100 INVALID")]
        [DataRow("PNL")]
        public void Test_ValidateInputTrade_SadPaths(string input)
        {
            // Arrrage
            InputParser inputParser = new InputParser();
            // Act
            string returnErrorMessage = inputParser.ValidateInputTrade(input);
            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(returnErrorMessage));
        }
    }
}
