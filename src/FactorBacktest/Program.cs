using System.Globalization;

var symbol = "SPY"; 
var marketData = new DataProcessed(); 
var candles = marketData.getDailyCandles(symbol); 
Console.WriteLine($"Number of candles for {symbol}: {candles.Count}"); 