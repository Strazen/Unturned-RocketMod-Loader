### Dökümantasyon

## Nedir bu?

Bu Unturned denen oyunda RocketMod Frameworkü ile yapılmış bir eklenti yükleyicisidir, isime göre yükler.

## Neden koydum?

Bu tarz projelerin özellikle Türk geliştirici topluluğuna faydalı olabileceğini düşündüğüm için.

## Geliştirilmeye devam edilecekmi?

Aslında hayır, ancak RocketMod frameworküne bir güncelleme tarzında bir şey gelirse elimden geldiğinde güncelleştirmeye çalışacağım.

## Uyarı!

Bu yapılan eklenti yükleyicisi üzerinde durulmamış bir şablondur, geliştirilmesi gerekir. Her yükleyici gibi bunu da işi bilen birisi kırabilir.

## Obfuscate edebilirmiyim?

Evet, projede MIT lisansı var istediğiniz gibi düzenleyebilirsiniz. Hatta isterseniz pazarlayabilirsiniz bile :)

## Nasıl obfuscate etmeliyim? (Nasıl şifrelemeliyim)

Yakında GitHub sayfamda obfuscator (Şifreleyici) paylaşacağım (Söz vermiyorum), onu kullanabilirsin. Bu konuda diyebileceğim tek bir şey var, o da ConfuserEx kullanmamandır. Çünkü ConfuserEx Unpackerlar ortalıkta geziyor, yoldan geçen birisi bile eklenti yükleyicini kırabilir.

## Nasıl çalışıyor?

Yükleyici eklentilerinizi attığınız website (Apache Server'de olur) den isime göre çekiyor ve çalışıyor.

## Hatalara veya sorulara bakıyormusun?

Zamanım oldukça evet, bir hata olursa bana ulaşmaktan çekinme. Discord adresim: **Strazen#2827**

## Nasıl eklenti ekleyeceğim?

Kodların içerisinde **wc.DownloadData($"http://URL/licenses/{Configuration.Instance.Lisanslar[i]}.dll")** kısmındaki URL kısımına website URL'ni yaz (veya apache server adresini) ardından website klasörlerine gir, eğer XAMPP kullanıyorsan **C:/xampp/htdocs** klasörü olacaktır diye tahmin ediyorum. Ardından **licenses** adında bir klasör oluştur ve eklentilerini o klasörün içerisinde at, ardından attığın eklentinin (.DLL dosyası) ismini eklentinin yapılandırma dosyasına (.configuration.xml) yaz. Ancak merak etme, yakında açık kaynak kodlu eklenti yükleyicisi paylaşacağım.
