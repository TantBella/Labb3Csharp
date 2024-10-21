# Labb 3, Bokningsapplikation med WPF

## Del 1: Kravspecifikation
Du ska skapa en bokningshanteringsapplikation för ett gym som tillåter användare
att:

• Boka träningspass.

• Se en lista över tillgängliga pass.

• Avboka pass.

Användarna ska kunna söka efter träningspass baserat på tid och träningskategori.
Systemet ska tillhandahålla ett enkelt GUI med interaktiva kontroller som knappar,
textfält och listor.

### GUI-krav:
• Ett fönster med en lista över tillgängliga pass.
• En sökfunktion för att filtrera pass baserat på träningstyp och tid.
• Knapp för att boka och avboka ett pass.
• Bekräftelsemeddelande efter att en bokning gjorts eller avbokats.

## Del 2: Objektorienterad design
Bygg applikationen med fokus på objektorienterad design. Skapa minst följande
klasser:

1. Pass: Innehåller information om träningspassen, t.ex. tid, kategori, antal platser
och om passet är fullbokat.
2. Användare: En enkel klass för att hantera användare som bokar och avbokar
pass.
3. Bokningshantering: Denna klass ansvarar för att hålla reda på pass och
bokningar, samt för att söka efter tillgängliga pass.

## Del 3: GUI-design och händelsestyrd programmering
1. Sökfunktion: Implementera en sökfunktion med ett textfält där användaren kan
filtrera pass baserat på träningstyp och tid.
2. ListView för träningspass: Visa alla tillgängliga pass i en ListView. Uppdatera
listan dynamiskt när användaren gör en sökning.
3. Boknings- och avbokningsfunktioner: Låt användare boka eller avboka pass
genom att klicka på knappar. Bekräftelse ska visas när åtgärden är slutförd.

## Del 4: Användning av LINQ och .NET-ramverket
Du ska använda LINQ för att filtrera och söka efter pass i listan.Använd inbyggda
datatyper och klasser för att hantera tid och datum.
Exempel på hur du söker efter pass:
var resultat = PassLista.Where(p => p.Workout == "Yoga" && p.Time == valdTid);

## Del 5: Skalbarhet och underhållbarhet (VG2)

### För att uppnå VG2-målet ska du:
• Säkerställa att nya pass och funktioner kan läggas till enkelt utan att koden
behöver skrivas om.
• Se till att klasserna är modulära (fristående och har tydligt separerade ansvar).

## Del 6: Händelsestyrd programmering (VG3)

### För att uppnå VG3-målet ska du:
• Implementera händelser i GUI, där knappar triggar metoder i C#.
• Se till att programmet reagerar korrekt på användarens interaktioner med
gränssnittet.

# Bedömningskriterier

## Godkänt: 
Du har skapat en fungerande grafisk applikation som uppfyller kraven
för bokning och avbokning av pass. Använder objektorienterade principer och
grundläggande GUI-komponenter.

## Väl godkänt (VG2 och VG3): 
Applikationen är skalbar, väl strukturerad och
använder händelsestyrd programmering effektivt.

# Leverans

1. Källkoden för din applikation (C#-filer).
2. En kort rapport (1-2 sidor) som förklarar din design och hur du uppnått
skalbarhet samt händelsestyrd programmering.
3. Koden och rapporten skall ligga i ett privat repo som du delar med mig
4. Uppgiften lämnas in genom att du laddar upp en fil med länk till ditt repo.
