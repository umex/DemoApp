
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
    public class BookControllerTest
    {
        BookController _controller;
        IMapper _mapper;
        public BookControllerTest()
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
            //TODO

        }

        public async System.Threading.Tasks.Task UserGetsBooks()
        {
            //TODO
        }
    }
}
