
using AutoMapper;
using API.Interfaces;
using API.Controllers;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Moq;
using API.Entities;
using API.DTO;
using API.Helpers;

namespace API.Test.Controllers
{
    public class UsersControllerTest
    {

        UsersController _controller;
        IMapper _mapper;

        public UsersControllerTest() 
        {


            var tmp = MockData.MockData.GetUsers();

            var mockMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMap());
            });

            _mapper = mockMapperConfig.CreateMapper();
            
        }

        [Fact]
        public async System.Threading.Tasks.Task GetUsers_ReturnsOkResultAsync()
        {
            var userRepositoryService = new Mock<IUserRepository>();
            userRepositoryService.Setup(mock => mock.GetUsersAsync()).ReturnsAsync(MockData.MockData.GetUsers());

            _controller = new UsersController(userRepositoryService.Object, _mapper);
            // Act
            var okResult =  await _controller.GetUsers();


            Assert.IsType<OkObjectResult>(okResult.Result);

        }

        [Fact]
        public async System.Threading.Tasks.Task GetUserById_ReturnsOkResultAsync()
        {
            AppUser tmp = MockData.MockData.GetUserForTest();
            var userRepositoryService = new Mock<IUserRepository>();
            userRepositoryService.Setup(mock => mock.GetUserByIdAsync(1)).ReturnsAsync(tmp);

            _controller = new UsersController(userRepositoryService.Object, _mapper);
            // Act
            var okResult = await _controller.GetUserById(1);

            Assert.IsType<UserDto>(okResult.Value);
            Assert.Equal(okResult.Value.Id, tmp.Id);
        }
        

        [Fact]
        public async System.Threading.Tasks.Task GetUserByIUsername_ReturnsOkResultAsync()
        {
            AppUser tmp = MockData.MockData.GetUserForTest();
            var userRepositoryService = new Mock<IUserRepository>();
            userRepositoryService.Setup(mock => mock.GetUserByUsernameAsync("test1")).ReturnsAsync(tmp);

            _controller = new UsersController(userRepositoryService.Object, _mapper);
            // Act
            var okResult = await _controller.GetUserByUsername("test1");

            Assert.IsType<UserDto>(okResult.Value);
            Assert.Equal(okResult.Value.UserName, tmp.UserName);
        }

        [Fact]
        public async System.Threading.Tasks.Task UserGetsBooks()
        {
            AppUser tmp = MockData.MockData.GetUserWithBookTest();
            var userRepositoryService = new Mock<IUserRepository>();
            userRepositoryService.Setup(mock => mock.GetUserByUsernameAsync("test1")).ReturnsAsync(tmp);

            _controller = new UsersController(userRepositoryService.Object, _mapper);
            // Act
            var okResult = await _controller.GetUserByUsername("test1");

            Assert.IsType<UserDto>(okResult.Value);
            Assert.Equal(okResult.Value.UserName, tmp.UserName);
        }

    }



}