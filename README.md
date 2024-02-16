Framework: .NET 8.0 – wykorzystany do budowy aplikacji webowej z użyciem ASP.NET Core, co zapewnia zaawansowane narzędzia i biblioteki do tworzenia dynamicznych aplikacji internetowych.

Baza danych: SQL Server – wybrana ze względu na skalowalność, bezpieczeństwo oraz wsparcie dla transakcji, co jest kluczowe dla aplikacji wymagających niezawodnego zarządzania danymi.

Dodatkowe biblioteki:

Microsoft.EntityFrameworkCore.SqlServer w wersji 8.0.1 – umożliwia integrację z bazami danych SQL Server przy użyciu Entity Framework Core, co ułatwia mapowanie obiektowo-relacyjne (ORM).

Microsoft.AspNetCore.Identity.EntityFrameworkCore w wersji 8.0.1 – wspiera system uwierzytelniania i zarządzania użytkownikami w aplikacjach ASP.NET Core.

Microsoft.EntityFrameworkCore.Tools w wersji 8.0.1 – zawiera narzędzia pomocne przy pracy z Entity Framework Core, np. do tworzenia migracji.

Dane przykładowych użytkowników:



Admin:

UserName: "admin@example.com"

Email: "admin@example.com"

Hasło: "AdminPassword123!"

Rola: "Admin"

Użytkownik:

UserName: "user@example.com"

Email: "user@example.com"

Hasło: "UserPassword123!"

Rola: "User"

Proces uruchomienia aplikacji:

Aby uruchomić aplikację, konieczne jest wcześniejsze przygotowanie bazy danych. Wymaga to zainstalowania narzędzia dotnet-ef za pomocą komendy:



csharp

Copy code

dotnet tool install --global dotnet-ef

Następnie, upewniając się, że w pliku konfiguracyjnym ustawiony jest odpowiedni connection string do bazy danych, należy wykonać komendę:



sql

Copy code

dotnet ef database update

która zastosuje przygotowane migracje do bazy danych, tworząc lub aktualizując jej schemat.



Opis własnych funkcji aplikacji:

Aplikacja oferuje zestaw operacji CRUD (Create, Read, Update, Delete) na obiektach typu Book oraz Author, co umożliwia zarządzanie biblioteką książek i informacjami o autorach. Zintegrowany system uwierzytelniania i zarządzania użytkownikami, wykorzystujący Microsoft.AspNetCore.Identity, umożliwia rejestrację, logowanie i wylogowywanie użytkowników. Dodatkowo, aplikacja na etapie pierwszego uruchomienia automatycznie inicjalizuje początkowe role ("Admin" i "User") oraz tworzy dwóch przykładowych użytkowników z przypisanymi rolami, co ułatwia zarządzanie dostępem i uprawnieniami w aplikacji.
