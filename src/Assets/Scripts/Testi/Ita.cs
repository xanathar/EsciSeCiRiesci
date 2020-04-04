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
            { EntityType.Lampadina, "Usa la lampadina." },
            { EntityType.Password, "Usa la segretissima password." },
            { EntityType.Forbici, "Usa le forbici" },
            { EntityType.Scala, "Usa la scala" },
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
            { EntityType.Password, "il foglietto con la password." },
            { EntityType.Forbici, "le forbici" },
            { EntityType.Scala, "la scala" },

            // cucina
            { EntityType.Lampadario, "il lampadario" },
            { EntityType.Porta, "la porta" },
            { EntityType.Frigorifero, "il frigorifero" },
            { EntityType.Rubinetti, "i rubinetti" },
            { EntityType.Lavandino, "il lavandino otturato" },

            // soggiorno
            { EntityType.Uscita, "la porta di casa" },
            { EntityType.Quadro_1, "il quadro" },
            { EntityType.Quadro_2, "il quadro" },
            { EntityType.Quadro_3, "il quadro" },
            { EntityType.Lampadina, "la lampadina" },
            { EntityType.Finestra, "la finestra" },
            { EntityType.Telefono, "il telefono" },
            { EntityType.Fiori, "i fiori" },
            { EntityType.Libri, "i libri" },
            { EntityType.CestinoCaramelle, "il cestino di caramelle" },
            { EntityType.Penne, "le penne" },
            { EntityType.PupazzoPrincipe, "il pupazzo del principe" },
            { EntityType.PupazzoSirenetta, "il pupazzo della sirenetta" },

            // balcone
            { EntityType.Serratura, "la serratura" },
            { EntityType.Capra, "la capra affamata" },
            { EntityType.CapraRuminante, "la capra" },
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
            { EntityType.Lampadina, "Usa la lampadina con" },
            { EntityType.Password, "Usa la segretissima password con" },
            { EntityType.Forbici, "Usa le forbici con" },
            { EntityType.Scala, "Usa la scala con" },
        });

        entity_texts.Add(EntityTextType.Inventario_Preso, new Dictionary<EntityType, string>()
        {
            { EntityType.Insalata, "Ho preso l'insalata, ma non ho nessuna intenzione di mangiarla." },
            { EntityType.Bicarbonato, "Ho preso il bicarbonato. Ha un sacco di usi, potrebbe tornare utile." },
            { EntityType.Calamaro, "Ho preso una cosa molliccia che credo sia un calamaro." },
            { EntityType.Olio, "---" },
            { EntityType.Lattuga, "---" },
            { EntityType.Aceto, "---" },
            { EntityType.Chiave, "Ho preso la chiave." },
            { EntityType.Lampadina, "Ho preso la lampadina." },
            { EntityType.Password, "Ho preso il foglietto. Sopra c'è scritto 'password'." },
            { EntityType.Forbici, "Ho preso le forbici." },
            { EntityType.Scala, "Ho preso la scala." },
        });

        entity_texts.Add(EntityTextType.Interagisci, new Dictionary<EntityType, string>()
        {
            { EntityType.Insalata, "Prendi l'insalata" },
            { EntityType.Porta, "Vai in un'altra stanza" },
            { EntityType.Frigorifero, "Apri il frigorifero" },
            { EntityType.Bicarbonato, "Prendi il bicarbonato di sodio" },
            { EntityType.Calamaro, "Prendi il calamaro" },
            { EntityType.Rubinetti, "Apri i rubinetti" },
            { EntityType.Guanti, "Prendi i guanti" },
            { EntityType.Chiave, "Prendi la chiave" },
            { EntityType.Password, "Prendi il foglietto nascosto" },
            { EntityType.Lampadario, "Tocca il lampadario" },
            { EntityType.Lavandino, "---" },
            // todo v
            { EntityType.Uscita, "Esci di casa" },
            { EntityType.Quadro_1, "Osserva il quadro con le persone" },
            { EntityType.Quadro_2, "Osserva il quadro con i fiori" },
            { EntityType.Quadro_3, "Osserva il quadro con la principessa" },
            { EntityType.Lampadina, "Prendi la lampadina" },
            { EntityType.Finestra, "Apri la finestra" },
            { EntityType.Telefono, "Usa il telefono" },
            { EntityType.Fiori, "Prendi i fiori" },
            { EntityType.Libri, "Leggi i libri" },
            { EntityType.CestinoCaramelle, "Mangia una caramella" },
            { EntityType.Penne, "Prendi le penne" },
            { EntityType.PupazzoPrincipe, "Prendi il pupazzo del principe" },
            { EntityType.PupazzoSirenetta, "Prendi il pupazzo della sirenetta" },

            { EntityType.Forbici, "Prendi le forbici." },
            { EntityType.Serratura, "L'armadietto è chiuso a chiave." },
            { EntityType.Scala, "Prendi la scala." },
            { EntityType.Capra, "La capra ha fame e non mi lascia passare." },
            { EntityType.CapraRuminante, "Ora la capretta mangia serena." },

        });

        entity_texts.Add(EntityTextType.ConfermaInterazione, new Dictionary<EntityType, string>()
        {
            // todo v
            { EntityType.Quadro_1, "E' un quadro con due persone che non rispettano il metro di distanza" },
            { EntityType.Quadro_2, "E' un quadro con dei fiori disegnati. E polline. Tanto polline." },
            { EntityType.Quadro_3, "Non c'è più niente dietro al quadro della principessa." },
            { EntityType.Finestra, "Non ci penso proprio con quel coso che mi aspetta lì fuori!!" },
            { EntityType.Telefono, "Non saprei a chi telefonare" },
            { EntityType.Fiori, "Sono fiori finti, e non saprei cosa farmene." },
            { EntityType.Libri, "'I promessi sposi'. Sempre di attualità." },
            { EntityType.CestinoCaramelle, "Gnam. Gnam. ... Gnam. Deliziosa." },
            { EntityType.Penne, "Non serve che le prenda, ne ho sempre una in tasca." },
            { EntityType.PupazzoPrincipe, "E' un principe e di azzurro ha solo i pantaloni." },
            { EntityType.PupazzoSirenetta, "Ha gli occhi da pesce lesso." },
            { EntityType.Serratura, "L'armadietto è chiuso a chiave." },
            { EntityType.Scala, "La capra ha fame e non mi lascia prendere la scala." },
            { EntityType.Capra, "La capra ha fame e non mi lascia passare." },
            { EntityType.CapraRuminante, "Ora la capretta mangia serena." },
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
            { "interagisci_commit_lampadario", "Ora che funziona è meglio non toccarlo!" },
       });

        rooms_texts.Add(RoomType.CucinaBuia, new Dictionary<string, string>()
        {
            { "welcome", "Sei in cucina, ma è tutto buio!" },
            { "confirm_lampadario", "Toccare il lampadario non è di aiuto" },
            { "confirm_porta", "Esci da questa stanza" },
            { "confirm_guanti", "Una scatola piena di... qualcosa." },
            { "confirm_bicarbonato", "E' una scatola di cartone. Non so cosa contenga." },
            { "confirm_lavandino", "Bleaaaaah!!! la mia mano si è sporcata di qualche liquame disgustoso!" },
            { "confirm_frigorifero", "Ahi! Ho sbattuto il mignolino del piede." },
            { "interagisci_lampadario", "La lampadina del lampadario è bruciata." },
            { "interagisci_porta", "La porta conduce verso... la luce" },
            { "interagisci_guanti", "Un oggetto misterioso" },
            { "interagisci_bicarbonato", "Non ho idea cosa" },
            { "interagisci_lavandino", "Lavandino" },
            { "interagisci_frigorifero", "Frigorifero" },
            { "complemento_lampadario", "il lampadario" },
            { "complemento_porta", "la porta" },
            { "complemento_guanti", "la scatola" },
            { "complemento_bicarbonato", "la scatola" },
            { "complemento_lavandino", "il lavandino" },
            { "complemento_frigorifero", "il frigorifero" },
        });

        rooms_texts.Add(RoomType.Corridoio, new Dictionary<string, string>()
        {
            { "welcome", "Scegli dove andare" },
        });

        rooms_texts.Add(RoomType.Soggiorno, new Dictionary<string, string>()
        {
            { "welcome", "Sei in soggiorno" },
            { "quadro_nascosto", "C'è qualcosa dietro al quadro..." },
            { "uscita", "Non posso uscire. Mi servono mascherina, guanti e autocertificazione." },
            { "uscitam", "Non posso uscire. Mi servono ancora guanti e autocertificazione." },
            { "uscitag", "Non posso uscire. Mi servono ancora mascherina e autocertificazione." },
            { "uscitaa", "Non posso uscire. Mi servono ancora guanti e mascherina." },
            { "uscitagm", "Non posso uscire. Mi serve ancora l'autocertificazione." },
            { "uscitaam", "Non posso uscire. Mi servono ancora i guanti." },
            { "uscitaag", "Non posso uscire. Mi serve ancora la mascherina." },
        });

        rooms_texts.Add(RoomType.Balcone, new Dictionary<string, string>()
        {
            { "welcome", "Sei sul balcone. La capra sta ruminando serena." },
            { "welcome_capra", "Sei sul balcone e una capra affamata ti blocca la strada" },
            { "kill_capra", "Siamo diventati matti?! Povera capra!" },
            { "scala_ruggine", "La scala è arrugginita e non riesco a chiuderla" },
            { "scala_olio", "Ora dovrei riuscire a chiudere la scala" },
            { "fail_capra", "Non posso, la capra mi blocca la strada" },
        });
    }
}

