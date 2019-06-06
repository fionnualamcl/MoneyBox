using Moneybox.App.Domain;
using System;

namespace Moneybox.App
{
    public class User
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public Address Address { get; set; }

        public Boolean IsDisabled { get; set; }

        public Boolean IsUserDisabled()
        {
            return this.IsDisabled;
        }
    }
}
