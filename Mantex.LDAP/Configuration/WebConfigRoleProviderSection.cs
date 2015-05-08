using System.Configuration;

namespace Mantex.LDAP.Configuration
{
	public class WebConfigRoleProviderSection : ConfigurationSection
	{
		[ConfigurationProperty("roles")]
		public WebConfigRolesCollection Roles
		{
			get { return (WebConfigRolesCollection)this["roles"]; }
			set { this["roles"] = value; }
		}

		public static WebConfigRoleProviderSection GetConfig()
		{
			return ConfigurationManager.GetSection("Mantex.LDAP.WebConfigRoleProvider") as WebConfigRoleProviderSection;
		}
	}
}