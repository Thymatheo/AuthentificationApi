using AuthentificationApi.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthentificationApi.DAL.Context
{
    public interface IGuardianBagPackContext
    {
        DbSet<Auth> Auths { get; set; }
    }
}