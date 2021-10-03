﻿using Core.Models;
using MyApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.ApplicationLogic
{
    public class TicketScreenUseCases : ITicketScreenUseCases
    {
        private readonly ITicketRepository ticketRepository;

        public TicketScreenUseCases(ITicketRepository ticketRepository)
        {
            this.ticketRepository = ticketRepository;
        }

        public async Task<int> AddTicket(Ticket ticket)
        {
            return await ticketRepository.CreateAsync(ticket);
        }
    }
}
