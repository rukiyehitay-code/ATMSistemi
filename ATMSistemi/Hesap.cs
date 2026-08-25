using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMSistemi
{
    internal class Hesap
    {
        public int HesapNumarasi{get; set;}
        public decimal Bakiye { get; set;} //parasal işlemlerde decimal daha güvenilir.

        public void ParaYatir(decimal miktar)
        {
            Bakiye += miktar;
        }
        public bool ParaCek(decimal miktar)
        {
            if (miktar <= Bakiye)
            {
                Bakiye -= miktar;
                return true;
            }
            else
            {
                Console.WriteLine("İşleminiz Gerçekleştirilemedi! ");
                return false;
            }
        }
    }
}
