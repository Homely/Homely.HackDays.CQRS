using System;

namespace Domain.Models
{
    public class User
    {
        public User(string name, string email)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            CreateUsername(Name);
            LastActive = DateTime.UtcNow;
        }

        private void CreateUsername(string name)
        {
            Username = name.ToLower(); // contrived example.
        }

        public void UpdateDetails(string name, string email)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            CreateUsername(Name);
        }

        public void Login()
        {
            LastActive = DateTime.UtcNow;
        }

        public void Activate()
        {
            IsActive = true;
        }

        public void Ban()
        {
            IsActive = false;
        }
        
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Username { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime LastActive { get; private set; }
    }
}