# Visual Studio-tips

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


## Een variabelennaam veranderen is zoveel werk!

Druk Ctrl-R-R: en de variabele krijgt in alle code (zelfs in andere bestanden) zijn nieuwe naam. Scheelt heel veel type- en verbeterwerk!


# C#-howtos

## Hoe geef je een euroteken weer in de console?

Als je een euroteken in de console probeert af te drukken, zie je normaal een vraagteken. Hoe komt dat?

Dat komt omdat de console geen karakters doorkrijgt, maar blokken bits. Zoiets als 10110010. Normaal interpreteert de console elke byte als een ASCII-karakter. Maar er is geen Ascii-karakter voor het euroteken. Dus geeft de console aan dat hij deze 'rare code' niet kent.

De manier om dat op te lossen is de console te vertellen dat hij de bits niet moet interpreteren als ASCII, maar als stukjes UTF-8-code (UTF-8 definieert vele duizenden karakters, ook het euro-teken); met 

```
Console.OutputEncoding = Encoding.UTF8; 
```


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

# C#-architectuur

## Wanneer interfaces?

Bij veel C# enterprise-projecten is de structuur tamelijk simpel. Er zijn drie soorten lagen/objecten.

1) de user-interface-laag. Dat is vaak een Console app of een WinForms app of een ASP.NET applicatie. Deze laag wordt meestal niet geunittest, objecten in deze laag hebben normaal _geen_ interface.

2) de service-laag. "all intelligence, barely any knowledge". Service-objecten worden gebruikt door de User-interface-laag om data op te halen en weg te schrijven. Ze slaan (normaal) zelf geen data op, hebben hoogstens een link naar een database. Bij veel C#-projecten zit alle logica van een app (waaraan moet een geldig object voldoen) in de servicelaag. De klassen in de service-laag hebben normaal _wel_ een interface, mede omdat ze dan makkelijk vervangen kunnen worden (van InMemoryPhoneService naar DatabasePhoneService naar WebScrapingPhoneService die allemaal IPhoneService kunnen implementeren).

3) de data-laag. In Enterprise-C# bevat de datalaag doorgaans "domme" objecten. "All knowledge, no intelligence". Meestal alleen simpele properties met { get; set; }. Deze dataklassen hebben geen interface, omdat ze geen gedrag en logica hebben buiten het simpelweg opslaan en ophalen van data. 

# Codestijl-tips

## Taal
Hoewel tekst die aan de gebruiker getoond wordt in het Nederlands kan zijn (bij grotere projecten heb je speciale internationalization-bestanden) schrijf je programmacode normaal in het Engels. Niet alleen omdat de code dan niet telkens wisselt van taal (if (mijnHuis.kleur == Color.Orange)...) maar ook omdat veel programmeerteams tegenwoordig multinationaal zijn, niet iedereen kan evengoed Nederlands lezen en schrijven. 

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


## YAGNI - You Ain't Gonna Need It (Yet)

Als je bezig bent met een programma, zie je vaak hoe een bepaalde klasse of methode breder kan worden toegepast. Bijvoorbeeld: je munteenheid is nu euro, maar misschien gaat het bedrijf in de toekomst internationaal uitbreiden en zullen prijzen dan ook in dollars of yen moeten. Is het dan niet beter aan de Phone-klasse (of aan de PhoneService of aan de Phone console-app) ook een extra parameter toe te voegen voor de currency?

En wat als de BTW verandert van 21% naar bijvoorbeeld 22%? Is het niet handig ergens een file te hebben met constanten zoals die van de BTW, of het percentage in een aparte tabel in de database te zetten zodat je altijd de actuele BTW hebt?

Misschien dat bovenstaande voorbeelden vergezocht lijken; maar ik kan je garanderen dat je als programmeur (heb ik zelf ook) vaak het idee krijgt: "maar wat als dit gaat gebeuren?"

Het probleem is dat je als programmeur slechts zelden gelijk hebt over wat de toekomst brengt of een klant wil of nodig heeft. En in de tussentijd loop je het risico dat je voor die extra flexibiliteit extra code nodig hebt; en code is dus geen asset, maar een liability (zie hierboven). Hoe meer code je hebt, hoe logger de applicatie wordt, en hoe minder makkelijk hij te veranderen is.

Een ander probleem zijn de extra nodige 'begripskosten'. Mensen die je code bekijken - je peer reviewers, je collega's die iets moeten veranderen, en mogelijk jijzelf na twee maanden - moeten immers extra tijd besteden omdat ze zich afvragen "waarom wordt dit nou gedaan? Dit is toch helemaal niet nodig?" Wat jou weer tijd kan kosten (via git blame) om het uit te leggen, of een commentaar te schrijven (dat dus ook weer gelezen moet worden). Ook in dit opzicht maken de 'mogelijk in de toekomst nodige features' het leven van jou en je team moeilijker.

Vandaar dat programmeurs het vaak hebben over YAGNI: You Ain't Gonna Need It (Yet). Mocht je daar meer over willen weten - of mogelijk nog wat sappiger voorbeelden willen weten dan ik hierboven heb gegeven, zou ik aanraden te kijken op de site van Martin Fowler (sowieso een handige praktijk-goeroe om te kennen https://martinfowler.com/bliki/Yagni.html) of van Vladimir Khorikov, die, met zijn karakteristieke diepgang, ook aangeeft in welke uitzonderingssituatie YAGNI géén goed idee is (https://enterprisecraftsmanship.com/posts/yagni-revisited/) - in het geval dat je een API voor een bibliotheek aan het verkopen bent aan andere bedrijven - voor je iets publiceert, zorg ervoor dat je goed hebt nagedacht over het mogelijk gebruik, want gebruikers zijn doorgaans niet blij als hun code niet meer werkt omdat je later iets hebt veranderd in de interface.

Hoe dan ook, voor normale softwareontwikkeling is YAGNI nog steeds een van de beste leidprincipes om te volgen.


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