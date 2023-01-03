using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ResponseValue<T> where T : class
    {
        public bool IsSucess { get; set; }
        public string ErrorMessage { get; set; }
        public T ObjectValue { get; set; }
    }
}
