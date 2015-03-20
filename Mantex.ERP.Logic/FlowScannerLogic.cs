using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mantex.ERP.Logic
{
	public class FlowScannerLogic
	{
		public Data.MachineStatusEnum GetStatus()
		{
			return FlowScannerFake.Default.Status;
		}

		public void StartMeasure(string transactionId, int materialTypeId)
		{
			if (FlowScannerFake.Default.Status != Mantex.ERP.Data.MachineStatusEnum.Working)
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

		public Data.MachineStatusEnum Status { get; set; }
		public void StartMeasure(string transactionId, int materialTypeId)
		{
			_timer.Change(5000, 5000);
			this.TransactionId = transactionId;
			this.MaterialTypeId = materialTypeId;
			this.Status = Data.MachineStatusEnum.WaitingForEmptyBelt;
		}
		public void StopMeasure()
		{
			this.TransactionId = null;
			Status = Mantex.ERP.Data.MachineStatusEnum.Working;
		}
		
		public string TransactionId { get; set; }
		public int MaterialTypeId { get; set; }

		readonly System.Threading.Timer _timer;

		public FlowScannerFake()
		{
			Status = Data.MachineStatusEnum.Offline;
			_timer = new System.Threading.Timer(new System.Threading.TimerCallback((obj) => {
				if (Status < Data.MachineStatusEnum.Working)
					Status++;
				else if(Status == Data.MachineStatusEnum.WaitingForEmptyBelt)
				{
					if (TransactionId == null)
						Status = Data.MachineStatusEnum.Working;
					else
						Status = Data.MachineStatusEnum.Measuring;
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
