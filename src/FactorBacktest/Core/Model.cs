using System.Reflection;

class Asset
{
    public int AssetId { get; init; }
    public string Name { get; init; }
    public string Symbol { get; init; }
    public AssetType Type { get; init; }
}

public enum AssetType
{
    Stock,
    ETF,
}