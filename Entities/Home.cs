using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Storee.Entities
{
	public class Home
	{
		public Home()
		{
			Product = new HashSet<Product>();
		}
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public short Id { get; set; }

		public string arTitle { get; set; }
		public string enTitle { get; set; }
		public string arLargeTitle { get; set; }
		public string enLargeTitle { get; set; }

        public virtual ICollection<Product> Product { get; set; }
	}
}
