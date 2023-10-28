using System.Xml.Linq;

namespace Test1.Model
{
    public class NoteTemplate
    {
        public string Text {  get; set; }

        public NoteTemplate(string text)
        {
            Text = text;
        }

        public NoteTemplate() { }
    }
}
