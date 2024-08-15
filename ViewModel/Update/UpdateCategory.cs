using Storee.Repository.Abstract;
using Storee.ViewModel.Create;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Storee.Validation;

namespace Storee.ViewModel.Update
{
    public class UpdateCategory : AbstractValidatableObject
    {
        public short Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
        public string pathImage { get; set; }

      
    }
}
