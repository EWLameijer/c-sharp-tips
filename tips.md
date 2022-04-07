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