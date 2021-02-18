using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food_Recipe
{
    class PagingInfo
    {
		public int RowsPerPage { get; set; }
		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }
		public int TotalItems { get; set; }
		public List<string> Pages
		{
			get
			{
				var result = new List<string>();

				for (var i = 1; i <= TotalPages; i++)
				{
					result.Add($"Trang {i} / {TotalPages}");
				}

				return result;
			}
		}
	}
}
