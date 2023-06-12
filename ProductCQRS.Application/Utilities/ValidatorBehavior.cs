using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCQRS.Application.Utilities
{
	public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
	{
		public ValidatorBehavior(IEnumerable<IValidator<TRequest>> validators)
		{
			Validators = validators;
		}

		protected IEnumerable<IValidator<TRequest>> Validators { get; }

		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			if (Validators.Any())
			{
				var context = new ValidationContext<TRequest>(request);

				var validationResults = await Task.WhenAll(Validators.Select(v => v.ValidateAsync(context, cancellationToken)));

				var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

				if (failures.Count != 0)
				{
					throw new ValidationException(failures);
				}
			}

			return await next();
		}
	}
}
