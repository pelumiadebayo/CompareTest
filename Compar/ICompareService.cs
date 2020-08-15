using Compar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Compar
{
    public interface ICompareService
    {
           Task<GenericResponse<Login>> AdminLogin(Login Input);
        Task<GenericResponse<Compare>> CompareInput(Compare Input);

    }
}
