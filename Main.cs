using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.API;
using Rocket.Core.Utils;
using Rocket.Unturned.Player;
using Rocket.Unturned;
using Rocket.Core.Plugins;
using Rocket.Core;
using System.Net;
using System.Reflection;
using System.IO;
using UnityEngine;

namespace RocketModLoader
{
    public class Main : RocketPlugin<Configuration>
    {
        protected override void Load()
        {
            WebClient wc = new WebClient();

            Console.WriteLine("[RocketModLoader] >> Loader versiyonu kontrol ediliyor!", Console.ForegroundColor = ConsoleColor.Yellow);
            //Versiyon'u indirir, belirli dizine version.html adında bir dosya aç ve içerisine 1.0 yaz.
            var version = wc.DownloadString("http://URL/licenses/version.html");
            //Eğer versiyon değiştiyse
            if(version != "1.0")
            {
                Console.WriteLine("[RocketModLoader] >> Loader versiyonu guncel degil, yeni versiyon indiriliyor.", Console.ForegroundColor = ConsoleColor.Yellow);
                //Yeni versiyonu indirir, versiyon güncellediğiniz zaman RocketModLoader1.0 şeklinde yazın. (1.0 yeni version olacak, version güncellendiğinde if in içerisindeki versiyonu da güncellenen versiyon yapın.)
                wc.DownloadFile($"http://URL/licenses/RocketModLoader{version}", Rocket.Core.Environment.PluginsDirectory);
                Console.WriteLine($"[RocketModLoader] >> Loader'in yeni versiyonu guncellendi, {Rocket.Core.Environment.PluginsDirectory} adresine RocketModLoader{version} adinda kuruldu.", Console.ForegroundColor = ConsoleColor.Green);
            }
            //Eğer versiyon güncelse
            else
            {
                Console.WriteLine("[RocketModLoader] >> IP Adresiniz kontrol ediliyor, lutfen bekleyin.", Console.ForegroundColor = ConsoleColor.Yellow);

                //Whitelist ve blacklist dosyalarını indiriyor, birisini blacklist'e veya whitelist'e eklemek isterseniz alt alta yazın ip leri)
                string whitelist = new System.Net.WebClient() { Proxy = null }.DownloadString("http://URL/licenses/whitelist.html");
                string blacklist = new System.Net.WebClient() { Proxy = null }.DownloadString("http://URL/licenses/blacklist.html");
                
                //Eğer sunucu ip adresi blacklist de ise
                if (blacklist.Contains(ServerIP()))
                {
                    Console.WriteLine("[RocketModLoader] >> IP Adresiniz blacklist'de, lutfen yetkililer ile gorusun!", Console.ForegroundColor = ConsoleColor.Yellow);
                }
                
                //Değilse
                else
                {
                    Console.WriteLine("[RocketModLoader] >> IP Adresi temiz, whitelist kontrol ediliyor.", Console.ForegroundColor = ConsoleColor.Yellow);
                    //Eğer sunucu whitelist deyse
                    if (whitelist.Contains(ServerIP()))
                    {
                        Console.WriteLine("[RocketModLoader] >> IP Adresi dogrulandi, lisanslar aktif ediliyor!", Console.ForegroundColor = ConsoleColor.Green);
                        
                        //Config dosyasına yazılan her lisans için kontrol eder
                        for (int i = 0; i < Configuration.Instance.Lisanslar.Count; i++)
                        {
                            //Lisansı yüklemeyi dener
                            try
                            {
                                //Eklentiyi indirir
                                byte[] rawBytes = wc.DownloadData($"http://URL/licenses/{Configuration.Instance.Lisanslar[i]}.dll");
                                //Eklentiyi sunucuya enjekte eder
                                foreach (Type tip in RocketHelper.GetTypesFromInterface(Assembly.Load(rawBytes), "IRocketPlugin"))
                                {
                                    GameObject hedef = new GameObject(tip.Name, new Type[]
                                    {
                                tip
                                    });
                                    DontDestroyOnLoad(hedef);
                                    //Konsol çıktısı verir
                                    Console.WriteLine(">> " + $"{tip.Assembly.FullName} adli eklenti aktif edildi.", Console.ForegroundColor = ConsoleColor.Green);
                                }
                            }
                            //Eğer lisans yanlışsa
                            catch (WebException webEx)
                            {
                                Console.WriteLine("[RocketModLoader] >> Lisans kodunuz hatali!", Console.ForegroundColor = ConsoleColor.Yellow);
                                //Yanlış bir lisansta işlemleri yapmaya devam eder
                                return;
                            }
                        }
                    }
                    //Eğer whitelist de yoksa
                    else 
                    {
                        Console.WriteLine("[RocketModLoader] >> Whitelist de edilsiniz!", Console.ForegroundColor = ConsoleColor.Yellow);
                    }
                }

            }

        }

        protected override void Unload()
        {
            Console.WriteLine("[RocketModLoader] >> Loader deaktif edildi!", Console.ForegroundColor = ConsoleColor.Red);
        }


        //Sunucu IP adresini almaya yarar
        private static string ServerIP()
        {
            String address = "";
            WebRequest request = WebRequest.Create("http://checkip.dyndns.org/%22");
            using (WebResponse response = request.GetResponse())

            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                address = stream.ReadToEnd();
            }

            int first = address.IndexOf("Address: ") + 9;
            int last = address.LastIndexOf("</body>");
            address = address.Substring(first, last - first);

            return address;
        }
    }
}
