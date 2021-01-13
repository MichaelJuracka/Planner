using Planner.Business.Interfaces;
using Planner.Data.Interfaces;
using Planner.Data.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Planner.Business.Managers
{
    public class ProviderManager : IProviderManager
    {
        private IProviderRepository providerRepository;
        public ProviderManager(IProviderRepository providerRepository)
        {
            this.providerRepository = providerRepository;
        }
        public Provider Add(string name)
        {
            if (name.Length == 0)
                throw new ArgumentException("Zadejte název dopravce");

            var provider = new Provider()
            {
                Name = name
            };

            providerRepository.Insert(provider);
            return provider;
        }

        public void Delete(int providerId)
        {
            providerRepository.Delete(providerId);
        }

        public ObservableCollection<Provider> GetAll()
        {
            return providerRepository.GetAll();
        }
        public IEnumerable<Provider> FilterProviders(IEnumerable<Provider> providers, string id, string name)
        {
            if (id.Length == 0 && name.Length == 0)
                return providers;

            if (id.Length != 0)
                providers = providers.Where(x => x.ProviderId == int.Parse(id));
            if (name.Length != 0)
                providers = providers.Where(x => x.Name.ToLower().Contains(name.ToLower()));

            return providers;
        }
    }
}
