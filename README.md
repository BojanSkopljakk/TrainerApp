# 🏋️‍♂️ Trainer App

**Trainer App** je ASP.NET Core MVC aplikacija za zakazivanje treninga. Omogućava korisnicima da zakažu ili otkažu trening, a trenerima da upravljaju sopstvenim kalendarom i sesijama.

---

## 🚀 Funkcionalnosti

- ✅ Zakazivanje treninga od strane korisnika
- ✅ Otkazivanje treninga (minimum 24h ranije)
- ✅ Trener vidi svoj nedeljni ili dnevni raspored
- ✅ Trener može da zakaže ili otkaže trening u ime korisnika
- ✅ Email notifikacije korisniku i treneru putem Mailtrap-a


---

## 🧑‍💻 Tehnologije

- **Backend:** ASP.NET Core 8, Entity Framework Core
- **Frontend:** Razor Views (MVC)
- **Baza:** SQL Server 
- **Email servis:** MailKit + Mailtrap


---

## 🛠️ Podešavanje

### 1. Kloniraj repozitorijum

```bash
git clone https://github.com/BojanSkopljakk/TrainerApp.git
```
### 2. Konfiguracija appsetings

U appsetings konfigurisati bazu i email po sablonu:
```json
"ConnectionStrings": {
  "DefaultConnection": 
},
"Email": {
  "From": "no-reply@trainerapp.local",
  "Username": "MAILTRAP_USERNAME",
  "Password": "MAILTRAP_PASSWORD",
  "SmtpHost": "sandbox.smtp.mailtrap.io",
  "Port": "2525"
}
```
### 3. Migracije
Potrebno je izvrsiti migracije za bazu

### 4. Pristup Trenerima

| Trener           | Access Code |
| ---------------- | ----------- |
| Ana Petrović     | ana123      |
| Marko Jovanović  | marko123    |
| Ivana Milinković | ivana123    |

## Poboljšanja

1. Poboljšati UI/UX Aplikacije



