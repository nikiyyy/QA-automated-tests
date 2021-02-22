using NUnit.Framework;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace ContactBook_RESTful_API
{

    public class Tests
    {
        const string baseURL = "http://localhost:8080/api";
        [SetUp]
        public void Setup()
        {
        }
        
        [Test]
        public void listContactsAndAssertFirstContact()
        {
            var client = new RestClient(baseURL);
            client.Timeout = 3000;
            var request = new RestRequest("/contacts", Method.GET);
            var response = client.Execute(request);

            
            var urls = new JsonDeserializer().Deserialize<List<Response>>(response);

            var expUrl = new Response
            {
                id = 1,
                firstName = "Steve",
                lastName = "Jobs",
                email = "steve@apple.com",
                phone = "+1 800 123 456",
                dateCreated = "2021-02-17T12:41:33.000Z",
                comments = "Steven Jobs was an American business magnate, industrial designer, investor, and media proprietor."
            };
            var expUrlJson = new JsonDeserializer().Serialize(expUrl);
            var UrlJson = new JsonDeserializer().Serialize(urls[0]);
            //Assert.IsTrue(response.ContentType.StartsWith("application/json"));
            Assert.AreEqual(UrlJson, expUrlJson);
        }

        [Test]
        public void FindContactsByKeyword()
        {
            var client = new RestClient(baseURL);
            client.Timeout = 3000;
            var request = new RestRequest("/contacts/search/albert", Method.GET);
            var response = client.Execute(request);


            var urls = new JsonDeserializer().Deserialize<List<Response>>(response);

            var expUrl = new Response
            {
                id = 3,
                firstName = "Albert",
                lastName = "Einstein",
                email = "albert.e@uzh.ch",
                phone = "+41 44 634 49 01",
                dateCreated = "2021-02-23T10:03:08.000Z",
                comments = "Albert Einstein was a German-born theoretical physicist, universally acknowledged to be one of the two greatest physicists of all time, the other being Isaac Newton."
            };
            var expUrlJson = new JsonDeserializer().Serialize(expUrl);
            var UrlJson = new JsonDeserializer().Serialize(urls[0]);

            Assert.AreEqual(UrlJson, expUrlJson);
        }
        [Test]
        public void FindContactsByKeyword_Invalid()
        {
            var client = new RestClient(baseURL);
            client.Timeout = 3000;
            Random rnd = new Random();
            int missingRand = rnd.Next(10000000);
            var request = new RestRequest("/contacts/search/missing" + missingRand, Method.GET);
            var response = client.Execute(request);

            Assert.AreEqual(response.Content, "[]");
        }
        [Test]
        public void createNewContact_Invalid()
        {
            var client = new RestClient(baseURL);
            client.Timeout = 3000;
            var request = new RestRequest("/contacts/", Method.POST);
            
            var Newdata = new Response
            {
             firstName = "not gonna pass",
            };

            request.AddJsonBody(Newdata);
            var response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public void createNewContact()
        {
            var client = new RestClient(baseURL);
            client.Timeout = 3000;
            var request = new RestRequest("/contacts/", Method.POST);

            var Newdata = new Response
            {
                firstName = "Marie",
                lastName = "Curie",
                email = "marie67@gmail.com",
                phone = "+1 800 200 300",
                comments = "Old friend"
            };
            
            request.AddJsonBody(Newdata);
            var response = client.Execute(request);
            //assert that user was created
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            var responseURL = new JsonDeserializer().Deserialize<CreateUrlResp>(response);
            Assert.AreEqual("Contact added.", responseURL.msg);
        }
    }
}