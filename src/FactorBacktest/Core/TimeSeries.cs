using System.Collections;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO.Compression;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Reflection;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.Serialization;
using System.Runtime.Serialization.DataContracts;
using System.Runtime.Serialization.Json;
using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Channels;
using System.Threading.Tasks.Dataflow;
using System.Xml;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

class TimeSeries
{
    private Dictionary<DateOnly, Candle> _candlesDict;
    private List<DateOnly> _dates;
    private List<Candle> _candles;
    public TimeSeries(Dictionary<DateOnly, Candle> candles)
    {
        _candlesDict = candles;
        _dates = _candlesDict.Keys.ToList();
        _candles = _candlesDict.Values.ToList();
    }
    public decimal GetCloseForDate(DateOnly date)
    {
        return _candlesDict[date].Close;
    }
    public DateOnly? GetPreviousDate(DateOnly date)
    {
        int index = _dates.IndexOf(date);
        if (index <= 0) return null;
        return _dates[index - 1];
    }
    public List<Candle> GetDateRange(DateOnly start, DateOnly asOf)
    {
        var candlesInRange = new List<Candle>();
        
        startIndex = _dates.IndexOf(start);
        asOfIndex = _dates.IndexOf(asOf);
        if (startIndex <= 0 || asOfIndex <= 0)
        {
            Console.WriteLine("Date not found in data");
            return;
        } 
        if (startIndex > asOfIndex)
        {
            Console.WriteLine("Start date is later than 'as of' date");
            return;
        }
        
        for (int i = _dates.IndexOf(start); i <= _dates.IndexOf(asOf); i++)
        {
            candlesInRange.Add(_candles[i]);
        } 
        return candlesInRange;
    }
}