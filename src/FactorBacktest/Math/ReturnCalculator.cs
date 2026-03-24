using System.ComponentModel;
class ReturnCalculator
{
    public Dictionary<DateOnly, decimal> DailyReturns(Dictionary<DateOnly, Candle> candles)
    {
        var returns = new Dictionary<DateOnly, decimal>();
        var candleVals = candles.Values.ToList();
        for (int i = 1; i < candles.Count - 1; i++)
        {
            decimal dailyReturn = candleVals[i].Close / candleVals[i - 1].Close;
            returns.Add(candleVals[i].Date, dailyReturn);
        }
        return returns;
    }
}