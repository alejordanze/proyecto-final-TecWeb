﻿using System;
namespace RadioTaxisAPI.Exceptions
{
    public class InternalServerException : Exception
    {
        public InternalServerException(string message)
        :base(message)
        {
        }
    }
}
