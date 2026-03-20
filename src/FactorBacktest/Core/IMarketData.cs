using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

interface IMarketData
{
    List<string> listSymbols();
    List<Candle> getDailyCandles(string symbol);
}