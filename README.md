# Otomobil Kiralama - Windows Forms Uygulaması

Kısa Açıklama

Bu proje, .NET Framework 4.7.2 üzerinde geliştirilmiş bir Windows Forms (WinForms) otomobil kiralama uygulamasıdır. Firebase Realtime Database kullanılarak ödemeler ve kiralama kayıtları arka tarafta saklanır.

Önkoşullar

- Windows 10/11
- Visual Studio 2022/2026 veya benzeri (Windows Forms geliştirme desteği)
- .NET Framework 4.7.2
- İnternet bağlantısı (Firebase erişimi için)

Kurulum ve Çalıştırma

1. Depoyu klonlayın veya dosyaları indirin.
2. Visual Studio ile çözümü (otokiralama.slnx) açın.
3. Gerekli NuGet paketleri restore edilecektir (ör. FireSharp, Newtonsoft.Json). Paketleri manuel restore etmeniz gerekirse: `Tools > NuGet Package Manager > Restore`.
4. Çözümü derleyin (Build > Build Solution).
5. Programı başlatmak için F5 tuşuna basın veya Debug > Start Debugging.

Firebase Konfigürasyonu

- Projede Firebase ayarları Form dosyalarında (ör. Form1/Form4) doğrudan kullanılıyor. Örnek: `AuthSecret` ve `BasePath`.
- Güvenlik nedeniyle gerçek anahtar/secret değerlerini kaynak koda gömmeyin. Üretimde environment variable, konfigürasyon dosyası veya gizli yönetimi (Secret Manager) kullanın.

Proje Yapısı (Öne Çıkan Dosyalar)

- WindowsFormsApp1/Program.cs — Uygulama giriş noktası
- WindowsFormsApp1/Form1.cs, Form2.cs, Form3.cs, Form4.cs, Form5.cs — Formlar
- WindowsFormsApp1/Class3.cs — Form4'ün kullandığı veri modeli
- Properties/ — Uygulama ayarları ve kaynaklar

Kod Değişiklikleri ve Temizlik

- Bazı şablon sınıflar (Class1.cs, Class2.cs) kaldırıldı; proje derlendi ve çalışır durumda.

Katkıda Bulunma

1. Değişiklik yapmadan önce yeni bir branch oluşturun: `git checkout -b feature/isim`.
2. Değişiklikleri ekleyin: `git add -A`.
3. Commit yapın: `git commit -m "Kısa açıklama"`.
4. Uzak depoya gönderin: `git push -u origin feature/isim`.
5. Pull request açın ve değişiklikleri açıklayın.

## Konfigürasyon Örneği

Projede kullanılan hassas bilgiler (ör. Firebase `AuthSecret` ve `BasePath`) kaynak kodunda yer almamalıdır. Aşağıdaki örnek `.env.example` dosyasında hangi environment değişkenlerinin gerektiği gösterilmiştir. Gerçek değerleri `.env` veya güvenli bir gizli yönetim sistemi içinde saklayın.

Örnek `.env.example` içeriği:

```
FIREBASE_AUTH_SECRET=
FIREBASE_BASE_PATH=
```

Uygulamada bu değerleri okumak için kendi yükleme/konfigürasyon yönteminizi kullanın.

## Ekran Görüntüleri

Projeye ait örnek ekran görüntüleri `docs/screenshots/` dizinine eklenebilir. Şu anda yer tutucu olarak bu alanın var olduğunu belirtin veya kendi ekran görüntülerinizi ekleyin.

## Lisans

Bu proje için varsayılan lisans eklenmemiştir. Aşağıda MIT lisansının kısa bir açıklaması yer alır; uygun bulursanız proje köküne `LICENSE` dosyası olarak ekleyin.

Bu depoya MIT lisansı eklendi — detaylar için `LICENSE` dosyasına bakın.

## .gitignore önerisi

Gizli bilgileri yanlışlıkla commitlememek için `.gitignore` dosyanıza aşağıdakileri eklemeniz önerilir:

```
.env
bin/
obj/
.vs/
*.user
```

Sık Karşılaşılan Sorunlar

- Derleme hatası alırsanız NuGet paketlerini restore edin ve hedef framework sürümünü kontrol edin.
- Firebase bağlantı hatalarında internet erişimi ve BasePath/AuthSecret değerlerini kontrol edin.

Lisans

Bu proje için lisans belirtilmemiştir. Kendi kullanımınız veya paylaşımınız için uygun bir lisans eklemeniz önerilir.

İletişim

Detaylı yardım isterseniz proje sahibiyle GitHub üzerinden iletişime geçin veya repo içindeki Issue alanını kullanın.

