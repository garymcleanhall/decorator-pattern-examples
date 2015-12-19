using System;

using DecoratorPattern.Client;
using DecoratorPattern.Logging;

using Xunit;
using Moq;

namespace DecoratorPattern.Tests
{     
    public class LoggingCustomerServiceTests
    {
        [Fact]
        public void ShouldBeCustomerService()
        {
            var sut = new LoggingCustomerService(Mock.Of<ICustomerService>(), Logger);

            Assert.IsAssignableFrom<ICustomerService>(sut);
        }

        [Fact]
        public void ShouldRequireCustomerService()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new LoggingCustomerService(null, Logger));

            Assert.NotNull(exception.ParamName);
        }

        [Fact]
        public void ShouldRequireLogger()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new LoggingCustomerService(Mock.Of<ICustomerService>(), null));

            Assert.NotNull(exception.ParamName);
        }

        [Fact]
        public void ShouldDelegateToCustomerService()
        {
            var mockCustomerService = new Mock<ICustomerService>();
            var sut = new LoggingCustomerService(mockCustomerService.Object, Logger);

            sut.GetCustomerNames();

            mockCustomerService.Verify(customerService => customerService.GetCustomerNames());
        }

        [Fact]
        public void ShouldLogMessage()
        {
            var sut = new LoggingCustomerService(Mock.Of<ICustomerService>(), Logger);

            sut.GetCustomerNames();

            Assert.NotNull(_loggedMessage);
            Assert.NotEmpty(_loggedMessage);
        }

        public void Logger(string message)
        {
            _loggedMessage = message;
        }

        private string _loggedMessage;
    }
}
