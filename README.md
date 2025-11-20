# Search Engine Service  

Farklı içerik sağlayıcılarından (JSON + XML) gelen verileri toplayıp normalize eden, skorlayıp tek bir arama motoru üzerinden erişilebilir hale getiren bir **Arama Motoru Servisi**.

Bu proje kapsamında:

- Farklı provider'lar (JSON/XML) entegre edildi  
- İçerikler standart ContentItem modeline dönüştürüldü  
- Gelişmiş bir puanlama & alakalılık algoritması uygulandı  
- API üzerinden arama, filtreleme, sıralama ve pagination sağlandı  
- Basit bir Dashboard UI ile sonuçlar görselleştirildi  
- Clean Architecture prensipleri ile ölçeklenebilir bir yapı kuruldu  
- Birim testler (xUnit) + API entegrasyon testleri gerçekleştirildi  

---

# 🚀 Teknoloji Tercihleri

### Backend
- .NET Core 9 / ASP.NET Core Web API  
- Entity Framework Core  
- PostgreSQL  
- HttpClient  
- InMemory Cache  
- Swagger  
- xUnit Test Framework

### Neden Bu Teknolojiler?

| Teknoloji | Tercih Nedeni |
|----------|----------------|
| **.NET Core 9** | Modern API geliştirme, yüksek performans |
| **Clean Architecture** | Provider’lar ve iş mantığı birbirinden tamamen ayrılır. Kod modüler, genişlemeye açık (Open/Closed), test edilebilir ve bakım maliyeti düşük olur. Bu proje özelinde JSON ve XML provider eklemenin çok kolay olmasını sağlar. |
| **Entity Framework Core** | Hızlı geliştirme, güçlü LINQ desteği ve PostgreSQL ile uyumlu migration yapısı sunar. Okunabilir ve sürdürülebilir veri erişimi sağlar. |
| **PostgreSQL** | Yüksek performanslı, ölçeklenebilir, kurumsal projelerde tercih edilen bir açık kaynak DB. JSON/XML desteği güçlüdür, bu nedenle provider verisini depolamak için idealdir. |
| **InMemory Cache** | Düşük latency, sık yapılan arama sonuçlarının 30 saniyelik kısa ömürlü cache ile tutulması sorgu performansını ciddi şekilde artırır. Ek olarak Redis veya distributed cache’e kolay geçiş sağlar. |
| **HttpClient** | Provider entegrasayonlarında bağımsız olarak kullanılır. |
| **xUnit** | .NET ekosisteminde standart test framework. Mock tabanlı unit testler ve API endpoint testleri için ideal. Clean Architecture ile doğal bir uyum sağlar. |

---

# 🧱 Mimari Yapı

```

/SearchEngine.Api              → API Katmanı (Controllers, Swagger)
/SearchEngine.Application      → İş Mantığı (SearchService, Scoring Algorithms)
/SearchEngine.Domain           → Entity Modelleri (ContentItem)
/SearchEngine.Infrastructure   → Provider'lar + EF Core Repo + DB + Caching

```

Avantajlar:
- Domain izolasyonu  
- Provider eklemek çok kolay  
- Test edilebilirlik mümkün  
- Performans ve maintainability yüksek

# 🏛 Kullanılan Mimari: Clean Architecture

Bu projede **Clean Architecture** yaklaşımı tercih edilmiştir. Amaç; provider'lara bağımlı olmayan, kolay test edilebilir, modüler, genişletilebilir ve uzun vadede sürdürülebilir bir yapı oluşturmaktır.

Clean Architecture’ın temel prensibi:

> İş kuralları ve domain modelleri dış katmanlardan etkilenmemeli; bağımlılıklar **dıştan içe doğru** akmalıdır.

---

# 🔗 Provider Entegrasyonu

Proje iki provider’dan veri alır:

### 1) JSON Provider  
### 2) XML Provider  

Her provider farklı format döndürür, fakat uygulama hepsini **ContentItem** modeline normalize eder.  

Yeni provider eklemek için:  
`IContentProvider` implement etmek yeterlidir.

# 🔍 API Özellikleri

## GET `/api/search`

Parametreler:

| Parametre | Açıklama |
|----------|----------|
| `query` | Anahtar kelime |
| `typeFilter` | video / text |
| `sortBy` | score / publishedTime |
| `page` | sayfa numarası |
| `pageSize` | sayfa boyutu |

## POST `/api/providers/sync`
- Provider'ları tetikler  
- JSON ve XML veri çekilir  
- Normalize edilip DB’ye yazılır  

---

# 🧪 Test Stratejisi

### Unit Testler:
- Skor hesaplama  
- Search filter/sort/pagination testleri  

### Provider Testleri:
- JSON parse testi  
- XML parse testi  

### API Entegrasyon Testleri:
- `/api/search` → 200 OK  
- `/api/providers/sync` → 200 OK  

Test klasörü:
```

/SearchEngine.Tests

````

---

# 🗄 PostgreSQL Yapısı

### Migration:
```bash
dotnet ef migrations add InitialCreate -p SearchEngine.Infrastructure -s SearchEngine.Api
dotnet ef database update -p SearchEngine.Infrastructure -s SearchEngine.Api
````
---

# 💾 Cache Yönetimi

* InMemory Cache decorator yapısı kullanılır
* Key: `query-type-page-pageSize`
* Süre: 30 saniye
* Aynı arama tekrarlandığında DB çağrısı yapılmaz

---

# 🖥 Dashboard UI

Dosya: `wwwroot/dashboard.html`

<img width="1700" height="327" alt="image" src="https://github.com/user-attachments/assets/58487dcf-186f-4364-8a70-80fc6b8c5681" />


Özellikler:

* Fetch API ile `/api/search` çağrısı
* Skor, tarih, başlık listelenir
* Basit sorter
* Responsive görselleştirme

---

# ▶ Projeyi Çalıştırma

### 1) Repo'yu klonla

```bash
git clone <repo-url>
cd SearchEngine
```

### 2) PostgreSQL’i başlat

(Docker veya local)

### 3) Migration çalıştır
Dotnet EF yüklü ise 
```bash
dotnet ef database update -p SearchEngine.Infrastructure -s SearchEngine.Api
```
Değil ise ;
Nuget Package Manager > Package Manager Console açılır. Aşağıdaki komut çalıştırılır.

```bash
update-database
```

### 4) API’yi başlat

```bash
dotnet run --project SearchEngine.Api
```

### 5) Swagger UI

[http://localhost:5000/swagger](http://localhost:5000/swagger)

### 6) Dashboard

[http://localhost:5000/dashboard.html](http://localhost:5000/dashboard.html)

---

# 📦 Sonuç

Bu proje:

* Clean Architecture
* Test odaklı gelişmiş API
* JSON + XML provider entegrasyonu
* Normalize edilmiş içerik modeli
* Gelişmiş skor algoritması
* Cache destekli hızlı API
* Dashboard UI

özelliklerini başarıyla karşılayan modern bir arama motoru servisidir.


**Hazırlayan:**
*Mert — Backend Developer (.NET)*

```
