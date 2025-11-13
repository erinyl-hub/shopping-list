# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is a **TDD (Test-Driven Development) educational lab** for a shopping list application. The codebase is intentionally structured as a learning exercise where students implement service layer methods using TDD practices. The UI is fully functional but the service methods need implementation.

## Technology Stack

- **.NET 9.0** - ASP.NET Core Blazor Server application
- **xUnit** - Testing framework
- **C# 9.0** with nullable reference types enabled

## Project Structure

```
ShoppingList.sln
├── ShoppingList.Web/              # Blazor Server web application
│   ├── Application/
│   │   ├── Interfaces/
│   │   │   └── IShoppingListService.cs      # Service contract (DO NOT MODIFY)
│   │   └── Services/
│   │       └── ShoppingListService.cs        # Implementation target
│   ├── Domain/
│   │   └── Models/
│   │       └── ShoppingItem.cs               # Domain model with validation (DO NOT MODIFY)
│   └── Components/                           # Blazor UI components (already complete)
└── ShoppingList.Tests/
    ├── ShoppingItemTests.cs                  # Example tests (reference only)
    └── ShoppingListServiceTests.cs           # Student test implementation target
```

## Development Commands

### Build
```bash
dotnet build
```

### Run Tests
```bash
dotnet test
```

### Run Application
```bash
cd ShoppingList.Web
dotnet run
```
Application runs at: `https://localhost:5001/shopping`

### Code Formatting
```bash
dotnet format
```

## Architecture & Implementation Constraints

### Array-Based Data Storage

The `ShoppingListService` uses a **fixed-size array** (not List<T>) for educational purposes:

```csharp
private ShoppingItem[] _items;
private int _nextIndex = 0;
```

**Key Implementation Requirements:**
- Initial array size: 5 elements
- **Dynamic expansion**: When full (`_nextIndex >= _items.Length`), double the array size
- **Element shifting**: On deletion, shift remaining elements left and decrement `_nextIndex`
- Manual array management simulates low-level data structure operations

### Service Methods to Implement

1. **Add(string name, int quantity, string? notes)** - Add item with array expansion logic
2. **GetAll()** - Return items from `_items[0.._nextIndex]`
3. **GetById(string id)** - Linear search for item by ID
4. **Delete(string id)** - Remove item and shift elements
5. **Search(string query)** - Case-insensitive search in Name/Notes fields
6. **Update(string id, ...)** - Modify existing item
7. **ClearPurchased()** - Remove all purchased items with compaction
8. **TogglePurchased(string id)** - Toggle IsPurchased flag
9. **Reorder(IReadOnlyList<string> orderedIds)** - Reorder items based on ID list

### Domain Model Validation

**ShoppingItem** now enforces business rules through property setters:

**Name Property:**
- Throws `ArgumentException` if set to null, empty, or whitespace
- Automatically trims leading/trailing whitespace
- Students must catch this exception or ensure valid input

**Quantity Property:**
- Auto-corrects to minimum value of 1 if set to zero or negative
- No exception thrown - silently corrects invalid values

**Notes Property:**
- Simple nullable string property with no validation
- Accepts any value including null, empty strings, and whitespace
- No automatic trimming or normalization

**Service Layer Validation:**
- **Id parameters**: Service methods must validate ID is not null/empty before operations
- The domain model handles property-level validation
- Service methods focus on business logic and array manipulation

**Search Requirements:**
- Case-insensitive matching in both Name and Notes
- Empty/null query returns all items
- Results should prioritize unpurchased items first

**Reorder Validation:**
- All IDs in `orderedIds` must exist
- No missing IDs (must include ALL current items)
- No duplicate IDs
- Return false on validation failure

### Demo Data

The constructor initializes with 4 demo items via `GenerateDemoItems()`. Students may comment this out for unit testing:

```csharp
// _items = GenerateDemoItems();
// _nextIndex = 4;
_items = new ShoppingItem[5];
_nextIndex = 0;
```

## TDD Workflow (Important Context)

This project follows strict TDD practices:

1. **RED** - Write a failing test
2. **GREEN** - Write minimal code to pass
3. **REFACTOR** - Improve code without changing behavior
4. **REPEAT** - Next test

Students are expected to commit frequently showing the red-green-refactor cycle in git history.

## Test Structure

**ShoppingItemTests.cs** - Complete example unit tests for the domain model:
- Demonstrates Arrange-Act-Assert pattern
- Shows proper test naming conventions (Method_Scenario_ExpectedBehavior)
- Covers constructor, property validation, and edge cases
- Students should reference this as an example of well-structured tests
- **This file should not be modified by students** - it serves as a reference

**ShoppingListServiceTests.cs** - Student implementation target:
- Contains comprehensive guidance comments
- Lists recommended test categories for each service method
- Currently empty - students write all tests here
- Focus: array manipulation, service orchestration, business logic
- Students practice TDD by implementing these tests

## GitHub Actions Workflow Requirements

A CI/CD workflow should be implemented in `.github/workflows/ci.yml` with:

**Required Features:**
- Triggers: manual, push to main, pull requests to main
- Matrix strategy: Linux + Windows OS × .NET 8 + 9 versions (4 total jobs)
- Print runner and GitHub context as JSON
- Build and test steps (tests only run if build succeeds)

**Optional Features:**
- Environment input parameter (development/production dropdown)
- Token validation (GITHUB_TOKEN existence check)
- Code formatting validation (`dotnet format --verify-no-changes`)

## Important Notes for Claude Code

**Files Students Should NOT Modify:**
- `IShoppingListService.cs` - Service interface contract
- `ShoppingItem.cs` - Domain model with built-in validation
- `ShoppingItemTests.cs` - Example tests for reference
- Blazor UI components - The UI is complete

**Student Implementation Targets:**
- `ShoppingListService.cs` - Implement all service methods
- `ShoppingListServiceTests.cs` - Write comprehensive unit tests using TDD

**Key Constraints:**
- Use arrays (not List<T>) for educational purposes - manual array manipulation required
- Domain model handles Name/Quantity/Notes validation automatically
- Service layer must validate ID parameters
- Follow TDD red-green-refactor cycle
- Commit frequently to show TDD progression
