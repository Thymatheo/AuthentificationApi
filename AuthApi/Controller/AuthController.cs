using AuthentificationApi.BLL.BS;
using AuthentificationApi.BLL.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IBsAuth _bsAuth;

        public AuthController(IBsAuth bsAuth)
        {
            _bsAuth = bsAuth;
        }

        /// <summary>
        /// This EndPoint is use to add a new Auth to the database it's required an appId.
        /// Must use the GetNewAppId EndPoint before
        /// </summary>
        /// <param name="appId"></param>
        /// <returns>A newly created auth</returns>
        /// <response code="200">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <response code="409">If the AppId already exist in the database</response>
        /// <response code="500">If an error occure during the process</response> 
        [HttpPost]
        [Route("add")]
        public async Task<ActionResult<DtoAuthCreated>> AddAuth([FromQuery] string appId)
        {
            try
            {
                if (_bsAuth.GetAuthByAppId(appId) != null)
                    return Conflict("This AppId already exist");
                var result = await _bsAuth.AddAuth(appId);
                if (result == null)
                    return BadRequest("Error while adding Auth");
                else
                    return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        /// <summary>
        /// This EndPoint is use to update the state of an auth it's required an appId and the state
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="state"></param>
        /// <returns>Return the item updated</returns>
        /// <response code="200">Returns the iten updated</response>
        /// <response code="400">If the item is null</response> 
        /// <response code="404">If the appid already exist in the database</response>
        /// <response code="500">If an error occure during the process</response> 
        [HttpPut]
        [Route("update/{appId}/state")]
        public async Task<ActionResult<DtoAuthUpdateState>> UpdateAuthState([FromRoute] string appId, [FromQuery] string state)
        {
            try
            {
                if (_bsAuth.GetAuthByAppId(appId)==null) 
                    return NotFound("Unable to find the auth");
                var result = await _bsAuth.UpdateAuthState(appId, state);
                if (result == null)
                    return BadRequest("Error while updating Auth state");
                else
                    return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        /// <summary>
        /// This EndPoint is use to update the code of an auth it's required an appId and the code
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="code"></param>
        /// <returns>Return the item updated</returns>
        /// <response code="200">Returns the iten updated</response>
        /// <response code="400">If the item is null</response>
        /// <response code="404">If the appid doesn't exist in the database</response>
        /// <response code="500">If an error occure during the process</response> 
        [HttpPut]
        [Route("update/{appId}/code")]
        public async Task<ActionResult<DtoAuthUpdateCode>> UpdateAuthCode([FromRoute] string appId, [FromQuery] string code)
        {
            try
            {
                if (_bsAuth.GetAuthByAppId(appId) == null)
                    return NotFound("Unable to find the auth");
                var result = await _bsAuth.UpdateAuthCode(appId, code);
                if (result == null)
                    return BadRequest("Error while updating Auth state");
                else
                    return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// This EndPoint is use to update the code of an auth it's required an state and the code
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns>Return the item updated</returns>
        /// <response code="200">Returns the iten updated</response>
        /// <response code="400">If the item is null</response>
        /// <response code="404">If the appid already exist in the database</response>
        /// <response code="500">If an error occure during the process</response> 
        [HttpGet]
        [Route("update/code")]
        public async Task<ActionResult<DtoAuthUpdateCode>> UpdateAuthCodeWithState([FromQuery] string code,[FromQuery] string state)
        {
            try
            {
                var auth = _bsAuth.GetAuthByState(state);
                if (auth == null)
                    return NotFound("Unable to find the auth");
                var result = await _bsAuth.UpdateAuthCode(auth.AppID,code);
                if (result == null)
                    return BadRequest("Error while updating Auth state");
                else
                    return Ok("Connection successfully establish");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// This EndPoint is use gather the auth, need the corresponding AppId
        /// </summary>
        /// <param name="appId"></param>
        /// <returns>Return the corresponding Auth </returns>
        /// <response code="200">Returns the corresponding auth</response>
        /// <response code="404">Unable to find the auth</response> 
        /// <response code="500">If an error occure during the process</response> 
        [HttpGet]
        [Route("{appId}")]
        public ActionResult<DtoAuth> GetAuth([FromRoute] string appId)
        {
            try
            {
                var auth = _bsAuth.GetAuthByAppId(appId);
                if (auth == null)
                    return NotFound("Unable to find the auth");
                else
                    return Ok(auth);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        /// <summary>
        /// This EndPoint generate a new AppId
        /// </summary>
        /// <returns>Return a new AppId</returns>
        /// <response code="200">Return a new AppId</response>
        /// <response code="400">If the item is null</response> 
        /// <response code="500">If an error occure during the process</response> 
        [HttpGet]
        [Route("appid/new")]
        public ActionResult<string> GetNewAppId()
        {
            try
            {
                var appId = _bsAuth.GetNewAppId();
                if (appId != null)
                    return Ok(appId);
                else
                    throw new Exception("Unable to generate a new AppId please try again");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        /// <summary>
        /// This EndPoint generate a new State
        /// </summary>
        /// <returns>Return a new State</returns>
        /// <response code="200">Return a new State</response>
        /// <response code="400">If the item is null</response> 
        /// <response code="500">If an error occure during the process</response> 
        [HttpGet]
        [Route("state/new")]
        public ActionResult<string> GetNewState()
        {
            try
            {
                var state = _bsAuth.GetNewState();
                if (state != null)
                    return Ok(state);
                else
                    throw new Exception("Unable to generate a new State please try again");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}
