using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

interface IMarketData
{
    List<string> ListSymbols();
    List<Candle> getDailyCandles(string symbol);
}