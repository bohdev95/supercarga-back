using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public List<string> ValdationErrors { get; set; } = new List<string>();

        public ValidationException(ValidationResult validationResult)
        {
            foreach (var validationError in validationResult.Errors)
            {
                ValdationErrors.Add(validationError.ErrorMessage);
            }
        }

        public ValidationException(string validationError)
        {
            ValdationErrors.Add(validationError);
        }

        public ValidationException(List<string> validationErrors)
        {
            ValdationErrors = validationErrors;
        }
    }
}
