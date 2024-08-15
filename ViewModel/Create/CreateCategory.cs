using Storee.Repository.Abstract;
using Storee.ViewModel.Get;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Storee.ViewModel.Update;

namespace Storee.ViewModel.Create
{
    public class CreateCategory 
    {
		public short Id { get; set; }
		public string NameAr { get; set; }
		public string NameEn { get; set; }
		public bool IsActive { get; set; } = true;
		
	}
}
