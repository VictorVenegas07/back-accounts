using FluentValidation;
using Aplication.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Behaviours
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            this.validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (validators.Any())
            {
                //Creamos un nuevo contexto con la Respuesta
                var context = new FluentValidation.ValidationContext<TRequest>(request);
                var resultadoValidacion = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var fallas = resultadoValidacion.SelectMany(e => e.Errors).Where(f=> f != null).ToList();

                if (fallas.Count() > 0)
                    throw new ValidationExceptions(fallas);
            }
            return await next();
        }
    }
}
