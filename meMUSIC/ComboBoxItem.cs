using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meMUSIC
{
    public class ComboBoxItem
    {
        public string hienThi { get; set; }
        public object Khoa { get; set; }

        public override string ToString()
        {
            return hienThi;
        }
    }
}
