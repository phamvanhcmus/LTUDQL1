using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food_Recipe
{
	public static class StringExtension_Ver2
	{
		public static bool isEmpty(this string str)
		{
			return str.Length == 0;
		}
	}
}
