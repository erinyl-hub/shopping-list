# 🚀 GitHub Actions Workflow Instruktioner

---

## 📖 Översikt
I denna laboration ska du skapa EN GitHub Actions workflow som demonstrerar förståelse för CI/CD pipelines. Du kommer att implementera olika triggers, matrix builds och context variables.

## 🎯 Lärandemål
- ✅ Konfigurera olika workflow triggers
- ✅ Implementera matrix strategies för cross-platform builds
- ✅ Arbeta med context variables och JSON output
- ✅ Bygga och testa .NET applikationer automatiskt
- ✅ Använda workflow inputs och environments (valfritt)

---

## 📋 Krav: CI Matrix Workflow

Skapa en workflow-fil `.github/workflows/ci.yml` som uppfyller följande krav:

### **1. Triggers**
Workflow ska triggas av:
- Manuell körning
- Push till `main` branch
- När pull requests öppnas mot main

### **2. Matrix Strategy**
Använd matrix för att köra jobbet på:
- Operativsystem: Linux och Windows
- .NET versioner: 8 och 9

### **3. Context Information**
Som första steg, skriv ut följande information som JSON till konsolen:
- Runner context
- GitHub context

### **4. Standard CI/CD Steg**
Implementera ett workflow som låter dig:

1. **Bygga källkoden**
2. **Köra testerna** - Exekvera enhetstester, men BARA om build steget lyckades

---

## 🔶 Valfria Utökningar som vi gärna vill se 😊

### **Input Parameter**
Lägg till en workflow input som:
- Heter `environment`
- Är en dropdown med valen `development` och `production` (utforska `type`)
- Har `development` som default värde

### **Environment Simulation**
- Konfigurera jobbet att köra i den miljö som specificeras via input
- Skriv ut vilket environment som används: "Running in environment: [värde]"
- Hantera fallback till 'development' om ingen input ges

### **Token Validering**
Lägg till villkor så att workflow bara körs om `GITHUB_TOKEN` existerar i secrets.

### **Formatera koden** - workflowet ska faila om inte koden är formaterad

---

## ✅ Inlämningskrav

### **Obligatoriskt:**
- [ ] Fungerande workflow som uppfyller alla grundkrav
- [ ] Screenshot av lyckad matrix build (alla 4 OS/version kombinationer)
- [ ] Screenshot som visar JSON context output i loggen
- [ ] Workflow körs framgångsrikt på både push och manuell trigger

### **Valfritt:**
- [ ] Input parameter för environment implementerad
- [ ] Environment selection fungerar korrekt
- [ ] GITHUB_TOKEN villkor tillagt
- [ ] kodformatering som villkor
- [ ] Eget kreativt tillägg till workflow

---

## 💡 Implementeringstips

- Börja enkelt med bara triggers och matrix, bygg sedan ut steg för steg
- Testa workflow med manuell trigger först
- Kontrollera syntax noga - YAML är känsligt för indentation
- Använd "Actions" tab i GitHub för att se detaljerade loggar
- Matrix skapar 4 separata jobb (2 OS × 2 .NET versioner)

**Lycka till! 🚀**
