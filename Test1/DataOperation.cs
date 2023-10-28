using Newtonsoft.Json;
using Test1.Model;

namespace Test1
{
    public class DataOperation
    {
        public static string path = Directory.GetCurrentDirectory();

        public static string clientsFileName = "\\Data\\SavedClients.txt";

        public static List<Client> Clients { get; set; } = new List<Client>();

        public static Client? GetClient(int? id)
        {
            return Clients.Find(x=> x.ClientId == id);
        }

        public static List<Note>? GetNotesFromClient(int id)
        {
            return Clients.Find(x => x.ClientId == id)?.Notes;
        }

        public static Note? GetNoteFromClient(int id, int noteId)
        {
            return Clients.Find(x => x.ClientId == id)?.Notes?.Find(x=> x.NoteId == noteId);
        }

        public static List<Client> GetClientsFromFile()
        {
            try
            {
                string file =  File.ReadAllText(path + clientsFileName);
                var clients =  JsonConvert.DeserializeObject<List<Client>>(file);
                if (clients is null) return new List<Client>();
                return clients;
            }
            catch(Exception ex)
            {
                ErrorHandler.Log(ex);
                return null;
            }

        }

        public static void SaveClients()
        {
            try
            {

                File.WriteAllText(path + clientsFileName, JsonConvert.SerializeObject(Clients.ToArray()));
            }
            catch (Exception ex)
            {
                ErrorHandler.Log(ex);
                File.WriteAllText(path + clientsFileName, JsonConvert.SerializeObject(Clients));
            }
        }

        public static int GetId()
        {
            return Random.Shared.Next();
        }

        public static void LoadClients()
        {
            Clients = GetClientsFromFile();
            if (Clients is null) throw new InvalidDataException();
        }

        public static async Task PeriodicSaveClients(TimeSpan interval, CancellationToken cancellationToken = default)
        {
            using PeriodicTimer timer = new PeriodicTimer(interval);
            while (true)
            {
                SaveClients();
                Console.WriteLine($"Clients Saved\nClients Now:{Clients.Count}");
                await timer.WaitForNextTickAsync(cancellationToken);
            }
        }
    }
}
