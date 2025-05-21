using Events_Web_Application.src.Application.Events.DTOs;
using Events_Web_Application.src.Domain.Entities;
using FluentValidation;

namespace Events_Web_Application.src.Application.Events.Validators
{
    public class CreateUserValidator : AbstractValidator<UserDTO>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Имя пользователя обязательно")
                .MaximumLength(50).WithMessage("Имя не может превышать 50 символов");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Фамилия пользователя обязательна")
                .MaximumLength(50).WithMessage("Фамилия не может превышать 50 символов");

            RuleFor(x => x.BirthdayDate)
                .NotEmpty().WithMessage("Дата рождения обязательна")
                .LessThan(DateOnly.FromDateTime(DateTime.UtcNow)).WithMessage("Дата рождения должна быть в прошлом")
                .GreaterThan(DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-120))).WithMessage("Дата рождения не должна быть старше 120 лет");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email обязателен")
                .EmailAddress().WithMessage("Некорректный формат email");
        }
    }
}
