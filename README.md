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
- il controller contenga la logica del mene e i richiami ai vari metodi
- la view faccia visualizzare i risultati di tutti i metodi richiamati dal menu del controller
</details>


## Following tasks 

- [x] creare un file ViewProdotti e un file ViewCategorie
- [x] sostituire il metodo Stampa di View con i metodi corrispondenti ai metodi del controller
- [x] i parametri dei metodi di View non prenderanno una variabile stringa ma un oggetto Prodotto (Prodotto prodotto) o un oggetto Categoria ( Categoria categoria), fare attenzione se è una lista o un oggetto singolo
- [x] ShowMainMenu sarà suddiviso in base alle funzioni che richiama con i rispettivi nomi di menu (ShowProductMenu, ShowCategoryMenu, ShowEndMenu)
- [x] creare un modello specifico per Prodotti e Categorie
- [x] modificare il Model del database togliendo il while del reader e ritornandolo nei vari metodi
- [x] modificare il Controller e il Model in modo che il reader venga letto nel Controller all'interno dei vari metodi
- [x] far si che i metodi del controller non passino una stringa alla view ma un modello (es Prodotto, Categoria)

## Nuove funzionalità
- [x] Modello Utenti
- [x] Modello Clienti
- [x] Funzione Menu : 14- visualizza clienti 
- [x] Model: Richiesta al database e return reader CRUD
- [ ] Controller: nuova opzione switch, reader assegna a un'istanza del modello Cliente, passa la lista clienti a view
- [ ] ClientiView: metodo che ha come parametro una lista Cliente, fa visualizzare i clienti
- [ ] ClientiView: aggiungere visualizzazione opzione menu
- [ ] Controller: aggiunta a ShowMainMenu delle due visualizzazioni menu in UtentiView e ClientiView
- [ ] Funzione Menu : 15- cerca cliente
- [ ] Funzione Menu : 16-Uscire (prima era numero 14 va spostata)