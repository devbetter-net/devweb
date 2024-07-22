using System.Security.Claims;
using Dev.Plugin.Auth.Areas.Auth.ViewModels;
using Dev.Plugin.Auth.Core.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Dev.Plugin.Auth.Areas.Auth.Controllers;

public class AuthenticationController : BaseController
{
    private SignInManager<DevUser> _signInManager;
    private UserManager<DevUser> _userManager;
    public AuthenticationController(SignInManager<DevUser> signInManager, UserManager<DevUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<IActionResult> Login()
    {
        var loginVM = new LoginVM()
        {
            Schemes = await _signInManager.GetExternalAuthenticationSchemesAsync()

        };

        return View(loginVM);
    }

    public async Task<IActionResult> LoginSubmitted(LoginVM loginVM)
    {
        if (!ModelState.IsValid)
        {
            return View("Login", loginVM);
        }

        var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
        if (user != null)
        {
            var userPasswordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
            if (userPasswordCheck)
            {
                var userLoggedIn = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);

                if (userLoggedIn.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (userLoggedIn.IsNotAllowed)
                {
                    return RedirectToAction("EmailConfirmation");
                }
                else if (userLoggedIn.RequiresTwoFactor)
                {
                    return RedirectToAction("TwoFactorConfirmation", new { loggedInUserId = user.Id });
                }

                else
                {
                    ModelState.AddModelError("", "Invalid login attempt. Please, check your username and password");
                    return View("Login", loginVM);
                }
            }
            else
            {
                await _userManager.AccessFailedAsync(user);

                if (await _userManager.IsLockedOutAsync(user))
                {
                    ModelState.AddModelError("", "Your account is locked, please try again in 10 mins");
                    return View("Login", loginVM);
                }

                ModelState.AddModelError("", "Invalid login attempt. Please, check your username and password");
                return View("Login", loginVM);
            }
        }


        return RedirectToAction("Index", "Home");
    }

    public IActionResult ExternalLogin(string provider, string returnUrl = "")
    {
        var redirectUrl = Url.Action("ExternalLoginCallback", "Authentication", new { ReturnUrl = returnUrl });

        var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

        return new ChallengeResult(provider, properties);
    }

    public async Task<IActionResult> ExternalLoginCallback(string returnUrl = "", string remoteError = "")
    {

        var loginVM = new LoginVM()
        {
            Schemes = await _signInManager.GetExternalAuthenticationSchemesAsync()
        };

        if (!string.IsNullOrEmpty(remoteError))
        {
            ModelState.AddModelError("", $"Error from extranal login provide: {remoteError}");
            return View("Login", loginVM);
        }

        //Get login info
        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info == null)
        {
            ModelState.AddModelError("", $"Error from extranal login provide: {remoteError}");
            return View("Login", loginVM);
        }

        var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

        if (signInResult.Succeeded)
            return RedirectToAction("Index", "Home");
        else
        {
            var userEmail = info.Principal.FindFirstValue(ClaimTypes.Email);
            if (!string.IsNullOrEmpty(userEmail))
            {
                var user = await _userManager.FindByEmailAsync(userEmail);

                if (user == null)
                {
                    user = new DevUser()
                    {
                        UserName = userEmail,
                        Email = userEmail,
                        EmailConfirmed = true
                    };

                    await _userManager.CreateAsync(user);
                    //await _userManager.AddToRoleAsync(user, "User");
                }

                await _signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToAction("Index", "Home");
            }

        }

        ModelState.AddModelError("", $"Something went wrong");
        return View("Login", loginVM);
    }

    // Action method to handle callback from Google
    public async Task<IActionResult> SignInGoogle(string state)
    {
        // The 'state' parameter is automatically bound from the query string.
        // You can use it here if needed for validation or correlation purposes.

        // External login callback processing
        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info == null)
        {
            // Log or handle error: external login info not found
            return RedirectToAction(nameof(Login)); // Assuming you have a Login action for redirection
        }

        // Sign in the user with the external login info
        var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
        if (result.Succeeded)
        {
            // Redirect to desired page after successful login
            return RedirectToAction("Index", "Home");
        }
        else
        {
            // Handle failed login attempt
            // For example, you might want to register the user if they do not exist
            // Or return to the login page with an error message
            return RedirectToAction(nameof(Login)); // Redirect or handle accordingly
        }
    }
}
