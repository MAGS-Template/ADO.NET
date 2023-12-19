# ADO.NET
Eksempler til ADO.NET - Læs mere om ADO.NET her [Notion ADO.NET](https://mercantec.notion.site/ADO-NET-3b3c18cd2eba409f824ae82c6e9d933c?pvs=4)
Enten skal man manuelt sætte databasen op eller starte med at køre consol applikationen en gang. 
## Opbygning af projekt med ADO.NET
### ADONETConsole
Her er en consoleapp, som implementeret meget lavpraktisk. Den bruger ikke et objekt, men bare 2 strenge til at intergerer med databasen.
Den indeholder 4 metoder udover main() og Init(). <br>
<strong>CreateDatabase</strong> Her tjekker den om ADONET databasen allerede findes på vores databaseserver eller laver den databasen. <br>
<strong>InsertData</strong> Her indsætter den værdierne som er skrevet ind for de 2 strenge inden programmet kører. <br>
<strong>CreateTable</strong> Her laver den vores tabel med en SQL commando som er gemt som en streng. <br>
<strong>ReadData</strong> Her læser den databasen ud fra SQL commando som er gemt som en streng og bruger et while loop til at skrive dem ud i konsollen. <br>
