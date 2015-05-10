using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mantex.ERP.Entities
{
	public enum MachineStatusEnum
	{
		Offline = 0,
		Online,
		Working,
		WaitingForEmptyBelt,
		Measuring
	}
}
