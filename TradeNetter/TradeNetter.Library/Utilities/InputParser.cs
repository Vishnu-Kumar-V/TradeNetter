using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeNetter.Library.Types;

namespace TradeNetter.Library.Utilities
{
    public class InputParser : IInputParser
    {
        public T ParseEnum<T>(string value)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), value, true);
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException("Invalid input! " + ex.Message);
            }
        }

        public string ValidateInputTrade(string inputString)
        {
            string[] data = inputString.Split(' ');
            try
            {
                DirectionType direction = ParseEnum<DirectionType>(data[0]);
            }
            catch (Exception ex)
            {
                return "Invalid direction type: 'Buy' or 'Sell' are valid types";
            }
            try
            {
                int quantity = int.Parse(data[1]);
            }
            catch (Exception ex)
            {
                return "Invalid quantity: enter a valid integer";
            }
            try
            {
                double price = double.Parse(data[2]);
            }
            catch (Exception ex)
            {
                return "Invalid price: enter a valid double";
            }
            try
            {
                UnderLyingType underLying = ParseEnum<UnderLyingType>(data[3]);
            }
            catch (Exception ex)
            {
                return "Invalid underlying type: 'Oil' or 'Gas' are valid types";
            }
            return string.Empty;
        }
    }
}
