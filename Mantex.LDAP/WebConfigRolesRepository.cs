using Mantex.LDAP.Configuration;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web.Configuration;

namespace Mantex.LDAP
{
	public class WebConfigRolesRepository //: Interfaces.IRolesRepository
	{

		public WebConfigRolesRepository()
		{
			var section = WebConfigRoleProviderSection.GetConfig();

			_lRoles.AddRange(section.Roles
				.Cast<WebConfigRolesElement>()
				.Select(r => new Role(r.Name, r.Users))
				.ToArray()
			);
		}

		private List<Role> _lRoles = new List<Role>();
		public System.Collections.Generic.List<Role> Roles
		{
			get { return _lRoles; }
		}

		public void Save()
		{
			var config = WebConfigurationManager.OpenWebConfiguration("~");
			var section = (WebConfigRoleProviderSection)config.GetSection("WebConfigRoleProvider");

			section.Roles.Clear();

			foreach (var role in _lRoles)
			{
				section.Roles.Add(new WebConfigRolesElement
				{
					Name = role.Name,
					Users = string.Join(", ", role.Users
												.Where(u => !string.IsNullOrEmpty(u))
												.OrderBy(u => u)
												.Distinct()
												.ToArray())
				});
			}

			config.Save();
		}
	}
}