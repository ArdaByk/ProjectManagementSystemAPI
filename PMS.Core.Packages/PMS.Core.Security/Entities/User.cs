using PMS.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Core.Security.Entities;

public class User : Entity<Guid>
{
    public string Username { get; set; }
    public string Email { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public bool EmailVerified { get; set; }
    public User()
    {
        Username = string.Empty;
        Email = string.Empty;
        PasswordHash = Array.Empty<byte>();
        PasswordSalt = Array.Empty<byte>();
        EmailVerified = false;
    }

    public User(string username, string email, byte[] passwordHash, byte[] passwordSalt, bool emailVerified)
    {
        Username = username;
        Email = email;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
        EmailVerified = emailVerified;
    }
    public User(Guid id, string username, string email, byte[] passwordHash, byte[] passwordSalt, bool emailVerified)
     :base(id)
    {
        Username = username;
        Email = email;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
        EmailVerified = emailVerified;
    }
}
