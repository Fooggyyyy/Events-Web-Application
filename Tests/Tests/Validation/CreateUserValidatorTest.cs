using Events_Web_Application.src.Application.Events.DTOs;
using Events_Web_Application.src.Application.Events.Validators;
using Events_Web_Application.src.Domain.Entities;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Tests.Validator
{
    public class CreateUserValidatorTest
    {
        private readonly CreateUserValidator _validator;

        public CreateUserValidatorTest()
        {
            _validator = new CreateUserValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            var model = new UserDTO { Name = "" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(u => u.Name);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Too_Long()
        {
            var model = new UserDTO { Name = new string('A', 51) };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(u => u.Name);
        }

        [Fact]
        public void Should_Have_Error_When_Surname_Is_Empty()
        {
            var model = new UserDTO { Surname = "" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(u => u.Surname);
        }

        [Fact]
        public void Should_Have_Error_When_Surname_Too_Long()
        {
            var model = new UserDTO { Surname = new string('S', 51) };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(u => u.Surname);
        }

        [Fact]
        public void Should_Have_Error_When_Birthday_Is_In_Future()
        {
            var model = new UserDTO { BirthdayDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1)) };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(u => u.BirthdayDate);
        }

        [Fact]
        public void Should_Have_Error_When_Birthday_Too_Old()
        {
            var model = new UserDTO { BirthdayDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-130)) };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(u => u.BirthdayDate);
        }

        [Fact]
        public void Should_Have_Error_When_Email_Is_Empty()
        {
            var model = new UserDTO { Email = "" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(u => u.Email);
        }

        [Fact]
        public void Should_Have_Error_When_Email_Invalid()
        {
            var model = new UserDTO { Email = "invalid-email" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(u => u.Email);
        }

        [Fact]
        public void Should_Not_Have_Errors_For_Valid_User()
        {
            var model = new UserDTO
            {
                Id = 1,
                Name = "John",
                Surname = "Doe",
                BirthdayDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-30)),
                Email = "john.doe@example.com"
            };

            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
