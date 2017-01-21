using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public enum CreateResults
    {
        Ok,
        Existing,
        Failed,
        BadRequest
    }
}
