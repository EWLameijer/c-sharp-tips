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

Oorspronkelijk werd dit in C# gedaan met de 'Parse-methoden': int myInt  = int.Parse(myString); double myDouble = double.Parse(myString); ... enzovoort.

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

<div style="page-break-after: always;"></div>

# C#-architectuur

<div style="page-break-after: always;"></div>

## Wanneer interfaces?

Bij veel C# enterprise-projecten is de structuur tamelijk simpel. Er zijn drie soorten lagen/objecten.

1) de user-interface-laag. Dat is vaak een Console app of een WinForms app of een ASP.NET applicatie. Deze laag wordt meestal niet geunittest, objecten in deze laag hebben normaal _geen_ interface.

2) de service-laag. "all intelligence, barely any knowledge". Service-objecten worden gebruikt door de User-interface-laag om data op te halen en weg te schrijven. Ze slaan (normaal) zelf geen data op, hebben hoogstens een link naar een database. Bij veel C#-projecten zit alle logica van een app (waaraan moet een geldig object voldoen) in de servicelaag. De klassen in de service-laag hebben normaal _wel_ een interface, mede omdat ze dan makkelijk vervangen kunnen worden (van InMemoryPhoneService naar DatabasePhoneService naar WebScrapingPhoneService die allemaal IPhoneService kunnen implementeren).

3) de data-laag. In Enterprise-C# bevat de datalaag doorgaans "domme" objecten. "All knowledge, no intelligence". Meestal alleen simpele properties met { get; set; }. Deze dataklassen hebben geen interface, omdat ze geen gedrag en logica hebben buiten het simpelweg opslaan en ophalen van data.

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

Dat was uiteraard veel handiger, ook voor functieaanroepen. Echter bleken structs ook problemen te hebben: er was geen 'kwaliteitscontrole' op, elk deel van een programma kon de inhoud van de struct aanpassen met willekeurige waarden. Zo maakte ik mee dat er soms een foute waarde zat in een struct, en elk van de 100,000 andere regels in het programma kon die fout veroorzaakt hadden! De 'openheid' van structs zorgde voor veel en moeilijk op te sporen bugs. Daarom besloten latere ontwerpers van programmeertalen, zoals die van Java in de jaren '90, dat velden van structs normaal alleen te wijzigen waren door code in de struct zelf. De velden werden dus 'private'.

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

Hoe dan ook: zo had je geen constructor meer nodig (je kon zeggen p.FirstName = "Hans"; - door de compiler omgezet in p.FirstName("Hans"), maar nog steeds was het behoorlijk wat leeswerk; en mensen gebruikten altijd het patroon "type _varName; public type { get { return _varName; } set { _varName = value; }}". Daarom introduceerde C#3.0 (in 2007) zogenaamde "auto-properties", waar die zich herhalende code werd geëlimineerd:

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

En _wanneer_ properties? Normaal gebruik je properties als een veld/data public moet zijn, dus in data-klassen als Phone. Als data private moet zijn (vaak readonly private, gezet in de constructor en absoluut niet iets dat andere objecten moeten zien, laat staan aan moeten zitten) maak je er een veld van, dus private readonly type _myField.

Let wel: heel soms gebruiken mensen private properties en public fields; maar zeker public fields zijn een slecht idee, want _als_ je moet gaan debuggen omdat ergens in de andere 100,000 regels code de waarde van dat veld onterecht wordt veranderd, moet je hele code hercompileren, en als je de pech hebt dat je code ook door andere applicaties gebruikt wordt, worden die mensen boos op je omdat hun code kapot gaat. Als je data public maakt, zet hem dan alsjeblieft in een property, voor het geval dat. Die 13 karakters extra leeswerk en paar karakters extra typewerk (prop TAB TAB is handig) zijn peanuts vergeleken met de problemen die je krijgt met anderen als je een public field later alsnog moet omzetten naar een property.

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

# Wat is "hexagonale architectuur" en waarom is het belangrijk?

Heel veel werk van een computer is niet 'berekenen' maar het inlezen, transformeren en opslaan van data.

Zo kan het zijn dat je een lijst telefoons moet inlezen van een CSV- of XML-bestand, en het op moet slaan in een database.

Nu zijn er methoden die XML rechtstreeks kunnen omzetten in databasetabellen (er zijn ongeveer overal methodes en libraries voor). Maar meestal is dat een slecht idee.

Een _klein_ bezwaar is nog dat als je de database of het inleesformaat gaat vervangen, je alle code overnieuw moet schrijven.

Een groter probleem is dat je normaal controle wilt hebben over wat het gebeurt. Wat als een leverancier een XML-file aanlevert met een fout erin? Dan crasht je database of wordt die gecorrumpeerd.

En als het formaat in de database anders is dan in de XML-file, wie heeft dan gelijk?

Daarom wil je een "single source of truth" hebben. En als programmeurs heb je die het liefst in broncode, wat compacter is dan een file en flexibeler dan databasecode.

Je gebruikt dus TWEE stappen in plaats van één: 
1) je zet de XML om je domeinobject, bijvoorbeeld een Phone
2) Je slaat de phone op in de database.

Voor beide stappen maak je normaal een aparte service. Je krijgt dus een XmlImportService en een DatabaseService (al kun je die DatabaseService ook bijvoorbeeld PhoneService noemen).

Dit wordt ook wel 'hexagonale architectuur' genoemd en heeft dus meerdere voordelen:

1) je weet altijd welke data je nodig hebt, er is een 'single source of truth'
2) die single source of truth is op een handige plaats, in de code
3) als er een inconsistentie is tussen input en datamodel merk je dat en wordt de data niet gecorrumpeerd.
4) als er een inconsistentie is tussen datamodel en database krijg je een foutmelding en wordt de data niet gecorrumpeerd.
5) het is veel minder werk een output (zoals een database) of input (zoals een XML-file) te vervangen door een alternatief.

Zelf zie ik een goede service als een pollepel: het ene uiteinde zit altijd in het echte eten (het domeinobject, bijvoorbeeld een Phone), het andere uiteinde zit in je hand (input, zoals XMLfile, of output, zoals database-uiteraard kunnen zowel een database als een XMLfile zowel voor input als output gebruikt worden).

Link: https://en.wikipedia.org/wiki/Hexagonal_architecture_(software)


<div style="page-break-after: always;"></div>

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