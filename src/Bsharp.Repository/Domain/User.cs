namespace Bsharp.Repository.Domain
{
    using System;

    public class User
    {
        public Guid Id          { get; set; } 
        public string Handle    { get; set; }
        public string Email     { get; set; }

        public User()
        {
        }

        public User(Guid id, string handle, string email)
        {
            Id = id;
            Handle = handle;
            Email = email;
        }
    }
}
