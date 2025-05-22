using Events_Web_Application.src.Application.Events.DTOs;
using Events_Web_Application.src.Application.Events.Validators;
using Events_Web_Application.src.Domain.Entities;
using Events_Web_Application.src.Domain.Enums;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Tests.Validator
{
    public class CreateEventValidatorTest
    {
        private readonly CreateEventValidator _validator;

        public CreateEventValidatorTest()
        {
            _validator = new CreateEventValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Id_Is_Empty()
        {
            var model = new EventDTO { Id = 0 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(e => e.Id);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            var model = new EventDTO { Name = "" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(e => e.Name);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Too_Long()
        {
            var model = new EventDTO { Name = new string('A', 101) };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(e => e.Name);
        }

        [Fact]
        public void Should_Have_Error_When_Description_Is_Empty()
        {
            var model = new EventDTO { Description = "" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(e => e.Description);
        }

        [Fact]
        public void Should_Have_Error_When_Description_Too_Long()
        {
            var model = new EventDTO { Description = new string('B', 501) };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(e => e.Description);
        }

        [Fact]
        public void Should_Have_Error_When_Date_Is_In_Past()
        {
            var model = new EventDTO { Date = DateTime.UtcNow.AddDays(-1) };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(e => e.Date);
        }

        [Fact]
        public void Should_Have_Error_When_Place_Is_Empty()
        {
            var model = new EventDTO { Place = "" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(e => e.Place);
        }

        [Fact]
        public void Should_Have_Error_When_Place_Too_Long()
        {
            var model = new EventDTO { Place = new string('P', 201) };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(e => e.Place);
        }

        [Fact]
        public void Should_Have_Error_When_Category_Is_Invalid()
        {
            var model = new EventDTO { Category = (Category)999 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(e => e.Category);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void Should_Have_Error_When_MaxUser_Is_Zero_Or_Negative(int invalid)
        {
            var model = new EventDTO { MaxUser = invalid };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(e => e.MaxUser);
        }

        [Fact]
        public void Should_Have_Error_When_MaxUser_Exceeds_Limit()
        {
            var model = new EventDTO { MaxUser = 10001 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(e => e.MaxUser);
        }

        [Fact]
        public void Should_Have_Error_When_PhotoPath_Is_Null()
        {
            var model = new EventDTO { PhotoPath = null };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(e => e.PhotoPath);
        }

        [Fact]
        public void Should_Not_Have_Errors_For_Valid_Event()
        {
            var model = new EventDTO
            {
                Id = 1,
                Name = "Valid Event",
                Description = "This is a valid description",
                Date = DateTime.UtcNow.AddDays(10),
                Place = "Valid Place",
                Category = Category.Conferences,
                MaxUser = 100,
                PhotoPath = new List<string> { "photo1.jpg" }
            };

            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
