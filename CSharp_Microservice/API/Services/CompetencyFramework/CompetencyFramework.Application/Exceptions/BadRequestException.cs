using System;

namespace CompetencyFramework.Application.Exceptions
{
    public class BadRequestException : ApplicationException
    {
        public BadRequestException()
            : base($"Bad Request.")
        {
        }
    }
}
