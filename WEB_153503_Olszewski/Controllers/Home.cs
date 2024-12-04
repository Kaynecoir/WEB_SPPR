using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_153503_Olszewski.Models;

namespace WEB_153503_Olszewski.Controllers
{
	public class Home : Controller
	{
		IEnumerable<ListDemo> list = new List<ListDemo>() {
			new ListDemo() { ID = 1, Name = "One" },
			new ListDemo() { ID = 2, Name = "Two" },
			new ListDemo() { ID = 3, Name = "Three" },
		};
		public IActionResult Index()
		{
			ViewData["LabNumber"] = "Лабораторная работа 2";
			ViewBag.SelectList = new SelectList(list, "ID", "Name");
			
			return View();
		}
	}
}
