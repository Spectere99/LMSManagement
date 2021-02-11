using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using LIMSData.DBObjects;
using LMSDataService.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace LMSDataServiceTests
{
    [TestClass]
    public class TestLookupController
    {
        [TestMethod]
        public void GetLookups_ShouldReturnAllLookups()
        {
            var lookupsController = new LookupsController
            {
                Request = new HttpRequestMessage()
                {
                    Properties = {{ HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration()}},
                    Headers = { {"userid", "admin"} }
                }
            };
            
            var getResult = lookupsController.GetLookups(lookupsController.Request);
            var getResponse = getResult.ExecuteAsync(CancellationToken.None).Result;

            List<Lookup> returnList = new List<Lookup>();
            Assert.IsTrue(getResponse.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.OK, getResponse.StatusCode);
            Assert.IsTrue(getResponse.TryGetContentValue(out returnList));  // Did we get a return result?
            
            Assert.AreEqual(13, returnList.Count);
            

        }

        [TestMethod]
        public void GetLookups_ShouldReturnBadRequestWhenMissingUserIdHeader()
        {
            var lookupsController = new LookupsController
            {
                Request = new HttpRequestMessage()
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } },
                    Headers = {}
                }
            };

            var getResult = lookupsController.GetLookups(lookupsController.Request);
            var getResponse = getResult.ExecuteAsync(CancellationToken.None).Result;

            Assert.IsTrue(!getResponse.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.BadRequest, getResponse.StatusCode);
        }

        [TestMethod]
        public void GetLookups_ShouldReturnModuleTypeLookups()
        {
            var lookupsController = new LookupsController
            {
                Request = new HttpRequestMessage()
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } },
                    Headers = { { "userid", "admin" }, { "lookupTypeId", "2" } }
                }
            };

            var getResult = lookupsController.GetLookups(lookupsController.Request);
            var getResponse = getResult.ExecuteAsync(CancellationToken.None).Result;

            List<Lookup> returnList = new List<Lookup>();
            Assert.IsTrue(getResponse.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.OK, getResponse.StatusCode);
            Assert.IsTrue(getResponse.TryGetContentValue(out returnList));  // Did we get a return result?

            Assert.AreEqual(2, returnList[0].LookupTypeId);
        }

        [TestMethod]
        public void GetLookups_ShouldReturnNullReturnOnTypeLookups()
        {
            var lookupsController = new LookupsController
            {
                Request = new HttpRequestMessage()
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } },
                    Headers = { { "userid", "admin" }, { "lookupTypeId", "0" } }
                }
            };

            var getResult = lookupsController.GetLookups(lookupsController.Request);
            var getResponse = getResult.ExecuteAsync(CancellationToken.None).Result;

            List<Lookup> returnList = new List<Lookup>();
            Assert.IsTrue(getResponse.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.OK, getResponse.StatusCode);
            Assert.IsTrue(getResponse.TryGetContentValue(out returnList));  // Did we get a return result?

            Assert.AreEqual(0, returnList.Count);
        }
        
        [TestMethod]
        public void GetLookups_ShouldReturnSingleLookup()
        {
            var lookupsController = new LookupsController
            {
                Request = new HttpRequestMessage()
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } },
                    Headers = {}
                }
            };

            var getResult = lookupsController.GetLookup(1);
            var getResponse = getResult.ExecuteAsync(CancellationToken.None).Result;

            Lookup returnLookup = new Lookup();
            Assert.IsTrue(getResponse.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.OK, getResponse.StatusCode);
            Assert.IsTrue(getResponse.TryGetContentValue(out returnLookup));  // Did we get a return result?

            Assert.AreEqual(1, returnLookup.Id);
        }

        [TestMethod]
        public void PostLookup_ShouldCreateSingleLookup()
        {
            Lookup newLookup = new Lookup()
            {
                LookupTypeId = 2,
                LookupValue = "TEST",
                CreatedBy = "TEST",
                Created = DateTime.Now,
                LastModifiedBy = "TEST",
                LastModified = DateTime.Now,
                Archived = false
            };

            var lookupsController = new LookupsController
            {
                Request = new HttpRequestMessage()
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } },
                    Headers = { { "userid", "admin" }, { "lookupTypeId", "2" } },
                    RequestUri = new Uri("http://localhost/api")
                },};
            _ = lookupsController.Configuration.Routes.MapHttpRoute(
                name: "GetLookupById",
                routeTemplate: "api/Lookups/{id}",
                defaults: new { id = RouteParameter.Optional });

            var postResult = lookupsController.PostLookup(newLookup);
            var postResponse = postResult.ExecuteAsync(CancellationToken.None).Result;

           // var getResult = lookupsController.GetLookups(lookupsController.Request);
           // var getResponse = getResult.ExecuteAsync(CancellationToken.None).Result;
            
            Lookup returnLookup = new Lookup();

            Assert.IsTrue(postResponse.IsSuccessStatusCode);
            Assert.IsTrue(postResponse.TryGetContentValue(out returnLookup));
            Assert.AreEqual(HttpStatusCode.Created, postResponse.StatusCode);

            var getResult = lookupsController.GetLookup(returnLookup.Id);
            var getResponse = getResult.ExecuteAsync(CancellationToken.None).Result;

            Assert.AreEqual("TEST", returnLookup.LookupValue);
            
            var deleteResult = lookupsController.DeleteLookup(returnLookup.Id);
            var deleteResponse = deleteResult.ExecuteAsync(CancellationToken.None).Result;

            Assert.IsTrue((deleteResponse.IsSuccessStatusCode));
            Assert.AreEqual(HttpStatusCode.OK, deleteResponse.StatusCode);

        }

        [TestMethod]
        public void PutLookup_ShouldUpdateSingleLookup()
        {

        }

        [TestMethod]
        public void PutLookup_ShouldArchiveSingleLookup()
        {

        }

        [TestMethod]
        public void DeleteLookup_ShouldDeleteSingleLookup()
        {

        }
    }
}
