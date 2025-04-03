# Naturguiden prototyp

Denna README beskriver hur du startar vår prototyp av Naturguiden.
## Krav
Följande program/verktyg måste installeras på datorn som ska köra prototypen.

- [Git Bash](https://git-scm.com/downloads) version 2.47.1 eller högre

- [Node.js](https://nodejs.org/) version 20.9.0 eller högre

- [NPM](https://docs.npmjs.com/downloading-and-installing-node-js-and-npm) version 10.1.0 eller högre

- [Microsoft .NET Framework 9.0](https://dotnet.microsoft.com/en-us/download) (kan funka med lägre versioner, egen risk)

- [PostgreSQL 16](https://www.postgresql.org/download/) (kan funka med lägre versioner, egen risk)

- [pgAdmin 4](https://www.pgadmin.org/download/) (ej krav men starkt rekommenderat för att följa denna readme)
## Beskrivning av strukturen
I detta repository finns 2st mappar:

- **front-end** (här finns ett React projekt som driver själva hemsidan)
- **back-end** (här finns en .NET Core projekt som driver vårt API)

samt en fil `naturguiden_prototype_db.backup` som du kommer använda för att återskapa vår databas på din egna dator.

## Skapa databasen
> [!NOTE]  
> Vi rekommenderar att du skapar databasen lokalt på din egna dator. Det vill säga, att du inte använder MAU postgres server.
1. Skapa en ny databas i pgAdmin:
2. Högerklicka på Databaser → Create → Database
3. Namnge den t.ex. naturguiden_kamratgranskning_db (eller valfritt namn).
4. Klicka på Save.
5. Högerklicka på naturguiden_kamratgranskning_db → "Restore"
6. Välj filen `naturguiden_prototype_db.backup`.
7. Klicka på Restore.
####
Nu ska din databas förhoppningsvis innehålla tre tables:
- nature_spots
- place_visits
- users
## Skapa back-end
1. Navigera till `back-end` foldern och öppna en terminal (Git bash förslagsvis) där
2. Slå commandot `dotnet restore`
3. Slå commandot `dotnet build`
4. Slå commandot `dotnet run`
####
Får du inga error meddelanden så här långt finns det goda förhoppningar för att du kommer lyckas. Men det finns ett viktigt steg kvar innan back-end biten fungerar.

Öppna filen `appsettings.json`. Här ska du leta efter en nyckel som heter `DefaultConnection` som kommer innehålla en tom sträng. Du ska ersätta den tomma strängen med en "connection string" till databasen som du skapade tidigare. 

Du kan använda mallen för en connection string här nedan. Du måste ersätta vissa delar av den:

`Host=localhost;Database=<DATABAS_NAMN_HÄR>;Username=<DATABAS_ANVÄNDARNAMN_HÄR>;Password=<DATABAS_LÖSENORD_HÄR>`

*Hint: om du inte vet ditt postgres username, testa då "postgres" som username.*

Spara filen `appsettings.json`.

1. Slå commandot `dotnet build`
2. Slå commandot `dotnet run`
####
Nu är servern igång.
## Skapa front-end
1. Navigera till `front-end` foldern
2. Skapa en ny fil och döp den till `.env`
3. Lägg in följande rad i `.env`-filen: `NEXT_PUBLIC_API_URL="http://localhost:5001/api"`
4. Öppna en terminal (Git bash förslagsvis)
5. Slå commandot `npm i --force`
6. Slå commandot `npm run dev`
####
Nu är web-servern igång.
#
Om allt fungerat kan du nu öppna en webläsare och navigera till http://localhost:3000/ varifrån du kan använda Naturguiden.

> [!IMPORTANT]
> Hemsidan kommer kännas väldigt långsam när du navigerar mellan sidor. Detta är eftersom du kör hemsidan i development mode. Om du önskar köra hemsidan i production mode behöver du [bygga projektet](https://nextjs.org/docs/pages/building-your-application/deploying).
### Readme skriven av

- Thor Anderberg Nilsson (discord: thoranilsson)
