using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf.Models
{
    class PatientDTO<T>
    {
        public string Id { get; set; }
        public List<T> Services { get; set; }
    }
}
