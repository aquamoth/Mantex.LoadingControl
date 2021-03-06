﻿using Mantex.ERP.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mantex.ERP.Data
{
	public interface IRepository
	{
		IDbSet<Batch> Batches { get; }
		IDbSet<LoadingPosition> LoadingPositions { get; }
		IDbSet<MaterialType> MaterialTypes { get; }
		IDbSet<Transaction> Transactions { get; }
		IDbSet<Observation> Observations { get; }
		int SaveChanges();
	}
}
