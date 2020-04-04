using System;
using System.Collections.Generic;


public class I64StreamDecoder
{
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
    const string reserved_for_future_use = "Z";
    const char negation = 'Z';
    const string SEPARATOR = "Z0";
    const string special_minvalue = "ZZ0";

    private string input;
    private int idx;

    public I64StreamDecoder(string input)
    {
        this.input = input;
        this.idx = 0;
    }

    public bool IsEnd()
    {
        return idx >= input.Length;
    }

    public long? DecodeNext()
    {
        if (idx >= input.Length)
            return null;

        char c = input[idx++];
        bool neg = false;
        long base_val = 0L;

        if (c == negation)
        {
            neg = true;
            c = input[idx++];
        }

        if (c == negation)
        {
            if (neg)
            {
                c = input[idx++];

                if (c == '0')
                {
                    return long.MinValue;
                }
            }
            // Only valid ZZ sequence is ZZ0, all others are reserved.
            throw new FormatException(string.Format("Found invalid Z sequence at {0}", idx));
        }

        if (neg && c == '0')
        {
            // Z0 is separator
            return null;
        }

        // enc32 encodings
        if (c >= ENC32_B1_FIRST && c <= ENC32_B1_LAST)
        {
            long v = (c - ENC32_B1_FIRST);
            return neg ? -v : v;
        }

        if (c >= ENC32_B2_FIRST && c <= ENC32_B2_LAST)
        {
            long v = (c - ENC32_B2_FIRST) + ENC32_B1_LENGTH;
            return neg ? -v : v;
        }

        base_val += 32;

        // enc4h encodings
        if (c >= ENC4H_FIRST && c <= ENC4H_LAST)
        {
            long hi = (c - ENC4H_FIRST);
            long lo = 0L;
            c = input[idx++];

            if (c >= ENC32_B1_FIRST && c <= ENC32_B1_LAST)
            {
                lo = (c - ENC32_B1_FIRST);
            }
            else if (c >= ENC32_B2_FIRST && c <= ENC32_B2_LAST)
            {
                lo = (c - ENC32_B2_FIRST) + ENC32_B1_LENGTH;
            }
            else
            {
                throw new FormatException(string.Format("Found invalid token '{0}' in middle of enc4h sequence at {1}", c, idx));
            }

            hi <<= 5;
            hi |= lo;
            return neg ? -(base_val + hi) : (base_val + hi);
        }

        base_val += 512;

        // varlen encodings
        int varlen = 0;

        if (c >= ENCVAR_B1_FIRST && c <= ENCVAR_B1_LAST)
        {
            varlen = (c - ENCVAR_B1_FIRST);
        }
        else if (c >= ENCVAR_B2_FIRST && c <= ENCVAR_B2_LAST)
        {
            varlen = (c - ENCVAR_B2_FIRST) + ENCVAR_B1_LENGTH;
        }
        else
        {
            throw new FormatException(string.Format("Found invalid token '{0}' at start of encvar sequence at {1}", c, idx));
        }

        long value = 0;

        for (int i = 0; i <= varlen; i++)
        {
            long lo;
            c = input[idx++];
            if (c >= ENC32_B1_FIRST && c <= ENC32_B1_LAST)
            {
                lo = (c - ENC32_B1_FIRST);
            }
            else if (c >= ENC32_B2_FIRST && c <= ENC32_B2_LAST)
            {
                lo = (c - ENC32_B2_FIRST) + ENC32_B1_LENGTH;
            }
            else
            {
                throw new FormatException(string.Format("Found invalid token '{0}' in middle of encvar sequence at {1}", c, idx));
            }

            lo <<= (5 * i);
            value |= lo;
        }

        return neg ? -(base_val + value) : (base_val + value);
    }

    public IEnumerable<long> DecodeChunk()
    {
        while (true)
        {
            long? v = DecodeNext();

            if (v.HasValue)
                yield return v.Value;
            else
                break;
        }
    }
}
