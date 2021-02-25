using AuthentificationApi.DAL.Context;
using AuthentificationApi.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthentificationApi.DAL.DP
{
    public class DpAuth : IDpAuth
    {
        
        public GuardianBagPackContext _db { get; set; }

        public DpAuth(GuardianBagPackContext db)
        {
            _db = db;
        }

        public Auth GetAuthByIdApp(string appId)
        {
            return _db.Auths.Where(x => x.AppId == appId).FirstOrDefault();
        }
        public Auth GetAuthByState(string state)
        {
            return _db.Auths.Where(x => x.State == state).FirstOrDefault();
        }
        public Auth GetAuthByAppIdAndState(string appId, string state)
        {
            return _db.Auths.Where(x => x.AppId == appId && x.State == state).FirstOrDefault();
        }

        public async Task<Auth> UpdateAuth(Auth auth)
        {
            var result = _db.Auths.Update(auth);
            await _db.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Auth> AddAuth(Auth auth)
        {
            var result = await _db.Auths.AddAsync(auth);
            await _db.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Auth> AddCodeToAuthById(int id, string code)
        {
            var auth = _db.Auths.Where<Auth>(x => x.IdAuth == id).FirstOrDefault();
            auth.Code = code;
            var result = await UpdateAuth(auth);
            return result;
        }

    }
}
