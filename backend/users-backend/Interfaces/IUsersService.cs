using users_backend.Models;

namespace users_backend.Interfaces
{
    public interface IUsersService
    {

        Task<UserLogicModel?> GetById(Guid id);
        Task<BasePageModel<UserLogicModel>> Get(string? userName = null,
                                                  string? phone = null,
                                                  string? userId = null,
                                                  int take = 10,
                                                  int page = 0);

        Task<UserLogicModel> Create(UserLogicModel model);

        Task<UserLogicModel> Update(UserLogicModel model);

        Task Delete(Guid id);
    }
}
