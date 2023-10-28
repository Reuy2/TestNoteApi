using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Test1.Model
{
    public class Note
    {
        public int NoteId { get; set; }

        public string Text { get; set; }

        public Note() 
        {
            Text = "";
            NoteId = DataOperation.GetId(); 
        }
        public Note(int id, string text)
        {
            NoteId = id;

            Text = text;
        }

    }
}
