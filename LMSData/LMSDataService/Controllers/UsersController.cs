using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Batch;
using System.Web.Http.Description;
using LIMSData;
using LIMSData.DBObjects;

namespace LMSDataService.Controllers
{
    public class UsersController : ApiController
    {
        private LMSDataDBContext db = new LMSDataDBContext();

        // GET: api/Users
        public IHttpActionResult GetUsers(HttpRequestMessage request)
        {
            var headers = request.Headers;
            if (headers.Contains("token"))
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
                }

                IQueryable<User> fullResults = db.Users.Include(I => I.Role).Include(t => t.UserLogin);
                return Ok(fullResults);
            }

            //if (headers.Contains("userid"))
            //{
            //    return db.Users;
            //}

            return null;
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id, HttpRequestMessage request)
        {
            var headers = request.Headers;
            if (headers.Contains("token"))
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
                }

                User user = db.Users.Include(t => t.Role).Include(r => r.UserLogin).FirstOrDefault(p => p.Id == id);
                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }

            return null;
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user, HttpRequestMessage request)
        {
            var headers = request.Headers;
            if (headers.Contains("token"))
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

                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    if (id != user.Id)
                    {
                        return BadRequest();
                    }

                    User updUser = db.Users.Find(id);

                    db.Entry(updUser).CurrentValues.SetValues(user);
                    
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!UserExists(id))
                        {
                            return NotFound();
                        }

                        throw;
                    }

                    return Ok(user);
                }
                return BadRequest(validationResult.Item2);
            }

            return BadRequest("Token invalid or not present");
        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(HttpRequestMessage request, User user)
        {
            var headers = request.Headers;
            if (headers.Contains("token"))
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

                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    db.Users.Add(user);
                    db.Entry(user.Role).State = EntityState.Unchanged;
                    db.SaveChanges();

                    return CreatedAtRoute("DefaultApi", new {id = user.Id}, user);
                }

                return BadRequest(validationResult.Item2);
            }

            return BadRequest("Token invalid or not present");
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id, HttpRequestMessage request)
        {
            var headers = request.Headers;
            if (headers.Contains("token"))
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

                    User user = db.Users.Find(id);
                    if (user == null)
                    {
                        return NotFound();
                    }

                    db.Users.Remove(user);
                    db.SaveChanges();

                    return Ok(user);
                }
            }

            return BadRequest("Token invalid or not present");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}