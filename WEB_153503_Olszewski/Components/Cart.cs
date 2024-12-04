using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WEB_153503_Olszewski.Components
{
	[ViewComponent]
	public class Cart
	{
		public string Invoke()
		{
			float price = 12.3f;
			int count = 2;
			string res = $"{price} руб ({count})";
			return res;
		}
		[HttpPost]
		public string Add(int id) 
		{
			return $"Your id = {id}";
		}
	}
}
