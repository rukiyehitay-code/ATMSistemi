using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMSistemi
{
    internal class Islem
    {
        public string IslemTuru { get; set; } //para yatırma - çekme, transfer gibi işlemler var.
        public decimal IslemTutari {  get; set; }
        public DateTime IslemZamani { get; set; }
    }
}
