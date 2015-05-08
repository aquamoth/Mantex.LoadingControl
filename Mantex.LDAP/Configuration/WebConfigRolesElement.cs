using System.Configuration;

namespace Mantex.LDAP.Configuration
{
	public class WebConfigRolesElement : ConfigurationElement
	{
		[ConfigurationProperty("name", IsRequired = true)]
		public string Name
		{
			get { return (string)this["name"]; }
			set { this["name"] = value; }
		}

		[ConfigurationProperty("users")]
		public string Users
		{
			get { return (string)this["users"]; }
			set { this["users"] = value; }
		}
	}
}