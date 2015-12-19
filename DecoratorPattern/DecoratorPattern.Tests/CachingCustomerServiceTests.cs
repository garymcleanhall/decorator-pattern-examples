using System;

using DecoratorPattern.Client;
using DecoratorPattern.Caching;

using Xunit;
using Moq;

namespace DecoratorPattern.Tests
{
    public class CachingCustomerServiceTests
    {     
        [Fact]
        public void ShouldBeCustomerService()
        {
            var sut = new CachingCustomerService(Mock.Of<ICustomerService>());

            Assert.IsAssignableFrom<ICustomerService>(sut);
        }

        [Fact]
        public void ShouldRequireCustomerService()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new CachingCustomerService(null));

            Assert.NotNull(exception.ParamName);
        }

        [Fact]
        public void ShouldDelegateToCustomerServiceWhenNotCached()
        {
            var mockCustomerService = new Mock<ICustomerService>();
            var sut = new CachingCustomerService(mockCustomerService.Object);

            sut.GetCustomerNames();

            mockCustomerService.Verify(customerService => customerService.GetCustomerNames());
        }

        [Fact]
        public void ShouldNotDelegateToCustomerServiceWhenCached()
        {
            var mockCustomerService = new Mock<ICustomerService>();
            var sut = new CachingCustomerService(mockCustomerService.Object);
            sut.GetCustomerNames();

            sut.GetCustomerNames();

            mockCustomerService.Verify(customerService => customerService.GetCustomerNames(), Times.Once());
        }
    }
}
