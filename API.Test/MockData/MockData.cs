using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Test.MockData
{
    public class MockData
    {

        public static List<AppUser> GetUsers()
        {
            return new List<AppUser>{
                new AppUser{
                    Id = 1,
                    UserName = "test1",
                    Created = DateTime.Now
                },
                new AppUser{
                    Id = 2,
                    UserName = "test1",
                    Created = DateTime.Now
                },
                new AppUser{
                    Id = 3,
                    UserName = "test1",
                    Created = DateTime.Now
                },
                new AppUser{
                    Id = 4,
                    UserName = "test1",
                    Created = DateTime.Now
                }

            };

        }

        public static AppUser GetUserForTest() {
            return new AppUser
            {
                Id = 1,
                UserName = "test1",
                Created = DateTime.Now
            };
        }

        public static AppUser GetUserWithBookTest()
        {
            return new AppUser
            {
                Id = 1,
                UserName = "test1",
                Created = DateTime.Now,
                Books = new List<Book>
                {
                    new Book{ Id = 1, Created  = DateTime.Now, Title = "title1", Description = "description1" ,Author="author1", LentOut = false }
                }
            };
        }

        public static List<Book> GetBooks()
        {
            return new List<Book>{
                new Book {
                    Id = 1,
                    Created = DateTime.Now,
                    Title = "title1",
                    Description = "description1",
                    Author = "author1",
                    LentOut = false

                },
                new Book {
                    Id = 2,
                    Created = DateTime.Now,
                    Title = "title2",
                    Description = "description2",
                    Author = "author2",
                    LentOut = false

                },                
                new Book {
                    Id = 3,
                    Created = DateTime.Now,
                    Title = "title3",
                    Description = "description3",
                    Author = "author3",
                    LentOut = false

                },
                new Book {
                    Id = 4,
                    Created = DateTime.Now,
                    Title = "title4",
                    Description = "description4",
                    Author = "author4",
                    LentOut = true,
                    LendFrom = new DateTime(2022, 04, 18),
                    LendTo = new DateTime(2022, 04, 30),

                }

            };
            
        }

        public static Book GetBook()
        {
            return new Book
            {
                Id = 4,
                Created = DateTime.Now,
                Title = "title4",
                Description = "description4",
                Author = "author4",
                LentOut = true,
                LendFrom = new DateTime(2022, 04, 18),
                LendTo = new DateTime(2022, 04, 30),

            };

        }

        public static Mock<UserManager<AppUser>> MockUserManager<AppUser>(List<AppUser> ls) where AppUser : class
        {
            var store = new Mock<IUserStore<AppUser>>();
            var mgr = new Mock<UserManager<AppUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<AppUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<AppUser>());

            mgr.Setup(x => x.DeleteAsync(It.IsAny<AppUser>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<AppUser, string>((x, y) => ls.Add(x));
            mgr.Setup(x => x.UpdateAsync(It.IsAny<AppUser>())).ReturnsAsync(IdentityResult.Success);

            return mgr;
        }

    }
}