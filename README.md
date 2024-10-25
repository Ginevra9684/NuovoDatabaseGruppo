# NuovoDatabaseGruppo

## DIVISIONE BRANCH

- branch supervisioneDaReadme

funzione : supervisionare i vari brench

- branch modificaDatabase

[x]  funzione : aggiungere tabella utente e cliente
[x]  campi utente : nome e cognome
[x]  campi cliente : id utente e codice cliente

<details>
<summary>Dettagli</summary>
La tabella cliente farà riferimento alla tabella utente tramite id univoco
</details>


- branch divisioneMVC

[ ]  funzione : suddividere applicazione in metodi

<details>
<summary>Dettagli</summary>
L'applicazione deve essere suddivisa utilizzando il pattern MVC in modo che:
- il Model contenga il database e i propri metodi
- il controller contenga la logica del mene e i richiami ai vari metodi
- la view faccia visualizzare i risultati di tutti i metodi richiamati dal menu del controller
</details>

- branch modifiche/modificaView

[ ] creare un file ViewProdotti e un file ViewCategorie
[ ] suddividere i metodi di view con nomi corrispondenti ai metodi del controller nei file View giusto 
[ ] i parametri dei metodi non prenderanno una variabile stringa ma un oggetto Prodotto (Prodotto prodotto) o un oggetto Categoria ( Categoria categoria), fare attenzione se è una lista o un oggetto singolo
[ ] il menu non sarà più ShowMainMenu ma sarà suddiviso in base alle funzioni che richiama con i rispettivi nomi di menu

- branch modifiche/modificaModelController

[ ] creare un modello specifico per Prodotti e Categorie
[ ] modificare il Model del database togliendo il while del reader e ritornandolo nei vari metodi
[ ] modificare il Controller il modo che ci sia il reader del Database e che il reader salvi nei campi dei modelli Prodotto e Categoria o liste di essi
[ ] creare i vari modelli per tutti i metodi 

