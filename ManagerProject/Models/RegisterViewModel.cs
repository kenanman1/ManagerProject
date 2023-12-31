﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ManagerProject.Models;

public class RegisterViewModel
{
    [Required]
    [EmailAddress]
    [Remote(action: "CheckEmail", controller: "Account", ErrorMessage = "This email is already in use")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [StringLength(100, ErrorMessage = "Password must be at least 6 characters long", MinimumLength = 6)]
    [RegularExpression(@"^(?=.*[0-9]).*$", ErrorMessage = "Password must contain at least 1 number")]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }

    [Required]
    public string Name { get; set; }
}