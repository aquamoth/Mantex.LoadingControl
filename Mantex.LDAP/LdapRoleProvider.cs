using Mantex.LDAP.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Mantex.LDAP
{
	public class LdapRoleProvider : RoleProvider
	{
		readonly LdapRoleProviderSection _configuration;
		readonly string _connectionString;

		public LdapRoleProvider()
		{
			_configuration = ConfigurationManager.GetSection("Mantex.LDAP.LdapRoleProvider") as LdapRoleProviderSection;
			_connectionString = ConfigurationManager.ConnectionStrings[_configuration.ConnectionString].ConnectionString;
		}

		public override void AddUsersToRoles(string[] usernames, string[] roleNames)
		{
			throw new NotImplementedException();
		}

		public override string ApplicationName { get; set; }

		public override void CreateRole(string roleName)
		{
			throw new NotImplementedException();
		}

		public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
		{
			throw new NotImplementedException();
		}

		public override string[] FindUsersInRole(string roleName, string usernameToMatch)
		{
			throw new NotImplementedException();
		}

		public override string[] GetAllRoles()
		{
			throw new NotImplementedException();
		}

		public override string[] GetRolesForUser(string username)
		{
			var allRoles = new List<string>();
			var root = new DirectoryEntry(_connectionString, _configuration.Username, _configuration.Password);

			var filter = string.Format(CultureInfo.InvariantCulture, "(&(objectClass=user)({0}={1}))", _configuration.AttributeMapUsername, username);
			var searcher = new DirectorySearcher(root, filter);

			searcher.PropertiesToLoad.Add("memberOf");
			SearchResult result = searcher.FindOne();
			if (result != null && !string.IsNullOrEmpty(result.Path))
			{
				DirectoryEntry user = result.GetDirectoryEntry();
				PropertyValueCollection groups = user.Properties["memberOf"];
				foreach (string path in groups)
				{
					string[] parts = path.Split(',');
					if (parts.Length > 0)
					{
						foreach (string part in parts)
						{
							string[] p = part.Split('=');
							if (p[0].Equals("cn", StringComparison.OrdinalIgnoreCase))
							{
								allRoles.Add(p[1]);
							}
						}
					}
				}
			}

			return lookupRoles(allRoles.Distinct());
		}

		private string[] lookupRoles(IEnumerable<string> securityGroups)
		{
			var roles = _configuration.Mappings
				.Cast<MappingElement>()
				.Where(me => me.SecurityGroups.Split(',').Any(sg => securityGroups.Contains(sg)))
				.Select(me => me.Name)
				.ToArray();
			return roles;
		}

		public override string[] GetUsersInRole(string roleName)
		{
			throw new NotImplementedException();
		}

		public override bool IsUserInRole(string username, string roleName)
		{
			throw new NotImplementedException();
		}

		public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
		{
			throw new NotImplementedException();
		}

		public override bool RoleExists(string roleName)
		{
			throw new NotImplementedException();
		}

	}
}
