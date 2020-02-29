using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TestTokenApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using TestTokenApp.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace TestTokenApp.Controllers
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Member")]
    [Route("api/[controller]")]
    [ApiController]
    public class LocalAdminPasswordController : Controller
    {
        private readonly TestTokenAppContext _context;

        public LocalAdminPasswordController(TestTokenAppContext context)
        {
            _context = context;
        }


        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }

        // GET: api/LocalAdminPassword/string The string is the hostname
        [HttpGet("{hostname}", Name = "Get")]
        public string Get(string hostname)
        {

            string rIp = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            LocalAdminPassword lap;
            string password = CreateRandomPassword(20); // password better not have any of these "%" 

            int count = _context.LocalAdminPasswords.Where(q => q.HostName == hostname).Count();
            if (count > 0)
            {
                // TODO: update the password
                lap = _context.LocalAdminPasswords.Where(q => q.HostName == hostname).First();

                lap.Password = password;
                lap.RemoteIpAddress = rIp;
                _context.LocalAdminPasswords.Update(lap);
            }
            else
            {
                // TODO: insert the host & password
                lap = new LocalAdminPassword();

                lap.HostName = hostname;
                lap.Password = password;
                lap.RemoteIpAddress = rIp;
                _context.LocalAdminPasswords.Add(lap);

            }

            _context.SaveChanges();


            return password;
        }

        private static string CreateRandomPassword(int passwordLength)
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-";
            char[] chars = new char[passwordLength];
            Random rd = new Random();

            for (int i = 0; i < passwordLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }
    }

}
