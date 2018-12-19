using EventCreator.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventCreator.Business.Services
{
    public class CustomerService
    {
        public List<CustomerBl> GetAll(EventBl eventBl)
        {
            return eventBl.CustomerList;
        }

        public void AddTicketToCustomer(CustomerBl customerBl, TicketTypeBl typeOfTicket)
        {
            customerBl.UserTicket = typeOfTicket;
        }

        public void AddCustomer(EventBl eventBl, CustomerBl customerBl)
        {
            eventBl.CustomerList.Add(customerBl);
        }
    }
}
