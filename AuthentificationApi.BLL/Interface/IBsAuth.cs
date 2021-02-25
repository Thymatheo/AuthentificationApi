using AuthentificationApi.BLL.DTO;
using System.Threading.Tasks;

namespace AuthentificationApi.BLL.BS
{
    public interface IBsAuth
    {
        DtoAuth GetAuthByState(string state);
        Task<DtoAuthCreated> AddAuth(string appId);
        Task<DtoAuthUpdateCode> UpdateAuthCode(string appId, string code);
        Task<DtoAuthUpdateState> UpdateAuthState(string appId, string state);
        DtoAuth GetAuthByAppId(string appId);
        string GetNewState();
        string GetNewAppId();
    }
}