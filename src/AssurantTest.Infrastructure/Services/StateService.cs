using AssurantTest.Application.Entities;
using AssurantTest.Application.Interfaces.Repository;
using AssurantTest.Application.Interfaces.Services;

namespace AssurantTest.Infrastructure.Services
{
    public class StateService(IStateRepository stateRepository) : IStateService
    {
        public async Task<IReadOnlyCollection<State>> GetStateListAsync()
        {
            return await Task.FromResult(stateRepository.GetAllStates());
        }
    }
}
