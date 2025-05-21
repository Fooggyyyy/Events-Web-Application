using Events_Web_Application.src.Application.Events.DTOs;
using Events_Web_Application.src.Domain.Entities;
using FluentValidation;

namespace Events_Web_Application.src.Application.Events.Validators
{
    public class CreateEventValidator : AbstractValidator<EventDTO>
    {
        public CreateEventValidator()
        {
            RuleFor(x => x.Id)
            .NotEmpty().WithMessage("ID не может быть пустым");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Название события обязательно")
                .MaximumLength(100).WithMessage("Название не может превышать 100 символов");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Описание события обязательно")
                .MaximumLength(500).WithMessage("Описание не может превышать 500 символов");

            RuleFor(x => x.Date)
                .GreaterThanOrEqualTo(DateTime.UtcNow).WithMessage("Дата события должна быть в будущем");

            RuleFor(x => x.Place)
                .NotEmpty().WithMessage("Место проведения обязательно")
                .MaximumLength(200).WithMessage("Место не может превышать 200 символов");

            RuleFor(x => x.Category)
                .IsInEnum().WithMessage("Некорректная категория события");

            RuleFor(x => x.MaxUser)
                .GreaterThan(0).WithMessage("Максимальное количество участников должно быть больше 0")
                .LessThanOrEqualTo(10000).WithMessage("Максимальное количество участников не может превышать 10 000");

            RuleFor(x => x.PhotoPath)
                .NotNull().WithMessage("Должен быть хотя бы один путь к фото");
        }
    }
}
