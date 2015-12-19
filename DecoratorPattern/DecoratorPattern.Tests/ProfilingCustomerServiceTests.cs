using System;

using DecoratorPattern.Client;
using DecoratorPattern.Profiling;

using Xunit;
using Moq;

namespace DecoratorPattern.Tests
{
    public class ProfilingCustomerServiceTests
    {
        [Fact]
        public void ShouldBeCustomerService()
        {
            var sut = new ProfilingCustomerService(Mock.Of<ICustomerService>(), Logger);

            Assert.IsAssignableFrom<ICustomerService>(sut);
        }

        [Fact]
        public void ShouldRequireCustomerService()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new ProfilingCustomerService(null, Logger));

            Assert.NotNull(exception.ParamName);
        }

        [Fact]
        public void ShouldRequireLogger()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new ProfilingCustomerService(Mock.Of<ICustomerService>(), null));

            Assert.NotNull(exception.ParamName);
        }

        [Fact]
        public void ShouldDelegateToCustomerService()
        {
            var mockCustomerService = new Mock<ICustomerService>();
            var sut = new ProfilingCustomerService(mockCustomerService.Object, Logger);

            sut.GetCustomerNames();

            mockCustomerService.Verify(customerService => customerService.GetCustomerNames());
        }

        [Fact]
        public void ShouldLogProfilingTime()
        {
            var sut = new ProfilingCustomerService(Mock.Of<ICustomerService>(), Logger);

            sut.GetCustomerNames();

            Assert.Matches(@"(\d)+\.(\d)+ms", _loggedMessage);
        }

        public void Logger(string message)
        {
            _loggedMessage = message;
        }

        private string _loggedMessage;
    }
}
