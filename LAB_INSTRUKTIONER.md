# 🛒 Shoppinglista - Testdriven Utveckling Laboration

---

## 📖 Översikt
Detta är en praktisk laboration där du kommer att implementera en shoppinglista-applikation med hjälp av **Testdriven Utveckling (TDD)**. Applikationens användargränssnitt är helt funktionellt, men servicelager-metoderna är tomma stubbar som väntar på att du ska implementera dem.

## 🎯 Lärandemål
- ✅ Skriva enhetstester
- ✅ Skriva ren, testbar kod
- ✅ Skapa feltoleranta applikationer (identifiera och hantera edge-cases)
- ✅ Använda Testdriven Utveckling (TDD)

### 🗒️ Från kursplanen för G:
- använda Test Driven Development
- skapa enhetstester
- skapa feltoleranta applikationer och system

### 📊 Bedömningskriterier

Din implementation kommer att utvärderas på:
1. **TDD-praxis** 🔄 - Bevis på röd-grön-refaktorera-cykel
2. **Testtäckning** 🧪 - Omfattande testsvit
3. **Testkvalité** 🧪 - Välstrukturerade tester
4. **Kodkvalitet** ✨ - Ren, läsbar kod
5. **Korrekthet** ✅ - Alla tester går igenom
6. **Edge Cases** 🛡️ - Identifiering av edge cases

---

## 📁 Projektstruktur

```
ShoppingList.Web/
  ├── Application/
  │   ├── Interfaces/
  │   │   └── IShoppingListService.cs      ⛔ FÅR EJ ÄNDRAS
  │   └── Services/
  │       └── ShoppingListService.cs        ✏️ IMPLEMENTERA HÄR
  ├── Domain/
  │   └── Models/
  │       └── ShoppingItem.cs               ⛔ FÅR EJ ÄNDRAS
  └── Components/                           ⛔ UI - Redan klart

ShoppingList.Tests/
  └── ShoppingListServiceTests.cs           ✏️ SKRIV TESTER HÄR

LAB_INSTRUCTIONS.md                         📖 Studentguide
```

---

## 💪 Utmaningen

### 📦 Datastruktur
`ShoppingListService` använder en **array** (inte en List!) för att lagra shoppingartiklar:
```csharp
private ShoppingItem[] _items;
private int _nextIndex = 0;
```

Du måste implementera algoritmer för att:
- 📈 Dynamiskt expandera arrayen när den är full
- ⬅️ Flytta element när du tar bort artiklar
- 🔍 Söka och filtrera artiklar
- 🔄 Sortera om artiklar baserat på användarens preferenser

### 🎯 Metoder att Implementera

Alla metoder i `ShoppingListService.cs` har TODO-kommentarer. Du behöver implementera:

1. **Add(string name, int quantity, string? notes)** - ➕ Lägg till en ny artikel (expandera array om behövs) *
2. **GetAll()** - 📋 Returnera alla artiklar *
3. **GetById(string id)** - 🔍 Hitta en artikel med dess ID *
5. **Delete(string id)** - 🗑️ Ta bort en artikel och flytta kvarvarande element
6. **Search(string query)** - 🔎 Sök efter namn/anteckningar (skiftlägesokänsligt)
7. (optional - lätt)  **ClearPurchased()** - 🧹 Ta bort alla handlade artiklar
4. (optional - lätt) **Update(string id, string name, int quantity, string? notes)** - ✏️ Uppdatera en befintlig artikel
8. (optional - lätt) **TogglePurchased(string id)** - ✅ Markera artikel som handlad/inte handlad
9. (optional - svårt) **Reorder(IReadOnlyList<string> orderedIds)** - 🔄 Sortera om artiklar i arrayen

---

## 🔄 TDD-Arbetsflöde

Följ denna cykel för **varje metod**:

### 1. 🔴 RÖD - Skriv ett Fallande Test
```csharp
[Fact]
public void Add_ShouldAddItem()
{
    // Arrange
    var service = new ShoppingListService();
    
    // Act
    var item = service.Add("Milk", 2, "Lactose-free");
    
    // Assert
    Assert.NotNull(item);
    Assert.Equal("Milk", item!.Name);
    Assert.Equal(2, item.Quantity);
}
```

### 2. 🟢 GRÖN - Få det att Fungera
Implementera minimal kod för att få testet att gå igenom.

### 3. 🟡 REFAKTORERA - Städa Upp
Förbättra koden utan att ändra beteendet.

### 4. 🔁 UPPREPA
Skriv nästa test!

---

## ⚠️ Viktiga Implementeringsregler

### ✔️ Inputvalidering

**Domänmodellen (`ShoppingItem`) hanterar:**
- **Name**: Kastar `ArgumentException` om null/tom/whitespace. Trimmar automatiskt whitespace.
- **Quantity**: Auto-korrigerar till minst 1 om ogiltigt värde sätts.
- **Notes**: Accepterar alla värden inklusive null. Ingen automatisk trimning eller normalisering.

**Service-lagret (`ShoppingListService`) måste validera:**
- **Id**: Kontrollera att det inte är null/tomt vid sökning/uppdatering/borttagning.
- **Notes** (valfritt): Om ni vill trimma eller normalisera Notes, gör det i service-lagret.

💡 **Tips**: Se `ShoppingItemTests.cs` för exempel på valideringstester.

### 📦 Array-hantering
- Börja med en array av storlek 5 (se `GenerateDemoItems()`)
- När den är full (`_nextIndex >= _items.Length`), **dubbla array-storleken**
- Vid borttagning, flytta element åt vänster och minska `_nextIndex`

### 🔎 Sökkrav
- Skiftlägesokänslig sökning i både Name och Notes
- Om sökningen är tom/null, returnera alla artiklar
- Sortera resultat: **inte handlade först**

### 🔄 Sorteringskrav
- Validera att alla ID:n finns
- Validera att inga ID:n saknas (måste sortera om ALLA artiklar)
- Validera att inga dubbletter finns
- Returnera false om validering misslyckas

---

## 🚀 Komma Igång

### 1. 💡 Kommentera Bort Demo-data (Valfritt)
För enhetstestning kan du vilja kommentera bort demo-data-initieringen:
```csharp
public ShoppingListService()
{
    // Initialize with demo data for UI demonstration
    // TODO: Students can remove or comment this out when running unit tests
    // _items = GenerateDemoItems();
    // _nextIndex = 4;
    
    _items = new ShoppingItem[5]; // Start fresh
    _nextIndex = 0;
}
```

### 2. ✍️ Skriv Ditt Första Test
Öppna `ShoppingList.Tests/ShoppingListServiceTests.cs` och börja med ett enkelt test.

**📚 Testexempel**: Se `ShoppingItemTests.cs` för fullständiga exempel på välstrukturerade enhetstester. Denna fil visar:
- Arrange-Act-Assert mönster
- Tydliga testnamn (Method_Scenario_ExpectedBehavior)
- Tester för både happy paths och edge cases
- Hur man testar valideringsregler

### 3. 🧪 Kör Tester
```bash
dotnet test
```

### 4. 💻 Implementera Metoden
Öppna `ShoppingListService.cs` och skriv kod för att få ditt test att fungera.

### 5. ✅ Verifiera
Kör testerna igen. Refaktorera om behövs.

---

## 🌐 Köra Applikationen

För att se din implementation i aktion:
```bash
cd ShoppingList.Web
dotnet run
```

Besök: `https://localhost:5001/shopping`

**OBS**: ⚠️ Användargränssnittet kommer bara att fungera korrekt när du har implementerat service-metoderna!

---

## 💡 Tips för Framgång

### 🏓 Ping-pong programmering
- 🔄 Turas om att skriva tester och metoder enligt TDD. 
- 🤔 Navigatören bör tänka framåt om edge cases
- 💬 Diskutera innan ni kodar

### 🧪 Testtäckning
Skriv tester för:
- ✅ Happy path (normalt flöde)
- ✅ Edge cases (tomma arrayer, null-värden)
- ✅ Validering (ogiltiga input)
- ✅ Gränsvillkor (array-expansion, sista element)

### ⚠️ Vanliga Fallgropar
- ❌ Glöm inte att trimma strängar
- ❌ Glöm inte att validera input
- ❌ Kom ihåg att uppdatera `_nextIndex`
- ❌ Hantera array-expansion korrekt (kopiera alla element)

---

## 📚 Resurser

- 📖 [xUnit Documentation](https://xunit.net/)
- 🔄 [Test Driven Development Guide](https://martinfowler.com/bliki/TestDrivenDevelopment.html)
- 💻 [C# Arrays](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/arrays/)

---

## ❓ Frågor?

Om du har frågor om kraven, fråga dina lärare. Kom ihåg: en del av inlärningen är att lista ut detaljerna genom TDD!

Lycka till! 🚀

