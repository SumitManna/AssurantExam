using AssurantTest.Application.Entities;
using AssurantTest.Application.Exception;
using AssurantTest.Application.Interfaces.Repository;
using AssurantTest.Infrastructure.Common.Utility;
using System.Net;
using System.Text.Json;

namespace AssurantTest.Infrastructure.Repository.States
{
    public class StateRepository : IStateRepository
    {
        public IReadOnlyCollection<State> GetAllStates()
        {
            var stateListString = SeedData.ReadSeedData("States.json");
            if (!string.IsNullOrEmpty(stateListString))
                return JsonSerializer.Deserialize<IReadOnlyCollection<Application.Entities.State>>(stateListString)!;

            throw new ApiException((int)HttpStatusCode.InternalServerError, "Product List not found");
        }

        public State? GetStateData(Guid stateId)
        {
            var productList = GetAllStates();
            return productList.FirstOrDefault(c => c.Id == stateId);
        }
    }
}
