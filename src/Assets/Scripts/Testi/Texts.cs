using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class Texts
{
    private static Ita ita = new Ita();

    public static string Get(string text)
    {
        string res;

        if (ita.texts.TryGetValue(text, out res))
            return res;

        Utils.Error("Testo non trovato {0}", text);
        return "?";
    }

    public static string GetEntityText(EntityTextType ett, EntityType et)
    {
        string res;

        if (ita.entity_texts[ett].TryGetValue(et, out res))
            return res;

        Utils.Error("Testo non trovato {0}/{1}", ett, et);
        return "?";
    }

    public static string GetRoomText(RoomType rt, string key)
    {
        string res;

        if (ita.rooms_texts[rt].TryGetValue(key, out res))
            return res;

        Utils.Error("Testo non trovato {0}/{1}", rt, key);
        return "?";
    }




}
