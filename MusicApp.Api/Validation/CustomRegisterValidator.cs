using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApp.Api.Validation
{
    public class CustomRegisterValidator
    {
        private readonly UserManager<IdentityUser> _userManager;

        public CustomRegisterValidator(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public bool UniqueName(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                var user = _userManager.FindByEmailAsync(email);
                if (user.Result == null) return true;
                return false;
            }
            return false;

        }
    }
}
