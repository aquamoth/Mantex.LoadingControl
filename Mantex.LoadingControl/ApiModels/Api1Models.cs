using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mantex.LoadingControl.ApiModels
{
	public class Api1Models
	{
		public abstract class Operation
		{
			[Required]
			public bool IsDeleted { get; set; }
		}

		// Transactions

		public class TransactionOperation : Operation
		{
			[Key]
			[Required]
			[StringLength(25)]
			public string Id { get; set; }

			[Required]

			public string Name { get; set; }
			public string Supplier { get; set; }
			public float Weight { get; set; }
			public string Article { get; set; }
			public int ArticleId { get; set; }
			public DateTime ArrivalDate { get; set; }
			public string Comments { get; set; }
		}

		// AnalysisResults

		public class AnalysisResults
		{
			public string Token { get; set; }
			public IEnumerable<AnalysisResult> AnalysisResult { get; set; }
		}

		public class AnalysisResult
		{
			public string TransactionId { get; set; }
			public IEnumerable<TransactionSession> Sessions { get; set; }
		}

		public class TransactionSession
		{
			public int BatchNumber { get; set; }
			public int SessionId { get; set; }
			public DateTime StartedAt { get; set; }
			public DateTime EndedAt { get; set; }
			public AnalysisResultValue WebWeight { get; set; }
			public AnalysisResultValue DryWeight { get; set; }
			public AnalysisResultValue Moisture { get; set; }
			public AnalysisResultValue Contamination { get; set; }
		}

		public class AnalysisResultValue
		{
			public double Value { get; set; }
			public double? StDev { get; set; }
		}
		
		// Lab Results
		public class LabResultOperation : Operation
		{
			public string Id { get; set; }
			public string TransactionId { get; set; }
			public AnalysisResultValue Moisture { get; set; }
		}
	}
}