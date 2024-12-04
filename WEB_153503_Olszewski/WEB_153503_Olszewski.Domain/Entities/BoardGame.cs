using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB_153503_Olszewski.Domain.Entities
{
	public class BoardGame
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
        [ForeignKey("Category")]
        public int? CategoryId { set; get; }
        public virtual Category? Category { set; get; }
        public float Price { get; set; }
		public string Image { get; set; } // path to image
		public string Mime { get; set; } // type of image


	}
}
