﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Exceptions
{
    public class BadRequestException : ApplicationException
    {
        public BadRequestException()
           : base($"Bad Request.")
        {
        }
    }
}
