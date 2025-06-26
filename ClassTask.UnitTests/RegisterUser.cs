using System.Linq.Expressions;
using ClassTask.Dtos;
using ClassTask.Models;
using ClassTask.Repositories.Interfaces;
using ClassTask.Services.Implementations;
using ClassTask.Services.Interfaces;
using ClassTask.UnitOfWork.Interface;
using Moq;

namespace ClassTask.UnitTests;

public class RegisterUser
{
    private readonly Mock<IMediaUserRepository> _mediaUserRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly IMediaUserService _mediaUserService;

    public RegisterUser()
    {
        _mediaUserRepositoryMock = new Mock<IMediaUserRepository>();
        _unitOfWork = new Mock<IUnitOfWork>();
        _mediaUserService = new MediaUserService(_mediaUserRepositoryMock.Object, _unitOfWork.Object);
    }
    [Fact]
    public async Task Handle_Register_User_Fails_When_Email_Already_Exists()
    {
        //Arrange
        var request = new SignUpRequestDto
        {
            Email = "fggffg",
            PhoneNumber = "fddff",
            LastName = "hhj",
            FirstName = "hhh",
            UserName = "",
            DateOfBirth = DateOnly.FromDayNumber(0),
        };
        _mediaUserRepositoryMock.Setup(x => x.CheckAsync(It.IsAny<Expression<Func<MediaUser, bool>>>())).ReturnsAsync(true);

        //Act
        var response = await _mediaUserService.RegisterUser(request);

        //Assert

        Assert.NotNull(response);
        Assert.False(response.IsSuccess);
        Assert.NotEmpty(response.Message);
        Assert.Equal("Email is associated with another account", response.Message);
        _unitOfWork.Verify(x => x.SaveChangesAsync(), Times.Never);

    }

    [Fact]
    public async Task Handle_Register_User_Sucessful_With_Valid_Request()
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
        _mediaUserRepositoryMock.Setup(x => x.CheckAsync(It.IsAny<Expression<Func<MediaUser, bool>>>())).ReturnsAsync(false);
        _mediaUserRepositoryMock.Setup(x => x.AddAsync(It.IsAny<MediaUser>()));
        _unitOfWork.Setup(x => x.SaveChangesAsync());
        //Act
        var response = await _mediaUserService.RegisterUser(request);

        //Assert

        Assert.NotNull(response);
        Assert.True(response.IsSuccess);
        Assert.NotEmpty(response.Message);
        Assert.Equal("Register Successfully", response.Message);
        Assert.NotNull(response.Data);
        _mediaUserRepositoryMock.Verify(x => x.CheckAsync(It.IsAny<Expression<Func<MediaUser, bool>>>()), Times.AtMost(2));
        _unitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
    }
}
