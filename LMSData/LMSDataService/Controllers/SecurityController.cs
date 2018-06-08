using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using LIMSData;
using LIMSData.DBObjects;

namespace LMSDataService.Controllers
{
    public class SecurityController : ApiController
    {
        private LMSDataDBContext db = new LMSDataDBContext();

        // GET: api/Security
        public IEnumerable<string> Get()
        {
            return new string[] {"value1", "value2"};
        }

        // GET: api/Security/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Security
        public IHttpActionResult Post(HttpRequestMessage request, [FromBody] string value)
        {
            var headers = request.Headers;
            var returnMessage = string.Empty;
            //Check the request object to see if they passed a userId
            if (headers.Contains("userid"))
            {
                var login = headers.GetValues("userid").First();
                if (headers.Contains("password"))
                {
                    var passwordTry = headers.GetValues("password").First();

                    try
                    {
                        var userDetails = db.Users.Where(p => p.UserName == login).Include(i => i.Role)
                            .FirstOrDefault();
                        if (userDetails == null || userDetails.Archived) return Unauthorized();
                        {
                            var userAccount = db.UserLogins.FirstOrDefault(p => p.Login == login);
                            if (userAccount != null && !UserIsLockedOut(userAccount))
                            {
                                if (LMSDataService.SecurityHelper.Sha256Hash(passwordTry).ToUpper() ==
                                    userAccount.PasswordHash.ToUpper()) //  Success!
                                {
                                    
                                    SecurityController securityController = new SecurityController();
                                    var token = securityController.RefreshToken(userAccount, userDetails);
                                    return Ok(token);
                                }

                                
                            }

                            if (userAccount != null)
                            {
                                returnMessage = "Token failed refresh check, User account disabled/locked-out";
                                userAccount.AccessFailedCount = userAccount.AccessFailedCount + 1;
                                if (userAccount.AccessFailedCount >= 5)
                                {
                                    userAccount.LockoutEnabled = true;
                                    userAccount.RefreshId = 0;
                                    userAccount.LockoutEnd = DateTime.Now.AddMinutes(15);
                                }

                                userAccount.LastModifiedBy = "SECURITY";
                                userAccount.LastModified = DateTime.Now;
                                db.Entry(userAccount).State = EntityState.Modified;
                            }

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                                throw;
                            }

                            return Content(HttpStatusCode.Unauthorized, returnMessage);
                        }
                    }
                    catch (Exception e)
                    {
                        //_log.Error("An error occurred while adding Users.", e);
                        return InternalServerError(e);
                    }
                }
            }

            return BadRequest("Header values not found.");
        }

        // PUT: api/Security/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Security/5
        public void Delete(int id)
        {
        }

        private string RefreshToken(UserLogin userAccount, User userDetails)
        {
            var refreshId = DateTime.Now.Ticks; // To verify freshness of issued Token.
            JwtPayload payload = new JwtPayload();
            payload.Add("isAdmin", userAccount.IsAdmin);
            payload.Add("userLoginId", userAccount.Id);
            payload.Add("displayName", userDetails.FirstName + " " + userDetails.LastName);
            payload.Add("refreshId", refreshId);

            var token = JwtTokenHelper.GenerateJwtToken(userAccount.Login,
                userDetails.Role.Name, payload);

            userAccount.AccessFailedCount = 0;
            userAccount.LockoutEnabled = false;
            userAccount.LockoutEnd = DateTime.Parse("1/1/1900");
            userAccount.RefreshId = refreshId;
            userAccount.LastModifiedBy = "SECURITY";
            userAccount.LastModified = DateTime.Now;
            db.Entry(userAccount).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return token;
        }

        private bool UserIsLockedOut(UserLogin userLogin)
        {
            var isLockedOut = userLogin.LockoutEnabled;

            if (userLogin.LockoutEnd < DateTime.Now && isLockedOut)
            {
                // Unlock account and pass back true.
                userLogin.LockoutEnabled = false;
                userLogin.LockoutEnd = DateTime.Parse("1/1/1900");
                userLogin.AccessFailedCount = 0;
                userLogin.LastModifiedBy = "SECURITY";
                userLogin.LastModified = DateTime.Now;

                db.Entry(userLogin).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                    isLockedOut = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            return isLockedOut;
        }

        
    }
}