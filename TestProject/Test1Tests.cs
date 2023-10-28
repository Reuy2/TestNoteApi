using Microsoft.AspNetCore.Mvc;
using Test1;
using Test1.Model;
using Test1.Tests;

namespace Test1Tests
{
    public class Test1Tests
    {
        [Fact]
        public void GetOneClientWithIdTest()
        {
            //arrange
            var controller = new Test1.Controllers.ClientsController();

            Test1.DataOperation.path = DataArrange.testMainPath;
            Test1.DataOperation.clientsFileName = DataArrange.testClientsPath;

            DataArrange.CreateTwoTestClientsWithId0And1();
            Test1.DataOperation.LoadClients();
            //act
            ObjectResult res = (ObjectResult)controller.GetOneClient(1);
            //assert
            Assert.NotNull(res);
            Assert.Equal(200,res.StatusCode);
        }

        [Fact]
        public void GetOneCLientWithIncorrectIdTest()
        {
            //arrange
            var controller = new Test1.Controllers.ClientsController();

            Test1.DataOperation.path = DataArrange.testMainPath;
            Test1.DataOperation.clientsFileName = DataArrange.testClientsPath;

            DataArrange.CreateTwoTestClientsWithId0And1();
            Test1.DataOperation.LoadClients();
            //act
            NoContentResult res = (NoContentResult)controller.GetOneClient(4);
            //assert
            Assert.NotNull(res);
            Assert.Equal(204, res.StatusCode);
        }

        [Fact]
        public void GetOneCLientWithNullIdTest()
        {
            //arrange
            var controller = new Test1.Controllers.ClientsController();

            Test1.DataOperation.path = DataArrange.testMainPath;
            Test1.DataOperation.clientsFileName = DataArrange.testClientsPath;

            DataArrange.CreateTwoTestClientsWithId0And1();
            Test1.DataOperation.LoadClients();
            //act
            var res = (BadRequestResult)controller.GetOneClient(null);
            //assert
            Assert.NotNull(res);
            Assert.Equal(400, res.StatusCode);
        }

        [Fact]
        public void GetAllCLientTest()
        {
            //arrange
            var controller = new Test1.Controllers.ClientsController();

            Test1.DataOperation.path = DataArrange.testMainPath;
            Test1.DataOperation.clientsFileName = DataArrange.testClientsPath;

            DataArrange.CreateTwoTestClientsWithId0And1();
            Test1.DataOperation.LoadClients();
            //act
            var res = (OkObjectResult)controller.Get(null);

            var listRes = (List<Client>?)res.Value;
            //assert
            Assert.NotNull(listRes);
            Assert.Equal(2, listRes.Count);
        }

        [Fact]
        public void GetAllCLientWithCount1Test()
        {
            //arrange
            var controller = new Test1.Controllers.ClientsController();

            Test1.DataOperation.path = DataArrange.testMainPath;
            Test1.DataOperation.clientsFileName = DataArrange.testClientsPath;

            DataArrange.CreateTwoTestClientsWithId0And1();
            Test1.DataOperation.LoadClients();
            //act
            var res = (OkObjectResult)controller.Get(1);

            var listRes = (List<Client>?)res.Value;
            //assert
            Assert.NotNull(listRes);
            Assert.Single(listRes);
        }

        [Fact]
        public void GetAllCLientWithCountEqualsClientsLengthTest()
        {
            //arrange
            var controller = new Test1.Controllers.ClientsController();

            Test1.DataOperation.path = DataArrange.testMainPath;
            Test1.DataOperation.clientsFileName = DataArrange.testClientsPath;

            DataArrange.CreateTwoTestClientsWithId0And1();
            Test1.DataOperation.LoadClients();
            //act
            var res = (OkObjectResult)controller.Get(DataOperation.Clients.Count);

            var listRes = (List<Client>?)res.Value;
            //assert
            Assert.NotNull(listRes);
            Assert.Equal(DataOperation.Clients.Count,listRes.Count);
        }

        [Fact]
        public void GetAllCLientWithIncorrectCountTest()
        {
            //arrange
            var controller = new Test1.Controllers.ClientsController();

            Test1.DataOperation.path = DataArrange.testMainPath;
            Test1.DataOperation.clientsFileName = DataArrange.testClientsPath;

            DataArrange.CreateTwoTestClientsWithId0And1();
            Test1.DataOperation.LoadClients();
            //act
            var res = (BadRequestObjectResult)controller.Get(int.MaxValue);

            //assert
            Assert.NotNull(res);
            Assert.Equal(400,res.StatusCode);
        }

        [Fact]
        public void GetNoteWithId0FromClientWithId0()
        {
            //arrange
            var controller = new Test1.Controllers.ClientsController();

            Test1.DataOperation.path = DataArrange.testMainPath;
            Test1.DataOperation.clientsFileName = DataArrange.testClientsPath;

            DataArrange.CreateOneClientWithId0AndFiveNotesSecondNoteIdIs0();
            Test1.DataOperation.LoadClients();
            //act
            var res = (OkObjectResult)controller.GetNote(0, 0);

            Note? note = (Note?)res.Value;
            //assert
            Assert.NotNull(note);
            Assert.Equal(200, res.StatusCode);
            Assert.Equal(0, note.NoteId);
        }

        [Fact]
        public void GetNoteWithIncorrectIdFromClientWithId0()
        {
            //arrange
            var controller = new Test1.Controllers.ClientsController();

            Test1.DataOperation.path = DataArrange.testMainPath;
            Test1.DataOperation.clientsFileName = DataArrange.testClientsPath;

            DataArrange.CreateOneClientWithId0AndFiveNotesSecondNoteIdIs0();
            Test1.DataOperation.LoadClients();
            //act
            var res = (NoContentResult)controller.GetNote(0, 18264);

            //assert
            Assert.NotNull(res);
            Assert.Equal(204, res.StatusCode);
        }

        [Fact]
        public void GetAllNotesFromClientWithId0()
        {
            //arrange
            var controller = new Test1.Controllers.ClientsController();

            Test1.DataOperation.path = DataArrange.testMainPath;
            Test1.DataOperation.clientsFileName = DataArrange.testClientsPath;

            DataArrange.CreateOneClientWithId0AndFiveNotesSecondNoteIdIs0();
            Test1.DataOperation.LoadClients();
            //act
            var res = (OkObjectResult)controller.GetNotes(0,null);

            var notes = (List<Note>?)res.Value;
            //assert
            Assert.NotNull(res);
            Assert.NotNull(notes);
            Assert.Equal(200, res.StatusCode);
            Assert.Equal(5,notes.Count);
        }

        [Fact]
        public void GetThreeNotesFromClientWithId0()
        {
            //arrange
            var controller = new Test1.Controllers.ClientsController();

            Test1.DataOperation.path = DataArrange.testMainPath;
            Test1.DataOperation.clientsFileName = DataArrange.testClientsPath;

            DataArrange.CreateOneClientWithId0AndFiveNotesSecondNoteIdIs0();
            Test1.DataOperation.LoadClients();
            //act
            var res = (OkObjectResult)controller.GetNotes(0, 3);

            var notes = (List<Note>?)res.Value;
            //assert
            Assert.NotNull(res);
            Assert.NotNull(notes);
            Assert.Equal(200, res.StatusCode);
            Assert.Equal(3,notes.Count);
        }

        [Fact]
        public void GetNotesWithIncorrectCountFromClientWithId0()
        {
            //arrange
            var controller = new Test1.Controllers.ClientsController();

            Test1.DataOperation.path = DataArrange.testMainPath;
            Test1.DataOperation.clientsFileName = DataArrange.testClientsPath;

            DataArrange.CreateOneClientWithId0AndFiveNotesSecondNoteIdIs0();
            Test1.DataOperation.LoadClients();
            //act
            var res = (BadRequestObjectResult)controller.GetNotes(0, int.MaxValue);

            //assert
            Assert.NotNull(res);
            //Assert.NotNull(notes);
            Assert.Equal(400, res.StatusCode);

        }

        [Fact]
        public void CreateClientWithNameRik()
        {
            //arrange
            var controller = new Test1.Controllers.ClientsController();

            Test1.DataOperation.path = DataArrange.testMainPath;
            Test1.DataOperation.clientsFileName = DataArrange.testClientsPath;

            DataArrange.CreateOneClientWithId0AndFiveNotesSecondNoteIdIs0();
            Test1.DataOperation.LoadClients();
            //act
            var res = (OkResult)controller.CreateClient(new ClientTemplate("Rik"));

            var clients = DataOperation.Clients;
            //assert
            Assert.NotNull(res);
            Assert.NotNull(clients);
            Assert.Equal(200, res.StatusCode);
            Assert.Equal(2, clients.Count);
            Assert.Equal("Rik", clients[1].Name);
        }

        [Fact]
        public void CreateClientWithEmptyName()
        {
            //arrange
            var controller = new Test1.Controllers.ClientsController();

            Test1.DataOperation.path = DataArrange.testMainPath;
            Test1.DataOperation.clientsFileName = DataArrange.testClientsPath;

            DataArrange.CreateOneClientWithId0AndFiveNotesSecondNoteIdIs0();
            Test1.DataOperation.LoadClients();
            //act
            var res = (BadRequestObjectResult)controller.CreateClient(new ClientTemplate(""));

            var clients = DataOperation.Clients;
            //assert
            Assert.NotNull(res);
            Assert.NotNull(clients);
            Assert.Equal(400, res.StatusCode);
            Assert.Single(clients);
        }

        [Fact]
        public void CreateNote()
        {
            //arrange
            var controller = new Test1.Controllers.ClientsController();

            Test1.DataOperation.path = DataArrange.testMainPath;
            Test1.DataOperation.clientsFileName = DataArrange.testClientsPath;

            DataArrange.CreateOneClientWithId0AndFiveNotesSecondNoteIdIs0();
            Test1.DataOperation.LoadClients();
            string text = "TestText";
            //act
            var res = (OkResult)controller.CreateNote(0,new NoteTemplate(text));

            var clients = DataOperation.Clients;
            //assert
            Assert.NotNull(res);
            Assert.NotNull(clients);
            Assert.Equal(200, res.StatusCode);
            Assert.Equal(6, clients[0].Notes.Count);
            Assert.Equal(text, clients[0].Notes[^1].Text);
        }

        [Fact]
        public void CreateNoteWithIncorrectClient()
        {
            //arrange
            var controller = new Test1.Controllers.ClientsController();

            Test1.DataOperation.path = DataArrange.testMainPath;
            Test1.DataOperation.clientsFileName = DataArrange.testClientsPath;

            DataArrange.CreateOneClientWithId0AndFiveNotesSecondNoteIdIs0();
            Test1.DataOperation.LoadClients();
            string text = "TestText";
            //act
            var res = (NoContentResult)controller.CreateNote(999, new NoteTemplate(text));

            var clients = DataOperation.Clients;
            //assert
            Assert.NotNull(res);
            Assert.NotNull(clients);
            Assert.Equal(204, res.StatusCode);
            Assert.Equal(5, clients[0].Notes.Count);
        }

        [Fact]
        public void EditNote()
        {
            //arrange
            var controller = new Test1.Controllers.ClientsController();

            Test1.DataOperation.path = DataArrange.testMainPath;
            Test1.DataOperation.clientsFileName = DataArrange.testClientsPath;

            DataArrange.CreateOneClientWithId0AndFiveNotesSecondNoteIdIs0();
            Test1.DataOperation.LoadClients();
            string text = "TestText";
            //act
            var res = (OkResult)controller.EditNote(0,0, new NoteTemplate(text));

            var clients = DataOperation.Clients;
            //assert
            Assert.NotNull(res);
            Assert.NotNull(clients);
            Assert.Equal(200, res.StatusCode);
            Assert.Equal(5, clients[0].Notes.Count);
            Assert.Equal(text, clients[0].Notes[1].Text);
        }

        [Fact]
        public void EditNoteWithIncorectClientId()
        {
            //arrange
            var controller = new Test1.Controllers.ClientsController();

            Test1.DataOperation.path = DataArrange.testMainPath;
            Test1.DataOperation.clientsFileName = DataArrange.testClientsPath;

            DataArrange.CreateOneClientWithId0AndFiveNotesSecondNoteIdIs0();
            Test1.DataOperation.LoadClients();
            string text = "TestText";
            //act
            var res = (NoContentResult)controller.EditNote(90, 0, new NoteTemplate(text));

            var clients = DataOperation.Clients;
            //assert
            Assert.NotNull(res);
            Assert.NotNull(clients);
            Assert.Equal(204, res.StatusCode);
            Assert.Equal(5, clients[0].Notes.Count);
        }

        [Fact]
        public void EditNoteWithIncorectNoteId()
        {
            //arrange
            var controller = new Test1.Controllers.ClientsController();

            Test1.DataOperation.path = DataArrange.testMainPath;
            Test1.DataOperation.clientsFileName = DataArrange.testClientsPath;

            DataArrange.CreateOneClientWithId0AndFiveNotesSecondNoteIdIs0();
            Test1.DataOperation.LoadClients();
            string text = "TestText";
            //act
            var res = (NoContentResult)controller.EditNote(0, int.MaxValue, new NoteTemplate(text));

            var clients = DataOperation.Clients;
            //assert
            Assert.NotNull(res);
            Assert.NotNull(clients);
            Assert.Equal(204, res.StatusCode);
            Assert.Equal(5, clients[0].Notes.Count);
        }

        [Fact]
        public void DeleteNote()
        {
            //arrange
            var controller = new Test1.Controllers.ClientsController();

            Test1.DataOperation.path = DataArrange.testMainPath;
            Test1.DataOperation.clientsFileName = DataArrange.testClientsPath;

            DataArrange.CreateOneClientWithId0AndFiveNotesSecondNoteIdIs0();
            Test1.DataOperation.LoadClients();
            //act
            var res = (OkResult)controller.DeleteNote(0, 0);

            var clients = DataOperation.Clients;
            //assert
            Assert.NotNull(res);
            Assert.NotNull(clients);
            Assert.Equal(200, res.StatusCode);
            Assert.Equal(4, clients[0].Notes.Count);
        }

        [Fact]
        public void DeleteNoteWithIncorrectClientId()
        {
            //arrange
            var controller = new Test1.Controllers.ClientsController();

            Test1.DataOperation.path = DataArrange.testMainPath;
            Test1.DataOperation.clientsFileName = DataArrange.testClientsPath;

            DataArrange.CreateOneClientWithId0AndFiveNotesSecondNoteIdIs0();
            Test1.DataOperation.LoadClients();
            //act
            var res = (NoContentResult)controller.DeleteNote(90, 0);

            var clients = DataOperation.Clients;
            //assert
            Assert.NotNull(res);
            Assert.NotNull(clients);
            Assert.Equal(204, res.StatusCode);
            Assert.Equal(5, clients[0].Notes.Count);
        }

        [Fact]
        public void DeleteNoteWithIncorrectNoteId()
        {
            //arrange
            var controller = new Test1.Controllers.ClientsController();

            Test1.DataOperation.path = DataArrange.testMainPath;
            Test1.DataOperation.clientsFileName = DataArrange.testClientsPath;

            DataArrange.CreateOneClientWithId0AndFiveNotesSecondNoteIdIs0();
            Test1.DataOperation.LoadClients();
            //act
            var res = (NoContentResult)controller.DeleteNote(0, -12);

            var clients = DataOperation.Clients;
            //assert
            Assert.NotNull(res);
            Assert.NotNull(clients);
            Assert.Equal(204, res.StatusCode);
            Assert.Equal(5, clients[0].Notes.Count);
        }

        [Fact]
        public void DeleteClient()
        {
            //arrange
            var controller = new Test1.Controllers.ClientsController();

            Test1.DataOperation.path = DataArrange.testMainPath;
            Test1.DataOperation.clientsFileName = DataArrange.testClientsPath;

            DataArrange.CreateTwoTestClientsWithId0And1();
            Test1.DataOperation.LoadClients();
            //act
            var res = (OkResult)controller.DeleteClient(0);

            var clients = DataOperation.Clients;
            //assert
            Assert.NotNull(res);
            Assert.NotNull(clients);
            Assert.Equal(200, res.StatusCode);
            Assert.Single(clients);
            Assert.Equal(1, clients[0].ClientId);
        }

        [Fact]
        public void DeleteClientWithIncorrectId()
        {
            //arrange
            var controller = new Test1.Controllers.ClientsController();

            Test1.DataOperation.path = DataArrange.testMainPath;
            Test1.DataOperation.clientsFileName = DataArrange.testClientsPath;

            DataArrange.CreateTwoTestClientsWithId0And1();
            Test1.DataOperation.LoadClients();
            //act
            var res = (NoContentResult)controller.DeleteClient(2);

            var clients = DataOperation.Clients;
            //assert
            Assert.NotNull(res);
            Assert.NotNull(clients);
            Assert.Equal(204, res.StatusCode);
            Assert.Equal(2,clients.Count);
            Assert.Equal(0, clients[0].ClientId);
        }
    }

    
}