using AuthService.Helpers;
using AuthService.Models;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace AuthService.Repository
{
    public interface IAuthenticationRepository
    {
        public UserAuthentication GetUserAuthenticationByUsername(string username);

        public UserAuthentication GetUserAuthenticationByToken(Guid token);

        public void CreateAuthentication(string username, byte[] passwordHash, byte[] passwordSalt);
    }

    public class AuthenticationRepository : IAuthenticationRepository
    {
        private DataContext _context;

        public AuthenticationRepository(DataContext context)
        {
            _context = context;
        }

        public UserAuthentication GetUserAuthenticationByUsername(string username)
        {
            if (_context == null)
                throw new Exception("Connection with datebase closed.");

            UserAuthentication userCredential = _context.UserAuthentications.Local.SingleOrDefault(x => x.Username.Equals(username));

            return userCredential;
        }

        public UserAuthentication GetUserAuthenticationByToken(Guid token)
        {
            if (_context == null)
                throw new Exception("Connection with datebase closed.");

            UserAuthentication userCredential = _context.UserAuthentications.Local.SingleOrDefault(x => x.ValidationToken.Equals(token));

            return userCredential;
        }

        public void CreateAuthentication(string username, byte[] passwordHash, byte[] passwordSalt)
        {
            if (_context == null)
                throw new Exception("Connection with datebase closed.");

            //Create the object to insert in the DB
            UserAuthentication userCredential = new UserAuthentication()
            {
                Username = username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            //Insert the new object in the DB
            _context.UserAuthentications.Add(userCredential);
        }
    }
}
