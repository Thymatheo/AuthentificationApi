using AuthentificationApi.DAL.Models;
using System.Threading.Tasks;

namespace AuthentificationApi.DAL.DP
{
    public interface IDpAuth
    {
        Auth GetAuthByIdApp(string appId);
        Auth GetAuthByAppIdAndState(string appId, string state);
        Auth GetAuthByState(string state);
        Task<Auth> UpdateAuth(Auth auth);
        Task<Auth> AddAuth(Auth auth);
        Task<Auth> AddCodeToAuthById(int id, string code);
    }
}