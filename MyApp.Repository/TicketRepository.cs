﻿using MyApp.Repository.ApiClient;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly IWebApiExecuter webApiExecuter;
        private readonly string apiVersion = "api-version=2.0";


        public TicketRepository(IWebApiExecuter webApiExecuter)
        {
            this.webApiExecuter = webApiExecuter;
        }

        public async Task<IEnumerable<Ticket>> GetAsync(string filter = null)
        {
            string uri = $"api/tickets?{apiVersion}";

            if (!string.IsNullOrWhiteSpace(filter))
                uri += $"&titleordescription={filter.Trim()}";

            return await webApiExecuter.InvokeGet<IEnumerable<Ticket>>(uri);
        }

        public async Task<Ticket> GetByIdAsync(int id)
        {
            return await webApiExecuter.InvokeGet<Ticket>($"api/tickets/{id}?{apiVersion}");
        }


        public async Task<int> CreateAsync(Ticket ticket)
        {
            ticket = await webApiExecuter.InvokePost($"api/tickets?{apiVersion}", ticket);
            return ticket.TicketId.Value;
        }
        public async Task UpdateAsync(Ticket ticket)
        {
            await webApiExecuter.InvokePut($"api/tickets/{ticket.TicketId}?{apiVersion}", ticket);

        }

        public async Task DeleteAsync(int id)
        {
            await webApiExecuter.InvokeDelete($"api/tickets/{id}?{apiVersion}");

        }
    }
}