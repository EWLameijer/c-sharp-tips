# Hoe werken objecten en parameters?

<div style="page-break-after: always;"></div>

## Wat zijn de stack en de heap en waarom zijn ze belangrijk?

In een C#-programma (en overigens ook in de meeste andere programmeertalen) maak je veel gebruik van variabelen. Nu denken beginnende programmeurs niet altijd na over variabelen, ze zien het hoogstens als vakjes waarin waarden worden opgeslagen. 

Maar een computer is geen wiskundige abstractie, maar een fysiek apparaat. Computerbouwers en programmeertaalontwerpers weten al decennia dat 'gewoon iets in het geheugen opslaan' een computer heel erg traag zou maken. Daarom splitsen ze het geheugen op in een paar delen, waarvan de stack en de heap de belangrijkste zijn. Nu zou je je daar als programmeur normaal geen zorgen over maken (of het geheugen in 1 of 100 delen is opgesplitst is toch een 'implementatiedetail'?) Helaas 'lekt' die keuze voor twee geheugendelen en de gevoelde noodzaak programma's snel te laten werken ook door in de meeste programmeertalen.

Beschouw de volgende code:

```
class Person
{
   public string FirstName { get; set; }
   public string LastName { get; set; }
}

class Test 
{
   static void MakeSix(int a)
   {
       a = 6;
   }
   
   static void MakeFlip(Person p)
   {
       p.FirstName = "Flip";
   }
   
   public static void Main()
   {
       int a = 3; 
       Person joe = new Person { FirstName = "Joe", LastName = "Biden" };
       Console.WriteLine($"The number is {a}, the person's called {joe.FirstName}");
       
       MakeSix(a);
       MakeFlip(joe);
       Console.WriteLine($"The number is {a}, the person's called {joe.FirstName}");
   }   
}
```

Wat komt hier uit?

Wel... "The number is 3, the person's called Flip".

a wordt dus NIET vervangen, maar joe's naam wel. Wat is hier aan de hand?

Wat hier aan de hand is, is dat een object (zoals de Person) anders wordt opgeslagen dan een getal (zoals de a).

Alle lokale variabelen in een methode (inclusief parameters) worden normaal op de "stack" gezet. De stack is een relatief klein geheugengebied, waar parameters als borden worden opgestapeld - en ook weer worden verwijderd. Het voordeel is dat het aanmaken van een lokale variabele erg snel is: je plaatst het gewoon bovenop de stapel, waar bijna altijd ruimte is. Mocht er geen ruimte zijn, door bijvoorbeeld een verkeerd geprogrammeerd of op een te groot probleem toegepast recursief algoritme, dan krijg je een zogenaamde 'stack overflow', tegenwoordig het meest bekend van de website.

De stack heeft wel twee nadelen; allereerst is de stack redelijk klein, dus grote datastructuren (zoals een jpeg-plaatje) passen er niet op, maar een fundamenteler nadeel is dat een stack net als een stapel borden altijd weer van boven naar onder wordt opgeruimd. Als je data langer wilt onthouden dan in 1 methode - wel, dat lukt normaal niet. Want alle data van de methode wordt van de stack verwijderd als de methode is afgelopen. Ook om ruimte te maken voor de data van methoden die daarna worden aangeroepen.

Om die problemen op te lossen reserveren programmeertalen het grootste deel van het geheugen als een soort 'algemene opslagruimte', die de 'heap' wordt genoemd. Een heap werkt als een boedelopslag: een runtime (het proces dat een programma uitvoert) gaat naar de heap toe, en zegt iets als "ik heb hier een JPEG-plaatje van 10232245 bytes. Dat wil ik graag opslaan. Heb je daar ruimte voor?" Dan gaat de heap-manager kijken of er een aaneengesloten blok van minstens 10232245 bytes vrij is. 

// variabele lengte slecht voor stack, grote lengte ook voor returnwaarde en parameters




<div style="page-break-after: always;"></div>

# Visual Studio-tips

<div style="page-break-after: always;"></div>

## Ik vergeet weleens spaties op de juiste plekken te zetten of op Ctrl-KD te drukken!

Automatisch code opschonen bij saven:

Tools > Options > Text Editor > Code Cleanup.
Kruis de "Run Code Cleanup profile on Save." aan.

<div style="page-break-after: always;"></div>


## Het programma runnen - zonder de muis te gebruiken!

Onthoud (en oefen!)

- Run het programma (met debuggen): F5

- Run het programma (zonder debuggen, breakpoints overslaan): Ctrl F5

- Stop het programma: Shift F5 [Shift betekent vaak 'anti-']

- Stop en herstart het programma: Ctrl Shift F5 

<div style="page-break-after: always;"></div>

## Vertikaal tekst selecteren

Probleem:
  

Ik moet op meerdere plaatsen dezelfde tekst toevoegen/verwijderen.

Voorbeeld:

```
phones.Brand = "Apple";
phones.Price = 129.29m;
phones.Type = "IPhone X2";
```

Stel dat je nu pas ziet dat je geen 'phones' moest gebruiken, maar 'phones[i]'. Hoe kan je dat makkelijk regelen?

Ga naar het eind van één van die 's' en van phones, druk op alt, en ga met de muis of cursor naar boven of beneden. Als het goed is zie je nu die blinkende vertikale lijntjes (de 'carets') op alle regels. Type dan in '[i]' (vierkant haakje openen, i, vierkant haakje sluiten), en voilà, de code is netjes

```
phones[i].Brand = "Apple";
phones[i].Price = 129.29m;
phones[i].Type = "IPhone X2";
``` 

<div style="page-break-after: always;"></div>

## Let op de grijze (en rode) vierkantjes!

In de rechter kantlijn van je Visual Studio editor-veld zie je soms grijze en rode (of ander gekleurde) vierkantjes. Rode moet je sowieso oplossen, anders compileert je code niet.

Maar moet je de grijze (de 'messages', die je kunt zien als je met de muis over het stippellijntje onder de verdachte code gaat staan, of in een lijst met Ctrl \ E -> Messages) ook aanpakken?

Er zijn deelnemers die het uit principe niet doen: mogelijk omdat het werk is en/of ze zich niet willen schikken naar enige tyrannie, of het van dictators of van computerprogamma's is.

Toen ik begon als C#-programmeur was mijn eigen argument: ja, de Microsoft-tips zijn vaak waardeloos, maar die stippellijntjes leiden mij af - ook als de code door anderen geschreven is. Dus ik los die problemen op. Een vakman/vakvrouw wordt ook geacht netjes werk te leveren inplaats van een spoor van zaagsel en koffiekringen achter te laten.

Maar toen ging ik lesgeven, en stond enige deelnemers bij die meer dan een half uur zich blind zaten te staren op code die niet werkte, iets als.

```
Console.WriteLine("Number of trinkets", trinkets);
```

Ik zat er ook een kwartier naar te staren, allerlei debug-technieken proberend, tot ik eindelijk zag wat het probleem was: Console.WriteLine toont alleen het EERSTE argument, tenzij in het eerste argument iets staat als {0} of {1} ofzo.

Het stomme was, dat toen ik beter keek, er een message op dezelfde regel stond: "CA2241: Provide correct arguments to formatting methods."

Kortom: die messages zijn vaak 'gezeur', maar er zal soms net die ene tussenzitten die je een uur debuggen kan besparen. Mijn advies: ruim dus ook de grijze vierkantjes religieus op!

<div style="page-break-after: always;"></div>


## Een variabelennaam veranderen is zoveel werk!

Druk Ctrl-R-R: en de variabele krijgt in alle code (zelfs in andere bestanden) zijn nieuwe naam. Scheelt heel veel type- en verbeterwerk!


<div style="page-break-after: always;"></div>

# Git

<div style="page-break-after: always;"></div>

## Hoe verwijder ik een bestand van git/Azure devops terwijl ik het lokaal wèl wil laten staan?

Soms ontdek je dat je een file remote hebt staan die je niet remote wilt hebben staan, ofwel omdat er geheime data in staan (zoals een connectionstring), ofwel omdat er per ongeluk niet-tekstfiles, zoals dlls of executable files, in je repository staan.

Anno 2022-05-2 lijkt helaas geen gemakkelijke manier zijn om dit op te lossen met Visual Studio (https://stackoverflow.com/questions/29975193/how-to-move-a-tracked-file-to-untracked-using-visual-studio-tools-for-git), maar het probleem is wel op te lossen.

De simpelste manier is via de commandline - al betekent dat mogelijk dat je nog wel apart git moet installeren (https://gitforwindows.org/). 

Hoe dan ook, als Git op je computer geïnstalleerd is kun je een commandprompt openen en naar de directory gaan met de file of directory die je uit git wil hebben. Dan, voor een enkele file (zoals 'my_password.txt'):

```
git rm --cached my_password.txt
```

Voor een directory, zoals 'Bin' (let op de -r):

```
git rm --cached -r Bin
```

Alternatief kan je ook een tool downloaden dat gespecialiseerd is in Git, dan kun je sowieso makkelijk veel dingen doen die moeilijk of onmogelijk zijn in Visual Studio; mijn huidige favoriet is Git Extensions (http://gitextensions.github.io/), waarmee het stoppen met het tracken van een file ("stop tracking this file") erg makkelijk is. Maar er is heel veel keuze (https://alternative.me/git-extensions), dus experimenteer gerust met een paar mogelijkheden.


<div style="page-break-after: always;"></div>

# C#-howtos

<div style="page-break-after: always;"></div>

## Hoe geef je een euroteken weer in de console?

Als je een euroteken in de console probeert af te drukken, zie je normaal een vraagteken. Hoe komt dat?

Dat komt omdat de console geen karakters doorkrijgt, maar blokken bits. Zoiets als 10110010. Normaal interpreteert de console elke byte als een ASCII-karakter. Maar er is geen Ascii-karakter voor het euroteken. Dus geeft de console aan dat hij deze 'rare code' niet kent.

De manier om dat op te lossen is de console te vertellen dat hij de bits niet moet interpreteren als ASCII, maar als stukjes UTF-8-code (UTF-8 definieert vele duizenden karakters, ook het euro-teken); met 

```
Console.OutputEncoding = Encoding.UTF8; 
```

<div style="page-break-after: always;"></div>

## Ik wil een string laten zien. Kan dat mooier dan a + ", " + b?

Ja, met string interpolation. Gebruik '$' aan het begin van de string, en zet elke term die je wil laten 'interpreteren' (dus niet letterlijk als woord wil gebruiken) tussen accolades (dus de {}).

```
// oude manier
Console.WriteLine(a + ", " + b);

// andere oude manier
Console.WriteLine("{0}, {1}", a, b);

// moderne manier
Console.WriteLine($"{a}, {b}");

```

<div style="page-break-after: always;"></div>

## Ik wil een string over meerdere regels laten lopen / ik wil een string met minder '/'-tekens

Als je een string over meerdere regels wilt laten lopen (met nette opmaak) of je je ergert aan alle // in een string: gebruik "verbatim strings", oftewel strings die beginnen met een @. Niet te verwarren met de $ die het begin vormt van geïnterpoleerde strings.

Er zijn twee gevallen die lastig zijn met een normale string: strings met veel 'escape characters / backslashes', zoals filenamen en regexes, en strings die over meerdere regels moeten lopen, maar wel mooi moeten worden opgemaakt. In beide gevallen gebruik je in C# een 'verbatim string'. Dat lijkt op een normale string, maar dan met een '@' ervoor. Zoals https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/verbatim al aangeeft:

```
// Filenamen

string filename1 = "c:\\documents\\files\\u0066.txt"; // normaal
string filename2 = @"c:\documents\files\u0066.txt"; // verbatim: is handiger

// Voor netjes opgemaakte tekst
string html1 = "<html>\n    <body>\n        <p>Hallo!</p>\n" + 
       "    </body>\n</html";
string html2 = @"
<html>
    <body>
        <p>Hallo</p>
    </body>
</html>";
```

Je kunt overigens @ en $ combineren, de volgorde maakt niet (meer) uit, dus @$"" en $@"" werken allebei hetzelfde!

<div style="page-break-after: always;"></div>

## Ik heb een heel simpele methode, kan die korter dan {... return a * a; }?

Als de methode slechts 1 statement heeft die 1 waarde teruggeeft kan je een "expression-bodied method" gebruiken.

```
public int Square(int n) 
{
    return n * n;
}

// wordt
public int Square(int n) => n * n;
```

Dat scheelt toch drie regels code!

Mogelijk komt dat pijltje je bekend voor: het wordt ook gebruikt in LINQ
```
var phone = _phones.Find(p => p.Id == soughtId);
```

en in 'expression-bodied properties' (properties zonder setter, alleen een getter, waarbij de {get { return ...;}} wordt vervangen door iets korters...)

```
public int HashCode => Year + 100 * Month;
```

(als je daar '=' zou gebruiken in plaats van '=>' zou het een public field zijn, geen property!)

Waarom overigens die dikke pijl (=>) in plaats van de mogelijk meer voor de hand liggende dunne pijl (->)? Wel, C# gebruikte de dunne pijl al als de 'pointer member access operator' (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/pointer-related-operators) voor zogeheten 'onveilige' C# code. Weliswaar gebruik je als enterprise C#-programmeur eigenlijk nooit unsafe code, omdat de optie bestaat en er anders mogelijk akelige programmeerfouten zouden ontstaan en/of de compiler trager zou worden door het moeten ontwarren van dubbelzinnige code is dus gekozen voor de 'dikke pijl'. 

<div style="page-break-after: always;"></div>

## Hoe check ik of een string die bijvoorbeeld door een gebruiker is ingevoerd een nummer is?

Vaak moet je bij programmeren een string (zoals "142") omzetten in een nummer (dus 142), om het te kunnen gebruiken in berekeningen of bij transacties met databases en dergelijke.

Oorspronkelijk werd dit in C# gedaan met de 'Parse-methoden' en/of de 'Convert-methoden': int myInt  = int.Parse(myString); double myDouble = double.Parse(myString); int myOtherInt = Convert.ToInt32(myString); double myOtherDouble = Convert.ToDouble(myString); ... enzovoorts.

Deze methodes waren eenvoudig te gebruiken - behalve als de string toevallig geen nummer representeerde. Dan gooiden ze een exceptie, zoals een ArgumentNullException, een FormatException, of een OverflowException (https://docs.microsoft.com/en-us/dotnet/api/system.int32.parse?view=net-6.0#system-int32-parse(system-string)). Het was nogal veel werk voor een programmeur om dat allemaal netjes af te handelen, en op zijn minst had je een lelijk try-catch-blok nodig.

In moderner C# wordt daarom een andere methode gebruikt: een TryParse-methode. In plaats van 

```
var userInput = Console.ReadLine();
try 
{
   var chosenNumber = int.Parse(userInput);
}
catch
{
   ...
}
```

gebruik je 

```
var userInput = Console.ReadLine();
var isValidNumber = int.TryParse(userInput, out int chosenNumber);
if (isValidNumber)
{
   //... use chosenNumber
}
```

Nu is TryParse relatief lastig te begrijpen: het is een methode met niet één, maar _twee_ outputs: de normale output (die dus, in tegenstelling tot bij Parse, niet het gevraagde getal is, maar een bool die aangeeft of de input überhaupt een getal was), en een zogenaamde 'output parameter', wat op een normale parameter lijkt, maar wordt voorafgegaan door het woordje 'out' (en hier ook door het type van de parameter, int).

Als de string geen geldig nummer voorstelt, wordt de normale returnwaarde (de bool) uiteraard false; de output-parameter wordt dan op 0 gezet. Dus zorg dat je altijd de bool returnwaarde checkt, zeker als 0 een legale waarde is voor de rest van het programma.

Voor degenen die zich afvragen of het niet simpeler had gekund: een taal als Go lost dit probleem op met een tuple-returnwaarde

```
i, err := strconv.Atoi(s)
if err != nil {
    // handle error
    fmt.Println(err)
    os.Exit(2)
}
```

wat in principe ook in C# had gekund (C# heeft ook tuples); het zou er dan hebben uitgezien als.

```
var (i, err) = int.SafeParse(s);
if (err != null) {
    // handle error
}
```

Andere talen, zoals Kotlin, gebruiken een nullable int als returntype:

```
val chosenNumber = userInput.toIntOrNull()
if (chosenNumber != null) show(phone[chosenNumber]))
```

Maar hoewel je een dergelijke methode ook in C# zou kunnen maken, kan je in C# bijvoorbeeld gewoon een nullable waarde optellen bij een int; er wordt dus niet altijd gewaarschuwd als je iets met het 'chosenNumber' probeert wat niet zou kunnen met een null-waarde, in tegenstelling tot bij Kotlin, dat de compilatie stopt als het een onveilige operatie ontdekt.

In principe zou je in C# wel een methode kunnen maken die een Result`<`int> teruggeeft, wat wel veilig zou zijn qua compiler en mogelijk iets simpeler (voor degenen die Result begrijpen) dan de TryParse.

Hoe dan ook, C# heeft TryParse, wat mogelijk niet de eenvoudigst te begrijpen methode is, maar (na gewenning) iets mooiere code produceert dan Parse. Het is jammer dat TryParse niet iets simpeler/mooier is, maar elke taal heeft zijn beperkingen (zelfs Kotlin is niet perfect! (https://dev.to/martinhaeusler/kotlin---the-good-the-bad-and-the-ugly-3jfo)). 

In C# zou ik aanraden voor het parsen van integers TryParse te gebruiken, en alleen als dat in de praktijk teveel bugs oplevert overleggen met je team en dan òf teruggaan naar Parse òf een Result-producerende versie maken en gebruiken. Maar als standaard voor C#-programmeren lijkt TryParse anno C#10 nog steeds het beste, zeker als je er eenmaal aan gewend bent!

<div style="page-break-after: always;"></div>

## Hoe maak ik een lijst en vul ik die gelijk met waarden?

Lijsten gebruik je vaak in C#. En - zeker voor oefenprogramma's - vul je die lijsten normaal via programmacode. Op het internet doen mensen dat vaak als

```
var myList = new List<Person>();
myList.Add(new Person { FirstName = "Pete", LastName = "Test1"});
myList.Add(new Person { FirstName = "Clara", LastName = "Test2"});
myList.Add(new Person { FirstName = "Dirk", LastName = "Test3"});
```

Maar omdat heel veel programmeurs dergelijke lijsten wilden maken, hebben de bedenkers van C# iets bedacht wat compacter is. En ook het voordeel heeft dat het ook werkt als de lijst een veld is, want anders zou je een aparte methode moeten maken om alle elementen toe te voegen.

De originele lijst-creatiemethode is relatief simpel:

```
List<int> _myList = new List<int>{ 1, 5, 9, 12 };
```

Je herkent hierin hopelijk het patroon dat je gebruikt als je een nieuw object aanmaakt. Het lijkt zelfs sterk op een object initializer, waarin je in plaats van Person p = new Person { FirstName = "Dirk", LastName = "Test3"} een lijst objecten / waarden meegeeft.

Nu is het kunnen gebruiken van minder karakters iets dat de ontwerpers van C# kennelijk graag doen, dus het bovenstaande kan worden afgekort om dat List`<int>` niet te hoeven herhalen:

```
List<int> _myList = new List<int>{ 1, 5, 9, 12 };

// kan ook worden geschreven als 

List<int> _myList = new(){ 1, 5, 9, 12 };
```

Zoals je ziet, moet je nog steeds _new_ gebruiken en () toevoegen; als je die weg zou laten krijg je een probleem omdat var _myList = {1, 5, 9, 12}; een _array_ van vier getallen zou declareren, en dat is geen lijst. En zonder () zou je een anoniem object maken, wat uiteraard ook geen lijst is (je kunt zo ontzettend veel verschillende dingen doen met C# dat het soms best verwarrend is).

Hoe dan ook, zo maak je een eenvoudige lijst van integers. Maar wat als je een lijst wilt maken met objecten? Wel, net zoals je een int kan initialiseren als "int i = 1;", en het "1" deel terugkomt in de lijst van ints, initialiseer je een Person normaal met Person p = new Person {FirstName = "Joe", LastName = "Biden"}; Wat je (dit is C#) uiteraard kan afkorten tot Person p = new(){FirstName = "Joe", LastName = "Biden"};

Nu zetten we dat dus in de lijst...

```
int x = 1;
Person p = new(){FirstName = "Joe", LastName = "Biden"};


List<int> _famousNumbers = new() {1, 3, 7, 12};
List<Person> _famousPeople = new() { 
    new(){FirstName = "Joe", LastName = "Biden"},
    new(){FirstName = "Max", LastName = "Verstappen"},
    new(){FirstName = "Bill", LastName = "Gates"}};
```

Uiteraard is dit bovenstaande nog steeds veel typen, maar het is minder typen dan meerdere keren "Add" te gebruiken, en je kunt het ook gebruiken als de lijst een veld is!

<div style="page-break-after: always;"></div>



## LINQ: een korte inleiding
Heel vaak werk je in C# met collecties (List`<T>`, IEnumerable`<T>`) waar je iets mee moet doen. Veelvoorkomende operaties zijn
- transformatie (ik heb een lijst strings en ik wil die omzetten in een lijst getallen). Qua typen: List`<T>` -> List`<R>` 
- filtering (ik heb een lijst getallen en wil daar alleen de priemgetallen uit hebben). Qua typen: List`<T>` -> List`<T>`
- samenvatting/reductie (ik heb een lijst getallen en wil daar het grootste getal uit hebben, of het gemiddelde): List`<T>` -> T 

In de informatica worden deze drie taken "map-filter-reduce" genoemd. Omdat C# echter geprobeerd heeft die operaties makkelijk te maken voor ontwikkelaars die weinig van informatica-theorie weten, gebruikt C# de namen van SQL: Transformatie ('map') wordt `Select`, Filteren ('filter') wordt `Where`, en reductie (ook wel 'fold' genoemd) heet `Aggregate`.

### LINQ gotcha 1: bij oude code moet je `using System.Linq` gebruiken
De bibliotheek om zulke bewerkingen op collecties uit te voeren zat niet oorspronkelijk in C#, maar is er pas in 2007 bij gekomen. En deze bibliotheek heet 'LINQ' (voor 'Language INtegrated Query'). Nu was het (tot eind vorig jaar) in werkelijkheid niet helemaal geïntegreerd in C#, je moest onthouden dat je een speciale 'using' aan het begin van een .cs file moest zetten om LINQ te kunnen gebruiken. Sinds C#10 met zijn 'implicit usings' wordt LINQ echter automatisch geladen, en is het eindelijk echt geïntegreerd. Maar als je met oudere versies van C# werkt en `Select` of `Where` lijken niet te werken, dan moet je aan de bovenkant van de file een `using System.Linq` toevoegen.

### LINQ gotcha 2: LINQ geeft collecties altijd als IEnumerable terug.
Een tweede 'gotcha' in LINQ is dat LINQ-queries weliswaar op allerlei typen collecties kunnen werken (zoals List, array of IEnumerable) maar, als ze een sequentie teruggeven (dus niet het ene element bij een reductie), altijd een IEnumerable teruggeven (of een soort IEnumerable, zoals een IOrderedEnumerable bij een sortering). Dat kan je omzetten in een andere sequentie met zeg .ToList() of .ToArray() of .AsQueryable(), maar vaak is dat niet nodig, een foreach loop kan bijvoorbeeld goed werken met een IEnumerable. Pas als je iets nodig hebt als alleen het zesde element (met `nameOfNewSequence[5]`) moet je het wel in een lijst omzetten.

### LINQ gotcha 3: LINQ verandert de oorspronkelijke collectie nooit
Als je iets wilt sorteren moet je bijvoorbeeld niet het volgende doen:
```
List<string> names = new() { "Bob", "Fred", "Alice" };
names.OrderBy(n => n);
foreach (string name in names) Console.WriteLine(name); 
// prints out Bob Fred Alice, want names blijft identiek
```

Maar DIT:

```
// beter
List<string> names = new() { "Bob", "Fred", "Alice" };
IOrderedEnumerable<string> sortedNames = names.OrderBy(n => n);
foreach (string name in sortedNames) Console.WriteLine(name); 
// prints out Alice Bob Fred
```

Let wel: Visual Studio rapporteert soms een message als je de eerste code gebruikt... als drie kleine grijze puntjes. Dus kleine les 1: als je bugs hebt, check je messages. Kleine les 2: je kunt dit soort messages ook door Visual Studio laten rapporteren als een (beter zichtbare) warning.


Hoe dan ook, voorbeelden van gebruik van LINQ:
```
List<int> numbers = new() { 1, 2, 5, 10, 20 };


// - transformatie/Select: 

IEnumerable<int> squares = numbers.Select(n => n * n);
// squares is [1, 4, 25, 100, 400] 


// - filteren/Where

IEnumerable<int> evenNumbers = numbers.Where(n => n % 2 == 0);
// evenNumbers is [2, 10, 20]


// -reductie: Sum/Max/Count (en een boel meer!)

int total = numbers.Sum();
// total is 38
```

Er zijn een heleboel LINQ-methodes (zie https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.count?view=net-6.0), een paar om te onthouden zijn:
- _Transformatie:_ Select, OrderBy, Reverse, ThenBy
- _Filtering:_ Where, Distinct, Take, TakeLast, Skip, SkipLast
- _Reductie:_ Any, All, Average, Concat, Count, First, FirstOrDefault, Max, MaxBy, Min, MinBy, Single, SingleOrDefault, Sum

Je hoeft deze niet uit je hoofd te leren, het belangrijkste is om te onthouden dat als je iets wilt doen met een lijst dat andere programmeurs ook weleens zouden willen (zoals filteren of sorteren), of als je één enkel stuk data wilt krijgen uit een lijst, dat er waarschijnlijk een LINQ-methode bestaat die precies dat doet!

### LINQ tip: Als LINQ een () heeft kan je de expressie vaak vereenvoudigen

Als het laatste deel van een LINQ-expressie geen argumenten heeft, en voorafgegaan wordt door een ander statement, kun je vaak het andere statement vervangen door het laatste deel, en het laatste deel de oorspronkelijke argumenten van het voorlaatste deel geven. Dus c.A(x).B(); kun je vaak vereenvoudigen tot c.B(x); Concreet:
```
class Customer 
{
    public string Name { get; set; }

    public int Id { get; set; }
}


List<Customer> customers = new() 
{ 
    new() { Name = "Willem", Id = 1},
    new() { Name = "Klara", Id = 2 }
};

// kan 
Customer customerOne = customers.Where(c => c.Id == 1).FirstOrDefault();
Console.WriteLine(customerOne.Name);

// beter/simpeler:
Customer customerTwo = customers.FirstOrDefault(c => c.Id == 2);
Console.WriteLine(customerTwo.Name);

// kan 
string lastInAlphabet = customers.Select(c => c.Name).Max();
Console.WriteLine(lastInAlphabet);

// beter/simpeler
string lastInAlphabet2 = customers.Max(c => c.Name);
Console.WriteLine(lastInAlphabet2);
```

<div style="page-break-after: always;"></div>

## C# enums

C# heeft een aantal bijzondere datastructuren, één ervan heet de `enum`. Een enum is een soort vermomd geheel getal met een leesbare naam die (iets) meer veiligheid biedt dan een integer-constante. Je definieert ze als volgt:

```
enum Weekday { WeekdayNotSet, Monday, Tuesday, Wednesday, 
	Thursday, Friday, Saturday, Sunday };

enum Direction { DirectionNotSet, North, South, East, West };
```

Let op: de eerste waarde van een enum hoeft niet iets te zijn als 'WeekdayNotSet', en ik zal deze in de volgende voorbeelden ook weglaten voor de compactheid. Maar in produktiecode is een EnumNameNotSet (of EnumNameError of EnumNameBug of iets dergelijks) wèl erg handig, zoals ik later zal uitleggen. 

Waarom gebruik je enums? Wel, omdat je anders vaak getallen met min of meer onduidelijke betekenissen moet teruggeven, zoals 0 bij succes, -1 als een file niet gevonden is, -2 als de gebruiker het programma onderbrak, of iets anders. Dan krijg je iets als 

```
int DoSomething(string fileName)
{
   // if file not found
   return -1;
   
   // if user interrupts process
   return -2; 
   
   // successful completion
   return 0;
}
```

Dit heeft meerdere nadelen. Allereerst houden programmeurs niet altijd van 
documentatie schrijven, dus vaak krijg je dan iets als 
```
int DoSomething(string fileName)
{
   // <some code>
   return -1;
   
   // <more code>
   return -2; 
   
   // <even more code>
   return 0;
}
```

Dan is het voor iemand die de code moet onderhouden en debuggen nogal een gezwoeg om te begrijpen wat het programma doet.

Maar ook op de plek waar de waarde wordt gebruikt is het lastig.
```
if (DoSomething("test.tmp") == -1) 
{
   // code
}
```

Je kunt je mogelijk voorstellen dat een programmeur zich gaat afvragen wat die -1 nou betekent.

Met enums wordt het een stuk duidelijker:

```
enum Result { Success, FileNotFound, CancelledByUser };

int DoSomething(string fileName)
{
   // <some code>
   return Result.FileNotFound;
   
   // <more code>
   return Result.CancelledByUser; 
   
   // <even more code>
   return Result.Success;
}

// ...

if (DoSomething("test.tmp") == Result.FileNotFound) 
{
   // code
}
```

De meest voor de handliggende toepassing van enums is de vervanging van integers door enum-constanten. Maar je kunt ook strings vervangen door enums. Iets als Result.Success voorkomt de problemen van spelfouten als in `return "succes"`. En daarnaast is het automatisch hernoemen makkelijker. 

En enums zijn vaak zelfs handig in plaats van booleans. Mogelijk heb je weleens methoden gezien als `Console.ReadKey(true)`, waarin je in de documentatie moest duiken om te zien wat `true` en `false` hier betekenden. Voor de lezer van code was het veel makkelijker geweest als de ontwerpers van C# hadden gekozen voor iets als Console.ReadKey(Mode.DisplayChar) versus Console.ReadKey(Mode.HideChar).

Ook zijn enums veiliger te gebruiken in switch statements en expressies, omdat ze beperkt zijn in hun waarden. Tenslotte kan je dan ook rekenlogica gebruiken; je kan dan bijvoorbeeld iets doen als

```
enum Weekday { Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday };

Weekday today = Weekday.Wednesday;
Console.WriteLine("How many days from now?");
int numDays = int.Parse(Console.ReadLine());
Weekday futureDay = (Weekday)(((int)today + numDays) % 7);
Console.WriteLine(futureDay);
```

Enums hebben ook het voordeel van een 'natuurlijke ordening'. Als je boodschappen wilt loggen met een minimum niveau aan belangrijkheid, kun je dat doen met de volgende code. Maar alles waar een 'natuurlijke volgorde' in zit kan profiteren van een enum.

```
enum LogLevel { Debug, Info, Warning, Error, Fatal };

// ... andere code
LogLevel _minLevel = LogLevel.Debug;

void SetMinLevelForLogging(LogLevel level) => _minLevel = level;

void Log(LogLevel level, string message)
{
   if (level >= _minLevel) // log
}
```
Tenslotte is het bij programmeren het beste om iets weer te geven in het 'meest beperkte type dat van toepassing is'; dus als je drie waarden hebt (zeg: mannelijk, vrouwelijk, onzijdig) dan zijn integers met hun 4 miljard waarden en strings met hun ongeveer oneindige hoeveelheid mogelijke waarden minder elegant/verstandig dan een enum met drie waarden. Ook omdat compilers dan beter kunnen checken als je ergens een denkfout of typefout maakt.

Wel zijn er een paar dingen om te onthouden over enums:

### Enum-tip 1: Je kunt C#-enums _erg_ ingewikkeld maken; in de meeste gevallen is dat echter een slecht idee
Je kunt een enum wijzigen met de modifier `[Flags]`, je kunt het basistype van een enum instellen op een ander type zoals long of byte met bijvoorbeeld enum MyEnum : byte { ... }, je kunt de enum-waarden linken met willekeurige integers, zoals North = 3, South = 17 ... Je kunt enums ook casten van en naar nummers (bijvoorbeeld (int)Weekday.Tuesday, maar ook (Weekday)40 ! C#'s enums hebben kort gezegd heel veel mogelijkheden en nauwelijks veiligheidsgordels, dus tenzij het ècht noodzakelijk is (en vraag het voor de zekerheid ook aan een collega): hou enums gewoon zo simpel mogelijk, dat voorkomt heel veel bugs!

### Enum-tip 2: Geef de eerste ('0')-waarde van een enum een waarde waaraan je ziet dat de enum niet geïnitialiseerd is
In C# wordt een enum altijd standaard op 0 gezet als je vergeet hem in te stellen op een bepaalde waarde, dus in code als

```
internal enum Weekday { Monday, Tuesday, Wednesday, 
    Thursday, Friday, Saturday, Sunday };

class Calendar
{
    Weekday today;
    
    public void Display()
    {
        Weekday otherDay = new Weekday();
        Console.WriteLine($"Today is {today}, our appointment is on {otherDay}");
    }
}
```

worden beide dagen op maandag gezet. En dat is waarschijnlijk niet wat je wilt! Je wilt altijd een correcte waarde hebben, òf een waarschuwing dat je programma een bug bevat. Definieer Weekday daarom liever als 
```
internal enum Weekday { WeekdayNotSet, Monday, Tuesday, Wednesday, 
    Thursday, Friday, Saturday, Sunday };
```

en je hebt het gelijk door als je je code moet verbeteren!

## Waarop Unit-test je?


### De doelen van unit-testen

Unit-tests hebben normaal één of meer van vier doelen: 

1. zorgen dat je complexe algoritmes goed programmeert
2. als hulpmiddel bij het oplossen van bugs
3. voorkomen dat functionaliteit kapot gaat als je code debugt of refactort of features toevoegt.
4. sommige soorten leidinggevenden die iets gehoord hebben over 'minimaal 80% code coverage' tevreden houden

Een voorbeeld van 1) (complexe algoritmes goed programmeren) was dat de makers van de browser Netscape een boel unit-tests schreven met alle mogelijke e-mail-headers, want daar zijn nogal veel verschillende varianten van, en die moesten allemaal correct geïnterpreteerd worden. Weinig programmeurs willen na elke codewijziging met de hand controleren dat alle 74 headerformaten nog steeds correct geïnterpreteerd worden, en verstandige programmeurs weten dat zij daar als mensen ook makkelijk fouten bij maken. Unittests zijn daar echter ideaal voor!

Categorie 2: het oplossen van bugs. Soms maak je unit-tests als je aan een relatief ingewikkeld algoritme aan het schrijven bent en je merkt dat bepaalde gevallen misgaan. Dan zijn unit-testen een complement aan de "categorie-1"-unit-testen die maakt vóórdat je gaat programmeren. Dus handig als je de complexiteit van een probleem blijkt te hebben onderschat-wat iedereen weleens gebeurt. Het tweede scenario is als een bug niet ontdekt wordt door de compiler of jouzelf of de peer-reviewer, maar pas in produktie. Of als de bug niet 1-2-3 te begrijpen of op te lossen is. Een unit-test maken leert je sneller of de bug is opgelost dan telkens het programma met de hand runnen, en beveiligt er hopelijk ook tegen dat een toekomstige bugfix, refactoring of nieuwe feature de bestaande code 'breekt'. 

Categorie 3 - voorkomen dat functionaliteit kapot gaat- is iets dat je normaal beslist per project, afhankelijk van hoeveel er aan het produkt gewerkt wordt, hoe kritisch/belangrijk het is, en hoe vaak features breken. Voor een programma dat alleen jijzelf maakt en gebruikt waarbij je af en toe een feature toevoegt zijn unit-tests tegen 'breken' vaak niet de moeite waard. Bij software voor banken of pacemakers, waar miljarden en/of mensenlevens op het spel staan, geldt uiteraard '_better safe than sorry_' en moet je de code goed dichttimmeren met goede unittests. Maar de meeste dingen die je maakt zitten ergens tussen die twee extremen in.

Categorie 4: eis van management. Dit is een moeilijke - veel managers houden van statistieken, om wat voor redenen dan ook. Het is overigens (bij relatief hoge vereisten van kwaliteit) niet slecht om 80% of meer 'code coverage' aan te houden, als management het die prijs waard vindt (als programmeur kun je vaak moeilijker de afweging maken van de kosten versus de waarde van extra kwaliteit voor de organisatie of voor klanten). Wel is belangrijk te beseffen dat '80% code coverage' op zich niets zegt: je kunt 80% code coverage halen door royaal gebruik te maken van `[ExcludeFromCodeCoverage]` of door geen asserts te gebruiken in je testmethodes, of zelfs met 'goedbedoelde' tests die zo zwak zijn dat ze fouten niet opmerken, zoals testen dat GetPhone(1) een Phone-object teruggeeft! En waardeloze testen zijn erger dan het niet hebben van testen, want ze kosten tijd om te schrijven, te doorzoeken, te runnen, en aan te passen. Een interessant praatje over de 'politiek' van unit testen is dat van Roy Osherove: [Lies, Damned Lies, and Metrics • Roy Osherove • GOTO 2019](https://www.youtube.com/watch?v=goihWvyqRow).

### Als je echt goed in unit-testen wilt worden

Drie dingen om te onthouden:

- Unit-testen is vaak niet het leukste onderdeel van je werk, maar als je een echt uitmuntende programmeur wilt worden is ontzettend goede unit-testen kunnen schrijven één van de pilaren daarvan.

- Een echt goede programmeur ziet testen niet als een dogma om de '80% code coverage' te halen, maar als een hulpmiddel bij het programmeren, waarbij de baten van de unit-test ideaal groter moeten zijn dan de kosten (het schrijven, lezen, onderhouden, en de runtime van de unit-tests).

- Goed unit testen is niet makkelijk! Met een handleiding Moq doorlezen ben je er niet. Je wordt pas een goede unit-tester door:
  - anderen om feedback op jouw tests te vragen
  - kritisch de tests van anderen te bekijken
  - een brede test-toolkit te leren beheersen die verder kijkt dan 'unit testen', en bijvoorbeeld ook integratietesten, end-to-end testen en BDD testen bevat
  - met andere programmeurs praten over unittesten
  - leren (via boeken en presentaties) van de grote unittest-experts. Ik ben zelf een grote fan van Khorikov's "Unit Testing Principles, Practices, and Patterns". En Roy Osherove heeft ook vaak interessante gedachten om te melden, bijvoorbeeld op [The Art of Unit Testing • Roy Osherove & Dave Farley • GOTO 2021](https://www.youtube.com/watch?v=6ndAWzc2F-I).


### Hoe test je nou (over wat je precies test, niet de test-naming-guidelines van je team enzo)

Als je wilt testen, doe je dat normaal op basis van kosten/baten: maak liever de test met de meeste waarde voor de minste inspanning en onderhoudskosten, testen die een relatief lage kans hebben op 'false positives' (de code doet het, maar de tests breken), en ook een relatief lage kans hebben op 'false negatives' (de feature werkt niet meer, maar de test doet alsof alles nog in orde is!)

Over het algemeen is testen op returnwaarde het beste. Als dat niet lukt: probeer op 'state' te testen. Als je noch op returnwaarde noch op state kan testen, kan je nog steeds unittesten - op communicatie-, maar doe dat echt alleen als er geen betere alternatieven zijn, want communicatie-testen hebben een relatief grote kans op false positives en false negatives, meer dan de alternatieven. En het schrijven ervan kost overigens ook meer tijd, dus ze hebben ook nog hogere kosten! 

De drie methoden naast elkaar gezet:

1) testen op returnwaarde: als de te testen methode een returnwaarde heeft, test daarop. Bij Phone GetPhoneById(int id) is de meest logische test of het juiste Phone-object wordt gereturnd.

2) testen op state: het testen van de (nieuwe) waarden van de publieke properties of fields van een klasse. In de praktijk doe je zoiets als de te testen methode void teruggeeft, maar het object wèl is veranderd. Maar als je via een andere (publieke) methode kan zien dat de state van het object veranderd is, kan je die methode gebruiken om te checken of void-methode werkt.

Contrasteer:
```
// return-waarde test
// te testen methode: Plate GetContentsOfPlate()

[Fact]
void GetContentsOfPlate_Should_ReturnContentsOfPlate()
{
    // arrange
	Plate plate = new Plate("fries");
	
	// act 
	List<string> contents = plate.GetContentsOfPlate();
	
	// assert 
	Assert.Equal(1, contents.Length);
	Assert.Equal("fries", contents[0]);
}

// state-test 
// te testen methode: void AddToPlate(string foodName)

[Fact]
void AddToPlate_Should_AddFoodstuffToEmptyPlate()
{
    // arrange
	Plate plate = new Plate();
	
	// act 
	plate.AddToPlate("ketchup");
	
	// assert : let erop dat dit alleen betrouwbaar is als GetContentsOfPlate() al goed getest is
	// let er ook op dat GetContentsOfPlate nu niet in de act-sectie zit, maar in feite een 
	// helperfunctie is van de assert-sectie
	List<string> contents = plate.GetContentsOfPlate();
	Assert.Equal(1, contents.Length);
	Assert.Equal("ketchup", contents[0]);
}
```

3) testen op communicatie. Vaak kan je niet testen op returnwaarde of state. Bijvoorbeeld omdat de state in een database wordt opgeslagen, en de database in je unittests gemockt wordt. Hoe moet je een methode dan unittesten? 

Één antwoord is dat je dan vaak een integratie-test schrijft of een in-memory database gebruikt, zodat je dan wèl state-testen kunt schrijven. Maar voor het vervolg zal ik aannemen dat je mogelijk nog niet geleerd hebt integratie-testen te schrijven of dat het team waarin je werkt integratietesten onacceptabel traag (of te moeilijk) vindt maar wèl 80% code coverage wil halen. Er zijn overigens ook situaties waarin integratie-testen niet werken en je wel op communicatie _moet_ testen; daar later meer over.

Hoe dan ook, bij een communicatietest test je of de (belangrijke) methoden die de te testen methode aanroept worden aangeroepen als de juiste voorwaarden gelden. En dat ze niet worden aangeroepen als de juiste voorwaarden _niet_ gelden.

Communicatie wordt (zoals je mogelijk weet) getest met bibliotheken als Moq, met Mock-objects, en Setup en Verify-methoden, wat op zich niet erg moeilijk is als je de 'trucjes' kent. Wel vraag je je misschien af waarom het testen van de communicatie een lagere prioriteit heeft dan return-waarde of state-tests.

Returnwaardetests, zoals je hebt kunnen zien, zijn simpeler goed te krijgen dan state-tests, die normaal minstens twee methoden moeten aanroepen om één methode te testen. Communicatietests hebben doorgaans nog meer code nodig dan state-tests, maar dat is niet hun enige probleem. 

Complexe logica kun je meestal uitstekend testen met simpele return-tests, heel soms is misschien een state-test nodig. Diezelfde return- en state-tests werken ook uitmuntend tegen regressiefouten.

Echter, als je code wijzigt ga je vaak methodennamen veranderen of naar andere methoden verwijzen of de logica in een methode aanpassen. Communicatietesten breken niet als je een fout maakt in de logica van een aangeroepen methode (ze checken alleen _dat_ de methode wordt aangeroepen), maar wel als je bijvoorbeeld een methodennaam verandert. Communicatietesten zorgen dus voor heel veel 'false positives' (valse alarmen) en 'false negatives' (onontdekte fouten). De kosten van een communicatie-unittest zijn daarom vaak groter dan de waarde, of tenminste veel groter dan de kosten van return-waarde- en state-testen. 

Zijn communicatietesten dan "nutteloos"? Dat niet. Er zijn omstandigheden waarin je communicatietesten wel _moet_ toepassen. Bijvoorbeeld als je communiceert met een externe API, dat je bijvoorbeeld checkt dat de zoekstring die je naar Google Maps stuurt correct is. En zo zijn er vast meer situaties waarin een communicatietest de enige optie is. 

Mijn persoonlijke indruk is dat communicatietesten (te) vaak worden gebruikt in situaties waar programmeurs oftewel niet weten hoe ze integratietests moeten maken, of geen tijd krijgen om ze te maken, of (soms) dogmatisch integratietests weren. Er zijn altijd mensen die het fijn vinden om één uniforme, simpele manier te hebben om alle problemen mee aan te pakken, of dat nou de 'beste' programmeertaal is of een simpel unit-testrecept. En het 'unit-testen is het testen van 1 methode' (de zogenaamde "London school" van unittesten) is inderdaad een methode die relatief simpel te leren en toe te passen is - het is alleen vaak niet de beste methode om te unittesten.

Echter, als programmeur wil je liever niet in 'heilige oorlogen' belanden met je team. Leer gewoon zelf zoveel mogelijk over unittesten! Mogelijk zijn de testen die je team maakt al goed genoeg voor het produkt waar je aan werkt. En mocht je team in de problemen komen door slechte unittesten, dan zal bijna iedereen behalve de eventuele 'teamidioot' ontvankelijk zijn voor een demo waarin je demonstreert hoe het beter zou kunnen.

Hou communicatietesten dus sowieso in je toolkit (soms zijn ze handig), maar leer liefst ook wat over integratie- en andere testen...   

<div style="page-break-after: always;"></div>

## Test logica, geen data 

Hoe weet je of je code goed is? Normaal check je het de eerste keer door het met de hand te testen. Maar als er veel code is wil je dat niet elke keer doen, en gebruik je unit-tests.

Het lastige van unit-tests is dat je doorgaans wil weten of de _logica_ van een methode goed is- maar datgene wat je in de praktijk kan testen is alleen maar de data die in en uit een methode gaat! En dat kan vervelend zijn, want zelfs als de logica in de methode hetzelfde blijft, kan de data veranderen: wat als Aardvark een telefoon uitbrengt? Dan is phones.Get(0) ineens geen Apple meer!

Laten we kijken naar de scenarios voor het testen van methoden. Laten we concreet kijken naar een unit test voor GetAllPhones().

```
// in PhoneService.cs
public class PhoneService : IPhoneService
{
    private readonly List<Phone> _phones = new()
    {
        new Phone { Id = 1, Brand = "Apple", Type = "IPhone 11" },
        new Phone { Id = 2, Brand = "Huawei", Type = "SpyPhone 1984" },
    };

    public IEnumerable<Phone> Get() => _phones;
}

// in PhoneServiceTests/Get.cs
public class Get : Base
{
    [Fact]
    private void Should_return_all_phones()
    {
        IEnumerable<Phone> phones = PhoneService.Get();

        Assert.Equal(2, phones.Count());
    }
}
```

In dit geval slaagt de unit test. Maar wat als je een extra telefoon wilt toevoegen? Bijvoorbeeld een tweede Apple? Dan gaat de test kapot. En dat is onterecht, wat de testmethode is niet veranderd of minder correct geworden. Dat de test 'breekt' en veranderd moet worden kost alleen maar tijd, zonder waarde op te leveren voor jou als programmeur of voor jouw team of voor jouw werkgever/opdrachtgever. Kan dat niet beter?

In dit geval zou je kunnen overwegen de data die je door wil geven in de unittest zelf te zetten, en de 'normale' data van PhoneService daarmee te vervangen. Het handigste daarvoor is constructor-injectie.

```
// in PhoneService.cs
public PhoneService(List<Phone>? phones = null)
{
    if (phones != null) _phones = phones;
}
```

Ik maak hier gebruik van een default parameter: als iemand 'gewoon' `IPhoneService phoneService = new PhoneService();` gebruikt krijg je precies het oude gedrag.

Maar je kan nu dus een lijst telefoons via de constructor 'injecteren', voor testdoeleinden:

```
// in Base.cs
protected static readonly List<Phone> TestPhones = new()
{
    new Phone { Id = 1, Brand = "Apple", Type = "iPhone 12" },
    new Phone { Id = 2, Brand = "Google", Type = "Pixel 7" },
    new Phone { Id = 3, Brand = "Apple", Type = "iPhone 13" },
};

protected PhoneService PhoneService = new(TestPhones);

// in Get.cs
public void Should_return_all_phones()
{
    // act
    IEnumerable<Phone> phones = PhoneService.Get();

    // assert
    Assert.Equal(3, phones.Count());
}
```

De tests werken nu weer. Mocht ik in de PhoneService iets veranderen, bijvoorbeeld 

```
private readonly List<Phone> _phones = new()
{
    new Phone { Id = 3, Brand = "Apple", Type = "IPhone 11" },
    new Phone { Id = 1, Brand = "Huawei", Type = "SpyPhone 1984" },
    new Phone { Id = 4, Brand = "Google", Type = "Pixel 8" },
    new Phone { Id = 2, Brand = "Samsung", Type = "Galaxy A53" },
};
```

dan werken alle unittests nog steeds.

Echter, bij het introduceren van een fout in de logica van de methode
```
public IEnumerable<Phone> Get() => _phones.Take(2);
```

dan zien de unittests dat onmiddellijk - wat ook is wat je wilt!

# Debuggen

<div style="page-break-after: always;"></div>

## Als ik een object uitprint krijg ik iets raars te zien op het scherm, zoals "PhoneShop.Domain.Phone" inplaats van "Apple IPhone 11"

Bij het aanroepen van een Console.WriteLine()-methode, of, expliciet, met een mijnVariabele.ToString()-methode, zet de .NET-runtime de variabele om in een string - maar die string is niet altijd wat je verwacht!

Standaardtypes als int, bool, double, char (en inderdaad ook string) worden een string zoals je die verwacht: "-1245", "false", "1.24E-7" en "Q". Maar bij de meeste objecten, en zeker bij objecten van klassen die je zelf hebt gedefinieerd, wordt een speciale methode aangeroepen die al gedefinieerd is op de 'superklasse' van alle klassen, de 'object' klasse.

En die methode zegt: als iemand de ToString() methode aanroept, produceer een string die de type van dit object beschrijft! Daarom zie je dus iets als "PhoneShop.Domain.Phone": je krijgt het 'volledig gekwalificeerde type', dus de naam van de klasse, voorafgegaan door de namen van de namespace(s) waar het in zit!

Maar hoe kan je nou de inhoud van het object bekijken? Dat is immers wat je meestal wilt. In de praktijk heb je drie mogelijkheden:

1) Je print handmatig de velden uit die je nodig hebt:
```
// werkt niet:
Console.WriteLine(phone); // output: "PhoneShop.Domain.Phone"

// werkt wel
Console.WriteLine($"{phone.Brand} {Phone.Type}"); // output "Apple IPhone 11"
```

2) Je maakt een aparte methode die het object in een string omzet (of afbeeldt)

```
// werkt niet:
Console.WriteLine(phone); // output: "PhoneShop.Domain.Phone"

// werkt wel
Display(phone);
// ... meer code

void Display(Phone phone)
{
    Console.WriteLine($"{phone.Brand} {Phone.Type}"); 
    // output "Apple IPhone 11"
}
```

3) Je 'overridet' de 'ToString()' methode in de klasse zelf.
```
// werkt wel:
Console.WriteLine(phone); // output "Apple IPhone 11"

// als 
class Phone 
{
    public override string ToString() => $"{Brand} {Type}";
}
```

Maar welke van de drie mogelijkheden moet je nou gebruiken?

Als je slechts op één plaats het object naar een string moet omzetten is de eerste methode waarschijnlijk het beste; een speciale methode maken is dan overdreven. Als je het vaker moet doen is een soort Display-methode maken beter, dan hoef je niet op tien plaatsen dezelfde code te typen.

De ToString() methode overriden doe je normaal alleen voor kleine (console)projecten die je moet debuggen; als je een WinForms- of web-applicatie gebruikt gebruik je toch eerder de debugger dan Console.WriteLine-statements. Maar het is handig te weten dat je het ook zó kan doen!

Opmerkingen:

1. Let op de juiste "signature" van de methode: public override string ToString() {}

2. Je hoeft ToString() meestal niet expliciet aan te roepen; Console.WriteLine bijvoorbeeld roept automatisch ToString() aan op zijn argumenten. Roep ToString() _alleen_ aan als de compiler erom zeurt.

3. ToString is vooral handig voor het debuggen, en voor kleine (meestal privé)-applicaties; in grotere programma's (zeker internationale) heb je meestal aparte klassen die de layout en communicatie met de gebruiker regelen -en ook bijvoorbeeld alles vertalen naar de juiste taal en opmaak. Maar ToString() is wèl enorm handig voor debuggen.

4. In moderne versies van C# kan je vaak ook 'records' gebruiken in plaats van klassen; een record definieert een bruikbare ToString().

5. Basistypes zoals int, char, bool en string hebben een goed werkende ToString-methode. Maar klassen die je zelf definieert hebben dat niet: als je die uitprint, krijg je de naam van de klasse, bijvoorbeeld "Phone" of "SimplePerson". Als je dus ooit rare console-output hebt met daarin niet de waarde die je zoekt, maar de naam van een klasse, dan print je waarschijnlijk ergens per ongeluk een klasse zelf in plaats van een property van de klasse.



<div style="page-break-after: always;"></div>

# C#-architectuur

<div style="page-break-after: always;"></div>

## Wanneer interfaces?

Bij veel C# enterprise-projecten is de structuur tamelijk simpel. Er zijn drie soorten lagen/objecten.

1) de user-interface-laag. Dat is vaak een Console app of een WinForms app of een ASP.NET applicatie. Deze laag wordt meestal niet geunittest, objecten in deze laag hebben normaal _geen_ interface.

2) de service-laag. "all intelligence, barely any knowledge". Service-objecten worden gebruikt door de User-interface-laag om data op te halen en weg te schrijven. Ze slaan (normaal) zelf geen data op, hebben hoogstens een link naar een database. Bij veel C#-projecten zit alle logica van een app (waaraan moet een geldig object voldoen) in de servicelaag. De klassen in de service-laag hebben normaal _wel_ een interface, mede omdat ze dan makkelijk vervangen kunnen worden (van InMemoryPhoneService naar DatabasePhoneService naar WebScrapingPhoneService die allemaal IPhoneService kunnen implementeren).

3) de data-laag. In Enterprise-C# bevat de datalaag doorgaans "domme" objecten. "All knowledge, no intelligence". Meestal alleen simpele properties met { get; set; }. Deze dataklassen hebben geen interface, omdat ze geen gedrag en logica hebben buiten het simpelweg opslaan en ophalen van data.

<div style="page-break-after: always;"></div>

## Circulaire dependencies en interfaces

Je hebt mogelijk gemerkt dat je allerlei problemen kreeg als je de DatabaseLogger in het Logger-project liet verwijzen naar de IRepository in het Business-project, en de PhoneService in het Business-project verwees naar de DatabaseLogger (of ILogger), als die in het Logger-project zaten.

Dat komt omdat C# (net als bijvoorbeeld Java) probeert een boom op te zetten van dingen die eerst gecompileerd moeten worden voor de rest gecompileerd kan worden. C# kijkt dus naar je solution en ziet: Oh, om Winforms te compileren moet ik eerst de Business compileren, en om de Business te kunnen compileren moet ik eerst het Logger-project compileren want daar zit de ILogger in. Dan gaat de compiler naar het Logger-project, en ziet dat om DAT te compileren hij eerst het _Business_-project moet compileren, want daar is de Repository`<T>` in gedefinieerd. Zoals je mogelijk begrijpt, geeft de C#-compiler dan op, met een 'circular dependency'-foutmelding, omdat jij valsspeelt.

Hoe los je dat probleem op?

Zoals je nu mogelijk beseft is één reden om alle interfaces in het domein-project te plaatsen en overal dependency-injection te doen op basis van interfaces een slimme (of tenminste gekluns-bestendige) methode om alles goed te laten gaan. Dus hoe goed (of hoe slecht) je architectuur ook is: als je ergens één of meer domeinprojecten hebt waar al je interfaces in zijn gedefinieerd, en overal dependency-injection gebruikt - of tenminste op de plaatsen waar je anders circulaire dependencies zou krijgen - is het probleem met circulaire dependencies eenvoudig opgelost.

Maar wat als je wat kritischer bent, en voelt dat een project met een goede architectuur geen centrale berg met onverwante interfaces nodig zou moeten hebben? Immers, een helder design heeft meer voordelen dan het niet hebben van circulaire dependencies.

In dit geval zou je normaal aparte databases gebruiken voor logging en voor de phones; mogelijk zelfs een ander type database omdat logging niet het complexe netwerk van relaties heeft dat normale data wel heeft, en omdat hoeveel je ernaar schrijft en ervan leest ook erg anders is dan bij de Phones en Brands-tabellen.

In dit geval zou ik waarschijnlijk een configsettings maken met aparte connectionstrings voor de Phone-database en voor de logging-database; het logging-project zou zijn eigen repository gebruiken en niet meer afhankelijk zijn van het Repository in de PhoneShop.Business, wat de cyclus ook zou verbreken.

Betekent dat dat een goede programmeur nooit een Domain-project met interfaces-folder nodig heeft? Wel, zeg nooit nooit; er zijn ingewikkelde projecten en veel gevallen waarin je met goede redenen afhankelijk wilt zijn van interfaces en niet van implementaties. Persoonlijk zou ik proberen de logische structuur van het project zo helder mogelijk te krijgen en files die sterk gerelateerd zijn (zoals IPhoneService en PhoneService) in hetzelfde project te zetten, en aparte "interface"-projecten alleen gebruiken in noodgevallen. Maar er zullen ongetwijfeld C#-teams zijn die 'interface-projecten' gebruiken en er geen grote problemen mee hebben, en waarbij 'nettere code' zich waarschijnlijk niet terugbetaalt ten opzichte van de tijdsinvestering die ervoor nodig is. Kortom: weet dat een apart 'interface-project' bijna nooit nodig is, maar wees tolerant voor teams die het door historische beslissingen toch gebruiken of nodig hebben, en wees bereid de 'interface-verplaatsing' uit te voeren als dat echt nodig is - maar uiteraard liefst in overleg met de architect!

<div style="page-break-after: always;"></div>

## Wanneer constructors?

De meeste programmeertalen hebben constructors, speciale methoden die een object initialiseren. Ook C# heeft constructors, die erg op methoden lijken maar te herkennen zijn aan hun naam (ze hebben dezelfde naam als de klasse) en aan hun returntype (dat ze niet hebben, zelfs geen void). Toch worden in C# constructors minder gebruikt dan in andere programmeertalen.

Constructors hebben twee voordelen: allereerst zorgen ze voor een compactere notatie om een object te initialiseren. En minder regels code helpt de leesbaarheid, schrijfbaarheid en onderhoudbaarheid van de code. In plaats van

```
var p = new Point();
p.x = 3;
p.y = 4;
p.z = 5;
```

wordt het afgekort tot

```
var p = new Point(3, 4, 5);
```


Het tweede voordeel is dat constructoren kunnen checken dat een object correct wordt geïnitialiseerd. Dat alle velden/properties een geldige waarde hebben, en dat de combinatie ook geldig is.

```
var p = new Person("Pietje", "", -3);
```

zou bijvoorbeeld een exceptie kunnen gooien: "Person needs a last name; person's age can not be negative." Heel handig voor debuggen.

Nu zie je bij C# echter dat constructoren minder gebruikt worden dan in andere talen. Dat heeft te maken met drie factoren:
1) C# heeft (in tegenstelling tot de meeste talen) "object initializers"
```
var p1 = new Point(3,4,5); // constructor
var p2 = new Point { X = 3, Y = 4, Z = 5}; // object inializer

```
2) C# heeft (in tegenstelling tot veel andere talen) properties - andere talen gebruiken vaak private fields, en die _moet_ je wel via een constructor vullen.
3) C# heeft een eigen afweging in de balans tussen schrijfbaarheid en de buggevoeligheid van een programma (Java heeft bijvoorbeeld zelfs 'checked exceptions' om te voorkomen dat een exceptie wordt gemist door de programmeur!)

Deze factoren leiden ertoe dat in C# in het bedrijfsleven constructoren normaal alleen gebruikt worden in services; services hebben vaak 1 of 2 private fields, en die kan je niet vullen met object initializers. Data objecten (zoals Phone) zijn in commerciële C# code meestal 'dom': ze hebben geen of nauwelijks logica, en normaal geen constructors. Als er bepaalde vereisten zijn voor de geldigheid van het object worden die vereisten normaal in de aanroepende service gezet, en niet in de constructor.

Uiteraard is dat niet altijd een goed idee omdat dat kan betekenen dat de validatielogica buiten de klasse zelf staat en minder makkelijk wordt geupdated als de properties veranderen; of dat de validatielogica gedupliceerd wordt tussen services. Of dat, als je properties toevoegt, aanroepende code vergeet die te initialiseren -object initializers tellen de velden niet. 

Desalniettemin, als je in een bedrijf gaat werken, en je ziet geen constructoren in dataklassen, overleg met de rest van je team als je er constructoren in wilt zetten; het is legaal in C#, maar wel onconventioneel in dataklassen, en wegens de 'vreemdheid' ervan (waardoor het moeilijker leesbaar is omdat mensen vragen gaan stellen als "Waarom heeft dit een constructor? Het is toch geen service?") feitelijk alleen te rechtvaardigen als er in de praktijk codeduplicatie of fouten optreden door het ontbreken van een constructor.

Samenvattend: gebruik constructoren voor services die private fields hebben; maar _niet_ voor dataklassen als Phone tenzij je daar zeer goede redenen voor hebt en er (bij een team) met de rest van het team over hebt overlegd. Gebruik anders voor dataklassen gewoon object initializers.

<div style="page-break-after: always;"></div>

## Wanneer (en waarom) properties?

_"If you're still creating public fields in your types, stop now."_ - Bill Wagner, "More Effective C#"

Lang geleden moesten programmeurs werken met simpele variabelen:

```
personFirstName = "Hans";
personLastName = "De Vries";
personDateOfBirth = "12-12-1916";

ShowPerson(personFirstName, personLastName, personDateOfBirth);
```

Dit had uiteraard nadelen, zo moest je het eerste deel ("person") telkens opnieuw schrijven, als je arrays had moest je 1 losse array hebben voor de firstName, 1 voor de lastName, enzovoorts, en was het te makkelijk per ongeluk de verkeerde voornaam aan de verkeerde achternaam te koppelen; methoden werden wel erg lang omdat je een aparte parameter had voor (in bovenstaand voorbeeld) de firstName, de lastName... je kon makkelijk methoden krijgen met 5 of 10 parameters! Moeilijk te lezen, te schrijven, te onderhouden en te debuggen.

Daarom kwam in de jaren '70 de programmeertaal C met structs (https://en.wikipedia.org/wiki/Struct_(C_programming_language)):

```
struct person {
   char firstName[50];
   char lastName[50];
   char dateOfBirth[9];
}

...

struct person p;
p.firstName = "Hans";
p.lastName = "De Vries";
p.dateOfBirth ="12121916";

showPerson(p);
```

Dat was uiteraard veel handiger, ook voor functieaanroepen. Echter bleken structs ook problemen te hebben: er was geen 'kwaliteitscontrole' op, elk deel van een programma kon de inhoud van de struct aanpassen met willekeurige waarden. Zo maakte ik bij een groot commercieel project mee dat er soms een foute waarde zat in een struct, en elk van de 100,000 andere regels in de broncode kon die fout veroorzaakt hebben! De 'openheid' van structs zorgde voor veel en moeilijk op te sporen bugs. Daarom besloten latere ontwerpers van programmeertalen, zoals die van Java in de jaren '90, dat velden van structs normaal alleen te wijzigen waren door code in de struct zelf. De velden werden dus 'private'.

C-code als 
```
p.dateOfBirth = "Amersfoort";
```

werd dus onmogelijk, je kreeg een foutmelding als "dateOfBirth is private in Person".

Wel betekende de Java-manier dat je als programmeur meer moest typen:

```
class Person {
    String firstName;
    String lastName;
    String dateOfBirth;
    
    public Person(String newFirstName, String newLastName, String newDateOfBirth) {
        firstName = newFirstName;
        lastName = newLastName;
        dateOfBirth = newDateOfBirth;
    }
    
    String getFirstName() {
        return firstName;
    }
    
    void setFirstName(String newFirstName) {
        firstName = newFirstName;
    }
    
    String getLastName() {
        return lastName;
    }
    
    void setLastName(String newLastName) {
        lastName = newLastName;
    }
    
    String getDateOfBirth() {
        return dateOfBirth;
    }
    
    void setDateOfBirth(String newDateOfBirth) {
        dateOfBirth = newDateOfBirth;
    }
}
```

Dit was niet zozeer veel typewerk (editors als Eclipse vulden automatisch de code in als je de veldnamen gaf), maar wel veel leeswerk! Wat je liever ook niet wilde. De makers van C# wilden graag mensen uit het Java-kamp naar het Microsoft-kamp trekken, en probeerden dus iets compacters te bedenken dan de Java-stijl. Eerst werd dat

```
class Person 
{
    string _firstName;
    string _lastName;
    string _dateOfBirth;
    
    public string FirstName 
    { 
        get 
        { 
            return _firstName;
        }
        set
        {
            firstName = value;
        }
    }
    
    public string LastName { 
        get 
        { 
            return _lastName;
        }
        set
        {
            lastName = value;
        }
    }
    
    public string DateOfBirth 
    { 
        get 
        { 
            return _dateOfBirth;
        }
        set
        {
            _dateOfBirth = value;
        }
    }
}
```

Hieraan is hopelijk nog te zien dat properties geen 'velden' of 'state' zijn, maar vermomde methoden! _Daarom mag je ook properties hebben in interfaces, terwijl velden in interfaces niet mogen_.

Hoe dan ook: zo had je geen constructor meer nodig (je kon zeggen p.FirstName = "Hans"; - door de compiler omgezet in p.set_FirstName("Hans"), maar nog steeds was het behoorlijk wat leeswerk; en mensen gebruikten bijna altijd het patroon "type _varName; public type { get { return _varName; } set { _varName = value; }}". Daarom introduceerde C#3.0 (in 2007) zogenaamde "auto-properties", waar die zich herhalende code werd geëlimineerd:

```
class Person 
{
    public string FirstName { get; set; } 
    
    public string LastName { get; set; } 

    public string DateOfBirth { get; set; } 
}
```

Zoals je ziet, veel minder code!

Let wel, als je zou willen controleren of de waarde van een property wel klopt (dat bijvoorbeeld de DateOfBirth niet in de toekomst ligt) dan moet je weer een _dateOfBirth-veld introduceren en de volledige get;/set; syntax. Mogelijk is dat één van de redenen waarom C# programmeurs vaak liever die moeite niet doen en validatielogica vaak in de services zetten in plaats van in de dataklasse. 

En _wanneer_ properties? Normaal gebruik je properties als een veld/data public moet zijn, dus in data-klassen als Phone. Als data private moet zijn (vaak readonly private, waarvan de waarde wordt toegekend in de constructor en absoluut niet iets dat andere objecten moeten zien, laat staan aan moeten zitten) maak je er een veld van, dus private readonly type _myField.

En ja, properties zijn ook veel beter in het geval je een soort kwaliteitscontrole wilt invoeren op je velden: beter een property veranderen dan 100 x in je code zeggen "if (string.IsNullOrEmpty(name)) throw new ArgumentException("Name is too short"); else person.Name = name;" Daarnaast kan je properties ook makkelijker veilig maken bij multithreading. Met properties kan je geen bugs krijgen door ze door te geven als 'ref' of 'out' parameters - in tegenstelling tot bij fields, waar dat wel kan. En properties geven meer controle over de toegankelijkheid van een waarde: je kunt een getter bijvoorbeeld public maken, maar de setter private. Tenslotte:mocht je denken dat je een public field later altijd wel in een property kan overzetten, vergeet dan niet dat alle code die dat veld gebruikt dan gehercompileerd moet worden, anders breekt het. En dat is uren werk voor een boel mensen, die je dat niet in dank zullen afnemen. Ellende die je jezelf bespaart door iets meteen een property te maken.

En... properties zijn de toekomst. Talen ontworpen ná 2010, zoals Kotlin en Swift, hebben geeneens velden meer: wat een veld lijkt (var x: Int) is eigenlijk een property, die je optioneel kan manipuleren met getters en setters. Net zoals programmeurs anno 2022 afwillen van switch-statements en for-loops en andere bagage die nodig leek in de jaren '70, is het (publieke) 'veld' ook iets dat langzamerhand onmogelijk wordt gemaakt; veel onderhoudbaarder, en compilers zijn tegenwoordig zo slim dat properties in de broncode worden 'weggecompileerd' en geen performance-verlies meer geven.

En zelfs als dàt je niet overtuigt moet je er nog steeds rekening mee houden dat je code vaak moet werken met de bibliotheken van Microsoft. En die zijn er op gebaseerd op properties. Dus WPF, Windows Forms, en Web Forms kunnen alleen gebruik maken van properties, niet van public fields. Dus vaak moet je sowieso properties gebruiken, anders werkt je programma niet!

Let wel: heel soms gebruiken mensen private properties en public fields; maar zeker public fields zijn een slecht idee, want _als_ je moet gaan debuggen omdat ergens in de andere 100,000 regels code de waarde van dat veld onterecht wordt veranderd, moet je hele code hercompileren, en als je de pech hebt dat je code ook door andere applicaties gebruikt wordt, worden die mensen boos op je omdat hun code kapot gaat. Als je data public maakt, zet hem dan alsjeblieft in een property, voor het geval dat. Die 13 karakters extra leeswerk en paar karakters extra typewerk (prop TAB TAB is handig) zijn peanuts vergeleken met de problemen die je krijgt met anderen als je een public field later alsnog moet omzetten naar een property.

**Meer lezen**: 

- _"More Effective C#"_ -Bill Wagner


<div style="page-break-after: always;"></div>

## Wanneer moet iets public zijn, en wanneer private?

C# heeft momenteel zeven verschillende "access modifiers": public, private, protected, internal, private protected en protected internal (https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/access-modifiers). "internal" zie je weleens voorbij komen in door Visual Studio gegenereerde code, "protected" in voorbeeldcode over object-georiënteerd programmeren (en soms in een Base class als je die gebruikt voor unit tests). En "private protected" en "protected internal" zijn eerder om andere programmeurs mee te ergeren ("Jij weet vast niet eens wat het verschil is tussen private protected en protected internal!"). Maar "public" en "private" moet je zeker kennen en toepassen.

Wanneer moet iets public zijn?
1) als het een property is (properties _mogen_ private zijn, maar dat is bijna nooit nuttig)
2) als het een methode is die 'gepubliceerd' is via een interface; als de IPhoneService zegt dat implementerende klassen een IEnumerable`<`Phone> Get()-methode moeten hebben, dan moet de IEnumerable`<`Phone> Get()-methode in PhoneService public zijn.

Bij conventie is de Main-methode die het aangrijppunt van je programma is doorgaans public (public static void Main(...), maar in theorie zou hij evengoed private kunnen zijn. Hier is "public" gewoon een conventie, mensen zouden raar opkijken als je hem private zou maken - hij werkt dan nog steeds, maar (bijna) niemand programmeert zo!

Wanneer moet iets private zijn?
Wel, in alle andere gevallen.
In het bijzonder zijn velden meestal private (en zelfs readonly private). Methoden die niet door een interface vereist worden zijn doorgaans ook private.

Waarom zoveel 'privacy'? Simpelweg omdat als je een methode public maakt, je min of meer aankondigt: tot in lengte van dagen kan je deze methode gebruiken-en zal hij altijd hetzelfde blijven werken! Dat klinkt misschien fijn, maar in de praktijk blijkt vaak dat je software moet veranderen of aanpassen. Als _alles_ public is is de kans enorm groot dat als je iets verandert, de programma's en bibliotheken die jouw code gebruiken niet meer werken of aangepast moeten worden - wat veel woedende managers en mede-programmeurs kan opleveren. Daarom probeer je zo veel mogelijk dingen 'private' te maken, dan kan je code makkelijker aanpassen zonder dat anderen daar problemen door ondervinden.

Een tweede reden is overigens dat dingen private maken ook fouten vermijdt. Neem bijvoorbeeld de volgende klasse:


```
public class MyStack
{
    public List<int> _list = new();

    public void Push(int n) => _list.Add(n);

    public int Pop()
    {
        if (_list.Count > 0)
        {
            var topElement = _list[^1];
            _list.RemoveAt(_list.Count -1);
            return topElement;
        }
        throw new InvalidOperationException();
    }
}
```

omdat _list public is, kan een programmeur iets doen als 

```
var stack = new MyStack();
stack.Push(1);
stack._list = new();
var x = stack.Pop();
```

Dat kan natuurlijk een foutje zijn (soms is het een poging jouw code te kraken!), maar je wilt zulke fouten uiteraard zoveel mogelijk vermijden. 

Een bijkomend voordeel van een scheiding tussen 'public' en 'private' is dat je ervoor kunt zorgen dat als een client of gebruiker een foute invoer kan geven, per ongeluk of expres (google maar eens op 'SQL injection'), dat je dan alleen in de publieke methoden hoeft te controleren of de invoer goed is, als die niet goed is moeten ze een exceptie gooien ofzo; de private methoden, omdat die alleen door de public methoden worden aangeroepen, krijgen dan per definitie gecontroleerde/schone data, en kunnen daardoor eenvoudiger en korter zijn. Vergelijk het met een huis: als elke kamer een deur naar de straat heeft moet je wel heel veel sloten en grendels en sleutels aanschaffen!

Kortom: weet wanneer je public gebruikt - voor properties en interface-implementerende methoden, en voor de static void Main, en private - voor de rest.

<div style="page-break-after: always;"></div>

## Hoe maak ik een lagenstructuur met een 'echte' service?

Als je van een simpele console-app gaat naar een app met lagenstructuur (dat de consoleapp niet meer de data bevat, maar de data opvraagt van een service), moet je aan de volgende dingen denken; beschouw het als een stappenplan.

1) Maak een nieuw  bestand aan met een nieuwe klasse, bv in het Business-project (maak uiteraard eerst een Business-project aan als je het nog niet hebt). Die klasse moet gaan heten "_X_Service", bijvoorbeeld PhoneService".

```
// in PhoneService.cs
public class PhoneService 
{
}
```

2) Verplaats de code die de dataobjecten genereert, opslaat of oplevert naar die service.

```
// vóór

// in Program.cs
class Program 
{
	private List<Phone> _myPhones = new() { new() {Brand="Apple", Type="IPhone XI"};
	
	public static void Main()
	{ 
	    // ....
	}
}

// in PhoneService.cs
public class PhoneService 
{
}


// na

// in Program.cs
class Program 
{
	public static void Main()
	{ 
	    // ....
	}
}

// in PhoneService.cs
public class PhoneService 
{
    private List<Phone> _myPhones = new() { new() {Brand="Apple", Type="IPhone XI"};
}
```

3) maak een (private readonly) veld in de consoleapp aan die een object van de service kan bevatten

```
// vóór

// in Program.cs
class Program 
{
	public static void Main()
	{ 
	    // ....
	}
}


// na

// in Program.cs
class Program 
{
    private readonly PhoneService _phoneService = new PhoneService();

	public static void Main()
	{ 
	    // ....
	}
}
```

4) De consoleapp zal nu niet meer compileren (allemaal rode kringeltjes). Op elke plaats waar je eerst bijvoorbeeld de lijst met phones gebruikte, vervang die code door een aanroep naar de service, en maak ook een geschikte methode aan in de service:

```
// vóór

// in Program.cs
class Program 
{
	private readonly PhoneService _phoneService = new PhoneService();
	
	public static void Main()
	{ 
	    foreach (var phone in _myPhones)
	    {
	    	Console.WriteLine($"{phone.Brand} {phone.Type}");
	    }
	}
}

// in PhoneService.cs
public class PhoneService 
{
	private List<Phone> _myPhones = new() { new() {Brand="Apple", Type="IPhone XI"};

}


// na

// in Program.cs
class Program 
{
    private readonly PhoneService _phoneService = new PhoneService();

	public static void Main()
	{ 
	    foreach (var phone in _phoneService.Get())
	    {
	    	Console.WriteLine($"{phone.Brand} {phone.Type}");
	    }
	}
}

// in PhoneService.cs
public class PhoneService 
{
    private List<Phone> _myPhones = new() { new() {Brand="Apple", Type="IPhone XI"};
    
    public IEnumerable<Phone> Get() => _myPhones;
}
```

5) Om het helemaal netjes te maken mag je in de consoleapp niet een concreet type als PhoneService gebruiken, maar alleen een interface als IPhoneService. Hoe je helemaal de PhoneService kwijtraakt leer je als je bezig gaat met dependency injection, maar voorlopig moet je dus een IPhoneService interface maken die alle methoden declareert die public moeten zijn in PhoneService, en het veld in de consoleapp het type IPhoneService geven in plaats van het type PhoneService.

Dus:

```
// 5.1 Maak een interface
public interface IPhoneService
{
	public IEnumerable<Phone> Get()
}

// 5.2 Laat de Service de interface implementeren
public class PhoneService : IPhoneService
{
    // ....


// 5.3 Verander het type van veld in de consoleapp
public class Program 
{
    private readonly IPhoneService _phoneService = new PhoneService();

    // ....
}
```

Ja, dit bovenstaande is nogal wat gedoe, en zeker voor kleine projecten is het 'overkill'. Maar dit is wel hoe veel commerciële C#-projecten worden opgezet.

Rationeel gezien is het zetten van code in een service handig als de Program-klasse zo groot wordt (honderden regels) dat je anders het overzicht verliest; en is het maken van een interface handig zodra je verschillende PhoneService-implementaties hebt. 

Voor de meeste commerciële projecten is het extraheren van een Service vroeg of laat sowieso nodig (het komt zelden voor dat een echt project minder dan 500 regels code bevat). 

Of je een interface voor elke service moet maken is discutabel (iemand als Vladimir Khorikov in zijn boek "Unit Testen" gelooft er niet echt in), maar het is in elk geval in de C#-wereld zo standaard dat teamgenoten mogelijk verward worden als je het niet doet - het overtreden van YAGNI wordt in dit geval als minder belangrijk gezien dan het "principle of least surprise" (of voor wat dat team als verrassend geldt). Hoe dan ook, hoewel teams verschillend omgaan met interfaces (vraag je team wat hun conventies of regels zijn!), moet je zeker interfaces kunnen maken, want soms hoort het bij de codestandaarden van een team, en soms zijn ze ook daadwerkelijk nuttig. Vandaar dat we ermee oefenen! 


<div style="page-break-after: always;"></div>


## Hoe ga ik om met mogelijke foute input voor methoden?

Methodes krijgen normaal een aantal parameters. Vaak is zo'n parameter een standaard-type als een int of een string. 

In de praktijk kunnen methoden zelden goed omgaan met alle 'legale' inputs. Bijvoorbeeld: wat als je een methode maakt die de eerste letter van een woord neemt, maar je geeft een lege string, of zelfs een null? Of een methode die het aantal kinderen van iemand opslaat, en die wordt aangeroepen met -1?

"" en null zijn legale strings, en -1 is een legale integer, dus de compiler zal niet klagen. Maar als het programma wordt uitgevoerd zal het crashen of - wat vaak nog erger is- foute data opslaan.

Hoe ga je met dat probleem om als programmeur?

De oplossing hangt af van verschillende factoren:

- hoe belangrijk is het dat het programma altijd correcte resultaten geeft versus hoe belangrijk is het dat het programma altijd blijft werken? Als een programma de dosis van een geneesmiddel moet berekenen is het beter dat het crasht met een foutmelding als iets onverwachts gebeurt dan dat het een dodelijke dosis voorschrijft; voor een webbrowser is het beter om de pagina in een verkeerde kleur of met een verkeerd lettertype af te beelden dan om te crashen.

- wat is de oorzaak van het probleem? Er zijn basaal vier verschillende oorzaken van problemen met foute invoer.

1) de programmeur is 'lui' - "er moet ook iets gedaan worden als deze waarde wordt gegeven/als dit optreedt, maar ik heb nu geen tijd dat te implementeren" (de invoer is in theorie geldig, maar wordt niet goed afgehandeld)

2) de programmeur heeft ergens een bug in zijn of haar programma (roept bijvoorbeeld een stringfunctie aan op een null-string).

3) een gebruiker geeft een rare invoer (bijvoorbeeld "a" in plaats van een getal) 

4) een hacker probeert het programma te kraken


### De programmeur is lui/heeft te weinig tijd
In het eerste geval (de programmeur is lui of heeft weinig tijd), moet je beslissen wat belangrijker is: correctheid of robuustheid. Als correctheid belangrijker is moet je een NotImplementedException gooien, als die ooit door je baas wordt genoemd zal zij/hij je wel tijd willen geven om het goed op te lossen. Als robuustheid (het programma moet blijven runnen) belangrijker is, dan moet je gokken wat een redelijke waarde is; als je een geweten hebt zorg je er wel voor dat de gebruiker een boodschap te zien krijgt dat het resultaat mogelijk incorrect is. Voorbeeld:

```
/* methode 1: weinig tijd, robuustheid belangrijker dan correctheid

  Opmerking: probeer dit te vermijden of tenminste een soort popup te laten
  zien... */
int CalculateSpeed(Animal animal)
{
   if (animal is Cheetah) return 98;
   if (animal is Rabbit) return 40;
   // Let's just hope the user never looks up other animals
   return 40; // Some default value. 
}

/* methode 2: weinig tijd, correctheid belangrijker dan robuustheid

   Opmerking: dit wordt minder erg als je de exceptie ergens opvangt, 
   dan kan 'hogere' code ergens de correcte communicatie naar de gebruiker 
   afhandelen.

   Opmerking 2: alternatief voor een exceptie is het resultaat te veranderen
   in Result<int>. Alternatief kan je in C#10 en hoger overwegen een int?
   terug te geven, hoewel dat nog steeds enigszins gevaarlijk is omdat de 
   C#-compiler heel veel toestaat met null. */
int CalculateSpeed(Animal animal)
{
   if (animal is Cheetah) return 98;
   if (animal is Rabbit) return 40;
   else throw new NotImplementedException("CalculateSpeed called " + 
       $"on unknown animal {animal}.");
}
```

### Er is een bug in de code
Als de methode _nooit_ zou moeten worden aangeroepen met een bepaalde waarde voor het argument ('name' mag bijvoorbeeld nooit null zijn) dan heb je meerdere keuzes.

Als de foute input zorgt voor een exceptie (NullReferenceException, bijvoorbeeld) èn robuustheid minder belangrijk is kun je ervoor kiezen de code zo te houden; mocht er ooit een foute invoer zijn, dan zie je dat gelijk. Het programma crasht namelijk met een verwijzing naar de regel waar het fout gaat, en dan kan je de bug eruit halen. En de code blijft zo simpel als mogelijk is.

Als de foute input niet zorgt voor een exceptie maar voor ander raar gedrag (bijvoorbeeld foute uitvoer), dan is het meestal beter om zelf een exceptie te gooien. Voorbeeld:

```
void CalculateAgeIn2030(int birthyear)
{
    if (birthyear > DateTime.Now.Year) throw new IllegalArgumentException(
        "CalculateAgeIn2030 error - can't be called with a birthyear " + 
        $"{birthyear} that lies in the future!");
	return 2030 - birthyear;
}
```

Overigens kun je dit soort bugs soms voorkomen door een beter datatype te kiezen; zo kun je met settings in je project ervoor zorgen dat de compiler aangeeft als een parameter null kan zijn (dan vind je de bug al als je het programma compileert, inplaats van pas als je het runt). En soms kan je een simpel type (zoals string of int) vervangen door een type dat je zelf hebt bedacht en dat alleen maar correcte waarden kan hebben, zoals een Birthyear-klasse die waarden kan hebben van 1900 tot nu. Natuurlijk verplaats je in dat geval het exceptiegooiwerk simpelweg naar een andere plaats, maar als het goed is, hoef je dan het checken niet overal in je code te dupliceren, en loop je ook niet het gevaar dat je het ergens vergeet!

Voor meer over alternatieven voor simpele typen (zie ook 'primitive obsession') zie https://enterprisecraftsmanship.com/posts/functional-c-primitive-obsession/

### De gebruiker geeft een rare invoer - bijvoorbeeld een "a" inplaats van een getal

Gebruikers (inclusief ikzelf) maken voortdurend fouten. We typen bijvoorbeeld een punt in plaats van een komma, of een letter in plaats van een getal. Zulke fouten werden vroeger in C# vaak afgehandeld door excepties:

```
var chosenNumberAsString = Console.ReadLine();
try 
{
   var chosenNumber = int.Parse(chosenNumberAsString);
   Display(_phones[chosenNumber]);
} 
catch (Exception e) 
{
   Console.WriteLine($"'{chosenNumberAsString}' is not a valid number.");
}
```

Deze aanpak had nadelen. Het kleinste nadeel was de snelheid-een exceptie gooien is ongeveer 10x zo traag als gewone if-else-logica. Een groter nadeel is dat het gooien van excepties in dit geval niet echt passed lijkt - een vuistregel bij programmeren is dat je excepties moet gebruiken voor 'exceptionele' situaties. Dat een gebruiker een typefout maakt is helemaal niet zo bijzonder of onverwacht.

Tegenwoordig geeft C# steeds meer manieren om te voorkomen dat je bij gebruikersfouten met excepties moet werken. Een modernere versie van bovenstaande code zou zijn:

```
var chosenNumberAsString = Console.ReadLine();
bool isValidNumber = int.TryParse(chosenNumberAsString,
    out int chosenNumber);
if (isValidNumber)
{
   Display(_phones[chosenNumber]);
} 
else
{
   Console.WriteLine($"'{chosenNumberAsString}' is not a valid number.");
}
```

Dit is weliswaar niet veel korter, maar wel helderder (zeker als je TryParse begrijpt).

Hoe dan ook moet je er rekening mee houden (zeker in een desktop-applicatie) dat gebruikers fouten kunnen maken; voor een deel los je dat op door met bijvoorbeeld keuzemenus en UI-elementen die gedeactiveerd kunnen worden de kans op foute invoer te verminderen. Voor een ander deel doe je dat door in het programma zelf te checken of de invoer correct is, en de gebruiker feedback te geven als dat niet het geval is. Gebruikersfouten handel je dus normaal niet af met excepties, maar met if-else-statements, meestal al in de 'frontend'-code.

### Een hacker probeert het programma te kraken

Als je applicatie _geen_ desktopapplicatie is maar ergens op een server draait als backend voor een webpagina of mobiele applicatie, loop je een risico dat onaangename lieden proberen je data te stelen of te vernietigen of andere ellende voor je organisatie te veroorzaken. En via het internet is dat behoorlijk makkelijk als jij de applicatie niet hebt 'dichtgetimmerd'. Geniepige GET of POST-requests kunnen heel veel ellende veroorzaken. Dat betekent dat je nooit een datapakket dat over het web komt moet vertrouwen (al vertrouw je liefst ook geen invoer van een desktopapplicatie; maar het web is zeker gevaarlijk!)

Hoe je met dat probleem omgaat: als programmeur moet je in theorie de OWASP-gidslijnen kennen voor veilige code (https://owasp.org/www-project-top-ten/), en bijna elke 'enterprise-taal', ook C#, heeft bibliotheken die in elk geval delen van de veiligheidsproblemen elimineren. En als je baas wat weet over softwareontwikkeling, huurt hij of zij ook pentesters in om te zien of de programmeurs iets over het hoofd gezien hebben.

Hoe dan ook, hackers kunnen akelige dingen je programma insturen, en zelfs als je dingen goed beveiligt is de kans groot dat de runtime van je programma zelf ergens een exceptie gooit, zelfs als je zelf geen enkele 'throw' in de code hebt gezet. Helaas, als die exceptie nergens wordt opgevangen kan je programma crashen. Dat is ook schade, want je website wordt onbereikbaar voor legitieme gebruikers. Hoe voorkom je dat?

Deze situatie kan je afhandelen door in de 'ingangen' van je programma (de actionmethods van de controllers) try-catch statements te zetten, al kun je C# (in elk geval ASP.NET, wat je normaal voor elke web-applicatie gebruikt) ook configureren zelf de fouten af te handelen. Maar hoe dan ook, als er iets onverwachts in je applicatie optreedt dat geen 'normale' gebruikersvergissing is, mag je gerust een exceptie gooien, want het is beter het proces te stoppen en een neutrale fout te geven (400: Bad Request) dan de hack te laten doorgaan. 

### Samenvattend

excepties gebruik ('throw') je normaal voor
- legitieme invoer die (nog) niet correct wordt verwerkt door een methode
- foute invoer die je verwacht en die je niet kunt of wilt afhandelen met een nullable of result-object - hoewel je dat vaak beter kunt oplossen door een ander datatype te gebruiken of mogelijke foute invoer op te vangen in de code die de methode aanroept, of bij de bron van de foute data, of het nou een gebruiker, een file of een website is.
- als de invoer een aanval/hack lijkt te zijn

In andere gevallen kun je beter proberen te werken met if-else logica of non-nullable of niet-primitieve typen, en de excepties die C# 'spontaan' gooit mogelijk nog te niet op te vangen als je nog in de development-fase bent (dan is het handig als je bugs snel ontdekt), maar in productiecode wel ergens aan de frontend af te vangen. Al zal je dat laatste in ASP.NET ook kunnen doen door middleware en filters goed te configureren; try-cathes zullen niet altijd nodig zijn. 

<div style="page-break-after: always;"></div>

## Waarom staat in C#-code zovaak IEnumerable als returntype in plaats van List?

Mensen die met C# beginnen gebruiken vaak lijsten om reeksen van objecten op te slaan (tenzij ze de tutorials hebben gelezen van heel oude(rwetse) programmeurs, dan gebruiken ze arrays). Het lijkt dan ook logisch om List te gebruiken om waarden terug te geven door methoden; bij interfaces zie je dan ook vaak bij gevorderde beginners

```
List<Person> GetAll();
```

De meeste C#-developers in het bedrijfsleven zouden echter nooit zulke code schrijven, zij geven bijna nooit een concrete collectie terug, maar bijna altijd een interface. Bijvoorbeeld ICollection. Maar het populairst is IEnumerable, dus een professionele C#-programmeur zou de GetAll-methode definiëren als 

```
IEnumerable<Person> GetAll();
```

Waarom zou een rationeel iemand dat doen? Dat is toch alleen maar meer typewerk?

Het is inderdaad meer typewerk. Maar dat typewerk is de moeite waard om twee redenen:

1) (klein voordeel) Je bent flexibeler in welk datatype je eigenlijk teruggeeft: je kan een array returnen, een list of iets anders-dat is dan een implementatiedetail. En implementatiedetails probeer je zoveel mogelijk verborgen te houden voor de "cliënten" van de klasse. Net zoals je zoveel mogelijk 'private' gebruikt in plaats van 'public'.

2) groter voordeel: je voorkomt bugs doordat je zorgt dat de compiler bepaalde dingen onmogelijk maakt. En, raar genoeg, is goed programmeren vaak juist het _beperken_ van wat iemand met je code kan doen.

Als je een typische methode hebt die een lijst teruggeeft:

```
// PhoneService.cs

public List<Phone> GetAll() => _phones;
```

En die ergens anders aanroept, kan je met Add() of Remove() telefoons toevoegen aan of verwijderen van de oorspronkelijke lijst. 

```
var phoneService = new PhoneService();
var phones = phoneService.GetAll();

phones.Add(new() { Brand = "Pear", Type = "Nonsense" });

var newPhones = phoneService.GetAll();
foreach (var phone in newPhones)
{
    Console.WriteLine(phone.Brand);
}
```

Waarschijnlijk is dat niet wat je wilt (niet elke gebruiker zou telefoons mogen toevoegen of verwijderen). En zelfs als dat wel is wat je wilt, zijn alle veranderingen verdwenen als je het programma opnieuw opstart, en dat is dan zeker niet wat je wilt!

Maar wat als je het returntype van GetAll() verandert in IEnumerable? Dan mag de Add niet meer (IEnumerable heeft geen Add()). En zelfs als je zegt var phones = phoneService.GetAll().ToList();, dan is die lijst van telefoons alleen maar een kopie van het origineel, en wordt het origineel niet veranderd als je een telefoon toevoegt.

@@@3: lazy evaluation, geen extra structuren maken voor tussendata...
https://typealias.com/guides/when-to-use-sequences/ Wel: doet LINQ immediate execution voor List? Of is alles deferred?

List<int> myList = new() {1, 0, -4, 3 , 7, -12};
var newList = myList.Select( x => {Console.WriteLine(x); return x;}).Where( x => x > 0).Select( x => {Console.WriteLine(x); return x;}).Select(x => x*x).Select( x => {Console.WriteLine(x); return x;});
foreach (var x in newList) { Console.WriteLine(x);} 

Een diepere uitleg van IEnumerable vs List (en IList) kun je vinden in https://www.claudiobernasconi.ch/2013/07/22/when-to-use-ienumerable-icollection-ilist-and-list/ . Samengevat zegt Bernasconi dat je het meest beperkende type moet gebruiken dat in jouw situatie werkt: IEnumerable is beter dan List, ICollection of IList omdat je niet zomaar elementen kan toevoegen of verwijderen.


<div style="page-break-after: always;"></div>

## Wat is "hexagonale architectuur" en waarom is het belangrijk?

Heel veel werk van een computer, in elk geval voor de 'enterprise'-programma's waarvoor je C# gebruikt, is niet 'berekenen' maar het inlezen, transformeren en opslaan van data.

Zo kan het zijn dat je een lijst telefoons moet inlezen van een CSV- of XML-bestand, en het op moet slaan in een database.

Nu zijn er methoden die XML rechtstreeks kunnen omzetten in databasetabellen (er zijn ongeveer overal methodes en libraries voor). Maar meestal is dat een slecht idee.

Een _klein_ bezwaar is nog dat als je de database of het inleesformaat gaat vervangen, je alle code overnieuw moet schrijven.

Een groter probleem is dat je normaal controle wilt hebben over wat het gebeurt. Wat als een leverancier een XML-file aanlevert met een fout erin? Dan crasht je database of wordt die gecorrumpeerd.

En als het formaat in de database anders is dan in de XML-file, wie heeft dan gelijk?

Daarom wil je een "single source of truth" hebben. En als programmeurs heb je die het liefst in broncode, wat compacter is dan een XML-file en flexibeler dan databasecode. En bovendien netjes in version control staat.

Je gebruikt dus TWEE stappen in plaats van één: 
1) je zet de XML om je domeinobject, bijvoorbeeld een Phone
2) Je slaat de Phone op in de database.

Voor beide stappen maak je normaal een aparte service. Je krijgt dus een XmlImportService en een DatabaseService (al kun je die DatabaseService ook bijvoorbeeld PhoneService noemen).

Dit wordt ook wel 'hexagonale architectuur' genoemd en heeft dus meerdere voordelen:

1. je weet altijd welke data je nodig hebt, er is een 'single source of truth'
2. die single source of truth is op een handige plaats, in de code
3. als er een inconsistentie is tussen input en datamodel merk je dat en wordt de data in de database niet gecorrumpeerd.
4. als er een inconsistentie is tussen datamodel en database krijg je een foutmelding en wordt de data in de database niet gecorrumpeerd.
5. het is veel minder werk een output (zoals een database) of input (zoals een XML-file) te vervangen door een alternatief.

Zelf zie ik een goede service als een pollepel: het ene uiteinde zit altijd in je hand (van jou als programmeur, dus het domeinobject). Het andere uiteinde zit in de pan (de database, de XML-file, de user interface). En het doel van de pollepel/service is data van de ene kant naar de andere kant te brengen. 

Let wel: het gaat hier niet om dat er een "input-kant" en een "output-kant" is; een database gebruik je normaal zowel om van te lezen als naar te schrijven. De kern is de transformatie van data in een bepaald formaat van (en mogelijk naar) data in 'code-formaat'/een domeinobject. 

Een klasse die rechtstreeks inputdata omzet in outputdata (bv JSON naar database) moet je dus liefst vermijden! Net zoals een topkok aparte lepels gebruikt voor het vlees en de groente en de aardappels, zo gebruik je als programmeur aparte services voor communicatie met database, XML-files, user interface (al wordt dat laatste wegens de architectuur van C# normaal geen service genoemd, maar een 'entry point' (wegens de main-functie) - al blijft de taak net zo gespecialiseerd: informatie van en naar de gebruiker brengen...

En als je nadenkt zie je in een goed ontwerp overal hexagonale architectuur: je Program-klasse is bijvoorbeeld een eenheid die consoleinput omzet in phones, en phones omzet in console-output... (daarom zou(den) Program en andere 'frontend-klassen') dan ook de enige klassen moeten zijn met Console-methoden als Console.WriteLine enzo...)

Links:   
- https://en.wikipedia.org/wiki/Hexagonal_architecture_(software)  
- https://alistair.cockburn.us/hexagonal-architecture/​  
- ​https://martinfowler.com/articles/badri-hexagonal/  


<div style="page-break-after: always;"></div>



## Datatype voor prijzen

Voor prijzen: gebruik decimal. Want meestal heeft een prijs cijfers achter de komma, dus int kan niet. En double telt licht onnauwkeurig op, en accountants houden niet van duizendsten van centen die ineens verdwijnen of verschijnen. _Waarom_ double onnauwkeurig is, is een andere vraag (het heeft te maken dat double in de computer als binair wordt aangegeven, en net als ons tientallig stelsel een oneindig lange rij 3en nodig heeft om 1/3 aan te geven, zo heeft een binair stelsel een oneindig lang getal nodig om 0.95 aan te geven, en getallen in de computer hebben natuurlijk geen oneindige lengte!)

Hoe dan ook: gebruik decimal voor prijzen. Voor 'normale' metingen, zoals lengte enzo, waar afrondingsfouten toch niet relevant zijn wegens de beperkte meetnauwkeurigheid, gebruik double, dat is kleiner en sneller.


## Omgaan met nullable

Normaal wil je geen waarschuwingen in je code. Hoe ga je om met alle waarschuwingen van "nullable".

Één mogelijkheid is `<nullable>` in de projectfile (.csproj) op "disable" te zetten.

Mocht je toch nullable willen enablen, dan heb je de volgende opties:
1) bij een methode die mogelijk null aflevert (zoals Console.ReadLine(), als de gebruiker op Ctrl+Z drukt)

- maak ontvangend type nullable: string? answer = Console.ReadLine();
	+ voordeel: geen waarschuwing meer
	+ nadeel: je moet mogelijk verder in het programma alsnog dingen iets complexer maken om de mogelijkheid van null af te vangen.
	+ nadeel: C# is nogal 'tolerant' voor nulls, je kunt alsnog onnodige logische fouten krijgen zonder crash (bijvoorbeeld null + 5 is perfect legaal C#, dus bijvoorbeeld een `int?` kan problemen veroorzaken).
	
- schakel de waarschuwing uit met !: string answer = Console.ReadLine()!;
	+ voordeel: geen waarschuwing meer
	+ voordeel: geen extra aanpassingen in de rest van het programma nodig
	+ voordeel: simpel als je niet verwacht dat je ooit zo'n rare input krijgt of als het niet erg is als het programma soms crasht
	+ nadeel: als de waarde toch null is, crasht het programma
	
- gebruik de null-coalescing operator om een default-waarde te geven: string answer = Console.ReadLine() ?? "0";
	+ voordeel: geen waarschuwing meer
	+ voordeel: geen extra aanpassingen in de rest van het programma nodig
	+ nadeel: op deze plaats iets meer code dan bij de alternatieven
	+ nadeel: soms is het moeilijk een goede default-waarde te bedenken, en is string? duidelijker.
	
Wat de beste methode is hangt af van de situatie. Voor programma's voor privégebruik is '!' het simpelst. Voor 'publieke' programma's is ?? waarschijnlijk het veiligst, alleen te vervangen door ? als je geen goede default-waarde kunt verzinnen.

Voor properties heb je drie alternatieven:

- public string? Name { get; set; } // voordelen en nadelen zoals bij de ?

- public string Name { get; set; } = "onbekend"; // voordelen en nadelen zoals bij de ??

-public string Name { get; set; }
Person(string name) { Name = name; } // dus een constructor. Wat je liever niet gebruikt voor dataklassen, maar soms is het de beste optie.

## Create: wat voor returnwaarde?

Create-methoden hebben doorgaans één van vier mogelijke returnwaarden:

1. `T Create(T t)`

- voordeel:
 - dit is traditie, kan gewoon de afspraak zijn in je team.
- nadelen:
 - vaak gebruik je het object helemaal niet (nogal zinloos)
 - als het misgaat zie je het niet tenzij je de returnwaarde checkt (wat je vaak vergeet)
 - niet erg duidelijk qua communicatie: returntype dat niet void is suggereert een query, terwijl dit een command is. Nu is "Create" natuurlijk wel een suggestieve naam, maar command-query separation negeren is meestal een niet bijster goed idee.
    
2. `int Create(T t)`. // geeft het Id terug van het gemaakte object. Kan overigens ook een `long` of `Guid` of andere identifier zijn, in plaats van `int`.

- voordeel: in sommige kringen traditie (zoals Java/Spring)
- nadelen: basaal hetzelfde als van T
  
3. `bool Create(T t)`

- voordeel:
 - relatief elegant en simpel
 - als je het checkt is het beter performant en heeft minder code nodig dan void/try-catch
 - nadelen: zelfde als voor T en int
   
4. `void Create(T t)`

- voordeel:
 - beste qua 'fool-proof' (of voor verstrooide programmeurs als ikzelf): zelfs als je als programmeur vergeet returnwaardes te checken wordt dat automatisch gecorrigeerd
 - duidelijke communicatie dat dit een command is
 - volgt de vuistregel dat command/query separation meestal goed is
 - automatisch uniforme codestijl, bij returnwaarden kunnen mensen vergeten de waarde op te vragen, wat voor gemengde code van bijvoorbeeld `Phone phone = _phoneService.Create(originalPhone)` en `_phoneService.Create(otherPhone)` leidt. Met een void returnwaarde heb je maar één uniforme stijl in de code, die iets makkelijker te leren, begrijpen en consistent toe te passen is.
- nadelen:
 - is meer code (try-catch nodig)
 - als create heel vaak misgaat kan dit eventueel zorgen voor nodeloos lage performance; dan beter bijvoorbeeld een bool of int teruggeven en (met voldoende unit- en andere testen) checken dat het echt robuust is en gecheckt wordt. Al is zal performance-verlies door exceptions in de meeste projecten niet belangrijk genoeg zijn om excepties te weren.
       
Dat is veel keuze, en het lijkt op het eerste gezicht een moeilijke keuze. Zelf zie ik het echter als een "3-euro keus" waar je normaal weinig tijd wilt besteden aan debatten of piekeren over wat de 'beste' manier is.

De verschillen tussen de types returnwaarden qua programmeertijd en onderhoudbaarheid zijn niet groot; normaal volg je de codestandaarden van je team, en alleen als je meerdere problemen ziet met de aanpak (en die voorkomen kunnen worden met een andere aanpak) dan switch je. Voor privé-projecten gebruik je uiteraard de stijl die jouw voorkeur heeft - ikzelf geef meestal de voorkeur aan de void, zoals je mogelijk al geraden hebt.


# Gebruiksvriendelijkheid

<div style="page-break-after: always;"></div>

## User-rage: waarom je altijd feedback moet geven

Heb je weleens gehad dat je computer het niet deed? Of een kraan? Dat je eraan draaide, eraan trok, op knoppen drukte, en dat niets gebeurde? Hoe voelde je je toen?

Of als je in een restaurant komt, en je ziet de ober ergens een krantje zitten lezen. Je zwaait naar hem- hij doet niets. Je zwaait nog eens -hij doet niets. Je roept: geen reactie. Hoe voel je je op dat moment?

Als een gebruiker iets doet, moet een computerprogramma feedback geven. Als de gebruiker een goede toets indrukt, moet die te zien zijn (in bijvoorbeeld een tekstverwerkingsprogramma) of moet er iets gebeuren (een menu wordt getoond). Als de gebruiker een verkeerde toets inklikt moet er een foutmelding met uitleg gegeven worden. Als het een tijd kan duren voordat een commando kan worden uitgevoerd, bijvoorbeeld bij het bewerken of downloaden van een grote file, moet je dat aangeven met een bewegend ikoontje (zo'n zandloper of Apple's regenboogbal). Anders worden gebruikers gefrustreerd, en boos, en gebruiken ze je software niet meer. Niet leuk! Zeker in de gevallen dat je dan een andere baan moet gaan zoeken.

Dus wat de gebruiker ook doet, zorg dat hij/zij altijd feedback krijgt!

<div style="page-break-after: always;"></div>

# Codestijl-tips

<div style="page-break-after: always;"></div>

## Taal
Hoewel tekst die aan de gebruiker getoond wordt in het Nederlands kan zijn (bij grotere projecten heb je speciale internationalization-bestanden) schrijf je programmacode normaal in het Engels. Niet alleen omdat de code dan niet telkens wisselt van taal (if (mijnHuis.kleur == Color.Orange)...) maar ook omdat veel programmeerteams tegenwoordig multinationaal zijn, niet iedereen kan evengoed Nederlands lezen en schrijven. 

<div style="page-break-after: always;"></div>
## Hoofdlettergebruik

In C# en de meeste andere moderne programmeertalen maakt het uit of je hoofdletters of kleine letters gebruikt in een naam.

'hallo' is dus NIET gelijk aan 'Hallo', 'hAllo', 'HALLO' of 'hallO'. In feite zijn 'hallo', 'Hallo', 'hAllo', 'HALLO' en 'hallO' vijf verschillende 'identifiers' (namen die gebruikt kunnen worden als naam voor een variabele, klasse, namespace of methode).

Zodat je zelf beter onthoudt of je voor een variabele een hoofdletter had gebruikt of ergens een underscore, en opdat andere programmeurs jouw code makkelijker kunnen begrijpen, heeft Microsoft stijlregels opgesteld (https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions). Dat zijn geen absolute wetten - elk bedrijf bepaalt zijn eigen stijlregels, Google heeft weer andere regels voor C#-code - maar het is handig om in elk geval in je eigen code consistent te zijn.

Microsoft kent vier soorten patronen, in volgorde van belangrijkheid:

- lowercase : voor ingebouwde C#-woorden zoals simpele types (string, int), controlestructuren (do, while, for), 'structuurmakende' termen (namespace, class), en 'modifiers' (public, private, virtual, etc.)
- PascalCase: JeBegintElkWoordMetEenHoofdletter
- camelCase: jeBegintElkWoordMetEenHoofdletterBehalveHetEerste
- hungarian_camelCase: bepaalde_identifiersBeginJeMetEenPrefixGevolgdDoorUnderscoreEnDanCamelcase


### PascalCase
PascalCase gebruik je bijna overal voor: de namen van namespaces, klassen, properties en methoden.
```

// namespace 
namespace Phoneshop.ConsoleApp
{

    // class	
    internal class Program
    {   
    	// property
    	public int Answer { get; set; }
    
        // public field (zelden gebruikt! Gebruik liever property...)
        public int OtherAnswer;
        
        // protected field 
        protected string Concern;
    
    	// methode
        static void Main()
```


### camelCase
camelCase gebruik je ALLEEN voor lokale variabelen. Dat zijn variabelen die je in een methode declareert, en parameters die een methode binnenkrijgt.

```
// naam van parameter candidateFriend is camelCase
// ook om het beter te kunnen onderscheiden als 
// het type hetzelfde heet, wat vaak voorkomt!
// Nu zie je dat CandidateFriend het type is, en
// candidateFriend de variabele / parameter
public AddFriend(CandidateFriend candidateFriend) 
{

    // lokale variabele
    int nicenessScore = candidateFriend.Niceness()


}
```

### hungarian_camelCase
LET OP: dit is een nogal omstreden advies. Dit is ook de regel die het meest gebroken wordt; niet iedereen vindt het even leesbaar. Zelf vind ik het wel handig voor constructorargumenten (zie hieronder), maar voor de volledigheid:

```
class DemoClass
{
    // normaal veld (private of protected): begin met underscore
    private string _myName = "Bill";

    // statisch veld: begin met s_ 
    private static int s_totalDemos = 0;
    
    // veld dat gedeeld wordt door verschillende draden: t_
    [ThreadStatic]
    private static TimeSpan t_timeSpan;

}

```

Waarom die '\_'  ? Wel, het argument is ongeveer hetzelfde als bij parameters: vaak worden private velden gevuld in een constructor. Zonder '\_' moet je dan iets doen als

```
class SecondDemo
{
    private int answer;
    
    public SecondDemo(int answer)
    {
        this.answer = answer;
    }
    
    public ShowAnswer()
    {
        Console.WriteLine(answer);
    }
}
```

waar je dus inconsistent bent hoe je het veld 'answer' noemt (this.answer of answer) - en liefst wil je hetzelfde begrip altijd hetzelfde noemen. Je kunt het wel DEELS oplossen...

```
class ThirdDemo
{
    private int answer;
    
    public SecondDemo(int answer)
    {
        this.answer = answer;
    }
    
    public ShowAnswer()
    {
        Console.WriteLine(this.answer);
    }
}
```

maar dat werkt ook niet perfect omdat "private int this.answer;" niet legaal is in C#!


Een veldnaam beginnen met een underscore vermijdt die rits inconsistenties...

```
class FourthDemo
{
    private int _answer;
    
    public SecondDemo(int answer)
    {
        _answer = answer;
    }
    
    public ShowAnswer()
    {
        Console.WriteLine(_answer);
    }
}

```

hungarian_camelCase is dusdanig omstreden dat niet elk team het zal gebruiken (de PascalCase en camelCase worden wel bijna universeel door C#-teams gebruikt), dus maak je eigen keuze. Maar wat je ook kiest: doe het wel consistent in je code!

<div style="page-break-after: always;"></div>

## Lengte van coderegels

Stel dat je veel tekst moet weergeven, bijvoorbeeld de beschrijving van een telefoon "De Apple IPhone X15 is zo modern en luxueus dat u blij zult zijn dat u een tweede hypotheek hebt genomen om hem te betalen, met zijn 10 megapixel camera, ingebouwde DVD-drive, intelligente ingebouwde assistent en 6G internetverbinding..."

In MarkDown (zoals hier) wordt dat allemaal keurig weergegeven. Maar in Visual Studio en in source control (zoals GitHub of Azure Devops) betekent het dat je horizontaal heen en weer moet scrollen om de hele regel te lezen, of zelfs te zien of hij eindigt met aanhalingstekens of een puntkomma.

Over hoe lang een regel mag zijn wordt uiteraard flink gediscussieerd, maar meestal is de vuistregel dat het op de monitoren van de meeste teamleden moet passen; rond de 100 karakters, soms 120. Als de regel van je scherm afloopt, is het beter hem op te splitsen.

Bij normale C#-code kan je dat simpelweg doen door enter in te drukken en de volgende regel vier extra spaties te indenteren (de compiler ziet toch alles tot de ';' als 1 regel), lange strings kan je in stukken hakken met '+', dus

```
// voor een lange regel code
for (var myCounter; myCounter<number; myCounter++) if (i[myCounter] == 3)

// wordt:
for (var myCounter; myCounter<number; myCounter++)
    if (i[myCounter] == 3)

// voor een lange string
var description = "This is the absolutely [lots of text] of all, since";

// wordt 
var description = "This is the absolutely " + 
    "[lots of text] " + 
    "of all, since";

```

Visual Studio voegt automatisch al de " + " toe als je op 'enter' drukt in het midden van een lange string, dus gebruik dat!

<div style="page-break-after: always;"></div>

## Lengte van methoden

Je kunt methodes zo lang maken als je wilt. De IT-consultant en spreker Venkat Subriamam vertelde eens dat hij in een zaal vroeg of mensen ooit hadden moeten werken met methoden van meer dan 100 regels. Een aantal handen gingen op. Meer dan 1000 regels? Een enkele hand. Meer dan 10.000 regels? Diezelfde hand.

Het bleek dat die programmeur had moeten werken met een methode van 20.000 regels. Venkat's verbaasde vraag "Where did you work? In hell?"

Zoals je mogelijk uit bovenstaande kan opmerken, houden programmeurs doorgaans niet van lange methoden. Allereerst omdat dat niet praktisch is; heel veel programmeerwerk in een organisatie is onderhoudswerk, waarbij je een feature moet toevoegen of code moet debuggen.

Zo heb ik zelf weleens gewerkt met methoden van 1000 regels. En ik bedacht me toen hoeveel praktischer het zou zijn als er in plaats van een methode van 1000 regels het zou zijn gedaan met een methode van 10 regels, waarvan elke methode een andere methode van 10 regels zou aanroepen, waarvan elke methode weer een methode met 10 regels zou aanroepen. In plaats van dan (gemiddeld) 500 regels code te moeten lezen voor ik de code vond die ik moest aanpassen, zou ik dan gemiddeld maar 15 (5+5+5) moeten lezen.

Er zijn ook andere argumenten om methoden relatief kort te houden. Vaak is een deel van een methode ook ergens anders nodig. Als de methode is uitgesplitst in korte methoden kun je gewoon die ene methode aanroepen. Maar als het stukje code dat je nodig hebt ergens in een monstermethode van 500 regels zit, hebben drukke programmeurs de neiging 1 van 2 dingen te doen:

- ze copy-pasten de code naar hun eigen methode
- ze voegen een parameter en een if-statement toe aan de oorspronkelijke methode dat als die oorspronkelijke methode wordt aangeroepen met dat argument, dat dan hun berekening wordt uitgevoerd.

Allebei de oplossingen hebben nadelen. Copy-pasten betekent dat als code veranderd of gedebugd moet worden, alles op 2 (of meer!) plaatsen veranderd moest worden. Zo heb ik ooit zelf een fout in produktie geïntroduceerd omdat ik slechts 7 kopieën van de code had aangepast - maar er bleken er 11 te zijn!

Extra parameters en if-statements zorgen ervoor dat de oorspronkelijke methode _nog_ langer en moeilijker te begrijpen (en debuggen) wordt. Als je pech hebt durft niemand dan nog die methode aan te raken of hem op te splitsen in kleinere methoden.

Maar wat is een goede maat voor hoe lang een methode mag/moet zijn? Niemand weet het echt; sommigen houden een praktische limiet aan van 1 pagina in de code-editor, zodat je de hele code kunt zien zonder op en neer te scrollen.

Persoonlijk vind ik de gidslijn van Visser (in zijn boek Building Maintainable Software - C# edition https://www.softwareimprovementgroup.com/wp-content/uploads/Building_Maintainable_Software_C_Sharp_SIG.compressed.pdf) redelijk handig en effectief. Visser zegt dat een C#-methode die langer is dan 16 regels code (15 regels + 1 regel voor de kop, andere mensen zouden dat 16 regels noemen) beter kan worden opgesplitst. Let wel: regels code: witregels en commentaarregels tellen niet mee. Visser geeft zelf het voorbeeld

```
public void Start()
{
    if (inProgress)
    {
        return;
    }
    inProgress = true;
 
    // Update observers if player died:
    if (!IsAnyPlayerAlive())
    {
        foreach (LevelObserver o in observers)
        {
            o.LevelLost();
        }
    }
    
    // Update observers if all pellets eaten:
    if (RemainingPellets() == 0)
    {
        foreach (LevelObserver o in observers)
        {
            o.LevelWon();
        }
    }
}

```

Bovenstaande code is 22 regels code (2 witregels, 2 commentaarregels). Visser raadt aan om methodes te extraheren, zodat je het volgende krijgt:

```
public void Start()
{
    if (inProgress)
    {
        return;
    }
    inProgress = true;
    UpdateObservers();
}

public void UpdateObservers()
{
    UpdateObserversPlayerDied();
    UpdateObserversPelletsEaten();
}

private void UpdateObserversPlayerDied()
{
    if (!IsAnyPlayerAlive())
    {
        foreach (LevelObserver o in observers)
        {
            o.LevelLost();
        }
    }
}

private void UpdateObserversPelletsEaten()
{
    if (RemainingPellets() == 0)
    {
        foreach (LevelObserver o in observers)
        {
            o.LevelWon();
        }
    }
}

```

Moet je nou religieus in de gaten houden of een methode niet boven de 16 regels komt? Dat ook niet. Een gidslijn als codelengte is meer als de brandstofwijzer in je auto dan als een wettelijke maximale snelheid; je hoeft er niet naar te streven dat methoden 15 of 16 regels zijn (methoden van 1 of 2 regels zijn soms ook nuttig), maar als een methode meer dan 20 regels of een scherm begint te worden moet een intern stemmetje in jou gaan zeggen "zou je deze methode niet kunnen opsplitsen"? Misschien een onaangename boodschap, maar prettig dat je het dan al hoort dan tijdens de peer review "WTF: maak deze methode korter", of erger, na een half jaar, dat je zelf de code moet wijzigen en de idioot vervloekt die die methode zo lang en ingewikkeld gemaakt heeft...

(en een methode extraheren is makkelijk met een moderne IDE: in Visual Studio selecteer je gewoon de code waarvan je een aparte methode wilt maken en druk je Ctrl-R-M).

<div style="page-break-after: always;"></div>

## Complexiteit van methoden

Sommige methoden zijn lang, maar niet erg complex:

```
public static void Main() 
{
	Console.WriteLine("Welkom!");
	Console.WriteLine();
	Console.WriteLine("Voordat u dit programma gaat...");
	// ... hier nog 100 regels tekst
    Console.Write("Selecteer 'J' of 'N' om aan te ");
    Console.Write("geven dat u akkoord gaat met de ");
    Console.WriteLine("voorwaarden.");
	var answer = Console.ReadKey();
	if (answer == 'J') RunRest();
}

```

Andere methode zijn niet erg lang, maar _wel_ complex:

```
public static string Part(long n)
{
    var ans = PartRec((int)n, new int[n + 1][]).OrderBy(x =>   
        x).ToArray();
    return $"Range: {ans.Max() - ans.Min()} Average:  
        {ans.Average():.00} Median: {Median(ans):.00}";
    
    IEnumerable<int> PartRec(int arg, int [][] memory) => 
        memory[arg] ?? (memory[arg] = Enumerable.Range(arg, 1)
            .Union(Enumerable.Range(1, arg / 2)
                .SelectMany(i => PartRec(arg - i, memory).Select(x => x * i))
                .Distinct())
            .ToArray());
    
    double Median(IList<int> list) => list.Count % 2 == 0
        ? ((double)list[list.Count / 2 - 1] + list[list.Count / 2]) / 2
        : list[list.Count / 2];
  }

```

Er zijn veel redenen waarom code complex is; als je niet weet wat het doet (wat moet 'Part' doen?), als de namen onduidelij zijn (PartRec?) als er onbekende constructies in voorkomen (??, of Enumerable.Range), of als er veel dingen gebeuren op een regel.

Meestal schrijf je duidelijkere code dan bovenstaande, maar kan een methode toch nog complex worden. Om die complexiteit uit te drukken gebruiken programmeurs wel eens het begrip "cyclomatic complexity", of het McCabe-nummer.

Deze cyclomatische complexiteit wordt normaal berekend door naar een methode te kijken. Een methode krijgt sowieso 1 punt. Voor elke if, voor elke while en for(each) en case (in een switch-statement), voor elke && en || wordt er 1 punt bij opgeteld.

```
bool CanEnterForFree(Person person) 
{
    if (IsVip(person) || HasSpecialInvitation(person))
    {
        return true;
    }
    return false;
}

```

Bovenstaande methode heeft een complexiteit van 3: 1 voor de methode zelf, 1 voor het if-statement, 1 voor de '||'. De methode is niet erg complex.

Hoe complex mag een methode zijn? Volgens Visser (https://www.softwareimprovementgroup.com/wp-content/uploads/Building_Maintainable_Software_C_Sharp_SIG.compressed.pdf) en ook anderen geldt normaal: 1 tot 5 is goed; 5 tot 10: opletten, overweeg te refactoren. 10 of meer: splits de methode op.

Dit heeft overigens ook praktische redenen als je unittests schrijft: bij elk punt extra complexiteit moet je 2x zoveel tests maken (totaal 2^(complexiteit - 1)); bij de CanEnterForFree moet je 2^(3-1) = 4 tests maken (persoon is Vip, persoon heeft uitnodiging, persoon s geen VIP, persoon heeft geen speciale uitnodiging).

Dat zou betekenen dat je voor een methode met een complexiteit van 11 1024 tests zou moeten maken - die methode opsplitsen in 3 methoden met complexiteiten 4, 5 en 5 (complexiteit 11 = 10 toegevoegde complexiteit, over drie methodes wordt dat 1+3, 1+4 en 1+4)zou het aantal tests terugbrengen tot 8 + 16 + 16 = 40, wat veel werk scheelt!

<div style="page-break-after: always;"></div>

### Command-Query Separation

Een handig programmeerprincipe voor het maken van onderhoudbare code is het onderscheiden van commando's van queries: methoden zijn dan òf een commando, dat de 'staat van de wereld verandert' maar geen waarde teruggeeft, òf een query, die de staat van de wereld niet verandert, maar wel een waarde teruggeeft.

Een voorbeeld van een command is `Console.WriteLine("Hallo");`: het geeft geen waarde terug, maar het verandert wel iets aan 'de staat van de wereld' (het schrijft tekst naar de console - de tekst op de console verandert).

Een voorbeeld van een query is `Math.round(3.141592, 2);`: het verandert niets aan de wereld of de waarde van 3.141592, maar geeft alleen een nieuwe waarde terug, namelijk 3.14.

Dat klinkt allemaal logisch, maar wat heb je nou aan die theorie? Wel, veel mensen maken methoden die zowel een waarde teruggeven àls de waarde van de wereld (bijvoorbeeld een input-parameter) veranderen. Maar dat leidt over het algemeen tot code die relatief moeilijk te begrijpen, te debuggen en te onderhouden is.

Vergelijk bijvoorbeeld 
```
bool isNumber = int.TryParse(userInput, out int number);
```

Met een hypothetische methode als de volgende
```
int? number = int.ParseOrNull(userInput);
```

Welke is het gemakkelijkst te begrijpen? Welke is het makkelijkst foutloos te gebruiken? De meeste mensen zouden zeggen dat de tweede dat is. Want bij de eerste kan je bijvoorbeeld vergeten de returnwaarde te controleren:
```
int.TryParse(userInput, out int number);
Console.WriteLine($"Jouw geluksgetal is {number}!");
```

Dat gaat goed totdat iemand "zes" invult, en "Jouw geluksgetal is 0!" wordt uitgeprint.

Het relatief lastig te begrijpen en gebruiken zijn van TryParse ligt, als je het gaat analyseren, niet alleen aan de 'rare' `out`-parameter, maar ook aan het feit dat het zowel een command als een query is: het geeft een waarde terug (een boolean), _en_ het 'verandert de wereld', namelijk de waarde van de variabele 'number'.

De (in C# hypothetische) methode ParseOrNull is echter een 'pure' query, dus veel makkelijker te begrijpen. Voor de oplettende lezer die zich afvraagt waarom C# dan-in tegenstelling tot andere talen-geen ParseOrNull heeft: de ontwerpers van C# hebben ooit besloten om null een legale waarde te maken in berekeningen (mogelijk omdat SQL ook zo werkt), wat betekent dat als number null is er geen compiler-error is als je number * 2 or number < 6 of currentTotal += number doet, de berekening levert alleen altijd null op. Of false bij een vergelijking als `number < 6`. Dat gedrag is, zoals je je mogelijk kan voorstellen, nogal lastig te debuggen, vermoedelijk heeft C# daarom geen ParseOrNull, zelfs al lijkt dat op het eerste gezicht mogelijk een goed idee.

Hoe dan ook, het kan helpen de command-query separation ook in jouw eigen code toe te passen. In de meeste gevallen moet een methode òf een commando zijn (void returnen en iets veranderen, of dat nou een waarde is of een database of de inhoud van een scherm) òf een query zijn (dat alleen een waarde teruggeeft). Andere mensen kunnen jouw code dan ook makkelijker lezen en debuggen. En belangrijker - jij kan ook jouw code dan makkelijker begrijpen, aanpassen en debuggen, zelfs maanden na het schrijven ervan!

Een kritische lezer vraagt zich wellicht af of er ook gevallen zijn waarin de command-query separation opgeofferd moet worden aan een 'hoger doel'. Dan gaat het uiteraard vooral om methodes die zowel een command als een query zijn (als een methode noch een command noch een query is doet hij niets en kun je hem gewoon deleten). [Martin Fowler geeft het voorbeeld van de Stack.Pop()-methode](https://martinfowler.com/bliki/CommandQuerySeparation.html): die geeft een waarde terug, maar verkleint ook de stack. Desalniettemin is het een redelijk makkelijk te gebruiken methode. Mijn eigen interpretatie is dat je hier eigenlijk werkt met een query, die weliswaar de wereld niet 'constant' houdt, maar wel 'invariant', omdat je het kunt beschouwen als het volgende:

```
Stack<int> unprocessedNumbers = new ();
unprocessedNumbers.Push(10);
unprocessedNumbers.Push(7);
unprocessedNumbers.Push(5);

int first = unprocessedNumbers.Pop();
Console.WriteLine(first); // print 5, unprocessed numbers = 7, 10
int second = unprocessedNumbers.Pop();
Console.WriteLine(second); // print 7, unprocessed numbers = 10
```

Dus terwijl _formeel_ Pop() een command èn query is omdat hij een waarde teruggeeft en de 'staat van de wereld' verandert, zou je het kunnen beschouwen als een pure query die de staat van de wereld conceptueel niet verandert. De 'unprocessedNumbers' is namelijk een invariant, want de abstracte inhoud ervan (de ongelezen nummers) blijft hetzelfde. 

In de praktijk zul je -vaak door oude tradities in andere programmeertalen- wel vaker 'querycommands' zien, zoals een T Create(T t) methode die een object savet in een database en het object ook teruggeeft. Soms is dat inderdaad praktisch (als je bijvoorbeeld het Id wilt hebben dat de database teruggeeft) en het is niet iets dat je als programmeur met wortel en tak moet willen uitroeien - veel mensen hechten aan tradities, zelfs als die nutteloos of zelfs licht schadelijk zijn. Maar de code die je zelf schrijft-tenzij een architect of docent het expliciet specificeert- kun je 'querycommands' normaal beter zoveel mogelijk vermijden: maak dingen òf een query, òf een command. Je collega's (en jij over een half jaar als je dingen moet debuggen) zullen je dankbaar zijn!

<div style="page-break-after: always;"></div>

## Vermijd 'magische constanten'

Als je een constante ziet in je programma, bijvoorbeeld een boodschap die meerdere malen letterlijk terugkomt (zoals "index mag niet kleiner zijn dan 1"), of een getal dat ongelijk is aan 0, 1 of 2, dan is het goed te kijken wat er aan de hand is. 

In sommige gevallen heb je ergens een waarde 'hardcoded' die in feite een reflectie is van een andere variabele ergens. Bijvoorbeeld:

```
if (outsideAllowedRange) Console.WriteLine("Het getal moet tussen 1 en 5 liggen");
```

Waarbij een betere code zou zijn:

```
if (outsideAllowedRange) Console.WriteLine($"Het getal moet tussen 1 en {_phones.Count} liggen.")
```

Immers, de 'constante' is feitelijk afhankelijk van een andere constante of variabele, en als die ooit verandert heb je een bug.

Het tweede scenario is als je 'externe' kennis gebruikt. Bijvoorbeeld:

```
if (numElements >= 10)
{
...
}
```

In dit geval bleek dat de website die door deze code gescraped werd zowel telefoons als simkaarten kon teruggeven; de simkaarten moesten worden uitgefilterd, wat makkelijk was, omdat die minder dan 10 elementen hadden. Betere code zou hier geweest zijn:

```
int minimumNumberOfPhoneElements = 10;

if (numElements >= minimumNumberOfPhoneElements)
{
...
}
```

of zelfs

```
if (IsPhone(rawPhoneData))
{
...
}

...

bool IsPhone(List<string> objectData)
{
    // this website also contains SIM cards, which however have only 9 
    // or 8 elements.
    int minimumNumberOfPhoneElements = 10;
    return (objectData.Count > minimumNumberOfPhoneElements);
}
```

Hoe dan ook, als je een string ziet die telkens weer terugkomt, of een nummer ongelijk aan 0, 1 of 2, kijk wat er aan de hand is en voer de juiste actie uit om deze 'magische constante' te elimineren!

<div style="page-break-after: always;"></div>

## Parameters verminderen

Meestal is het fijn als methoden weinig parameters hebben. Visser (https://www.softwareimprovementgroup.com/wp-content/uploads/Building_Maintainable_Software_C_Sharp_SIG.compressed.pdf) houdt het op maximaal vier parameters per methode; maar dat lijkt ook wel de bovengrens; ikzelf vind Fowler's advies in Refactoring beter:
(http://principles-wiki.net/anti-patterns:long_parameter_list)

- _Methods with 0 and 1 arguments are fine_
- _2 parameters still good_
- _3 parameters can be considered OK_
- _4 and more parameters are usually too much_

Meestal is het beter als je minder parameters kan gebruiken. Maar hoe? 

Er zijn verschillende oplossingen:

1) kan je een parameter lokaal maken, dat je hem een waarde geeft in de methode die het daadwerkelijk gebruikt? Is handig als je een waarde slechts op 1 plaats gebruikt, voorkomt ook dat je hoofdfunctie gaat micromanagen en alle details bevat.

2) kan je van de parameter een veld maken?

3) kan je twee of meer parameters samenvoegen in een apart object?

4) kan je de methode in een apart object zetten? (waarbij de parameters velden kunnen worden)

5) herberekenen in plaats van doorgeven (parameter omzetten in methode)

Hieronder voorbeelden van elke methode.


1: Parameter lokaal maken

```
// vóór refactoring
public static void Main()
{
    char currencySign = '€';
    DisplayMenu(currencySign);
}

public static void DisplayMenu(char currencySign)
{
    var option = AskForOption();
    DisplayPhone(option, currencySign);
}

public static void DisplayPhone(int option, char currencySign)
{
    var phone = _phones[option];
    Console.WriteLine($"{phone.Name}: {currencySign}{phone.Price});
}


// na refactoring 

public static void Main()
{
    DisplayMenu();
}

public static void DisplayMenu()
{
    var option = AskForOption();
    DisplayPhone(option);
}

public static void DisplayPhone(int option)
{
    char currencySign = '€';
    var phone = _phones[option];
    Console.WriteLine($"{phone.Name}: {currencySign}{phone.Price});
}
```

2: Van de parameter een veld maken

```
// vóór refactoring
public static void Main()
{
    char currencySign = '€';
    DisplayMenu(currencySign);
}

public static void DisplayMenu(char currencySign)
{
    var option = AskForOption();
    DisplayPhone(option, currencySign);
}

public static void DisplayPhone(int option, char currencySign)
{
    var phone = _phones[option];
    Console.WriteLine($"{phone.Name}: {currencySign}{phone.Price});
}


// na refactoring 

private readonly char currencySign = '€';

public static void Main()
{
    DisplayMenu();
}

public static void DisplayMenu()
{
    var option = AskForOption();
    DisplayPhone(option);
}

public static void DisplayPhone(int option)
{
    
    var phone = _phones[option];
    Console.WriteLine($"{phone.Name}: {currencySign}{phone.Price});
}
```


3: Samenvoegen van de parameters in een apart object

```
// vóór refactoring

void ShowPrice(char currency, decimal price) 
{
    Console.WriteLine($"{currency}{price});
}

// na refactoring
class Price 
{
    public char Currency { get; set; }
    
    public decimal Amount { get; set; }
}

...

void ShowPrice(Price price) 
{
    Console.WriteLine($"{price.Currency}{price.Amount});
}
```

4: de methode in een apart object zetten

```
// vóór refactoring
static List<string> Parse(string input)
{
    var lines = new List<string>();
    var currentLine = new StringBuilder();
    for (var i = 0; i < input.Length; i++)
    {
        ProcessChar(input[i], currentLine, lines);
    }
    var trimmedLine = currentLine.ToString().Trim();
    if (trimmedLine.Length > 0) lines.Add(trimmedLine);
    return lines;
}

static void ProcessChar(char ch, StringBuilder currentLine, List<string> lines)
{
    if (ch == ';')
    {
        var trimmedLine = currentLine.ToString().Trim();
        if (trimmedLine.Length > 0) lines.Add(trimmedLine);
        currentLine.Clear();
    }
    else
    {
        currentLine.Append(ch);
    }
}


// na refactoring

class CodeParser
{
    readonly List<string> lines = new();
    readonly StringBuilder currentLine = new();

    public List<string> Parse(string input)
    {
        for (var i = 0; i < input.Length; i++)
        {
            ProcessChar(input[i]);
        }
        var trimmedLine = currentLine.ToString().Trim();
        if (trimmedLine.Length > 0) lines.Add(trimmedLine);
        return lines;
    }

    private void ProcessChar(char ch)
    {
        if (ch == ';')
        {
            var trimmedLine = currentLine.ToString().Trim();
            if (trimmedLine.Length > 0) lines.Add(trimmedLine);
            currentLine.Clear();
        }
        else
        {
            currentLine.Append(ch);
        }
    }
}
```

5: herberekenen in plaats van doorgeven

```
// vóór refactoring
void ShowPrices(decimal priceExclVat, decimal vatPercentage, decimal priceInclVat) 
{
	Console.WriteLine($"The price is {priceInclVat},({priceExclVat} including {vatPercentage}% VAT");
}


// na refactoring
void ShowPrices(decimal priceExclVat, decimal vatPercentage) 
{
	var priceInclVat = priceExclVat * (1 + vatPercentage / 100);
	Console.WriteLine($"The price is {priceInclVat},({priceExclVat} including {vatPercentage}% VAT)");
}
```

<div style="page-break-after: always;"></div>

## DRY: Don't Repeat Yourself

Een belangrijk principe in softwareontwerp is "DRY" ("Don't Repeat Yourself"). Elk stuk kennis moet maar op één plaats staan. Nu is er veel over DRY te zeggen, zie bijvoorbeeld "The Pragmatic Programmer" (Andrew Hunt, David Thomas). Zowel het originele boek als de "Anniversary Edition" bespreken DRY uitgebreid, inclusief de subtiliteiten zoals de problemen van een klasse als

```
class Line
{
	public Point Start { get; set; }
	public Point End { get; set; }
	public double Length { get; set; }
}
```

waarbij je problemen krijgt omdat een andere programmeur Start of End kan veranderen, en dan Length niet automatisch meeverandert; Length is een duplicatie van data. Dat kan tot akelige bugs leiden (betere code zou 'public double Length => Start.DistanceTo(End);' gebruiken).

Hunt en Thomas noemen ook andere problemen, zoals een systeem voor een vrachtvervoersbedrijf waarbij een levering een truck, een chauffeur en een route heeft, maar waarbij een truck een type, een nummerbord en een chauffeur heeft. Wat als de chauffeur ziek is? Dan moeten twee dingen veranderen - een signaal dat het datamodel beter kan.

Maar op een basaal niveau heb je meestal met DRY te maken in herhaalde code. Als je ziet dat je telkens dezelfde of ongeveer dezelfde code schrijft, dan kan dat een overtreding van DRY betekenen.

Voorbeeld:
```
if (chosenNumber == 1)
{
    Console.WriteLine($"{phones[0].Brand} {phones[0].Name}\n");
    Console.WriteLine($"{phones[0].Description});
    Console.WriteLine("Press any key to continue...");
    Console.ReadLine();
}
else if (chosenNumber == 2)
{
    Console.WriteLine($"{phones[1].Brand} {phones[0].Name}\n");
    Console.WriteLine($"{phones[1].Description});
    Console.WriteLine("Press any key to continue...");
    Console.ReadLine();
} // vervolgd voor nummers 3, 4 en 5
```

Nu zeg je misschien: waarom is dit erg? De code is toch duidelijk?

In de praktijk zorgt dergelijke herhaalde code voor verschillende problemen.

Allereerst krijg je veel meer code dan noodzakelijk is, dus meer code waar bugs in zitten, meer code om te lezen, te testen en doorheen te scrollen (en het compileert ook trager!).

Ten tweede zorgt het copy-pasten en "lokaal aanpassen" van code vaak voor kleine typefouten (en dus debugsessies). Zie je de kopieerfout in bovenstaande code?

Ten derde betekent gedupliceerde code dat aanpassen moeilijker gaat. Dat niet alleen als je een extra telefoon toevoegt (waarvoor je dus 7 extra regels code moet toevoegen.) Maar wat als je de prijs wilt weergeven? Dan moet je in bovenstaand geval op 5 verschillende regels de code veranderen.

En het vierde argument is dat gedupliceerde code erg naar is om te onderhouden: mensen kopiëren code, soms naar andere bestanden, en als dan 1 brok code wordt aangepast omdat ergens de logica verandert of een bug wordt opgelost (een waarde wordt bijvoorbeeld in een ander veld opgeslagen) dan wordt die verandering niet in de hele codebase doorgevoerd, en de bug niet overal opgelost. Zoals ik mogelijk een keer heb vermeld heb ik ooit een akelige bug geïntroduceerd in de code van een klant omdat ik slechts 7 kopieën van gedupliceerde code had gevonden en aangepast, en niet alle 11!

Je vraagt je misschien af of er subtiliteiten zijn: hoeveel regels code mag je dupliceren, en hoe vaak mag je het doen voordat het een probleem is? Visser houdt het op vier regels, maar dat vooral omdat zijn programma voor codeduplicatiedetectie teveel vals-positieven geeft als je minder dan vier regels zou vereisen!

Sommige programmeurs zeggen dat als je 2x hetzelfde schrijft, het nog okee is, en dat je bij 3x moet gaan refactoren. Mogelijk is dat nog okee, hoewel ik momenteel de indruk heb dat hoe ervarener een programmeur is, des te eerder ze geneigd zijn bij 2x hetzelfde schrijven al te gaan refactoren (door bittere, bittere ervaring). En zeker mag je nooit bestaande code kopiëren, zelfs al doe je het maar 1x! Want wie weet hoe vaak anderen die code al gekopieërd hebben...

En zelfs een enkele regel moet je al de-dupliceren (in een aparte methode zetten) _als_ je weet dat als je de regel op deze plaats zou moeten veranderen, dezelfde regel op andere plaatsen ook moet veranderen.

Let wel: er is dus alleen echte duplicatie als een verandering op 1 plaats ook tot veranderingen op andere plaatsen zou moeten leiden. Neem bijvoorbeeld het volgende geval:

```
Console.WriteLine($"{phone.Brand} {phone.Type}");


//...

Console.WriteLine($"{priceInclVat} {priceExclVat}");
```

Zou je hier een methode moeten maken die twee argumenten neemt en die met een spatie tussenruimte uitprint op de console? Nee, want ze hebben (voorzover we weten) niets met elkaar te maken; als het afbeelden van de naam van de telefoon verandert, hoeft de afbeelding van de prijs niet noodzakelijkerwijs ook te veranderen, en andersom. Dit is geen gedupliceerde code, maar onafhankelijke code die alleen sterk op elkaar lijkt.

Hoe dan ook, als je het gevoel hebt dat je bepaalde code al ergens eerder geschreven of gezien hebt, maak een aparte methode die op beide plaatsen kan worden gebruikt. En dat is makkelijk omdat Visual Studio geselecteerde code kan omzetten in een aparte methode met Ctrl R M. Toegepast op de oorspronkelijke code zou dat leiden tot

```
if (chosenNumber == 1)
{
    DisplayPhone(0);
}
else if (chosenNumber == 2)
{
    DisplayPhone(1);
} // vervolgd voor nummers 3, 4 en 5


// ...

void DisplayPhone(int index)
{
    Console.WriteLine($"{phones[index].Brand} {phones[index].Name}\n");
    Console.WriteLine($"{phones[index].Description});
    Console.WriteLine("Press any key to continue...");
    Console.ReadLine();
}
```

Nu zie je als het goed is nog steeds een partroon (if chosenNumber == n) { DisplayPhone(n-1); }, wat kan leiden tot 

```
if (chosenNumber >= 1 && chosenNumber <= phones.Count)
{
    DisplayPhone(chosenNumber - 1);
}

// ...

void DisplayPhone(int index)
{
    Console.WriteLine($"{phones[index].Brand} {phones[index].Name}\n");
    Console.WriteLine($"{phones[index].Description});
    Console.WriteLine("Press any key to continue...");
    Console.ReadLine();
}
```

Kortom: minder code, en makkelijk onderhoudbare code. Dus als je bij het schrijven of lezen van code het gevoel krijgt van 'heb ik dit al niet eens eerder geschreven of gezien?', kijk of DRY van toepassing is en probeer de code uit te factoren naar een aparte methode!


### Meer lezen?
- The Pragmatic Programmer, Andrew Hunt & David Thomas
- https://martinfowler.com/bliki/BeckDesignRules.htm
- https://en.wikipedia.org/wiki/Don%27t_repeat_yourself

<div style="page-break-after: always;"></div>

## Code isn't an asset, it's a liability

Sommige mensen denken dat developers worden gehuurd om code te schrijven. En dat is deels waar. Maar waar we meestal eigenlijk voor worden ingehuurd is het oplossen van de problemen van de klant/cliënt. Code schrijven is slechts een hulpmiddel om dat doel te bereiken. Onze taak is dus NIET persé het schrijven van (veel) code.

Over het algemeen is het zelfs aan te raden een probleem op te lossen met zo weinig mogelijk code. Dat omdat code geen asset (kapitaal) is, maar een liability (zwakte/schuld). 

Allereerst: elke regel code moet worden geschreven. Elke regel code moet worden gedebugd (want in elke regel code kan een bug zitten). Vaak moeten regels code ook worden getest, en/of gedocumenteerd. Elke regel code kost tijd om gecompileerd te worden - elke keer weer; tijd bij het runnen, en kost tijd aan degene die de code moet reviewen. Het kost zeker tijd aan jou of je collega's die die regel code moeten doorlezen op zoek naar een gerapporteerde bug, of moet overdenken voor het implementeren van een nieuwe feature.

Immers, waar denk je dat je eerder een bug in vindt: in een programma van tien regels of in een programma van 100,000 regels?

Nu zijn de grootste kosten van code in de praktijk niet zozeer de compileer- of runkosten, maar de leeskosten- hoe snel is code te begrijpen. Nog steeds zijn minder regels beter dan meer regels, bijvoorbeeld

```
var phone = phones.Find(p => p.Id == chosenId);
```

is vaak sneller te begrijpen dan 

```
Phone phone = null;
for (var i = 0; i < phones.Count; i++)
{
   if (phones[i].Id == chosenId)
   {
       phone = phones[i];
       break;
   }
}
``` 

Omdat de leestijd/begripskosten het belangrijkst zijn is het vaak zelfs beter iets _meer_ regels te hebben (doordat je kleinere methoden gebruikt, of witregels tussen secties in een methode) dan dat je het aantal regels probeert te minimaliseren. Mocht je dat niet geloven: hier is Conway's 'game of life' geprogrammeerd in de programmeertaal APL (https://en.wikipedia.org/wiki/APL_(programming_language)).

```
life ← {⊃1 ⍵ ∨.∧ 3 4 = +/ +⌿ ¯1 0 1 ∘.⊖ ¯1 0 1 ⌽¨ ⊂⍵}
```


Hoe compact dit ook is, ik denk dat de meeste mensen dit niet voor hun beroep zouden willen onderhouden of debuggen.

Overigens: het feit dat de goedkoopste regels code de regels code zijn die je _niet_ hoeft te schrijven is ook een zeer belangrijke reden voor de opkomst van open source software bibliotheken; open source geeft je de lage kosten van bijna nooit andermans code hoeven lezen, terwijl je in noodgevallen nog steeds de bug kan squashen of de feature kan implementeren die je zo hard nodig hebt.

<div style="page-break-after: always;"></div>

## YAGNI - You Ain't Gonna Need It (Yet)

Als je bezig bent met een programma, zie je vaak hoe een bepaalde klasse of methode breder kan worden toegepast. Bijvoorbeeld: je munteenheid is nu euro, maar misschien gaat het bedrijf in de toekomst internationaal uitbreiden en zullen prijzen dan ook in dollars of yen moeten. Is het dan niet beter aan de Phone-klasse (of aan de PhoneService of aan de Phone console-app) ook een extra parameter toe te voegen voor de currency?

En wat als de BTW verandert van 21% naar bijvoorbeeld 22%? Is het niet handig ergens een file te hebben met constanten zoals die van de BTW, of het percentage in een aparte tabel in de database te zetten zodat je altijd de actuele BTW hebt?

Misschien dat bovenstaande voorbeelden vergezocht lijken; maar ik kan je garanderen dat je als programmeur (heb ik zelf ook) vaak het idee krijgt: "maar wat als dit gaat gebeuren?"

Het probleem is dat je als programmeur slechts zelden gelijk hebt over wat de toekomst brengt of een klant wil of nodig heeft. En in de tussentijd loop je het risico dat je voor die extra flexibiliteit extra code nodig hebt; en code is dus geen asset, maar een liability (zie hierboven). Hoe meer code je hebt, hoe logger de applicatie wordt, en hoe minder makkelijk hij te veranderen is.

Een ander probleem zijn de extra nodige 'begripskosten'. Mensen die je code bekijken - je peer reviewers, je collega's die iets moeten veranderen, en mogelijk jijzelf na twee maanden - moeten immers extra tijd besteden omdat ze zich afvragen "waarom wordt dit nou gedaan? Dit is toch helemaal niet nodig?" Wat jou weer tijd kan kosten (via git blame) om het uit te leggen, of een commentaar te schrijven (dat dus ook weer gelezen moet worden). Ook in dit opzicht maken de 'mogelijk in de toekomst nodige features' het leven van jou en je team moeilijker.

Vandaar dat programmeurs het vaak hebben over YAGNI: You Ain't Gonna Need It (Yet). Mocht je daar meer over willen weten - of mogelijk nog wat sappiger voorbeelden willen weten dan ik hierboven heb gegeven, zou ik aanraden te kijken op de site van Martin Fowler (sowieso een handige praktijk-goeroe om te kennen https://martinfowler.com/bliki/Yagni.html) of van Vladimir Khorikov, die, met zijn karakteristieke diepgang, ook aangeeft in welke uitzonderingssituatie YAGNI géén goed idee is (https://enterprisecraftsmanship.com/posts/yagni-revisited/) - in het geval dat je een API voor een bibliotheek aan het verkopen bent aan andere bedrijven - voor je iets publiceert, zorg ervoor dat je goed hebt nagedacht over het mogelijk gebruik, want gebruikers zijn doorgaans niet blij als hun code niet meer werkt omdat je later iets hebt veranderd in de interface.

Hoe dan ook, voor normale softwareontwikkeling is YAGNI nog steeds een van de beste leidprincipes om te volgen.

<div style="page-break-after: always;"></div>

## Namespaces - advies: probeer file-scoped te gebruiken

Optioneel maar wel handig: in .NET 6/C#10 kun je file-scoped namespaces te gebruiken. Dat wil zeggen, in plaats van 
```
namespace PhoneShop 
{
    class PhoneApp 
    {
        public static void Main() 
        {
            if // ...
        }
    }
}
```

krijg je dan iets als 

```
namespace PhoneShop; 

class PhoneApp 
{
    public static void Main() 
    {
        if // ...
    }
}
```

Nu weet ik niet of alle C#-programmeurs in bedrijven dit al gebruiken, maar ik kan het zeker aanbevelen: het scheelt een beetje typewerk en maakt het makkelijker om te zien of je ergens een accolade teveel of te weinig hebt gebruikt. (Minder accolades vind ik overigens ook fijn, al kan het zijn dat jij het bovenste voorbeeld prettiger lezen vindt dan het onderste).

Bovendien lijkt het erop dat file-scoped namespaces de toekomst hebben -hoewel C++ nog namespaces had met extra indentatie werd daar al flink over geklaagd (Google had zelfs standaarden dat de indentatie maar 1 karakter moest worden, wat gretig werd overgenomen door mijn C++-programmerende collega). Talen als Java en ongeveer alles wat na Java kwam (_behalve_ C#, mogelijk omdat C# niet teveel op Java wilde lijken) hadden hebben gewoon een declaratie aan het begin van een file inplaats van een extra niveau van identatie. C# begint in dit opzicht eindelijk bij te trekken van een tamelijk onpraktische beslissing van 20 jaar terug. Dus hoewel ik niet weet of nullable-enabled een grote hit zal worden in de .NET community, verwacht ik dat file-scoped namespaces wel door het merendeel van de programmeurs gebruikt zullen worden nu het kan!

## Pas op voor recursie!

Recursie is dat een methode zichzelf aanroept, oftewel direct

```
void CallMe()
{
    CallMe();
}

```

ofwel indirect

```
void WatchMovie() 
{
    EatPopcorn();
}

void EatPopcorn() 
{
    WatchMovie();
}

```

Nu is recursie zeer veelzijdig, het is het belangrijkste code-herhaalmechanisme in een programmeertaal als Haskell, en het is heel erg handig voor de opgaven op sites als CodeWars.

Maar als professioneel programmeur pas je het heel zeldzaam toe; ik denk dat ik voor mijn *werk* in 20 jaar ongeveer 3x heb moeten gebruiken, eens bij een hierarchische structuur van accounts, en een andere keer om een molecuulnotatie te ontrafelen. Meestal vermijd je juist recursie.

Waarom?

1) omdat herhaling (for en while loops) voor de meeste mensen makkelijker te begrijpen zijn dan recursie. En begrijpelijkheid is belangrijk voor code!

2) omdat door de zeldzaamheid ervan de meeste programmeurs recursie lastig te begrijpen vinden.

3) omdat je bij recursie makkelijk het geheugen van de computer kunt overbelasten, omdat bij elke recursieve aanroep een extra kopie van parameters en returnwaarden op het geheugengebiedje genaamd de 'stack' wordt geplaatst. Dat leidt gemakkelijk tot een crashende computer en een foutmelding 'Stack overflow' 

(probeer maar eens)

```
static int Factorial(int x) 
{
  if (x == 0) return 1;
  return x * Factorial(x-1);
}

public static void Main() 
{
    Console.WriteLine(Factorial(-1));
}
```


Wel, we hebben er in elk geval de naam van de beroemde programmeursvragensite "Stack Overflow" aan te danken...

Hoe dan ook, tenzij je een interessant wiskundig probleem aan het oplossen bent (bijvoorbeeld https://www.codewars.com/kata/55cf3b567fc0e02b0b00000b), probeer een for, do of while loop te gebruiken in plaats van recursie. Je medeprogrammeurs zullen je dankbaar zijn voor de extra leesbaarheid, en de gebruikers zullen dankbaar zijn omdat hun applicatie niet onverwachts crasht. 

## Maak variabelen zoveel mogelijk constant of invariant

Het is voor iedere programmeur moeilijk computerprogramma's te maken die geen bugs bevatten. Sowieso heb je de denkfouten en het dingen over het hoofd zien waar je weinig aan kunt doen zonder peer review en testen. Aan het andere uiteinde heb je dingen die goede codeanalyse bij een geschike programmeertaal automatisch kan ontdekken, zoals Visual Studio je erop wijst als je bijvoorbeeld een getal aftrekt van een string - wat JavaScript bijvoorbeeld niet doet. Probeer maar eens in een JavaScript-terminal: "15" - 1 + 1 in te typen (dat levert 15 op), en dan een regel met "15" + 1 - 1, wat resulteert in... wel, dat moet je zelf zien...

Maar tussen imperfecties in het denken en hulp van de taal en/of tools zit een grijs gebied waarop onze programmeerkeuzes de fouten die we maken kunnen verminderen. En een aantal van de technieken die je als programmeur daarvoor gebruikt komen uit het zogenaamde "functioneel programmeren". Nu heeft het functioneel programmeren meerdere sterke (en zwakke) punten, maar één techniek die voor alle programmeurs nuttig is, is het zo weinig mogelijk gebruiken van 'veranderlijke' variabelen: een variabele moet liefst altijd hetzelfde begrip aangeven. Dat is meestal oftewel een constante (string name = "Joe Biden";) of een invariant, iets dat altijd waar blijft al verandert de waarde (int today = "Tuesday"; // morgen is het "Wednesday"). 

Hoe implementeer je die? Constanten zijn doorgaans makkelijk (int quitIndex = 6;), invarianten gebruik je meestal in loops (currentIndex), en buiten loops zet je ze vaak in een methode of property (string nameOfToday = Today().Name;, waarbij Today een methode is die altijd een dag-object met de op dat moment correcte naam teruggeeft)

Maar hoe lossen constanten en invarianten je bugs op? Beschouw twee codevoorbeelden die ik afgelopen week tegenkwam. Let wel: dit zijn geen letterlijke kopieën, mijn geheugen is ook weer niet zo goed. Maar ze illustreren wel het probleem van 'variabele variabelen'. 

Voorbeeld 1a:
```
phoneIndex++;
Console.WriteLine($"{phoneIndex}. Quit");

// boel code 

if (input <= phoneIndex) ShowPhone(input)
```

Voorbeeld 2a:
```
// need to calculate "ultimate digit" in numerological way
// 0 to 9 stay the same, 11 becomes 1+1=2, 999 becomes 27 becomes 9

int NumerologicalNumber(int n)
{
    int y = n;
    string a = y.ToString();
    while (a.Length > 1) 
    {
       for (int i = 0; i < a.Length; i++) 
       {
           y += a[i] - '0';
       }
       a = y.ToString();
    }
	return y;
}
```

In beide stukken code is er een bug (die kennelijk niet makkelijk te ontdekken was). Maar er zijn wel variabele variabelen! Laten we die eens aanpakken.

Voorbeeld 1b:
```
int quitIndex = phoneIndex + 1;
Console.WriteLine($"{quitIndex}. Quit");

// boel code 

if (input <= phoneIndex) ShowPhone(input)
```

Voorbeeld 2b:
```
// need to calculate "ultimate digit" in numerological way
// 0 to 9 stay the same, 11 becomes 1+1=2, 999 becomes 27 becomes 9

int NumerologicalNumber(int n)
{
    int y = n;
    string a = y.ToString();
    while (a.Length > 1) 
    {
       y = a.Sum(c => c - '0');
       a = y.ToString();
    }
	return y;
}
```

Je ziet dat de bugs op 'mysterieuze wijze' verdwenen zijn!

Uiteraard worden niet _alle_ bugs voorkomen door variabelen altijd constant of invariant te maken, maar het is een redelijk makkelijke techniek die je veel hoofdpijn kan besparen!

Als je merkt dat je het moeilijk vindt om code te schrijven waarbij variabelen constant of invariant zijn: dat kan ik me voorstellen, talen als Java en C# hebben daar geen ontzettend goede support voor (al kan je wel dingen doen met const en ReadOnlyList etc., maar dan zullen veel mensen je C#-code raar vinden). Oefenen met invarianten/constanten is handiger met een functionele of recenter ontworpen taal, die hebben betere support voor 'onveranderlijkheid', zeker als ze een goede editor hebben die je erop wijst dat je iets constant kunt maken. Mijn persoonlijke aanbeveling als je wilt oefenen met constante en invariante variabelen zou de programmeertaal Kotlin zijn, de taal lijkt nog enigszins op C# (in elk geval meer dan Haskell dat doet) en er is een heel goede editor voor (IDEA, van JetBrains, die ook de C#-analysetool Resharper maakt). Voor meer informatie, zie https://www.w3schools.com/kotlin/index.php. Rust, Swift, Haskell, Elm of F# zouden ook kunnen, maar die hebben vaak slechtere editors en/of lijken minder op C#.

## "Nullers" versus "throwers" versus "listers"

Bij bepaalde methodes-in het bijzonder functies die informatie moeten teruggeven, kan de methode falen. Een veelvoorkomend scenario is dat iemand informatie zoekt, die niet gevonden kan worden. Hoe ga je daarmee om?

De eerste vraag is wat voor soort output de methode geacht wordt te geven. Als je een collectie/lijst teruggeeft, zoals zoekresultaten van Google of Bol.com of Amazon, is het het beste een lege lijst terug te geven als niets gevonden is. Ja, sommige programmeurs geven in dat geval null terug, maar dat is niet echt 'best practice'. Zie bijvoorbeeld "Effective Java, Third Edition", Item 54 "Return empty collections or arrays, not nulls". 

De reden om een lege collectie terug te geven in plaats van `null` zijn tweevoudig. Technisch zorgt een null teruggeven dat code die de methode aanroept altijd voor null moet checken (wat je als programmeur weleens vergeet). En vanuit een domain-driven-design/logisch perspectief is het ook niet juist: je kunt misschien een lijst van 10,427 personen vinden die Bas heten, de lijst van mensen die Bas heten en een poedel hebben is misschien 30, maar mensen die Bas heten, een poedel hebben, en die ook nog professioneel pianist zijn (Bas of de poedel) is waarschijnlijk nul. En 0 is net als 1, 30 of 10,427 een getal, wat je nooit zou weergeven met een apart niet-getal-type als `null`.

De "listers" zijn dus gemakkelijk. Maar wat als je een enkele waarde wilt teruggeven? Een lijst die alleen 1 waarde of 0 waarden kan hebben is raar: als een methode een lijst teruggeeft suggereert dat dat een aanroep naar die methode 0 tot (bijna) oneindig veel waarden kan returnen, dat is immers het 'lijst-contract'. Dan moet je dus iets anders verzinnen.

Welke oplossing je kiest hangt doorgaans af van de omstandigheden. Mocht je als programma van tevoren weten welke keuzes juist zijn, en de gebruiker kunnen beperken die keuzes op te geven, dan is het het beste een exceptie te gooien als het item niet gevonden wordt-dan is het een programmeerfout, die je het beste zo snel mogelijk kunt ontdekken, liefst voordat je software in produktie gaat. Gelukkig hoef je als programmeur dan lang niet altijd zelf een exceptie te gooien, vaak gebeurt dat al automatisch als je bepaalde LINQ-commando's aanroept, zoals Single() of First().

In het geval dat je niet de hele database in het geheugen kunt laden en/of de gebruiker heel rare waarden kan doorgeven (bijvoorbeeld bij een webapplicatie) is het meestal beter een null terug te geven en dat in de rest van het programma af te handelen. Dat is meestal wat meer werk, maar dan reserveer je de exceptie voor een echt uitzonderlijk geval; dingen die normaal kunnen gebeuren handel je normaal _niet_ met excepties af, want bijvoorbeeld een gebruiker die een typefout maakt is niet echt uitzonderlijk.

Een paar voorbeelden:
```
enum Race { Invalid, Dwarf, Elf, Hobbit, Human, Orc, Wizard };

class Character
{
    public string Name { get; set; }
    
    public Race Race { get; set; }
}

List<Character> fellowShip = new() { 
   new() { Name = "Frodo", Race = Race.Hobbit },
   new() { Name = "Gimli", Race = Race.Dwarf },
   new() { Name = "Legolas", Race = Race.Elf },
   new() { Name = "Aragorn", Race = Race.Human },
   new() { Name = "Gandalf", Race = Race.Wizard },
   new() { Name = "Samwise", Race = Race.Hobbit },
   new() { Name = "Merry", Race = Race.Hobbit },
   new() { Name = "Pippin", Race = Race.Hobbit },
   new() { Name = "Boromir", Race = Race.Human },
};

// 'lister'
IEnumerable<string> AllFromRace(Race race) =>
    fellowShip.Where( c => c.Race == race ).Select(c => c.Name);
    
Console.WriteLine(AllFromRace(Race.Dwarf)); // 1 karakter, Gimli
Console.WriteLine(AllFromRace(Race.Human)); // 2 karakters, Aragorn en Boromir
Console.WriteLine(AllFromRace(Race.Orc)); // 0 karakters, lege lijst 

// 'nuller': bijvoorbeeld voor algemeen zoeken: is er een...
string? FirstMemberWhoseNameStartsWith(char ch) =>
    fellowShip.FirstOrDefault(c => c.Name[0] == char.ToUpper(ch))?.Name;
    
Console.WriteLine(FirstMemberWhoseNameStartsWith('A')); // Aragorn
Console.WriteLine(FirstMemberWhoseNameStartsWith('G')); // Gimli (Gandalf komt later)
Console.WriteLine(FirstMemberWhoseNameStartsWith('T')); // null. Nobody's name starts with T...

// 'thrower'; bijvoorbeeld bij een winforms-app waar alleen de 'A', 'B'
// 'F', 'G', 'L', 'M', P' en 'S' worden getoond. Dan is een andere letter
// een bug! (ik heb de signature ook aangepast naar string om 
// dat te communiceren)
string GetFirstMemberWhoseNameStartsWith(char ch) =>
    fellowShip.First(c => c.Name[0] == ch).Name;
    
Console.WriteLine(GetFirstMemberWhoseNameStartsWith('A')); // Aragorn
Console.WriteLine(GetFirstMemberWhoseNameStartsWith('G')); // Gimli (Gandalf komt later)
Console.WriteLine(GetFirstMemberWhoseNameStartsWith('T')); // throws   
    // InvalidOperationException. Nobody's name starts with T...
``` 

## Elke klasse in een aparte file

De regels die C#-programmeurs hebben verschillen per bedrijf; maar bij mijn beste weten gebruiken de meeste groepen de regel dat elke klasse in een aparte file moet staan.

Dit is inderdaad vaak onpraktisch voor kleine projecten. Bij een groot project is het vaak wel handig (anders moet je beslissen of "Phone" bij de "PhoneService" of bij "Program" moet staan). En feitelijk valt de "prijs" wel mee met moderne IDEs, vaak kan je sneller naar de gezochte code met een sneltoets dan door te scrollen...

```
vuistregel: bij professionele C#-projecten gebruik je normaal 1 file per klasse.
```
 
Dit kan bij kleine projecten enigszins onpraktisch zijn , maar het doel is dat jullie kunnen gaan werken aan projecten van tienduizenden regels, waarvoor het wèl nuttig is.