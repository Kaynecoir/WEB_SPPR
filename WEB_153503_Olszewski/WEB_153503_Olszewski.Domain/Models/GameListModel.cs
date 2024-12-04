using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB_153503_Olszewski.Domain.Models
{
	public class GameListModel<T>
	{
		public List<T> Items { get; set; } = new();
		public int CurrentPage { get; set; } = 1;
		public int TotalPages { get; set; } = 1;
	}
}
