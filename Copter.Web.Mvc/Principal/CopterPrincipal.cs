using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace Copter.Web.Mvc.Principal
{
    /// <summary>
    /// 登录人 - 主体对象
    /// </summary>
    public class CopterPrincipal : IPrincipal
    {
        private IList<string> m_roles;
        public CopterPrincipal(IIdentity identity) : this(identity, null)
        {
        }
        public CopterPrincipal(IIdentity identity, IList<string> roles)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            Identity = identity;

            m_roles = roles;
        }

        public IIdentity Identity { get; }

        public bool IsInRole(string role)
        {
            if ((role == null) || (m_roles == null))
            {
                return false;
            }
            for (int i = 0; i < m_roles.Count; i++)
            {
                if ((m_roles[i] != null) && (string.Compare(m_roles[i], role, StringComparison.OrdinalIgnoreCase) == 0))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
