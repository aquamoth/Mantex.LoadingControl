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

		//public Configuration.ChannelElement GetChannel(string name)
		//{
		//	foreach (Configuration.ChannelElement channel in Channels)
		//	{
		//		if (channel.Name == name)
		//		{
		//			return channel;
		//		}
		//	}

		//	throw new ArgumentException("Bad channel name");
		//}

		//[ConfigurationProperty("ContinuousFlowScanOrders", IsDefaultCollection = false)]
		//[ConfigurationCollection(typeof(ContinuousFlowScanOrderElement), AddItemName = "ContinuousFlowScanOrder")]
		//public ContinuousFlowScanOrderCollection ContinuousFlowScanOrders { get { return (ContinuousFlowScanOrderCollection)this["ContinuousFlowScanOrders"]; } }

		//[ConfigurationProperty("CalibrateMachineOrders", IsDefaultCollection = false)]
		//[ConfigurationCollection(typeof(CalibrateMachineOrderElement), AddItemName = "CalibrateMachineOrder")]
		//public CalibrateMachineOrderCollection CalibrateMachineOrders { get { return (CalibrateMachineOrderCollection)this["CalibrateMachineOrders"]; } }

		//[ConfigurationProperty("DigitalSignalIOs", IsDefaultCollection = false)]
		//[ConfigurationCollection(typeof(DigitalSignalIOElement), AddItemName = "DigitalSignalIO")]
		//public DigitalSignalIOCollection DigitalSignalIOs { get { return (DigitalSignalIOCollection)this["DigitalSignalIOs"]; } }

		//[ConfigurationProperty("FuseMonitoringIOs", IsDefaultCollection = false)]
		//[ConfigurationCollection(typeof(FuseMonitoringIOElement), AddItemName = "FuseMonitoringIO")]
		//public FuseMonitoringIOCollection FuseMonitoringIOs { get { return (FuseMonitoringIOCollection)this["FuseMonitoringIOs"]; } }
	}
}
