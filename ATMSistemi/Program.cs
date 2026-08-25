using ATMSistemi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        Kullanici k1 = new Kullanici();
        k1.KullaniciAdi = "Rukiye";
        k1.PIN = "1234";
        k1.HesapNumarasi = 1001;
        Kullanici k2 = new Kullanici();
        k2.KullaniciAdi = "Ahmet";
        k2.PIN = "5678";
        k2.HesapNumarasi = 1002;

        Hesap h1 = new Hesap();
        h1.HesapNumarasi = 1001;
        h1.Bakiye = 5000;
        Hesap h2= new Hesap();
        h2.HesapNumarasi = 1002;
        h2.Bakiye = 3000;

        k2.Hesabim = h2;
        k1.Hesabim = h1;

        List < Kullanici >Kullanicilar= new  List <Kullanici>(); //liste 'Kullanici' türünde.
        Kullanicilar.Add(k1);
        Kullanicilar.Add(k2);

        bool kullaniciBulundu = false;

        Console.Write("Kullanıcı Adınızı Giriniz: ");
        string isim = Console.ReadLine();
        
        foreach(Kullanici kullanici in Kullanicilar)
        {
            if (kullanici.KullaniciAdi == isim)
            {
                Console.WriteLine("Kullanıcı Bulundu!");
                bool girisBasarisi = false;
                kullaniciBulundu = true;

                for(int i =0; i < 3; i++){
                    Console.Write("PIN Giriniz: ");
                    string sifre = Console.ReadLine();
                    if (kullanici.PIN == sifre)
                    {
                        Console.WriteLine("Giriş Başarılı!");
                        girisBasarisi = true;
                        break; //PIN doğru olduğunda döngüden çık.
                    }
                    else
                    {
                        Console.WriteLine("PIN Yanlış!");
                    }
                }
                if (girisBasarisi == false)
                {
                    Console.WriteLine("3 kez yanlış PIN girdiniz. Hesabınız kilitlendi.");
                } 
                if (girisBasarisi == true)
                {
                    while (true)
                    {
                        Console.WriteLine("==== ATM SİSTEMİ ====");
                        Console.WriteLine("1 - Bakiye Görüntüle");
                        Console.WriteLine("2 - Para Yatır");
                        Console.WriteLine("3 - Para Çek");
                        Console.WriteLine("4 - Para Transferi");
                        Console.WriteLine("5 - İşlem Geçmişi");
                        Console.WriteLine("6 - Çıkış");

                        Console.Write("Seçiminizi yapınız: ");
                        int secim = Convert.ToInt32(Console.ReadLine());

                        switch (secim)
                        {
                            case 1:
                                Console.WriteLine("Bakiye görüntüleme seçildi.");
                                Console.WriteLine($"Bakiyeniz: { kullanici.Hesabim.Bakiye } TL"); //giriş yapan kullanıcının hesabının bakiyesi.
                                break;

                            case 2:
                                Console.WriteLine("Para yatırma işlemi seçildi.");
                                Console.Write("Yatırmak İstediğiniz Tutarı Girin: ");
                                decimal tutar= Convert.ToDecimal(Console.ReadLine());

                                if (tutar > 0)
                                {
                                    kullanici.Hesabim.ParaYatir(tutar);

                                    Islem yeniIslem = new Islem();
                                    yeniIslem.IslemTuru = "Para Yatırma";
                                    yeniIslem.IslemTutari = tutar;
                                    yeniIslem.IslemZamani = DateTime.Now;
                                    kullanici.IslemGecmisi.Add(yeniIslem);
                                    Console.WriteLine("İşleminiz gerçekleştirildi.");
                                }
                                else
                                {
                                    Console.WriteLine("Geçerli tutar giriniz!");
                                }
                                 break;

                            case 3:
                                Console.WriteLine("Para çekme işlemi seçildi.");
                                Console.Write("Çekmek İstediğiniz Tutarı Girin: ");
                                decimal tutar2= Convert.ToDecimal(Console.ReadLine());

                                if (tutar2 >0)
                                {
                                    bool sonuc = kullanici.Hesabim.ParaCek(tutar2);

                                    if (sonuc == true)
                                    {
                                        Console.WriteLine("İşleminiz gerçekleştirildi.");
                                        Islem yeniIslem2 = new Islem();
                                        yeniIslem2.IslemTuru = "Para Çekme";
                                        yeniIslem2.IslemTutari = tutar2;
                                        yeniIslem2.IslemZamani = DateTime.Now;
                                        kullanici.IslemGecmisi.Add(yeniIslem2);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Yetersiz Bakiye!");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Geçerli tutar giriniz!");
                                }
                                break;

                            case 4:
                                Console.WriteLine("Para transferi menüsü seçildi.");
                                Console.Write("Transfer yapılacak kullanıcı adını girin: ");
                                string aliciAdi=Console.ReadLine();
                                bool aliciBulundu = false;
                                foreach(Kullanici alici in Kullanicilar)
                                {
                                    if (aliciAdi == alici.KullaniciAdi)
                                    {
                                        aliciBulundu = true;

                                        if (alici.KullaniciAdi == kullanici.KullaniciAdi)
                                        {
                                            Console.WriteLine("Kendinize para transferi yapamazsınız!");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Alıcı Bulundu!");
                                            Console.Write("Transfer tutarını girin: ");
                                            decimal transferTutari = Convert.ToDecimal(Console.ReadLine());

                                            if(transferTutari > 0)
                                            {
                                                if (transferTutari <= kullanici.Hesabim.Bakiye)
                                                {
                                                    kullanici.Hesabim.ParaCek(transferTutari);
                                                    alici.Hesabim.ParaYatir(transferTutari);
                                                    Console.WriteLine("Transfer Başarılı");
                                                    Islem yeniIslem3 = new Islem();
                                                    yeniIslem3.IslemTuru = "Transfer";
                                                    yeniIslem3.IslemTutari = transferTutari;
                                                    yeniIslem3.IslemZamani = DateTime.Now;
                                                    kullanici.IslemGecmisi.Add(yeniIslem3);
                                                    
                                                    Islem yeniIslem4= new Islem();
                                                    yeniIslem4.IslemTuru = "Gelen Transfer";
                                                    yeniIslem4.IslemTutari= transferTutari;
                                                    yeniIslem4.IslemZamani= DateTime.Now;
                                                    alici.IslemGecmisi.Add(yeniIslem4);
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Bakiyeniz Yetersiz!");
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Geçerli tutar giriniz.");
                                            }
                                           
                                        }
                                    }
                                }
                                if (!aliciBulundu)
                                {
                                    Console.WriteLine("Alıcı Bulunamadı!");
                                }
                                break;

                            case 5:
                                Console.WriteLine("İşlem geçmişi menüsü seçildi.");
                                foreach(Islem islem in kullanici.IslemGecmisi)
                                {
                                    Console.WriteLine($"{islem.IslemTuru} - {islem.IslemTutari} TL - {islem.IslemZamani} ");
                                }
                                break;

                            case 6:
                                Console.WriteLine("Çıkış yapıldı.");
                                return; // Main metodundan çıkar, programı bitirir.
                        }
                    }
                   
                }
            }
        }
        if (!kullaniciBulundu)
        {
            Console.WriteLine("Kullanıcı Bulunamadı!");
        }
    }
}