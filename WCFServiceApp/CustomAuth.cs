using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;

namespace WCFServiceApp
{
    public class CustomValidator : UserNamePasswordValidator
    {
        // demo only
        public override void Validate(string userName, string password)
        {
            if (null == userName || null == password)
            {
                throw new ArgumentNullException();
            }

            if (!(userName == "minx" && password == "wcf_test"))
            {
                throw new SecurityTokenException("Unknown Username or Password");
            }
        }
    }
}