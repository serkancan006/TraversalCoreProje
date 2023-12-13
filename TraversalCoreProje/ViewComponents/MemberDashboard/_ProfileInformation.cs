using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TraversalCoreProje.Areas.Member.Models;

namespace TraversalCoreProje.ViewComponents.MemberDashboard
{
    public class _ProfileInformation : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
        public _ProfileInformation(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.memberName = values.UserName + " " + values.Surname;
            ViewBag.memberPhone = values.PhoneNumber;
            ViewBag.memberMail = values.Email;
            //UserEditViewModel userEditViewModel = new UserEditViewModel();
            //userEditViewModel.name = values.Name;
            //userEditViewModel.surname = values.Surname;
            //userEditViewModel.phonenumber = values.PhoneNumber;
            //userEditViewModel.mail = values.Email;
            return View();
        }
    }
}
