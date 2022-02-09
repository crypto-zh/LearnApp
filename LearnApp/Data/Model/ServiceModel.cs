using LearnApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnApp.Data.Model
{
    public class ServiceModel
    {
        public ServiceModel()
        {
            this.ClientService = new HashSet<ClientService>();
            this.ServicePhoto = new HashSet<ServicePhoto>();
        }
        public ServiceModel(int iD, string title, decimal cost, int durationInSeconds, string description, double? discount, string mainImagePath, ICollection<ClientService> clientService, ICollection<ServicePhoto> servicePhoto)
        {
            ID = iD;
            Title = title;
            Cost = cost;
            DurationInMinute = durationInSeconds / 60;
            if (Discount != null && Discount != 0)
                CostWithDiscount = (decimal)(Convert.ToDouble(cost) * discount);
            else
                CostWithDiscount = Cost;
            Description = description;
            if (discount == null)
                Discount = 0;
            else
                Discount = discount * 100;
            MainImagePath = mainImagePath;
            ClientService = clientService;
            ServicePhoto = servicePhoto;
        }

        public Boolean isHighlited { get; set; }
        public Boolean IsClient { get; set; }
        public int ID { get; set; }
        public string Title { get; set; }
        public decimal Cost { get; set; }
        public int DurationInMinute
        {
            get; set;
        }
        public decimal CostWithDiscount { get; set; }
        public string Description { get; set; }
        public Nullable<double> Discount { get; set; }
        public string MainImagePath { get; set; }
        public virtual ICollection<ClientService> ClientService { get; set; }
        public virtual ICollection<ServicePhoto> ServicePhoto { get; set; }

        public Service ToServiceEntities()
        {
            return new Service(ID, Title, Cost, DurationInMinute * 60, CostWithDiscount, Description, Discount / 100, MainImagePath, ClientService, ServicePhoto);
        }
    }
}
