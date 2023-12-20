using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudyGroup.Data;

namespace StudyGroup.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {

            public string Username { get; set; }
            public string Bio { get; set; }

            public string NewUsername { get; set; }

            public string NewBio { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(IdentityUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var bio = _context.AppUsers.FirstOrDefault(a => a.UserName == userName).BioInfo;
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                Username = userName,
                PhoneNumber = phoneNumber,
                Bio = bio
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            //var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            //if (Input.PhoneNumber != phoneNumber)
            //{
            //    var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
            //    if (!setPhoneResult.Succeeded)
            //    {
            //        StatusMessage = "Unexpected error when trying to set phone number.";
            //        return RedirectToPage();
            //    }
            //}

            var username = await _userManager.GetUserNameAsync(user);
            var appUser = _context.AppUsers.FirstOrDefault(a => a.UserName == username);
            var bio = appUser.BioInfo;

            if(Input.NewUsername!= username && Input.NewBio != bio)
            {
                if(Input.NewUsername != username && !String.IsNullOrEmpty(Input.NewUsername))
                {
                    var setNewUsername = await _userManager.SetUserNameAsync(user, Input.NewUsername);
                    if (!setNewUsername.Succeeded)
                    {
                        StatusMessage = "Error! There was an error when trying to save your new username; Please try again";
                        return RedirectToPage();
                    }
                }
                if (Input.NewBio != bio)
                {
                    try
                    {
                        if (String.IsNullOrEmpty(Input.NewBio))
                        {
                            StatusMessage = "Error! There was an error when trying to save your new Bio; Please try again";
                            return RedirectToPage();
                        }
                        else
                        {
                            appUser.BioInfo = Input.NewBio;
                            await _context.SaveChangesAsync();
                        }
                        
                    }
                    catch
                    {
                        StatusMessage = "Error! There was an error when trying to save your new Bio; Please try again";
                        return RedirectToPage();
                    }
                }
                await _signInManager.RefreshSignInAsync(user);
                StatusMessage = "Your profile has been updated";
                return RedirectToPage();
            }
            else
            {
                if(Input.NewUsername == username)
                {
                    StatusMessage += "Error! Your New Username is Exactly like your old one, Try a new one";
                }
                if (Input.NewBio == bio)
                {
                    StatusMessage += "Error! Your New Bio is Exactly like your old one, Try entering a new one";
                }
                return RedirectToPage();
            }

            
        }
    }
}
