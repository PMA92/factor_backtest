using System.ComponentModel;
using System.Globalization;

var symbol = "SPY"; 
var marketData = new DataProcessed(); 
var candles = marketData.GetDailyCandles(symbol); 

var returnCalculator = new ReturnCalculator();
var dailyReturns = returnCalculator.DailyReturns(candles);


var last5 = dailyReturns.TakeLast(5);

var date = new DateOnly(2024, 2, 18);
var asOf = new DateOnly(2026, 2, 20);
var timeSeries = new TimeSeries(candles);
List<Candle> dates = timeSeries.GetDateRange(date, asOf);
foreach (var d in dates)
{
    Console.WriteLine(d);
    Console.WriteLine(d.Date);
}