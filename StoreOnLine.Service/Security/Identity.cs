using System.Security.Principal;

namespace StoreOnLine.Service.Security
{
    public class Identity : IIdentity
    {
        public string Name { get; private set; }

        public string AuthenticationType
        {
            get { return "Custom"; }
        }

        public bool IsAuthenticated
        {
            get { return !string.IsNullOrEmpty(Name); }
        }

        public Identity(string name)
        {
            Name = name;
        }
    }
}
