using System.Collections.Generic;
using System.Linq;

namespace Mantex.LDAP
{
	public class Role
	{
		public Role(string Name, string Users)
		{
			this.Name = Name;

			if ((Users != null) && !string.IsNullOrEmpty(Users))
			{
				this.Users = Users.Split(',').Select(u => u.Trim().ToLowerInvariant()).ToList();
			}
		}

		public List<string> Users { get; set; }
		public string Name { get; set; }
	}
}