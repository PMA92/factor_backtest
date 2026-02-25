using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

class DataProcessed : IMarketData
{
    public List<string> ListSymbols()
    {
        List<string> symbols = new List<string>();
        foreach (var file in Directory.GetFiles(_dataDir, FilePattern))
        {
            symbols.Add(Path.GetFileNameWithoutExtension(file));
        }
        return symbols;
    }

    public List<Candle> getDailyCandles(string symbol)
    {
        //add validation class and check before s
        var file = Path.Combine(ProcessedDataDict, string.Format(FilePattern, symbol));
        var candles = new List<Candle>();

        var lines = File.ReadAllLines(file);
        var headers = lines[0].Split(',').Select(h => h.Trim()).ToList();        
        var col = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        for (int i = 0; i < headers.Length; i++)
        {
            col[headers[i]] = i;
        }

        int dateIdx = Find(col, "date");
        int openIdx = Find(col, "open");
        int highIdx = Find(col, "high");
        int lowIdx = Find(col, "low");
        int closeIdx = Find(col, "close");
        int volumeIdx = Find(col, "volume");

        for (int r = 1; r < lines.Length; r++)
        {
            var line = lines[r].Trim();
            var parts = line.Split(',');

            var dateStr = parts[dateIdx].Trim();
            DateOnly date = DateOnly.Parse(dateStr);

            decimal open = decimal.Parse(parts[openIdx].Trim(), CultureInfo.InvariantCulture);
            decimal high = decimal.Parse(parts[highIdx].Trim(), CultureInfo.InvariantCulture);
            decimal low = decimal.Parse(parts[lowIdx].Trim(), CultureInfo.InvariantCulture);
            decimal close = decimal.Parse(parts[closeIdx].Trim(), CultureInfo.InvariantCulture);
            long volume = long.Parse(parts[volumeIdx].Trim());

            candles.Add(new Candle(date, open, high, low, close, volume));
        }
        candles.Sort((a, b) => a.Date.CompareTo(b.Date));
        candles = candles.DistinctBy(d => d.Name).ToList();
        return candles;
    }
    }
