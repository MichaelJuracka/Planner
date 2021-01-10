using Planner.Business.Interfaces;
using Planner.Data.Interfaces;
using Planner.Data.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner.Business.Managers
{
    public class BusTypeManager : IBusTypeManager
    {
        private IBusTypeRepository busTypeRepository;
        public BusTypeManager(IBusTypeRepository busTypeRepository)
        {
            this.busTypeRepository = busTypeRepository;
        }
        public ObservableCollection<BusType> GetAll()
        {
            var collection = busTypeRepository.GetAll();
            collection.Insert(0, new BusType() { Name = "", BusTypeId = 0, NumberOfSeats = 0 });
            return collection;
        }
    }
}
