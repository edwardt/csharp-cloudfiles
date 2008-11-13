using System;
using System.Net;
using com.mosso.cloudfiles.domain;
using com.mosso.cloudfiles.domain.request;
using com.mosso.cloudfiles.domain.response;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace com.mosso.cloudfiles.integration.tests.domain.RetrieveContainerRequestSpecs
{
    [TestFixture]
    public class When_requesting_a_list_of_containers : TestBase
    {
        [Test]
        public void Should_return_OK_status()
        {
            GetContainers request = new GetContainers(storageUrl, storageToken);
            request.UserAgent = "NASTTestUserAgent";

            GetContainersResponse response = new ResponseFactoryWithContentBody<GetContainersResponse>().Create(new CloudFilesRequest(request));

            Assert.That(response.Status, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.ContentBody, Is.Not.Null);
            response.Dispose();
        }

        [Test]
        public void Should_return_the_list_of_containers()
        {
            Console.WriteLine("Begin listing containers");
            GetContainers request = new GetContainers(storageUrl, storageToken);

            IResponseWithContentBody response = new ResponseFactoryWithContentBody<GetContainersResponse>().Create(new CloudFilesRequest(request));
            Assert.That(response.ContentBody.Count, Is.GreaterThan(0));
//            foreach (string s in response.ContentBody)
//                Console.WriteLine(s);
            Console.WriteLine("End of listing containers");
            response.Dispose();
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void Should_throw_an_exception_when_the_storage_url_is_null()
        {
            GetContainers request = new GetContainers(null, "a");
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void Should_throw_an_exception_when_the_storage_token_is_null()
        {
            GetContainers request = new GetContainers("a", null);
        }
    }
}