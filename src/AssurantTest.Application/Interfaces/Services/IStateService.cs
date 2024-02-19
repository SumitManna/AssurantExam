using AssurantTest.Application.Entities;

namespace AssurantTest.Application.Interfaces.Services
{
    public interface IStateService
    {
        Task<IReadOnlyCollection<State>> GetStateListAsync();
    }
}
