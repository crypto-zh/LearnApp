using LearnApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnApp.Data.Model
{
    public class ClientServiceModel
    {
        public ClientServiceModel(int iD, int clientID, int serviceID, DateTime startTime, string comment, Client client, Service service)
        {
            ID = iD;
            ClientID = clientID;
            ServiceID = serviceID;
            StartTime = startTime;
            Comment = comment;
            Client = client;
            Service = service;
            FIO = client.LastName + " " + client.FirstName[0] + ". ";
            if (client.Patronymic != null)
                FIO = FIO + client.Patronymic[0] + ".";

        }

        public int ID { get; set; }
        public Boolean isHighlited { get; set; }
        public int ClientID { get; set; }
        public int ServiceID { get; set; }
        public System.DateTime StartTime { get; set; }
        public string Comment { get; set; }
        public string FIO { get; set; }

        public String DateTime { get; set; }
        public double Hours { get; set; }
        public double Minutes { get; set; }
        public virtual Client Client { get; set; }
        public virtual Service Service { get; set; }
    }
}
