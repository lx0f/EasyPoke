namespace EasyPoke.API.Data;

public class CsvReader
{
    private readonly StreamReader _sr;
    private readonly int _cC;
    private readonly bool _ih;
    private readonly char _d;

    public CsvReader(StreamReader streamReader, int columnCount, bool skipHeader)
    {
        _sr = streamReader;
        _cC = columnCount;

        if (skipHeader)
        {
            _sr.ReadLine();
        }
    }

    public List<List<string>> Read()
    {
        List<string> rawResult = new();

        int cInt = _sr.Peek();
        char c;
        while (cInt != -1)
        {
            c = (char)cInt;
            string buffer = string.Empty;
            if (c == '"')
            {
                buffer = GetQuoteCell();
            }
            else
            {
                buffer = GetCell();
            }

            rawResult.Add(buffer);

            cInt = _sr.Peek();
        }

        List<List<string>> result = Format(rawResult);

        return result;
    }

    private List<List<string>> Format(List<string> rawResult)
    {
        List<List<string>> result = new();
        for (int i = 0; i < rawResult.Count(); i += _cC)
        {
            List<string> row = rawResult.GetRange(i, _cC);
            result.Add(row);
        }

        return result;
    }

    private string GetCell()
    {
        char c = (char)_sr.Read();
        string buffer = "";
        while (c != ',' && c != '\n')
        {
            buffer += c;
            c = (char)_sr.Read();
        }

        return buffer;
    }

    private string GetQuoteCell()
    {
        string buffer = ((char)_sr.Read()).ToString();
        char c = (char)_sr.Read();
        while (c != '"')
        {
            buffer += c;
            c = (char)_sr.Read();
        }

        // skip char(10) after the last "
        _sr.Read();

        buffer += '"';

        return buffer;
    }
}

