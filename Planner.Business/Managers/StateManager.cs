using System;
using Planner.Data.Interfaces;
using Planner.Business.Interfaces;
using Planner.Data.Models;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

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
        public IEnumerable<State> FilterStates(IEnumerable<State> states, string id, string name)
        {
            if (id.Length == 0 && name.Length == 0)
                return states;

            if (id.Length != 0)
                states = states.Where(x => x.StateId == int.Parse(id));
            if (name.Length != 0)
                states = states.Where(x => x.Name.ToLower().Contains(name.ToLower()));

            return states;
        }
    }
}
