using System.Collections.Generic;
using System.Text;


public class I64StreamEncoder
{
    StringBuilder output = new StringBuilder();

    const string ENC32_ALL = "0123456789abcdefghijklmnopqrstuv";
    const char ENC32_B1_FIRST = '0';
    const char ENC32_B1_LAST = '9';
    const long ENC32_B1_LENGTH = 10L;
    const char ENC32_B2_FIRST = 'a';
    const char ENC32_B2_LAST = 'v';
    const string ENC4H_ALL = "ABCDEFGHIJKLMNOP";
    const char ENC4H_FIRST = 'A';
    const char ENC4H_LAST = 'P';
    const string ENCVAR = "wxyzQRSTUVWXY";
    const char ENCVAR_B1_FIRST = 'w';
    const char ENCVAR_B1_LAST = 'z';
    const int ENCVAR_B1_LENGTH = 4;
    const char ENCVAR_B2_FIRST = 'Q';
    const char ENCVAR_B2_LAST = 'Y';
    const char ESCAPE_PREFIX = 'Z';
    const string SEPARATOR = "Z0";
    const string HARDCODED_MINVALUE = "ZZ0";

    public void NextChunk()
    {
        output.Append(SEPARATOR);
    }

    public void Append(IEnumerable<long> v)
    {
        foreach (long l in v)
            Append(l);
    }

    public void AppendChunk(IEnumerable<long> v)
    {
        foreach (long l in v)
            Append(l);

        NextChunk();
    }

    public void Append(long v)
    {
        if (v == long.MinValue)
        {
            output.Append(HARDCODED_MINVALUE);
            return;
        }

        if (v < 0)
        {
            output.Append(ESCAPE_PREFIX);
            v = -v;
        }

        if (v < 32)
        {
            output.Append(ENC32_ALL[(int)v]);
            return;
        }
        v = v - 32;

        if (v < 512)
        {
            long vlo = v & 31;
            long vhi = (v >> 5);

            output.Append(ENC4H_ALL[(int)vhi]);
            output.Append(ENC32_ALL[(int)vlo]);
            return;
        }
        v = v - 512;

        long varencode = output.Length;
        long shift_count = 0;
        output.Append(' ');

        do
        {
            output.Append(ENC32_ALL[(int)(v & 31)]);
            v >>= 5;
            shift_count += 1;
        } while (v != 0);

        output[(int)varencode] = ENCVAR[(int)shift_count - 1];
    }

    public override string ToString()
    {
        return output.ToString();
    }
}
