using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Mantex.LDAP.Configuration
{
	public class MappingElement : ConfigurationElement
	{
		[ConfigurationProperty("name")]
		public string Name { get { return (string)this["name"]; } }

		[ConfigurationProperty("securityGroups")]
		public string SecurityGroups { get { return (string)this["securityGroups"]; } }
	}
}
