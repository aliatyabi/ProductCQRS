using FluentValidation;
using ProductCQRS.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProductCQRS.Application.Products.Commands
{
	public class CreateProductValidator : AbstractValidator<CreateProductRequest>
	{
        public CreateProductValidator()
        {
			RuleFor(x => x.Name).NotEmpty().WithMessage(Messages.RequiredError);

			RuleFor(x => x.ManufactureEmail).NotEmpty().WithMessage(Messages.RequiredError);
			RuleFor(x => x.ManufactureEmail).Must(ValidateEmail).WithMessage(Messages.EmailInvalidError);
		}

		private bool ValidateEmail(string email)
		{
			if (string.IsNullOrWhiteSpace(email))
			{
				return true;
			}

			string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

			var regex = new Regex(pattern, RegexOptions.IgnoreCase);

			return regex.IsMatch(email);
		}
	}
}
