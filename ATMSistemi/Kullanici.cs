using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMSistemi
{
    internal class Kullanici 
    {
        public Hesap Hesabim {  get; set; } //Hesap tipinde bir nesne tutacağım.
        public string KullaniciAdi {  get; set; }
        public string PIN {  get; set; } //başında 0 olan pin de mümkün olabilir.
        public int HesapNumarasi {  get; set; }
        public int Hesap {  get; set; }

        public List<Islem> IslemGecmisi {  get; set; }
        public Kullanici()
        {
            IslemGecmisi = new List<Islem>(); //her yeni Kullanici oluşturulduğunda işlem geçmişi otomatik olarak boş bir listeyle gelir. 
        }
    }
}
