using System;
using System.IdentityModel.Claims;
using System.ServiceModel;

namespace WCFServiceApp
{
    public class AuthManager : ServiceAuthorizationManager
    {
        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            string action = operationContext.RequestContext.RequestMessage.Headers.Action;
            Console.WriteLine("action: {0}", action);
            foreach (ClaimSet cs in operationContext.ServiceSecurityContext.AuthorizationContext.ClaimSets)
            {
                if (cs.Issuer == ClaimSet.System)
                {
                    foreach (Claim c in cs.FindClaims("http://example.com/claims/allowedoperation", Rights.PossessProperty))
                    {
                        Console.WriteLine("resource: {0}", c.Resource.ToString());
                        if (action == c.Resource.ToString())
                            return true;
                    }
                }
            }
            return false;
        }
    }
}