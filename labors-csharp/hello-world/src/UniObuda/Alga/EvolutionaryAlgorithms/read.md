# Lineáris egyenlet megoldása evolúciós algoritmussal

A példa az `a*A + b*B + c*C + ... = 0` egyenlet egy megoldását keresi. A kisbetűs súlyok rögzítettek; egy egyed génjei a keresett `[A, B, C, ...]` értékek. Az egyenlet több megoldása közül bármelyik megfelelő.

## Futtatás és kiírás

A repository gyökeréből (.NET 10):

```sh
dotnet run --project labors-csharp/hello-world/src/HelloWorld.csproj
dotnet test labors-csharp/hello-world/tests/HelloWorld.Tests.csproj
```

A `Program` a `LinearEquationDemo.ExecuteDemo()` metódust hívja. A kiírás szélességét a hívásnál lehet megadni:

```csharp
LinearEquationDemo.ExecuteDemo(numberWidth: 12);
```

Ez 12 karakter széles, jobbra igazított mezőket és legfeljebb 8 tizedesjegyet jelent. Az alapérték 10, ekkor legfeljebb 6 tizedesjegyet írunk ki. Általánosan **X a mezőszélesség, X−4 a tizedesjegyek száma**, és X legalább 4. X=4 esetén egészre kerekítve jelenítjük meg az értékeket.

Önálló kiírások előtt a közös beállítás is megváltoztatható:

```csharp
LinearEquationDemo.NumberWidth = 12;
Console.WriteLine(equation);
Console.WriteLine(individual.Print(equation, options.Tolerance));
Console.WriteLine(individual.PrintVerify());
```

A `FormatNumber` egyetlen helyen végzi a formázást és a paddinget. Ugyanez a szélesség vonatkozik a változónevekre, a súlyokra, a génekre, az eredményre, a fitnessre és a toleranciától való eltérésre. Nincs tudományos jelölés vagy felesleges záró nulla; a nullára kerekített kis negatív szám is `0`. A szélesség minimum: a hosszabb számokat nem vágjuk le. A tizedesjel az aktuális kultúrát követi. A formázás nem változtatja meg a számolást vagy a solver toleranciáját.

## Osztályok

| Osztály | Felelősség |
|---|---|
| `LinearEquation` | A rögzített súlyok, az egyenlet kiírása, ismert ellenőrző megoldás generálása. |
| `LinearEquationIndividual` | Gének, normalizálás, kiértékelés, fitness, négyzetösszeg, keresztezés és mutáció. |
| `LinearEquationGeneration` | Populáció, rendezés, túlélők kiválasztása, következő generáció. |
| `LinearEquationSolver` | A keresés léptetése a beadott `Func` segítségével, leállás és a legjobb egyed visszaadása. |
| `EvolutionOptions` | A keresés paraméterei. |
| `LinearEquationDemo` | A bemutató összeállítása és a közös számformázás. |

A tanpélda egyszerűségét szolgálja, hogy a `Genes` és `Weights` közvetlen `double[]`. A konstruktorok a kapott tömböt tárolják; az egyed normalizálása helyben módosítja ezt a tömböt. Emiatt egyedhez új géntömböt adjunk, és a túlélő szülők génjeit ne módosítsuk. A `Reproduce` új tömböt készít.

## Normalizálás és ismert megoldás

A csupa nulla vektor minden ilyen egyenletet megoldana. Egy hibás jelölt zsugorításával is tetszőlegesen csökkenthető lenne a hiba. Ezért az egyed konstruktorában normalizálunk:

```text
A² + B² + C² + ... ≈ 1
```

Elosztjuk a vektort a hosszával, megőrizve az elemek arányát. A legnagyobb abszolút génnel végzett előzetes skálázás a négyzetre emelés numerikus biztonságát segíti. Nullvektor esetén `[1, 0, ...]` lesz az egyed.

A `GenerateKnownSolution` a legnagyobb abszolút súlyhoz tartozó változót kiszámítja a többi, véletlenül megválasztott változóból. Például `C = -(a*A+b*B)/c`. Ezután normalizálja a vektort, ami megőrzi a nulla összeget. Az ismert megoldások csak ellenőrzéshez készülnek; a solver nem használja őket.

## Egy generáció működése

1. P véletlen egyedet hozunk létre és normalizálunk.
2. A fitness `abs(a*A+b*B+c*C+...)`; a kisebb jobb. A populációt növekvő fitness szerint rendezzük.
3. Kiértékeljük a leállási feltételeket: a legjobb fitness legfeljebb a tolerancia, vagy elértük K új generációt.
4. Eldobjuk a legrosszabb `floor(P*ExtinctionRate)` egyedet, legalább egyet, de legalább egy túlélőt megtartunk.
5. Az üres helyekre túlélő szülőkből készítünk utódokat. Génenként külön döntünk, melyik szülőtől öröklünk, majd hozzáadjuk az epszilon mutációt.
6. Génenként a bevándorlási valószínűség beütésekor öröklés és mutáció helyett teljesen új véletlen értéket választunk.
7. Az utódot normalizáljuk, és kezdődik a következő kiértékelés.

Két vagy több túlélőnél különböző szülőket választunk; egy túlélőnél mindkét szerepet ugyanaz az egyed tölti be. A túlélők változatlanul maradnak (elitizmus). A normalizálás az egész utódot átskálázza, ezért az epsilon a **normalizálás előtti** eltérés korlátja.

## Paraméterek a demóban

| Paraméter | Érték | Jelentés |
|---|---:|---|
| PopulationSize | 20 | Egyedek száma. |
| MaxGenerations | 200 | Legfeljebb ennyi új generáció. |
| ExtinctionRate | 0.5 | Kieső hányad. |
| MutationEpsilon | 0.05 | Örökölt génenként legfeljebb ±0,05 eltérés. |
| ImmigrationRate | 0.1 | Teljesen új gén esélye. |
| MinX / MaxX | -1 / 1 | Kezdeti és bevándorló gének sorsolási intervalluma. |
| Tolerance | 0.001 | Elfogadható abszolút maradékhiba. |

Ezek a demóban megadott értékek; az `EvolutionOptions` néhány alapértéke ettől eltér. A sorsolási intervallum nem a normalizált gének korlátja. A kezdeti mintavétel nem egyenletes a gömbfelszínen, de a demonstrációhoz elegendő.

A tanpélda érvényes bemenetet feltételez: legalább két véges súly, P≥2, K≥0, 0<kihalás<1, 0≤bevándorlás≤1, véges nem negatív epsilon, véges pozitív tolerancia, valamint véges MinX<MaxX. Az egyedek és az egyenlet dimenziója egyezzen. A saját generátor nem üres, megfelelő méretű és dimenziójú generációt adjon vissza. Nincs minden bemenetre külön védelmi ellenőrzés.

## A Func használata

```csharp
var equation = new LinearEquation(new[] { 3.4, -1.4, 0.34 });
var options = new EvolutionOptions { MinX = -1, MaxX = 1 };
var random = new Random(42);
var solver = new LinearEquationSolver(options,
    previous => previous.CreateNextGeneration(random, equation, options));

LinearEquationIndividual solution = solver.Solve(random, equation,
    (number, generation) => Console.WriteLine(generation.Population[0].Print(equation, options.Tolerance)));

bool success = solution.Fitness(equation) <= options.Tolerance;
```

A lambda a környezetéből használja az egyenletet, a paramétereket és a véletlengenerátort. A solvernek csak az előző generációból újat előállító függvényt kell ismernie. A callback a rendezett generációt kapja meg megfigyeléshez; a hívó ne módosítsa.

A 0. generáció a kezdeti populáció. K=0 esetén csak ezt értékeljük ki, egyébként legfeljebb K generációváltás történik. A solver `LinearEquationIndividual` eredményt ad, az összes megvizsgált generáció legjobbját. Generációkorlátnál ez még nem feltétlenül megoldás, ezért ellenőrizni kell a fitnesst.

## Mit látunk a konzolon?

Először az egyenlet, majd három ismert megoldás és ezek négyzetösszege jelenik meg. Ezután generációnként a legjobb fitness és legfeljebb húsz rendezett egyed látható. A végén az addigi legjobb egyedet írjuk ki.

- `f`: a fitness, vagyis az egyenlet abszolút maradékhibája.
- `t`: `abs(fitness − tolerance)`, a tolerancia határától mért távolság. **Nem sikerjelző**: például tökéletes megoldásnál a toleranciával egyenlő. A siker feltétele mindig `fitness <= tolerance`.

A feladat súlyai minden futáskor új véletlen értékek. A keresés seedje 42, de az eltérő egyenletek miatt a lefutás így is különbözik. Azonos feladathoz és azonos seedekhez ismételhető eredmény tartozik. Véges K mellett nincs találati garancia.

Ez a lineáris feladat közvetlenül is megoldható; a cél az öröklés, a keresztezés, a mutáció és a szelekció szemléltetése. A kis maradékhiba és a normalizálási feltétel a teljes double pontosságú értékekre vonatkozik; a kiírt, kerekített számokat visszahelyettesítve kis eltérés lehetséges.
