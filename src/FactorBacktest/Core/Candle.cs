class Candle
{
    public string name { get; init; }
    public DateOnly date { get; init; }
    public decimal open { get; init; }
    public decimal high { get; init; }
    public decimal low { get; init; }
    public decimal close { get; init; }
    public decimal volume { get; init; }

    public Candle(string name, DateOnly date, decimal open, decimal high, decimal low, decimal close, long volume)
    {
        this.name = name;
        this.date = date;
        this.open = open;
        this.high = high;
        this.low = low;
        this.close = close;
        this.volume = volume;
    }
}





//date, open, high, low, close, volume
