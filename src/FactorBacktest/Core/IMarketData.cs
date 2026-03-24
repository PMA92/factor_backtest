using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

interface IMarketData
{
    List<string> ListSymbols();
    Dictionary<DateOnly, Candle> GetDailyCandles(string symbol);
}