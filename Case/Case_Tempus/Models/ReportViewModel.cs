using System;
using System.Collections.Generic;
using System.Linq;

namespace CaseTempus.Models
{
    public class ReportViewModel
    {
        public ReportViewModel(IEnumerable<UserViewModel> usersViewModel, string period = "")
        {
            if (!usersViewModel.Any())
            {
                return;
            }

            IEnumerable<UserViewModel> usersByPeriod = UsersByPeriod(usersViewModel, period);

            SetClassA(usersByPeriod);
            SetClassB(usersByPeriod);
            SetClassC(usersByPeriod);
            SetGreaterThanEighteen(usersByPeriod);
        }

        public int ClassA { get; private set; }
        public int ClassB { get; private set; }
        public int ClassC { get; private set; }
        public int GreaterThanEighteen { get; private set; }

        private static IEnumerable<UserViewModel> UsersByPeriod(IEnumerable<UserViewModel> usersViewModel, string period)
        {
            switch (period)
            {
                case "today":
                    return usersViewModel
                        .Where(x => x.RegisterDate.ToString("dd/MM/yyyy").Equals(DateTime.Now.ToString("dd/MM/yyyy")))
                        .ToList();

                case "week":
                    DateTime initialDate = DateTime.Now.AddDays(-7);

                    return usersViewModel
                        .Where(x => x.RegisterDate >= initialDate && x.RegisterDate <= DateTime.Now)
                        .ToList();

                case "month":
                    int currentYear = DateTime.Now.Year;
                    int currentMonth = DateTime.Now.Month;

                    return usersViewModel
                        .Where(x => x.RegisterDate.Year.Equals(currentYear) && x.RegisterDate.Month.Equals(currentMonth))
                        .ToList();

                default:
                    return usersViewModel;
            }
        }

        private void SetClassA(IEnumerable<UserViewModel> usersViewModel)
        {
            ClassA = usersViewModel.Count(x => x.FamilyIncome <= 980);
        }

        private void SetClassB(IEnumerable<UserViewModel> usersViewModel)
        {
            ClassB = usersViewModel.Count(x => x.FamilyIncome > 980 && x.FamilyIncome <= 2500);
        }

        private void SetClassC(IEnumerable<UserViewModel> usersViewModel)
        {
            ClassC = usersViewModel.Count(x => x.FamilyIncome > 2500);
        }

        private void SetGreaterThanEighteen(IEnumerable<UserViewModel> usersViewModel)
        {
            decimal familyIncomeAverage = usersViewModel.Average(x => x.FamilyIncome);

            GreaterThanEighteen = usersViewModel
                .Count(x =>
                    (DateTime.Now.Subtract(x.BirthDate).TotalDays / 365.25) > 18 &&
                    x.FamilyIncome > familyIncomeAverage);
        }
    }
}