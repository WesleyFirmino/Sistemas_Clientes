using CaseTempus.Data;
using CaseTempus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseTempus.Validations
{
    public class ReportValidate
    {
        private readonly DataContext _dataContext;

        public ReportValidate()
        {

        }

        public IList<UserViewModel> Average()
        {
            IList<UserViewModel> income = _dataContext.Users.Where(x => x.FamilyIncome == x.FamilyIncome).ToList();

            return income;
        }
    }
}
