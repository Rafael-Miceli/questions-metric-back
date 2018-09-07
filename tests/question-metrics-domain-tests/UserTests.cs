using System;
using System.Threading.Tasks;
using question_metrics_domain;
using Xunit;

namespace question_metrics_domain_tests
{    
    public class UserTests
    {
        [Fact]
        public async Task Given_A_User_When_Creating_A_new_One_Then_Should_Hash_Its_Password()
        {
            var user = new User(
                "Rafael",
                "123",
                "rafael.miceli@hotmail.com",
                new DateTime(1989, 12, 07)
            );

            Assert.NotEqual("123", user.Password);
            Assert.Equal("/Td/DjwLDlpGFI/IKqeA4lhKRjvf3iotr3jnQ5AnuLs=", user.Password);
        }
        
    }
}