using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeNetter.Library.Models;
using TradeNetter.Library.Service;
using TradeNetter.Library.Types;
using TradeNetter.Library.Utilities;
using Unity;

namespace TradeNetter
{
    class Program
    {
        static void Main(string[] args)
        {
            (new Program()).ProcessPNL(); ;
        }

        private void ProcessPNL()
        {
            var container = new UnityContainer();
            ITradeNetterService tradeNetterService = container.Resolve<TradeNetterService>();
            IInputParser inputParser = container.Resolve<InputParser>();

            Console.WriteLine("Please enter each trade in new line in the below format");
            Console.WriteLine("DIRECTION QUANTITY PRICE UNDERLYING");
            Console.WriteLine("Buy 1 100 Oil");
            Console.WriteLine("Sell 1 100 Oil");
            Console.WriteLine("Enter 'PNL' to get netted PNL value");
            Console.WriteLine("====================================================\n");
            try
            {
                int i = 0;
                string inputString = string.Empty;
                string errorString = string.Empty;
                string[] data;
                bool inValidInput;
                while (true)
                {
                    do
                    {
                        inValidInput = true;
                        inputString = Console.ReadLine();
                        data = inputString.Split(' ');
                        if (data[0].ToUpper().Equals("PNL"))
                            break;
                        errorString = inputParser.ValidateInputTrade(inputString);
                        if (!string.IsNullOrEmpty(errorString))
                        {
                            inValidInput = false;
                            Console.WriteLine(errorString);
                        }
                    } while (!inValidInput);
                    if (data[0].ToUpper().Equals("PNL"))
                        break;
                    DirectionType direction = inputParser.ParseEnum<DirectionType>(data[0]);
                    int quantity = int.Parse(data[1]);
                    double price = double.Parse(data[2]);
                    UnderLyingType underLying = inputParser.ParseEnum<UnderLyingType>(data[3]);
                    Trade newTrade = new Trade()
                    {
                        Direction = direction,
                        Quantity = quantity,
                        Price = price,
                        UnderLying = underLying,
                        TradeID = i++
                    };
                    tradeNetterService.AddTrade(newTrade);
                }
                Console.WriteLine("Netted PNL = " + tradeNetterService.GetNettedPNL());

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}
