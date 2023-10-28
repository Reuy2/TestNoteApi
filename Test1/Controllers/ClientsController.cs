using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Newtonsoft.Json;
using System;
using System.Net.Mime;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Test1.Model;
using static System.Net.Mime.MediaTypeNames;

namespace Test1.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    public class ClientsController : Controller
    {

        [HttpGet("{id}")]
        [ResponseCache(VaryByQueryKeys = new string[] {""},Duration = 10)]
        public IActionResult GetOneClient([FromRoute]int? id)
        {
            try
            {
                if (id is null) return BadRequest();
                Client? client = DataOperation.GetClient(id);
                return client switch
                {
                    null => NoContent(),
                    not null => Ok(client)
                };
            }
            catch (Exception ex)
            {
                ErrorHandler.Log(ex);
                return StatusCode(500);
            }
        }

        [HttpGet("")]
        [ResponseCache(VaryByQueryKeys = new string[] { "count" }, Duration = 20)]
        public IActionResult Get(int? count)
        {
            try
            {
                if (count is null)
                {
                    return Ok(DataOperation.Clients);
                }
                else if (count <= DataOperation.Clients.Count())
                {
                    return Ok(DataOperation.Clients.GetRange(0, (int)count));
                }
                else return BadRequest("invalid count");
            }
            catch(Exception ex)
            {
                ErrorHandler.Log(ex);
                return StatusCode(500);
            }
        }

        [HttpGet("{id}/Notes/{noteId}")]
        [ResponseCache(VaryByQueryKeys = new string[] { "" }, Duration = 10)]
        public IActionResult GetNote([FromRoute]int id, [FromRoute]int noteId)
        {
            try
            {
                Note? note = DataOperation.GetNoteFromClient(id, noteId);
                if (note is null) return NoContent();
                return Ok(note);
            }
            catch(Exception ex)
            {
                ErrorHandler.Log(ex);
                return StatusCode(500);
            }
        }

        [HttpGet("{id}/Notes")]
        [ResponseCache(VaryByQueryKeys = new string[] {"count"}, Duration = 20)]
        public IActionResult GetNotes([FromRoute] int id, int? count)
        {
            try
            {
                if (count is null) return Ok(DataOperation.GetNotesFromClient(id));

                if (count > DataOperation.GetNotesFromClient(id)?.Count()) return BadRequest("invalid count");

                return Ok(DataOperation.GetNotesFromClient(id)?.GetRange(0, (int)count));
            }
            catch(Exception ex)
            {
                ErrorHandler.Log(ex);
                return StatusCode(500);
            }
        }

        [HttpPost]
        public IActionResult CreateClient([FromBody] ClientTemplate client)
        {
            try
            {
                if (client is null || client.Name == "") return BadRequest("Name is empty");

                DataOperation.Clients.Add(new Client(DataOperation.GetId(), client.Name));

                return Ok();
            }
            catch(Exception ex)
            {
                ErrorHandler.Log(ex);
                DataOperation.LoadClients();
                return StatusCode(500);
            }
        }

        [HttpPost("{id}")]
        public IActionResult CreateNote([FromRoute]int id,[FromBody] NoteTemplate noteTemp)
        {
            try
            {
                Client? client = DataOperation.Clients.Find(x => x.ClientId == id);

                if (client is null) return NoContent();

                Note note = new Note(DataOperation.GetId(), noteTemp.Text);

                client.Notes.Add(note);

                return Ok();
            }
            catch(Exception ex)
            {
                ErrorHandler.Log(ex);
                DataOperation.LoadClients();
                return StatusCode(500);
            }
        }

        [HttpPatch("{id}/Notes/{noteId}")]
        public IActionResult EditNote([FromRoute]int id, [FromRoute]int noteId, [FromBody]NoteTemplate noteTemp)
        {

            try
            {
                Note? note = DataOperation.GetNoteFromClient(id, noteId);

                if (note is null) return NoContent();

                note.Text = noteTemp.Text;

                return Ok();
            }
            catch (Exception ex)
            {
                ErrorHandler.Log(ex);
                DataOperation.LoadClients();
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}/Notes/{noteId}")]
        public IActionResult DeleteNote([FromRoute] int id, [FromRoute] int noteId)
        {
            try
            {
                Note? note = DataOperation.GetNoteFromClient(id, noteId);

                if (note is null) return NoContent();

                Client? client = DataOperation.GetClient(id);

                if (client is null) return NoContent();

                client.Notes.Remove(note);

                return Ok();
            }
            catch (Exception ex)
            {
                ErrorHandler.Log(ex);
                DataOperation.LoadClients();
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClient([FromRoute] int id)
        {
            try
            {
                Client? client = DataOperation.GetClient(id);

                if (client is null) return NoContent();

                if (DataOperation.Clients.Remove(client))
                {
                    return Ok();
                }
                else return NoContent();
            }
            catch(Exception ex)
            {
                ErrorHandler.Log(ex);
                DataOperation.LoadClients();
                return StatusCode(500);
            }
        }



    }
}
