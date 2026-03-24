class Candle
{
    public string Name { get; init; }
    public DateOnly Date { get; init; }
    public decimal Open { get; init; }
    public decimal High { get; init; }
    public decimal Low { get; init; }
    public decimal Close { get; init; }
    public decimal Volume { get; init; }

    public Candle(string Name, DateOnly Date, decimal Open, decimal High, decimal Low, decimal Close, long Volume)
    {
        this.Name = Name;
        this.Date = Date;
        this.Open = Open;
        this.High = High;
        this.Low = Low;
        this.Close = Close;
        this.Volume = Volume;
    }
}





//date, open, high, low, close, volume
