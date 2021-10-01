using App.Repository;
using App.Repository.ApiClient;
using Core.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

HttpClient httpClient = new();
IWebApiExecuter apiExecuter = new WebApiExecuter("https://localhost:44323", httpClient);

await TestTickets();

async Task TestTickets()
{
    Console.WriteLine("////////////////////");
    Console.WriteLine("Reading Tickets...");
    await GetTickets();

    Console.WriteLine("////////////////////");
    Console.WriteLine("Reading Tickets with id 1...");
    await GetTicketById(1);

    Console.WriteLine("////////////////////");
    Console.WriteLine("Create Ticket...");
    var tId = await CreateTicket();
    await GetTickets();

    Console.WriteLine("////////////////////");
    Console.WriteLine("Update Ticket...");
    var ticket = await GetTicketById(tId);
    await UpdateTicket(ticket);
    await GetTickets();

    Console.WriteLine("////////////////////");
    Console.WriteLine("Delete Ticket...");
    await DeleteTicket(tId);
    await GetTickets();
}

async Task GetTickets(string filter = null)
{
    TicketRepository ticketRepository = new(apiExecuter);
    var tickets = await ticketRepository.GetAsync(filter);
    foreach (var ticket in tickets)
    {
        Console.WriteLine($"Ticket: {ticket.Title}");
    }
}

async Task<Ticket> GetTicketById(int id)
{
    TicketRepository ticketRepository = new(apiExecuter);
    var ticket = await ticketRepository.GetByIdAsync(id);
    return ticket;
}

async Task<int> CreateTicket()
{
    TicketRepository ticketRepository = new(apiExecuter);
    return await ticketRepository.CreateAsync(new Ticket
    {
        ProjectId = 2,
        Title = " This is a very difficult bug, fix it!",
        Description = "The server crashed...."
    });
}

async Task UpdateTicket(Ticket ticket)
{
    TicketRepository ticketRepository = new TicketRepository(apiExecuter);
    ticket.Title = "Updated";
    await ticketRepository.UpdateAsync(ticket);
}

async Task DeleteTicket(int id)
{
    TicketRepository ticketRepository = new TicketRepository(apiExecuter);

    await ticketRepository.DeleteAsync(id);
}

# region Project Testing

async Task TestProjects()
{
    Console.WriteLine("////////////////////");
    Console.WriteLine("Reading projects...");
    await GetProjects();

    Console.WriteLine("////////////////////");
    Console.WriteLine("Reading project tickets...");
    await GetProjectTickets(1);

    Console.WriteLine("////////////////////");
    Console.WriteLine("Creating a Project...");
    var pId = await CreateProject();
    await GetProjects();

    Console.WriteLine("////////////////////");
    Console.WriteLine("Update a Project...");
    var project = await GetProject(pId);
    await UpdateProject(project);
    await GetProjects();

    Console.WriteLine("////////////////////");
    Console.WriteLine("Delete a Project...");
    await DeleteProject(pId);
    await GetProjects();

}

async Task GetProjects()
{
    ProjectRepository repository = new(apiExecuter);
    var projects = await repository.GetAsync();
    foreach (var project in projects) 
    {
        Console.WriteLine($"Project: {project.Name}");
    }
}

async Task<Project> GetProject(int id)
{
    ProjectRepository repository = new(apiExecuter);
    return await repository.GetByIdAsync(id);
}


async Task GetProjectTickets(int id)
{
    var project = await GetProject(id);
    Console.WriteLine($"Project: {project.Name}");

    ProjectRepository repository = new(apiExecuter);
    var tickets = await repository.GetProjectTicketsAsync(id);
    foreach (var ticket in tickets)
    {
        Console.WriteLine($"    Ticket: {ticket.Title}");
    }

}

async Task<int> CreateProject()
{
    var project = new Project { Name = "Another Project" };

    ProjectRepository repository = new(apiExecuter);
    return await repository.CreateAsync(project);
}

async Task UpdateProject(Project project)
{
    ProjectRepository repository = new(apiExecuter);
    project.Name += " updated";

    await repository.UpdateAsync(project);
}

async Task DeleteProject(int id)
{
    ProjectRepository repository = new(apiExecuter);

    await repository.DeleteAsync(id);
}
#endregion
