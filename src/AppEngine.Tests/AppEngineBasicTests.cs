using AB.AppEngine;
using AB.AppEngine.Exceptions;
using AppEngine.Base;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace AppEngine.Tests
{
    public class AppEngineBasicTests
    {
        private readonly ApplicationEngine _appEngine;

        public AppEngineBasicTests()
        {
            _appEngine = new ApplicationEngine();
        }

        [Fact]
        public void When_executing_request_that_has_no_corresponding_worker_it_returns_empty_response()
        {
            var request = new RequestTest();
            TestResponse response = null;
            Assert.DoesNotThrow(()=>response = _appEngine.Execute(request));
            response.Should().BeNull();
        }

        [Fact]
        public void When_executing_request_information_is_passed_to_request_sink()
        {
            var testRequest = new RequestTest();
            var testSink = Substitute.For<IRequestSink>();
            _appEngine.RegisterRequestSink(testSink);

            _appEngine.Execute(testRequest);

            testSink.Received().OnIncomingRequest(testRequest);
        }

        [Fact]
        public void Request_is_passed_to_worker_that_handles_given_request()
        {
            var request = new RequestTest();
            var testWorker = new TestWorker();
            _appEngine.RegisterWorker(testWorker);
            var response = _appEngine.Execute(request);

            response.Should().NotBeNull();
            response.Should().BeAssignableTo<TestResponse>();
            response.Id.Should().Be(1234);

        }

        [Fact]
        public void When_adding_another_worker_handling_same_request_response_it_should_throw_exception()
        {
            var testWorker1 = Substitute.For<IHandle<RequestTest, TestResponse>>();
            var testWorker2 = Substitute.For<IHandle<RequestTest, TestResponse>>();

            _appEngine.RegisterWorker(testWorker1);
            Assert.Throws<WorkerForGivenPairAlreadyExistException>(()=>_appEngine.RegisterWorker(testWorker2));
        }

        [Fact]
        public void When_adding_another_worker_handling_different_request_it_should_not_throw_exception()
        {
            var testWorker1 = Substitute.For<IHandle<RequestTest, TestResponse>>();
            var testWorker2 = Substitute.For<IHandle<RequestTest, OtherTestResponse>>();

            _appEngine.RegisterWorker(testWorker1);
            Assert.DoesNotThrow(() => _appEngine.RegisterWorker(testWorker2));

        }


        public class TestWorker : IHandle<RequestTest, TestResponse>
        {
            public TestResponse Process(IRequest<RequestTest, TestResponse> request)
            {
                return new TestResponse
                {
                    Id = 1234
                };
            }
        }

        public class RequestTest : IRequest<RequestTest, TestResponse>
        {
        }

        public class TestResponse
        {
            public int Id { get; set; }
        }

        public class OtherTestResponse
        {
        }
    }
}
