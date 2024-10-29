# NuovoDatabaseGruppo

## DIVISIONE BRANCH

- branch supervisioneDaReadme

funzione : supervisionare i vari brench

- branch modificaDatabase

- [x]  funzione : aggiungere tabella utente e cliente
- [x]  campi utente : nome e cognome
- [x]  campi cliente : id utente e codice cliente

<details>
<summary>Dettagli</summary>
La tabella cliente farà riferimento alla tabella utente tramite id univoco
</details>


- branch divisioneMVC

- [x]  funzione : suddividere applicazione in metodi

<details>
<summary>Dettagli</summary>
L'applicazione deve essere suddivisa utilizzando il pattern MVC in modo che:
- il Model contenga il database e i propri metodi
- il controller contenga la logica del main e i richiami ai vari metodi
- la view faccia visualizzare i risultati di tutti i metodi richiamati dal menu del controller
</details>

<details>
<summary>Procedure</summary>

## Task sucessivi 

- [x] creare un file ViewProdotti e un file ViewCategorie
- [x] sostituire il metodo Stampa di View con i metodi corrispondenti ai metodi del controller
- [x] i parametri dei metodi di View non prenderanno una variabile stringa ma un oggetto Prodotto (Prodotto prodotto) o un oggetto Categoria ( Categoria categoria), fare attenzione se è una lista o un oggetto singolo
- [x] ShowMainMenu sarà suddiviso in base alle funzioni che richiama con i rispettivi nomi di menu (ShowProductMenu, ShowCategoryMenu, ShowEndMenu)
- [x] creare un modello specifico per Prodotti e Categorie
- [x] modificare il Model del database togliendo il while del reader e ritornandolo nei vari metodi
- [x] modificare il Controller e il Model in modo che il reader venga letto nel Controller all'interno dei vari metodi
- [x] far si che i metodi del controller non passino una stringa alla view ma un modello (es Prodotto, Categoria)

## Nuove funzionalità
- [x] Modello Clienti
- [x] Funzione Menu : 14- visualizza clienti 
- [x] Model: Richiesta al database e return reader CRUD
- [x] Controller: nuova opzione switch, reader assegna a un'istanza del modello Cliente, passa la lista clienti a view
- [x] ClientiView: metodo che ha come parametro una lista Cliente, fa visualizzare i clienti
- [x] ClientiView: aggiungere visualizzazione opzione menu (tutte operazini CRUD relative al cliente)
- [x] Controller: aggiunta a ShowMainMenu delle due visualizzazioni menu ClientiView
- [x] Funzione Menu : 15- cerca cliente
- [x] Funzione Menu : 16-Uscire (prima era numero 14 da spostare)

## Aggiustamenti definitivi
- [x] Divisione del controller in ProductController, CategoryController, CustomerController
- [x] Divisione dei menu:
- [x] In ProductView menu relativo ai prodotti
- [x] In ProductController metodi relativi ai prodotti
- [x] In CategoryView menu relativo alle categorie
- [x] In CategoryController metodi relativi alle categorie
- [x] In CustomerView menu relativo ai clienti
- [x] In CustomerController metodi relativi ai clienti
- [x] In BaseView menu principale
- [x] In BaseController richiamati gli altri controller
- [x] Aggiunta nuova tabella ordini
- [ ] Creare OrdersView
- [ ] Creare OrdersController
- [ ] Metodo VisualizzaOrdini
- [ ] Metodo InserisciOrdine
- [ ] Aggiunta commenti
- [ ] Conversione lingua
- [ ] Revisione


</details>

## VERSIONE DEFINITIVA CON SQL

<details>
<summary>Funzionalità(da aggiungere)</summary>

</details>

<details>
<summary>Schema(da aggiungere)</summary>

    ```mermaid

    ```

</details>


## PASSAGGIO AD ENTITY FRAMEWORK

- [ ] Aggiungere i pacchetti per entity framework

- dotnet add package Microsoft.EntityFrameworkCore;
- oppure dotnet add package Microsoft.EntityFrameworkCore.Sqlite;
- dotnet run
- dotnet add package Microsoft.EntityFrameworkCore.Design
- dotnet add package Microsoft.EntityFrameworkCore.Tools
- Se non lo si ha mai installato in locale ----> dotnet tool install --global dotnet-ef 

- [ ] Visto che Model.cs dovrebbe essere Database.cs lo si commenta e si crea il nuovo file Database.cs
- [ ] Creare proprietà DB Context per far si che i modelli corrispondano ai campi delle tabelle
- [ ] Interagire con le proprietà DB Context per estrapolare/aggiungere/modificare dati nelle varie tabelle invece di passare per stringhe SQL

- [ ] Per creare la migrazione delle DB Context properties dotnet ef migrations add InitialCreate
- [ ] Per salvare dotnet ef database update

