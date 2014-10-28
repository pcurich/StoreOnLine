using System.Security.Principal;

namespace StoreOnLine.Service.Security
{
    public class CustomIdentity : IIdentity
    {
        #region IIdentity Members

        public string Name { get; private set; }

        public string AuthenticationType
        {
            get { return "Custom"; }
        }

        public bool IsAuthenticated
        {
            get
            {
                return !string.IsNullOrEmpty(Name);
            }
        }

        #endregion
        public CustomIdentity(string name)
        {
            Name = name;
        }
    }
}
