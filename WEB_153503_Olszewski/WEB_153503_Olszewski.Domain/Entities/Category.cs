using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB_153503_Olszewski.Domain.Entities
{
	public class Category
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string NormalizedName { get; set; }
		public ICollection<BoardGame> BoardGames { get; set; }
	}
}
