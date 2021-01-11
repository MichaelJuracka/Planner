using System;
using Planner.Data.Interfaces;
using Planner.Business.Interfaces;
using Planner.Data.Models;
using System.Collections.ObjectModel;

namespace Planner.Business.Managers
{
    public class StateManager : IStateManager
    {
        private IStateRepository stateRepository;
        public StateManager(IStateRepository stateRepository)
        {
            this.stateRepository = stateRepository;
        }
        public State Add(string name)
        {
            if (name.Length == 0)
                throw new ArgumentException("Zadejte název státu");

            var state = new State()
            {
                Name = name
            };

            stateRepository.Insert(state);

            return state;
        }

        public void Delete(int StateId)
        {
            stateRepository.Delete(StateId);
        }

        public ObservableCollection<State> GetAll()
        {
            return stateRepository.GetAll();
        }
    }
}
