using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025TPI.Domain.Exceptions;

public class DuplicatedEntityException : Exception
{
    public DuplicatedEntityException() : base() { }

    public DuplicatedEntityException(string message) : base(message) { }

    public DuplicatedEntityException(string message, Exception innerException)
        : base(message, innerException) { }
}

