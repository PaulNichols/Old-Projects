using System.Web;
using Discovery.Utility;
using Microsoft.Practices.EnterpriseLibrary.Security;

namespace Discovery.ComponentServices.Security
{
    /// <summary>
    /// Summary description for CustomSiteMapProvider
    /// </summary>
    public class CustomSiteMapProvider : XmlSiteMapProvider
    {
        

        public CustomSiteMapProvider()
        {
            //
            // TODO: Add constructor logic here
            //
           // ruleProvider = AuthorizationFactory.GetAuthorizationProvider("RuleProvider");
        }
        private static IAuthorizationProvider ruleProvider;

        private static IAuthorizationProvider GetRuleProvider
        {
            get
            {
                if (ruleProvider==null) ruleProvider= CacheManager.Get("RuleProvider") as IAuthorizationProvider;
                if (ruleProvider==null)
                {
                    ruleProvider = AuthorizationFactory.GetAuthorizationProvider("RuleProvider");
                    CacheManager.Add("RuleProvider",ruleProvider);
                }

                return ruleProvider;
            }
        }

        public override bool IsAccessibleToUser(HttpContext context, SiteMapNode node)
        {
            if (string.IsNullOrEmpty(node.Url))
            {
                //this has been commented out because of performance reasons

                ////if the node is a parent with no url then check at least one 
                ////of it's child nodes will be visible
                //foreach (SiteMapNode childNode in node.ChildNodes)
                //{
                //    if (IsAccessibleToUser(context, childNode))
                //    {
                //        return true;
                //    }
                //}
                return true;
            }
            else
            {
                //if there is no role defined then the node is accessible as far as roles go
                bool IsAccessible_Role = (node.Roles == null || node.Roles.Count == 0) || base.IsAccessibleToUser(context, node);
                //accessible as fare as rules go if no rule has been defined
                bool isAccessible_Rule = true;

                //get rules comma seperated string
                string rules = node["rules"];

                if (rules != null && SecurityTrimmingEnabled)
                {
                    foreach (string rule in rules.Split(','))
                    {
                        isAccessible_Rule = GetRuleProvider.Authorize(context.User, rule);
                        if (isAccessible_Rule)
                        {
                            break;
                        }
                    }
                }

                return isAccessible_Rule & IsAccessible_Role;
            }
        }
    }
}