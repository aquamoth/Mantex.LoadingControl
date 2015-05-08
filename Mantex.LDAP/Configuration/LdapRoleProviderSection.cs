using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Mantex.LDAP.Configuration
{
	public class LdapRoleProviderSection : ConfigurationSection
	{
		private LdapRoleProviderSection() { }

		[ConfigurationProperty("username")]
		public string Username { get { return (string)this["username"]; } }

		[ConfigurationProperty("password")]
		public string Password { get { return (string)this["password"]; } }

		[ConfigurationProperty("attributeMapUsername")]
		public string AttributeMapUsername { get { return (string)this["attributeMapUsername"]; } }

		[ConfigurationProperty("connectionString")]
		public string ConnectionString { get { return (string)this["connectionString"]; } }

		[ConfigurationProperty("mappings", IsDefaultCollection = false)]
		[ConfigurationCollection(typeof(MappingElement))]
		public MappingsCollection Mappings { get { return (MappingsCollection)this["mappings"]; } }
	}
}
