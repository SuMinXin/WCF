using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;

namespace WCFServiceApp
{
    public class AuthPolicy : IAuthorizationPolicy
    {
        string id;

        public AuthPolicy()
        {
            id = Guid.NewGuid().ToString();
        }

        public bool Evaluate(EvaluationContext evaluationContext,
                                                ref object state)
        {
            bool bRet = false;
            CustomAuthState customstate = null;

            if (state == null)
            {
                customstate = new CustomAuthState();
                state = customstate;
            }
            else
                customstate = (CustomAuthState)state;
            Console.WriteLine("In Evaluate");
            if (!customstate.ClaimsAdded)
            {
                IList<Claim> claims = new List<Claim>();

                foreach (ClaimSet cs in evaluationContext.ClaimSets)
                    foreach (Claim c in cs.FindClaims(ClaimTypes.Name, Rights.PossessProperty))
                        foreach (string s in GetAllowedOpList(c.Resource.ToString()))
                        {
                            claims.Add(new Claim("http://example.com/claims/allowedoperation", s, Rights.PossessProperty));
                            Console.WriteLine("Claim added {0}", s);
                        }
                evaluationContext.AddClaimSet(this, new DefaultClaimSet(this.Issuer, claims));
                customstate.ClaimsAdded = true;
                bRet = true;
            }
            else
            {
                bRet = true;
            }
            return bRet;
        }

        private IEnumerable<string> GetAllowedOpList(string username)
        {
            IList<string> ret = new List<string>();

            if (username == "minx")
            {
                ret.Add("http://localhost:60366/Service/GetData");
                ret.Add("http://localhost:60366/Service.svc");
            }
            return ret;
        }

        // internal class for state
        class CustomAuthState
        {
            bool bClaimsAdded;

            public CustomAuthState()
            {
                bClaimsAdded = false;
            }

            public bool ClaimsAdded
            {
                get { return bClaimsAdded; }
                set { bClaimsAdded = value; }
            }
        }

        public ClaimSet Issuer
        {
            get { return ClaimSet.System; }
        }

        public string Id
        {
            get { return id; }
        }
    }
}