using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForagerTier1.Models;

namespace ForagerTier1.Persistance
{
    interface ICompanyService
    {
        Company CreateCompany(Company newCompany);
    }
}
