using AssurantTest.Application.Entities;

namespace AssurantTest.Application.Interfaces.Repository
{
    public interface IStateRepository
    {
        State? GetStateData(Guid stateId);
        IReadOnlyCollection<State> GetAllStates();
    }
}
