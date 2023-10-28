using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test1.Model;

namespace Test1.Tests
{
    internal static class DataArrange
    {
        public static string testMainPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        public static string testClientsPath = "\\TestData\\TestClients.txt";

        public static void CreateTwoTestClientsWithId0And1()
        { 
            List<Client> clients = new List<Client>() { new Client(0,"Jho0"),new Client(1,"Jho1")};
            File.WriteAllText(testMainPath + testClientsPath, JsonConvert.SerializeObject(clients));

        }

        internal static void CreateOneClientWithId0AndFiveNotesSecondNoteIdIs0()
        {
            List<Client> clients = new List<Client>() { new Client(0, "Jho0") };
            clients[0].Notes.Add(new Note(0,"TestNote"));
            clients[0].Notes.Add(new Note());
            clients[0].Notes.Add(new Note());
            clients[0].Notes.Add(new Note());
            File.WriteAllText(testMainPath + testClientsPath, JsonConvert.SerializeObject(clients));
        }

        internal static object LoadClients()
        {
            throw new NotImplementedException();
        }
    }
}
