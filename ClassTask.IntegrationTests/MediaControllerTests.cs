using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClassTask.Dtos;
using PaymentGateway.Tests.Integration.Shared;

namespace ClassTask.IntegrationTests
{
    public class MediaControllerTests : IntegrationTestBase, IClassFixture<IntegrationTestFactory>
    {
        private readonly IntegrationTestFactory _factory;
        public MediaControllerTests(IntegrationTestFactory factory) : base(factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Should_Create_Media_Record_When_Request_Is_Valid()
        {
            //Arrange

            var request = new SignUpRequestDto
            {
                Email = "fggffg",
                PhoneNumber = "fddff",
                LastName = "hhj",
                FirstName = "hhh",
                UserName = "fygghgh",
                DateOfBirth = DateOnly.FromDayNumber(0),
            };

            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            //Act

            var response = await AnonymousClient.PostAsync("api/mediauser", content);
            var result = await response.Content.ReadFromJsonAsync<ResponseDto<string?>>(_jsonSerializerOptions);
            //Assert
            await Verify(result, GetVerifySettings());
        }

    }
}