using adonet_db_videogame;
using System.Data.SqlClient;

bool esegui = true;
var nl = Environment.NewLine;
var line = $"-----------------------------";

var connStr = "Data Source=localhost; Initial Catalog=videogiochi-db; Integrated Security=True; Encrypt=False";

VideogameManager Manager = new VideogameManager(connStr);

while (esegui)
{
    Console.WriteLine(
        $"Benvenuti nel gestore videogames: {nl}" +
        $"Prego scegliere un comando: {nl}" +
        $"filter -> Ricerca giochi per nome.{nl}" +
        $"search -> Cerca gioco per ID. {nl}" +
        $"add ->  Aggiungi gioco alla lista.{nl}" +
        $"delete -> Elimina gioco dalla lista.{nl}" +
        $"exit -> Chiudi il programma.");

    Console.Write($"Prego digitare il comando:");
    string comando = Console.ReadLine() ?? "";
    comando = comando.Replace(" ", "");
    comando = comando.ToLower();

    string cmd = "";
    cmd = cmd.Replace(" ", "");
    cmd = cmd.ToLower();

    switch (comando)
    {
        case "filter":
            Console.Write(
            $"{line}{nl}" +
            $"Prego inserisca il nome gioco: ");
            cmd = Console.ReadLine() ?? "";

            var videogameList = Manager.GetVideogameByNameLike(cmd);
            foreach (var v in videogameList)
            {
                Console.WriteLine(v);
            }
            Console.WriteLine(line);
            break;


        case "search":
            Console.Write(
                $"{line}{nl}" +
                $"Prego inserisca l' ID del gioco:");
            cmd = Console.ReadLine() ?? "";

            var videogame = Manager.GetVideogameById(cmd);
            Console.WriteLine($"{videogame}{nl}{line}");
            break;


        case "add":
            Console.Write(
            $"{line}{nl}" +
            $"Prego inserisca il nome del gioco:");
            string name = Console.ReadLine() ?? "";
            Console.Write($"Prego inserisca la descrizione del gioco:");
            string overview = Console.ReadLine() ?? "";
            Console.Write("Prego inserisca l'Id della software house:");
            int softwearHouse_id = Convert.ToInt32(Console.ReadLine());
            //da fixare
            var release_date = DateTime.Now;
            //
            Videogame newVg = new(null, name, overview, release_date, softwearHouse_id);
            Manager.AddVideogame(newVg);
            break;

        case "delete":
            Console.Write(
            $"{line}{nl}" +
            $"Prego inserisca il nome del gioco:");
            long id = Convert.ToInt64(Console.ReadLine());
            Manager.DeleteGame(id);
            Console.WriteLine(line);
            break;

        case "exit":
            esegui = false;
            break;

        default:
            Console.WriteLine(
                $"{line}{nl}" +
                $"Comando '{comando}' non riconosciuto. {nl}" +
                $"{line}{nl}");
            break;
    }
}