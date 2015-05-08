using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Mantex.LDAP.Configuration
{
	public class MappingsCollection : BaseConfigurationElementCollection<MappingElement>
	{
	}

	//public class FuseMonitoringIOElement : ConfigurationElement
	//{
	//	[ConfigurationProperty("Name")]
	//	public string Name { get { return (string)this["Name"]; } }

	//	[ConfigurationProperty("SecurityGroup")]
	//	public string SecurityGroup { get { return (string)this["SecurityGroup"]; } }
	//}

}
