using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using log4net;
using LIMSData;
using LIMSData.DBObjects;
using LMSDataService.Models;

namespace LMSDataService.Controllers
{
    public class UserLoginCreationController : ApiController
    {
        private LMSDataDBContext db = new LMSDataDBContext();

        private static ILog _log = log4net.LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // POST: api/UserLoginCreation
        public IHttpActionResult Post(HttpRequestMessage request, UserLoginCreation userLogin)
        {
            var headers = request.Headers;
            var requestingUser = string.Empty;
            if (headers.Contains("token"))
            {
                try
                {
                    var userToken = headers.GetValues("token").First();
                    Tuple<bool, string> validationResult = JwtTokenHelper.ValidateToken(userToken);
                    if (validationResult.Item1)
                    {
                        // Check to see if the refreshId's match.  If they do not, then it means something (i.e. invalid password)
                        //   has caused the issued token to become invalidated.  If so, then we need to send a message back to 
                        //   the calling client.

                        long refreshId = 0;
                        if (long.TryParse(JwtTokenHelper.GetTokenPayloadValue(userToken, "refreshId"), out refreshId))
                        {
                            if (db.UserLogins.FirstOrDefault(p => p.RefreshId == refreshId) == null)
                            {
                                return Content(HttpStatusCode.Unauthorized,
                                    "Token failed refresh check, User account disabled/locked-out");
                            }

                        }

                        requestingUser = JwtTokenHelper.GetTokenPayloadValue(userToken, "unique_name");
                        if (!ModelState.IsValid)
                        {
                            return BadRequest(ModelState);
                        }

                        UserLogin newUserLogin = db.UserLogins.Create();

                        newUserLogin.Id = userLogin.Id;
                        newUserLogin.AccessFailedCount = 0;
                        newUserLogin.Created = DateTime.Now;
                        newUserLogin.CreatedBy = requestingUser;
                        newUserLogin.LastModifiedBy = requestingUser;
                        newUserLogin.LastModified = DateTime.Now;
                        newUserLogin.LockoutEnabled = false;
                        newUserLogin.LockoutEnd = DateTime.Parse("1/1/1900");
                        newUserLogin.Login = userLogin.Login;
                        newUserLogin.IsAdmin = userLogin.IsAdmin;
                        newUserLogin.RefreshId = 0;
                        newUserLogin.PasswordHash = LMSDataService.SecurityHelper.Sha256Hash(userLogin.Password);

                        db.UserLogins.Add(newUserLogin);
                        db.SaveChanges();

                        return CreatedAtRoute("DefaultApi", new { id = newUserLogin.Id }, newUserLogin);
                    }
                }
                catch (Exception e)
                {
                    _log.Error(string.Format(Resource.GeneralError_Pre, "UserLoginCreation:Post"), e);
                }
                
            }

            return BadRequest("Token invalid or not present");
        }

        // PUT: api/UserLoginCreation/5
        public IHttpActionResult Put(int id, HttpRequestMessage request, UserLoginCreation userLogin)
        {
            var headers = request.Headers;
            var requestingUser = string.Empty;
            if (headers.Contains("token"))
            {
                try
                {

                    var userToken = headers.GetValues("token").First();
                    Tuple<bool, string> validationResult = JwtTokenHelper.ValidateToken(userToken);
                    if (validationResult.Item1)
                    {
                        // Check to see if the refreshId's match.  If they do not, then it means something (i.e. invalid password)
                        //   has caused the issued token to become invalidated.  If so, then we need to send a message back to 
                        //   the calling client.

                        long refreshId = 0;
                        if (long.TryParse(JwtTokenHelper.GetTokenPayloadValue(userToken, "refreshId"), out refreshId))
                        {
                            if (db.UserLogins.FirstOrDefault(p => p.RefreshId == refreshId) == null)
                            {
                                return Content(HttpStatusCode.Unauthorized,
                                    "Token failed refresh check, User account disabled/locked-out");
                            }

                        }

                        requestingUser = JwtTokenHelper.GetTokenPayloadValue(userToken, "unique_name");
                        if (!ModelState.IsValid)
                        {
                            return BadRequest(ModelState);
                        }

                        UserLogin newUserLogin = db.UserLogins.Find(id);

                        if (newUserLogin == null)
                        {
                            return NotFound();
                        }
                        newUserLogin.Id = userLogin.Id;
                        newUserLogin.LastModifiedBy = requestingUser;
                        newUserLogin.LastModified = DateTime.Now;
                        newUserLogin.Login = userLogin.Login;
                        newUserLogin.IsAdmin = userLogin.IsAdmin;
                        newUserLogin.LockoutEnabled = false;
                        newUserLogin.LockoutEnd = DateTime.Parse("1/1/1900");
                        newUserLogin.AccessFailedCount = 0;
                        newUserLogin.PasswordHash = LMSDataService.SecurityHelper.Sha256Hash(userLogin.Password);

                        //db.UserLogins.Add(newUserLogin);
                        db.Entry(newUserLogin).State = EntityState.Modified;
                        db.SaveChanges();

                        return Ok(newUserLogin);
                    }

                }
                catch (Exception e)
                {
                    _log.Error(string.Format(Resource.GeneralError_Pre, "UserLoginCreation:Put"), e);
                }            }

            return BadRequest("Token invalid or not present");
        }

        // DELETE: api/UserLoginCreation/5
        public void Delete(int id)
        {
        }
    }
}
