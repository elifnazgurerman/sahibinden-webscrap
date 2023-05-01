
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace WebScrap
{
    class Program
    {
        static void Main(string[] args)
        {
            // dizi tanımlamaları 
            List<string> li_link = new List<string>();
            List<string> li_title = new List<string>();
            List<string> li_price = new List<string>();

            // sayfa içeriği alma
            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load("https://www.sahibinden.com/");

            // ilan linklerini ve isimlerini alma
            foreach (var item in doc.DocumentNode.SelectNodes("//ul[@class='vitrin-list clearfix']//a"))
            {
                li_link.Add(item.GetAttributeValue("href", ""));
                li_title.Add(item.GetAttributeValue("title", ""));
            }

            // detay sayfasına girip fiyat alma
            foreach (var link in li_link)
            {
                HtmlAgilityPack.HtmlDocument doc1 = web.Load(link);
                var price= doc1.DocumentNode.SelectSingleNode("//span[@class='sticky-header-attribute price']");
                li_price.Add(price.InnerText);
            }


            // ilan isimleri ve fiyatları liste şeklinde yazdırma
            for (int i = 0; i < li_price.Count; i++)
            {
                Console.WriteLine("ilan: " + li_title[i] + " fiyat: " + li_price[i]);
            }

            // dosya oluşturma
            StreamWriter Yaz = new StreamWriter("C:\\Yaz.txt");

            // dosya yolu belirtme
            string DosyaYolu = "C:\\Yaz.txt";

            // dosyanın varlığını kontrol edip varsa fiyat ve isimleri dosyaya kaydetme
            if (File.Exists(DosyaYolu))
            {
                for (int j = 0; j < li_price.Count; j++)
                {
                    Yaz.WriteLine("ilan: " + li_title[j] + " fiyat: " + li_price[j]);
                }
            }
            else
            {
                Console.WriteLine("Dosya bulunamadı.");
            }
            Yaz.Close();
        }
    }
}
