using Planner.Business.Interfaces;
using Planner.Data.Interfaces;
using Planner.Data.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Planner.Business.Managers
{
    public class OwnerManager : IOwnerManager
    {
        private readonly IOwnerRepository ownerRepository;
        public OwnerManager(IOwnerRepository ownerRepository)
        {
            this.ownerRepository = ownerRepository;
        }
        public ObservableCollection<Owner> GetAll()
        {
            return ownerRepository.GetAll();
        }
        public Owner Add(string name)
        {
            if (name.Length == 0)
                throw new ArgumentException("Zadejte název prodejce");

            var owner = new Owner()
            {
                Name = name
            };
            ownerRepository.Insert(owner);
            return owner;
        }
        public IEnumerable<Owner> FilterOwners(IEnumerable<Owner> owners, string name, string id)
        {
            int? filterId = null;
            if (id.Length != 0)
                filterId = int.Parse(id);

            IEnumerable<Owner> filteredOwners = owners;

            if (filterId == null && name.Length == 0)
                return filteredOwners;
            else
            {
                if (name.Length != 0)
                    filteredOwners = filteredOwners.Where(x => x.Name == name);
                if (filterId != null)
                    filteredOwners = filteredOwners.Where(x => x.OwnerId == filterId);

                return filteredOwners;
            }
        }
    }
}
