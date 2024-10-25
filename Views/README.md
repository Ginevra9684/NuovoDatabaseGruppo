# Documentazione: Conversione di "View" a MVC

## Obiettivo
L'obiettivo principale di questa ristrutturazione è iniziare la conversione del codice esistente verso il pattern architetturale Model-View-Controller (MVC). Il primo passo è stato la separazione e organizzazione delle view in modo più modulare e mantenibile.

## Struttura Implementata

### 1. BaseView (Classe Base Astratta)
```csharp
public abstract class BaseView
```
- Contiene le funzionalità comuni di input/output
- Metodi principali:
  - `GetInput()`: Gestisce l'input da console
  - `Stampa()`: Gestisce l'output su console
- È dichiarata abstract per evitare istanziazioni dirette

### 2. ProductView (View Specializzata)
```csharp
public class ProductView : BaseView
```
- Gestisce tutte le interazioni utente relative ai prodotti
- Eredita le funzionalità base da BaseView
- Implementa metodi specifici come:
  - `ShowProductMenu()`
  - `NomeProdotto()`
  - `PrezzoProdotto()`

### 3. CategoryView (View Specializzata)
```csharp
public class CategoryView : BaseView
```
- Gestisce tutte le interazioni utente relative alle categorie
- Eredita le funzionalità base da BaseView
- Implementa metodi specifici come:
  - `ShowCategoryMenu()`


## Vantaggi della Nuova Struttura

1. **Separazione delle Responsabilità**
   - Ogni view si occupa solo della sua area specifica
   - Facilita la manutenzione

2. **Riutilizzo del Codice in BaseView**
   - La classe BaseView elimina la duplicazione del codice che andrebbe usata per entrambi i view (valutare migliorie da apportare)
   - Funzionalità comuni centralizzate

## Prossimi Passi per Completare la Transizione MVC

# Modifica Controller
- Separare il Controller attuale in  `ProductController` e `CategoryController`
- Rinominare l'attuale `Controller` in `MainController`
- Iniettare `ProductController` e `CategoryController` invece di gestire direttamente le operazioni

# Modifica Model
- Separare il Model attuale in  `ProductModel` e `CategoryModel`

## Note sulla Manutenzione

- Mantenere le view il più semplici possibile
- Evitare di aggiungere logica di business nelle view
- Documentare ogni nuovo componente aggiunto

## Priorità di Implementazione
- [ ]   Separazione Controller e Model
- [ ]   Gestione Database
- [x]   Completamento View


