using System;
namespace eStore.Shared.Models
{
	public class BaseModel
	{
		public int? StoreId { get; set; }
		public string UserId { get; set; }
		public bool ReadOnly { get; set; }
	}
}

