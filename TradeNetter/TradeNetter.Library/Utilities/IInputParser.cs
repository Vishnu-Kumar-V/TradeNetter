namespace TradeNetter.Library.Utilities
{
    public interface IInputParser
    {
        T ParseEnum<T>(string value);
        string ValidateInputTrade(string inputString);
    }
}