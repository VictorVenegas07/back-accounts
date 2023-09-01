using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Exceptions
{
    public class ValidationExceptions: Exception
    {
        public ValidationExceptions():base("Se han producido uno o más errores de validación")
        {
            Errors = new List<string>();
        }
        public ValidationExceptions(IEnumerable<ValidationFailure> failures):this()
        {

            foreach (var failure in failures)
            {
                Errors.Add(failure.ErrorMessage);
            }
        }
        public List<string> Errors { get;}
    }
}
