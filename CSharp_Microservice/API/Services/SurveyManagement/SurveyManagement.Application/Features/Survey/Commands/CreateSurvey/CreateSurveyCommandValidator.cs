using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyManagement.Application.Features.Survey.Commands.CreateSurvey
{
    public class CreateSurveyCommandValidator : AbstractValidator<CreateSurveyCommand>
    {
        public CreateSurveyCommandValidator()
        {

            RuleFor(s => s.ToPeriod).GreaterThan(a => a.FromPeriod)
                .WithMessage("To Period must be greater than From Period");



        }
    }
}
