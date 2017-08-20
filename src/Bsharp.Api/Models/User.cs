using System;
namespace Bsharp.Api.Models
{
    public class User
    {
        public Guid Id          { get; set; } 
        public string Handle    { get; set; }
        public string FirstName { get; set; }
        public string LastName  { get; set; }
        public string Email     { get; set; }

        public User()
        {
        }
    }
}
