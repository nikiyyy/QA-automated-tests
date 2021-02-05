using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace Github_API_Tests {
    public class Tests
    {
        const string GitHubAPIUsername = "";// Add your github username 
        const string GitHubAPIPass = "";// Add your Token 
     
        [SetUp]
        public void Setup(){}

        [Test]
        public void GitHubAPI_Test_GetIssuesByID()
        {
            var client = new RestClient("http://api.github.com/repos/testnakov/test-nakov-repo/issues/1");
            client.Timeout = 3000;
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(response.ContentType.StartsWith("application/json"));
            var issues = new JsonDeserializer().Deserialize<List<IssueResponse>>(response);

            Assert.Pass();

        }
        [Test]
        public void GitHubAPI_Test_GetLabelsForIssue()
        {
            var client = new RestClient("https://api.github.com/repos/testnakov/test-nakov-repo/issues/6/labels");
            client.Timeout = 3000;
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(response.ContentType.StartsWith("application/json"));
            var issues = new JsonDeserializer().Deserialize<List<IssueResponse>>(response);

            Assert.Pass();

        }

        [Test]
        public void GitHubAPI_Test_GetCommentsForIssue()
        {
            var client = new RestClient("https://api.github.com/repos/testnakov/test-nakov-repo/issues/6/comments");
            client.Timeout = 3000;
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(response.ContentType.StartsWith("application/json"));
            var issues = new JsonDeserializer().Deserialize<List<IssueResponse>>(response);

            Assert.Pass();

        }

        [Test]
        public void GitHubAPI_Test_GetIssuesByRepo()
        {
            var client = new RestClient("https://api.github.com/repos/testnakov/test-nakov-repo/issues");
            client.Timeout = 3000;
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(response.ContentType.StartsWith("application/json"));
            var issues = new JsonDeserializer().Deserialize<List<IssueResponse>>(response);

            Assert.Pass();
        }

        [Test]
        public void GitHubAPI_Test_CreateNewIssue()
        {
            var client = new RestClient("https://api.github.com/repos/testnakov/test-nakov-repo/issues");
            client.Timeout = 3000;
            var request = new RestRequest(Method.POST);
            client.Authenticator = new HttpBasicAuthenticator(GitHubAPIUsername, GitHubAPIPass);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new
            {
                title = "some title",
                body = "some body",
                labels = new string[] { "bug", "importance:high", "type:UI" }
            });
            var response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.IsTrue(response.ContentType.StartsWith("application/json"));
            var issue = new JsonDeserializer().Deserialize<IssueResponse>(response);

            Assert.IsTrue(issue.id > 0);
            Assert.IsTrue(issue.number > 0);
            Assert.IsTrue(!String.IsNullOrEmpty(issue.title));
            Assert.IsTrue(!String.IsNullOrEmpty(issue.body));
        }

        [Test]
        public void GitHubAPI_Test_CreateNewIssue_Unauthorized()
        {
            var client = new RestClient("https://api.github.com/repos/testnakov/test-nakov-repo/issues");
            client.Timeout = 3000;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new
            {
                title = "some title",
                body = "some body",
                labels = new string[] { "bug", "importance:high", "type:UI" }
            });
            var response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public void GitHubAPI_Test_DeleteComment()
        {
            var client = new RestClient("https://api.github.com/repos/testnakov/test-nakov-repo/issues/6/comments");
            client.Timeout = 3000;
            var request = new RestRequest(Method.POST);
            client.Authenticator = new HttpBasicAuthenticator(GitHubAPIUsername, GitHubAPIPass);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new{ body = "comment body" });
            var response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            var newComment = new JsonDeserializer().Deserialize<CommentResponse>(response);

            var clientDel = new RestClient("https://api.github.com/repos/testnakov/test-nakov-repo/issues/comments/" + newComment.id);
            clientDel.Timeout = 3000;
            var delRequest = new RestRequest(Method.DELETE);
            clientDel.Authenticator = new HttpBasicAuthenticator(GitHubAPIUsername, GitHubAPIPass);
            var delResponse = clientDel.Execute(delRequest);

            Assert.AreEqual(HttpStatusCode.NoContent, delResponse.StatusCode);
        }

        [Test]
        public void GitHubAPI_Test_CreateNewIssue_MissingTitle()
        {
            var client = new RestClient("https://api.github.com/repos/testnakov/test-nakov-repo/issues");
            client.Timeout = 3000;
            var request = new RestRequest(Method.POST);
            client.Authenticator = new HttpBasicAuthenticator(GitHubAPIUsername, GitHubAPIPass);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new
            {
                body = "some body",
                labels = new string[] { "bug", "importance:low", "type:UI" }
            });
            var response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.UnprocessableEntity, response.StatusCode);
        }
        [Test]
        public void GitHubAPI_Test_CreateNewCommentForIssue()
        {
            var client = new RestClient("https://api.github.com/repos/testnakov/test-nakov-repo/issues/6/comments");
            client.Timeout = 3000;
            var request = new RestRequest(Method.POST);
            client.Authenticator = new HttpBasicAuthenticator(GitHubAPIUsername, GitHubAPIPass);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new {body = "This is a comment" });

            var response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.IsTrue(response.ContentType.StartsWith("application/json"));
            var issue = new JsonDeserializer().Deserialize<IssueResponse>(response);

            Assert.IsTrue(!String.IsNullOrEmpty(issue.body));
        }

    }
}
