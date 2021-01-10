using Planner.Business.Interfaces;
using Planner.Data.Interfaces;
using Planner.Data.Models;
using System;
using System.Collections.ObjectModel;

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

    }
}
