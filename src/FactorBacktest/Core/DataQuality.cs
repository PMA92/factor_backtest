class DataQuality
{
    public string symbol { get; init; }
    public int totalDays { get; init; }
    public DateOnly startDate { get; init; }
    public DateOnly endDate { get; init; }
    public List<DateOnly> missingDates { get; init; }
    public List<string> errors { get; init; }
}

