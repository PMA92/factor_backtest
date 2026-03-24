using System.ComponentModel;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;

record ProcessedDataConfig(
    string Dir = "data/processed/",
    string FilePattern = "{symbol}.csv"
);

class DataProcessed : IMarketData
{
    private readonly string _dir;
    private readonly string _filePattern;
    private readonly Dictionary<string, List<Candle>> _cache = new();

    public DataProcessed(ProcessedDataConfig? config = null)
    {
        config ??= new ProcessedDataConfig();
        _dir = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "..", config.Dir);       
        _filePattern = config.FilePattern;
    }

    private string GetFullPath(string symbol) =>
        Path.Combine(_dir, _filePattern.Replace("{symbol}", symbol));

    public List<string> ListSymbols()
    {
        return Directory
            .GetFiles(_dir, "*.csv")
            .Select(Path.GetFileNameWithoutExtension)
            .Where(s => s is not null)
            .ToList()!;
    }

    public Dictionary<DateOnly, Candle> GetDailyCandles(string symbol)
    {
        var file = GetFullPath(symbol);
        if (!File.Exists(file))
            throw new FileNotFoundException($"No data file found for symbol '{symbol}'.", file);

        var lines = File.ReadAllLines(file);
        var headers = lines[0].Split(',').Select(h => h.Trim()).ToList();

        var col = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        for (int i = 0; i < headers.Count; i++)
            col[headers[i]] = i;

        int dateIdx   = FindColumn(col, "date");
        int openIdx   = FindColumn(col, "open");
        int highIdx   = FindColumn(col, "high");
        int lowIdx    = FindColumn(col, "low");
        int closeIdx  = FindColumn(col, "close");
        int volumeIdx = FindColumn(col, "volume");

        var candles = new List<Candle>();

        for (int r = 1; r < lines.Length; r++)
        {
            var line = lines[r].Trim();
            if (string.IsNullOrEmpty(line)) continue;

            var parts = line.Split(',');

            var date   = DateOnly.Parse(parts[dateIdx].Trim());
            var open   = decimal.Parse(parts[openIdx].Trim(),   CultureInfo.InvariantCulture);
            var high   = decimal.Parse(parts[highIdx].Trim(),   CultureInfo.InvariantCulture);
            var low    = decimal.Parse(parts[lowIdx].Trim(),    CultureInfo.InvariantCulture);
            var close  = decimal.Parse(parts[closeIdx].Trim(),  CultureInfo.InvariantCulture);
            var volume = long.Parse(parts[volumeIdx].Trim());

            candles.Add(new Candle(symbol, date, open, high, low, close, volume));
        }

        var result = candles
            .DistinctBy(c => c.Date)
            .OrderBy(c => c.Date)
            .ToList();

        
        return result.ToDictionary(c => c.Date);

    }

    private static int FindColumn(Dictionary<string, int> col, string name)
    {
        if (!col.TryGetValue(name, out int idx))
            throw new Exception($"Column '{name}' not found in CSV header.");
        return idx;
    }
}