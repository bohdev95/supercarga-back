using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Validation
{
    public static class ValidationMessage
    {
        public static string NotEmpty(string propName) => $"'{propName}' can not be empty.";
        public static string NotExist(string entityName) => $"'{entityName}' not exist.";
        public static string IsNotActive(string entityName) => $"'{entityName}' is not active.";
        public static string InvalidState(string entityName) => $"'{entityName}' invalid state.";
        public static string AlreadyExist(string entityName) => $"'{entityName}' alredy exist.";
        public static string GreaterThan(string propName, int value) => $"'{propName}' nust be grater than {value}.";
        public static string GreaterThan(string propName1, string propName2) => $"'{propName1}' nust be grater than '{propName2}'.";
        public static string NotCorrectFormat(string propName) => $"'{propName}' is in not correct format.";
        public static string NotMatch(string propName1, string propName2) => $"'{propName1}' not match '{propName2}'.";
        public static string NotFutureOrToday(string propName) => $"'{propName}' must be future or today date.";

    }
}
