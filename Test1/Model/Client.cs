using Microsoft.AspNetCore.Mvc;

namespace Test1.Model
{
    public class Client : IDisposable
    {
        public List<Note> Notes { get; set; }

        public int ClientId { get; set; }

        public string Name { get; set; }

        public Client() 
        {
            Notes = new List<Note>();
            ClientId = DataOperation.GetId();
        }

        public Client(int id, string name)
        {
            Notes = new List<Note>() { new Note(DataOperation.GetId(), "Привет") };
            ClientId = id;
            Name = name;
        }

        public void Dispose()
        {
            Notes.Clear();
        }
    }
}
