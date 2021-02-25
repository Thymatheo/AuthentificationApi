using AuthentificationApi.BLL.DTO;
using AuthentificationApi.DAL.DP;
using AuthentificationApi.DAL.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuthentificationApi.BLL.BS
{
    public class BsAuth : IBsAuth
    {
        private readonly IDpAuth _dpAuth;

        public BsAuth(IDpAuth dpAuth)
        {
            _dpAuth = dpAuth;
        }

        public DtoAuth GetAuthByState(string state)
        {
            var auth = _dpAuth.GetAuthByState(state);
            if (auth == null)
                return null;
            else
                return ToDtoAuth(auth);
        }

        public DtoAuth GetAuthByAppId(string appId)
        {
            var auth = _dpAuth.GetAuthByIdApp(appId);
            if (auth == null)
                return null;
            else
                return ToDtoAuth(auth);
        }

        public async Task<DtoAuthCreated> AddAuth(string appId)
        {
            var auth = new Auth() { AppId = appId };
            var result = await _dpAuth.AddAuth(auth);
            if (result == null)
                return null;
            else
                return ToDtoCreated(result);

        }

        public async Task<DtoAuthUpdateState> UpdateAuthState(string appId, string state)
        {
            var auth = _dpAuth.GetAuthByIdApp(appId);
            auth.State = state;
            var result = await _dpAuth.UpdateAuth(auth);
            if (result == null)
                return null;
            else
                return ToDtoUpdateState(result);
        }

        public async Task<DtoAuthUpdateCode> UpdateAuthCode(string appId, string code)
        {
            var auth = _dpAuth.GetAuthByIdApp(appId);
            auth.Code = code;
            var result = await _dpAuth.UpdateAuth(auth);
            if (result == null)
                return null;
            else
                return ToDtoUpdateCode(result);
        }

        public string GetNewState()
        {
            bool check = false;
            string state = "";
            while (!check)
            {
                var tryState = BsUtils._RandomString(40);
                if (_dpAuth.GetAuthByState(tryState) == null)
                {
                    check = true;
                    state = tryState;
                }
            }
            return state;
        }

        public string GetNewAppId()
        {
            bool check = false;
            string appId = "";
            while (!check)
            {
                var tryAppId = BsUtils._RandomString(40);
                if (_dpAuth.GetAuthByIdApp(tryAppId) == null)
                {
                    check = true;
                    appId = tryAppId;
                }
            }
            return appId;
        }

        public DtoAuthCreated ToDtoCreated(Auth auth)
        {
            return new DtoAuthCreated() { IdAuth = auth.IdAuth, AppId = auth.AppId };
        }

        public DtoAuthUpdateState ToDtoUpdateState(Auth auth)
        {
            return new DtoAuthUpdateState() { AppId = auth.AppId, State = auth.State };
        }

        public DtoAuthUpdateCode ToDtoUpdateCode(Auth auth)
        {
            return new DtoAuthUpdateCode() { AppId = auth.AppId, Code = auth.Code };
        }

        public DtoAuth ToDtoAuth(Auth auth)
        {
            return new DtoAuth()
            {
                AppID = auth.AppId,
                State = auth.State,
                Code = auth.Code
            };
        }
    }
}
