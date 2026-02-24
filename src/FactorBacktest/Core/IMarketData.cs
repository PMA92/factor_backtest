using System.ComponentModel;
using System.Reflection;

interface IMarketData
{
    Asset getAsset(int assetId);
    Candle getDailyCandle(int assetId, DateTime date);
}