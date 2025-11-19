
# Search Engine API Documentation

Bu doküman, SearchEngine API servisinin tüm endpoint'lerini, request/response modellerini, hata yapısını ve kullanım detaylarını içerir.

---

# 📌 Genel Bilgiler

- Base URL (Local):  


[http://localhost:5000](http://localhost:5000)


- Formatlar:  
- Request: JSON / QueryString  
- Response: JSON

- Authentication:  
- Gerekmiyor (public endpoints)

---

# 📚 Endpoint Listesi

| HTTP | Endpoint | Açıklama |
|------|----------|-----------|
| GET | `/api/search` | İçerik arama + filtre + sıralama + pagination |
| POST | `/api/providers/sync` | Provider'lardan veri çekme ve veritabanına kaydetme |

---

# 🔍 1. Search Endpoint

## GET `/api/search`

Bu endpoint; kullanıcı aramaları, filtreler, sıralama ve sayfalama parametreleri üzerinden içerik araması yapar.

---

## 🧩 Query Parametreleri

| Parametre | Tip | Zorunlu | Açıklama |
|-----------|------|----------|----------|
| `query` | string | hayır | Başlık veya açıklama içinde aranacak anahtar kelime |
| `typeFilter` | enum (0=Article, 1=Video) | hayır | İçerik türüne göre filtre |
| `sortBy` | string | hayır | `publishedTime` veya `score` (varsayılan: title) |
| `page` | int | hayır | Varsayılan: `1` |
| `pageSize` | int | hayır | Varsayılan: `10` |

---

## 🧪 Örnek İstek

```

GET /api/search?query=go&typeFilter=1&sortBy=score&page=1&pageSize=5

````

---

## 📤 Örnek Response

```json
{
  "totalCount": 150,
  "items": [
    {
      "title": "Go Programming Tutorial",
      "type": 1,
      "score": 12.5,
      "providerName": "JsonProvider",
      "url": "https://...",
      "publishedTime": "2024-03-15T10:00:00Z"
    }
  ]
}
````

---

## 📌 Response Modeli

### `SearchResultDto`

| Alan         | Tip                       | Açıklama                             |
| ------------ | ------------------------- | ------------------------------------ |
| `totalCount` | int                       | Aranan kriterlere uygun toplam sonuç |
| `items`      | List<SearchResultItemDto> | Sayfadaki içerikler                  |

### `SearchResultItemDto`

| Alan            | Tip      | Açıklama                                     |
| --------------- | -------- | -------------------------------------------- |
| `title`         | string   | İçerik başlığı                               |
| `type`          | enum     | Article / Video                              |
| `score`         | double   | Skorlama algoritmasına göre hesaplanan değer |
| `providerName`  | string   | İçeriği sağlayan provider                    |
| `publishedTime` | datetime | Yayınlanma zamanı                            |
| `url`           | string   | İçerik linki                                 |

---

# 🔄 2. Provider Sync Endpoint

## POST `/api/providers/sync`

JSON + XML provider'lardan içerikleri çekerek normalize eder ve `ContentItems` tablosuna UPSERT (insert/update) olarak yazar.

---

## 📌 Upsert Mantığı

| Koşul                                     | İşlem  |
| ----------------------------------------- | ------ |
| ProviderName + ProviderItemId bulunamazsa | INSERT |
| ProviderName + ProviderItemId bulunursa   | UPDATE |

Bu davranış duplicate key hatalarını engeller.

---

## 🧪 Örnek Response

```json
{
  "success": true,
  "message": "Providers synced successfully"
}
```

---

# ⚠ Hata Yapısı

API tüm hataları aşağıdaki formatta döndürür:

```json
{
  "error": "BadRequest",
  "message": "Invalid type filter",
  "status": 400
}
```

---

# 🔢 HTTP Response Kodları

| Kod                         | Açıklama           |
| --------------------------- | ------------------ |
| **200 OK**                  | Başarılı           |
| **400 BadRequest**          | Geçersiz parametre |
| **404 NotFound**            | Kayıt bulunamadı   |
| **500 InternalServerError** | Beklenmeyen hata   |

---

# 🔐 Rate Limit & Cache

Search endpoint'inde:

* InMemory Cache devreye girer
* Aynı query + typeFilter + sortBy + page + pageSize sorguları **30 saniye cache**
* Provider Sync sonrası cache temizlenir

---

# 📊 Pagination Yapısı

```
GET /api/search?page=2&pageSize=10
```

Response:

* `totalCount`: tüm sonuç sayısı
* `items`: mevcut sayfa verileri

---

# 📁 Dashboard

UI dosyası:

```
wwwroot/dashboard.html
```

* Fetch API ile search endpoint'i çağırır
* Title, Type, Score, Date sütunları
* Basit sıralama ve filtreleme
* Responsive mini UI

---

