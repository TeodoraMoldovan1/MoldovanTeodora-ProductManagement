# ProductManagement
Design pattern-uri folosite: 
  * **Singleton** pentru gestionarea unei singure instanțe a unei clase de stocare a datelor despre produse.
  * **Strategy** pentru a implementa: strategiile de aplicare a reducerii și a taxei  în functie de prețul produsului (dacă este mai mic de 50 de lei se aplică taxă de 10%, iar dacă este mai mare se aplică o reducere de 10% a prețului);  sortarea/ filtrarea produselor
    
Unit tests:
  * Testarea funcționalității de adăugare, ștergere și actualizare a produselor din baza de date.
  * Testarea calcularii noului preț cu aplicarea discount-ului / taxelor
