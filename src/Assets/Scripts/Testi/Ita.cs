using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Ita
{
    public Dictionary<string, string> texts = new Dictionary<string, string>();
    public Dictionary<EntityTextType, Dictionary<EntityType, string>> entity_texts = new Dictionary<EntityTextType, Dictionary<EntityType, string>>();
    public Dictionary<RoomType, Dictionary<string, string>> rooms_texts = new Dictionary<RoomType, Dictionary<string, string>>();

    public Ita()
    {
        texts.Add("inventario_disabilitato", "Non ha molto senso usare un oggetto qui");
        texts.Add("insalata_nocondimento", "Non voglio mangiarla, quindi non ha senso condirla.");
        texts.Add("insalata_aperta", "Hai aperto la vaschetta di insalata.");

        texts.Add("fail_1", "Non è una buona idea.");
        texts.Add("fail_2", "Uhm... no.");
        texts.Add("fail_3", "Non credo proprio.");
        texts.Add("fail_4", "Non ha molto senso.");

        entity_texts.Add(EntityTextType.Inventario_Usa, new Dictionary<EntityType, string>()
        {
            { EntityType.Insalata, "Apri la vaschetta di insalata preconfezionata, marca 'Dammi Del Tu'." },
            { EntityType.Olio, "Usa la bustina di olio extravergine di oliva." },
            { EntityType.Lattuga, "Usa la lattuga." },
            { EntityType.Aceto, "Usa la bustina di aceto." },
            { EntityType.Bicarbonato, "Usa il bicarbonato di sodio." },
            { EntityType.Chiave, "Usa la chiave." },
            { EntityType.Calamaro, "Usa il... calamaro ?" },
        });

        entity_texts.Add(EntityTextType.Inventario_Complemento, new Dictionary<EntityType, string>()
        {
            // oggetti
            { EntityType.Insalata, " la vaschetta di insalata" },
            { EntityType.Olio, " la bustina di olio" },
            { EntityType.Lattuga, " la lattuga" },
            { EntityType.Aceto, " la bustina di aceto" },
            { EntityType.Bicarbonato, " il bicarbonato di sodio" },
            { EntityType.Chiave, " la chiave" },
            { EntityType.Calamaro, " il calamaro" },
            { EntityType.Guanti, "i guanti" },

            // cucina
            { EntityType.Porta, "la porta" },
            { EntityType.Frigorifero, "il frigorifero" },
            { EntityType.Rubinetti, "i rubinetti" },
            { EntityType.Lavandino, "il lavandino otturato" },
        });

        entity_texts.Add(EntityTextType.Inventario_Soggetto, new Dictionary<EntityType, string>()
        {
            { EntityType.Insalata, "---" },
            { EntityType.Olio, "Usa la bustina di olio con" },
            { EntityType.Lattuga, "Usa la lattuga con" },
            { EntityType.Aceto, "Usa la bustina di aceto con" },
            { EntityType.Bicarbonato, "Usa il bicarbonato di sodio con" },
            { EntityType.Chiave, "Usa la chiave con" },
            { EntityType.Calamaro, "Usa il calamaro con" },
        });

        entity_texts.Add(EntityTextType.Inventario_Prendi, new Dictionary<EntityType, string>()
        {
            { EntityType.Insalata, "Ho preso l'insalata, ma non ho nessuna intenzione di mangiarla." },
            { EntityType.Bicarbonato, "Ho preso il bicarbonato. Ha un sacco di usi, potrebbe tornare utile." },
            { EntityType.Calamaro, "Ho preso una cosa molliccia che credo sia un calamaro." },
            { EntityType.Olio, "---" },
            { EntityType.Lattuga, "---" },
            { EntityType.Aceto, "---" },
            { EntityType.Chiave, "Ho preso la chiave." },
        });

        entity_texts.Add(EntityTextType.Interagisci, new Dictionary<EntityType, string>()
        {
            {  EntityType.Insalata, "Prendi l'insalata" },
            {  EntityType.Porta, "Esci da questa stanza" },
            {  EntityType.Frigorifero, "Apri il frigorifero" },
            {  EntityType.Bicarbonato, "Prendi il bicarbonato di sodio" },
            {  EntityType.Calamaro, "Prendi il calamaro" },
            {  EntityType.Rubinetti, "Apri i rubinetti" },
            {  EntityType.Guanti, "Prendi i guanti" },
            {  EntityType.Chiave, "Prendi la chiave" },
            {  EntityType.Lavandino, "---" },
        });

       rooms_texts.Add(RoomType.Cucina, new Dictionary<string, string>()
        {
            { "welcome", "Sei in cucina" },
            { "action_frigorifero", "Ho aperto il frigorifero" },
            { "stop_rubinetti", "Ci manca solo aggiungere altra acqua!" },
            { "stop_guanti", "La confezione è sigillata e non riesco ad aprirla" },
            { "stop_lavandino", "Non metterei le mani lì dentro per nessun motivo" },
            { "stop_melmoso", "E' già abbastanza melmoso così" },
            { "action_sturato", "Il lavandino si è liberato! E c'è qualcosa sul fondo." },
            { "action_aceto", "Ho versato l'aceto nel lavandino" },
            { "action_bicarbonato", "Ho versato il bicarbonato nel lavandino" },
            { "lavandino_bicarbonato", "Libera lavandino pieno di acqua sporca e bicarbonato" },
            { "lavandino_aceto", "Libera lavandino pieno di acqua sporca e aceto" },
            { "lavandino_base", "Libera lavandino otturato" },
       });

        rooms_texts.Add(RoomType.Corridoio, new Dictionary<string, string>()
        {
            { "welcome", "Scegli dove andare" },
        });

    }
}

