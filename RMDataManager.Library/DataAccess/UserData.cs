﻿using RMDataManager.Library.Internal.DataAccess;
using RMDataManager.Library.Models;

using System.Collections.Generic;

namespace RMDataManager.Library.DataAccess
{
	public class UserData
	{
		public List<UserModel> GetUserbyId(string Id)
		{
			SqlDataAccess sql = new SqlDataAccess();

			var p = new { Id };

			var output = sql.LoadData<UserModel, dynamic>("dbo.spUserLookup", p, "RMData");

			return output;
		}
	}
}