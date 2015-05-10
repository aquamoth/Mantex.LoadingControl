using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mantex.ERP.Services
{
	public class FlowScannerLogic
	{
		public Entities.MachineStatusEnum GetStatus()
		{
			return FlowScannerFake.Default.Status;
		}

		public void StartMeasure(string transactionId, int materialTypeId)
		{
			if (FlowScannerFake.Default.Status != Mantex.ERP.Entities.MachineStatusEnum.Working)
				throw new NotSupportedException("FlowScanner kan inte mäta material just nu.");

			FlowScannerFake.Default.StartMeasure(transactionId, materialTypeId);
		}

		public void StopMeasure()
		{
			FlowScannerFake.Default.StopMeasure();
		}
	}



	class FlowScannerFake
	{

		public Entities.MachineStatusEnum Status { get; set; }
		public void StartMeasure(string transactionId, int materialTypeId)
		{
			_timer.Change(5000, 5000);
			this.TransactionId = transactionId;
			this.MaterialTypeId = materialTypeId;
			this.Status = Entities.MachineStatusEnum.WaitingForEmptyBelt;
		}
		public void StopMeasure()
		{
			this.TransactionId = null;
			Status = Mantex.ERP.Entities.MachineStatusEnum.Working;
		}
		
		public string TransactionId { get; set; }
		public int MaterialTypeId { get; set; }

		readonly System.Threading.Timer _timer;

		public FlowScannerFake()
		{
			Status = Entities.MachineStatusEnum.Offline;
			_timer = new System.Threading.Timer(new System.Threading.TimerCallback((obj) => {
				if (Status < Entities.MachineStatusEnum.Working)
					Status++;
				else if (Status == Entities.MachineStatusEnum.WaitingForEmptyBelt)
				{
					if (TransactionId == null)
						Status = Entities.MachineStatusEnum.Working;
					else
						Status = Entities.MachineStatusEnum.Measuring;
				}
			}), null, 5000, 5000);
		}

		static FlowScannerFake()
		{
			Default = new FlowScannerFake();
		}

		public static FlowScannerFake Default { get; set; }
	}
}
