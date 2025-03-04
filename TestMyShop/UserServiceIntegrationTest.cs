using System;
using System.Threading.Tasks;
using Entity;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Services;
using Tests;
using Xunit;
using Zxcvbn;

namespace TestProject
{
    public class UserServiceIntegrationTest : IClassFixture<DatabaseFixure>
    {
        private readonly ShopContext _context;
        private readonly UserService _service;

        public UserServiceIntegrationTest(DatabaseFixure fixture)
        {
            _context = fixture.Context;
            _service = new UserService(new UserRepository(_context));
        }
        [Fact]
        public async Task GetById_ShouldReturnUser_WhenUserExists()
        {
            // Arrange: Create and save a user
            var user = new User
            {
                Email = "getbyid@example.com",
                Password = "StrongPassword!123",
                FirstName = "John",
                LastName = "Doe"
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Act: Retrieve the user by ID
            var retrievedUser = await _service.GetById(user.UserId);

            // Assert: Check that the retrieved user matches the saved user
            Assert.NotNull(retrievedUser);
            Assert.Equal(user.UserId, retrievedUser.UserId);
            Assert.Equal(user.Email, retrievedUser.Email);
            Assert.Equal(user.FirstName, retrievedUser.FirstName);
            Assert.Equal(user.LastName, retrievedUser.LastName);
        }

        [Fact]
        public async Task Post_ShouldSaveUser_WithStrongPassword()
        {
            // Arrange: Create a user with a strong password
            var user = new User
            {
                Email = "strong@example.com",
                Password = "StrongPassword!123",
                FirstName = "John",
                LastName = "Doe"
            };

            // Act: Send the user to the function being tested
            var savedUser = await _service.Post(user);

            // Assert: Check data validity
            Assert.NotNull(savedUser);
            Assert.Equal("strong@example.com", savedUser.Email);
            Assert.Equal("John", savedUser.FirstName);
            Assert.Equal("Doe", savedUser.LastName);
        }

        [Fact]
        public async Task Post_ShouldNotSaveUser_WithWeakPassword()
        {
            // Arrange: Create a user with a weak password
            var user = new User
            {
                Email = "weak@example.com",
                Password = "weak",
                FirstName = "Jane",
                LastName = "Doe"
            };

            // Act: Send the user to the function being tested
            var savedUser = await _service.Post(user);

            // Assert: Check that the user was not saved
            Assert.Null(savedUser);
        }

    [Fact]
        public async Task Put_ShouldNotUpdateUser_WithWeakPassword()
        {
            // Arrange: Create and save a user with an initial password
            var user = new User
            {
                Email = "u@example.com",
                Password = "gvytf6SR",
                FirstName = "Jane",
                LastName = "Doe"
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Attempt to update the user's password to a weaker one
            var updatedUser = new User
            {
                UserId = user.UserId,
                Email = user.Email,
                Password = "123",
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            // Act: Send the updated user to the function being tested
            var result = await _service.Put(user.UserId, updatedUser);

            // Assert: Check that the user was not updated
            Assert.Null(result);

            // Verify that the original password remains unchanged in the database
            var originalUser = await _context.Users.FindAsync(user.UserId);
            Assert.Equal("gvytf6SR", originalUser.Password);
        }
    }
}
