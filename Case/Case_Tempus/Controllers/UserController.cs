using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CaseTempus.Data;
using CaseTempus.Models;
using System.Collections.Generic;

namespace CaseTempus.Controllers
{
    [Route("users")]
    public class UserController : Controller
    {
        private readonly DataContext _dataContext;

        public UserController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            IEnumerable<UserViewModel> users = _dataContext.Users.ToList();

            return View(users);
        }

        [HttpPost]
        [Route("")]
        public IActionResult Index(string searchUser)
        {
            ViewData["currentSearch"] = searchUser;

            IEnumerable<UserViewModel> users = _dataContext.Users.Where(x => x.Name.Contains(searchUser)).ToList();
            return View(users);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserViewModel userViewModel = await _dataContext
                .Users
                .FirstOrDefaultAsync(x => x.Id == id);

            if (userViewModel == null)
            {
                return NotFound();
            }

            return View(userViewModel);
        }

        [HttpGet]
        [Route("add")]
        public IActionResult Create()
        {
            return View(new UserViewModel());
        }

        [HttpPost]
        [Route("add")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel userViewModel)
        {
            if (IsCpfTaken(userViewModel.CPF))
            {
                ModelState.AddModelError("CPF", "Este CPF já está cadastadro");
            }

            if (!ModelState.IsValid)
            {
                return View(userViewModel);
            }

            _dataContext.Add(userViewModel);

            await _dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("update")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserViewModel userViewModel = await _dataContext.Users.FindAsync(id);

            if (userViewModel == null)
            {
                return NotFound();
            }

            return View(userViewModel);
        }

        [HttpPost]
        [Route("update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id, Name, CPF, BirthDate, FamilyIncome")] UserViewModel userViewModel)
        {
            if (id != userViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(userViewModel);
            }

            try
            {
                _dataContext.Update(userViewModel);
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserViewModelExists(userViewModel.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("delete")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserViewModel userViewModel = await _dataContext
                .Users
                .FirstOrDefaultAsync(m => m.Id == id);

            if (userViewModel == null)
            {
                return NotFound();
            }

            return View(userViewModel);
        }

        [Route("delete")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            UserViewModel userViewModel = await _dataContext.Users.FindAsync(id);

            _dataContext.Users.Remove(userViewModel);
            await _dataContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("report")]
        public IActionResult Report()
        {
            IEnumerable<UserViewModel> users = _dataContext.Users.ToList();
            ReportViewModel reportViewModel = new ReportViewModel(users);

            return View(reportViewModel);
        }

        [HttpPost]
        [Route("report")]
        public IActionResult Report(string reportPeriod)
        {
            if (string.IsNullOrEmpty(reportPeriod))
            {
                return View();
            }

            IEnumerable<UserViewModel> users = _dataContext.Users.ToList();
            ReportViewModel reportViewModel = new ReportViewModel(users, reportPeriod);

            return View(reportViewModel);
        }

        #region HELPERS

        private bool IsCpfTaken(string cpf)
        {
            return _dataContext.Users.Any(x => x.CPF == cpf);
        }

        private bool UserViewModelExists(long id)
        {
            return _dataContext.Users.Any(e => e.Id == id);
        }

        #endregion
    }
}
