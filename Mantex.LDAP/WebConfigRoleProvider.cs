using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace Mantex.LDAP
{
	public class WebConfigRoleProvider : System.Web.Security.RoleProvider
	{
		private WebConfigRolesRepository _rolesRepository;// Interfaces.IRolesRepository _rolesRepository;

		public WebConfigRoleProvider()
		{
			_rolesRepository = new WebConfigRolesRepository();
		}

		public override void AddUsersToRoles(string[] usernames, string[] roleNames)
		{
			CheckUserNames(usernames);
			CheckRoleNames(roleNames);

			foreach (var roleName in roleNames)
			{

				var role = (from r in _rolesRepository.Roles where r.Name == roleName select r).SingleOrDefault();

				if ((role != null))
				{
					role.Users.AddRange(usernames);
				}
				else
				{
					throw new ProviderException(string.Format("Role '{0}' does not exist.", roleName));
				}
			}

			Save();
		}

		private string _strApplicationName;
		public override string ApplicationName
		{
			get { return _strApplicationName; }
			set { _strApplicationName = value; }
		}

		public override void CreateRole(string roleName)
		{
			CheckRoleName(roleName);

			if (!RoleExists(roleName))
			{
				_rolesRepository.Roles.Add(new Role(roleName, null));
				Save();
			}
			else
			{
				throw new ProviderException(string.Format("Role '{0}' already exists.", roleName));
			}
		}

		public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
		{
			CheckRoleName(roleName);

			var role = (from r in _rolesRepository.Roles where r.Name == roleName select r).SingleOrDefault();

			if ((role != null))
			{
				int intUserCount = role.Users.Count;

				if ((intUserCount != 0) & throwOnPopulatedRole)
				{
					throw new ProviderException(string.Format("Role '{0}' cannot be deleted because {1} user{2} has this role.", roleName, intUserCount, intUserCount == 1 ? "" : "s"));
				}

				_rolesRepository.Roles.Remove(role);
				Save();
				return true;
			}
			else
			{
				throw new ArgumentException(string.Format("Role '{0}' does not exist.", roleName));
			}
		}

		public override string[] FindUsersInRole(string roleName, string usernameToMatch)
		{
			Role role = (from r in _rolesRepository.Roles where r.Name == roleName select r).SingleOrDefault();

			if ((role != null))
			{
				return (from u in role.Users where u.StartsWith(usernameToMatch, StringComparison.OrdinalIgnoreCase) orderby u select u).ToArray();
			}
			else
			{
				throw new ProviderException(string.Format("Role '{0}' does not exist.", roleName));
			}
		}

		public override string[] GetAllRoles()
		{
			return (from r in _rolesRepository.Roles select r.Name).ToArray();
		}

		public override string[] GetRolesForUser(string username)
		{
			CheckUserName(username);

			return (from r in _rolesRepository.Roles where r.Users.Contains(username) select r.Name).ToArray();
		}

		public override string[] GetUsersInRole(string roleName)
		{
			CheckRoleName(roleName);

			Role role = (from r in _rolesRepository.Roles where r.Name == roleName select r).SingleOrDefault();

			if ((role != null))
			{
				return role.Users.ToArray();
			}
			else
			{
				return new string[0];
			}
		}

		public override bool IsUserInRole(string username, string roleName)
		{
			CheckRoleName(roleName);
			CheckUserName(username);

			Role role = (from r in _rolesRepository.Roles where r.Name == roleName select r).SingleOrDefault();

			if ((role != null))
			{
				return role.Users.Contains(username);
			}
			else
			{
				throw new ProviderException(string.Format("Role '{0}' does not exist.", roleName));
			}
		}

		public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
		{
			CheckRoleNames(roleNames);
			CheckUserNames(usernames);

			foreach (var roleName in roleNames)
			{
				Role role = (from r in _rolesRepository.Roles where r.Name == roleName select r).SingleOrDefault();

				if ((role != null))
				{
					role.Users.RemoveAll(u => usernames.Contains(u));
				}
				else
				{
					throw new ProviderException(string.Format("Role '{0}' does not exist.", roleName));
				}
			}

			Save();
		}

		public override bool RoleExists(string roleName)
		{
			CheckRoleName(roleName);

			Role role = (from r in _rolesRepository.Roles where r.Name == roleName select r).SingleOrDefault();

			return (role != null);
		}

		private void CheckRoleName(string roleName)
		{
			if (roleName == null)
			{
				throw new ArgumentNullException("Role name cannot be null/nothing.");
			}
			else if (string.IsNullOrEmpty(roleName))
			{
				throw new ArgumentException("Role name cannot be empty.");
			}
			else if (roleName.Contains(","))
			{
				throw new ArgumentException(string.Format("Role names cannot contain commas: '{0}'.", roleName));
			}
		}

		private void CheckRoleNames(string[] roleNames)
		{
			foreach (var roleName in roleNames)
			{
				CheckRoleName(roleName);
			}
		}

		private void CheckUserName(string username)
		{
			if (username == null)
			{
				throw new ArgumentNullException("User name cannot be null/nothing.");
			}
			else if (string.IsNullOrEmpty(username))
			{
				throw new ArgumentException("User name cannot be empty.");
			}
		}

		private void CheckUserNames(string[] usernames)
		{
			foreach (string username in usernames)
			{
				CheckUserName(username);
			}
		}

		private void Save()
		{
			_rolesRepository.Save();
		}
	}
}