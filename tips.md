# Visual Studio-tips

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