using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Constants;

public static class AuthMessages
{
    public const string UserDontExists = "User doesnt exist.";
    public const string UserMailAlreadyExists = "User mail already exists.";
    public const string PasswordDontMatch = "Password don't match.";
    public static string EmailIsNotVerified = "Email is not verified.";
    public static string UserAlreadyExists = "User already exists.";
    public static string MailAlreadyVerified = "The mail is already verified.";
    public static string TokenExpired = "Token expired.";
    public static string AccessDenied = "You do not have sufficient privileges to perform this operation.";
    public static string TokenDoesntExist = "Token doesnt exist.";
}