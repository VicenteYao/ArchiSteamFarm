# Productcode-activering op de achtergrond

Productcode-activering op de achtergrond is een speciaal ingebouwde ASF-functie waarmee je een serie Steam-productcodes (en de productnamen) kunt importeren die op de achtergrond worden geactiveerd. Dit is vooral handig als je een heleboel codes moet activeren waarvan het zeker is dat je de `Ratelimited` **[status](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/FAQ#what-is-the-meaning-of-status-when-redeeming-a-key)** zal krijgen voordat je klaar bent met het activeren van de batch.

Productcode-activering op de achtergrond is gemaakt zodat het werkt voor één bot. Dit houdt in dat het geen gebruik maakt van de `RedeemingPreferences`. Deze functie kan worden gebruikt samen met (of in plaats van) de `redeem` **[commando](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands)**, indien nodig.

* * *

## Importeren

Het importeren kan worden gedaan op twee manieren - door gebruik te maken van een bestand of via IPC.

### Bestand

ASF is in staat in de `config` map een file met de naam `BotName.keys` te herkennen waarvan `BotNaam` de naam van je bot is. That file has expected and fixed structure of name of the game with cd-key, separated from each other by a tab character and ending with a newline to indicate the next entry. Als meerdere tabs worden gebruikt, wordt de eerste invoer beschouwd als de naam van het spel en de laatste invoer als de productcode. Alles daar tussenin wordt genegeerd. Bijvoorbeeld:

    POSTAL 2    ABCDE-EFGHJ-IJKLM
    Domino Craft VR 12345-67890-ZXCVB
    A Week of Circus Terror POIUY-KJHGD-QWERT
    Terraria    DitWordtGenegeerd  DitWordtOokGenegeerd    ZXCVB-ASDFG-QWERT
    

Alternatively, you're also able to use keys only format (still with a newline between each entry). ASF in this case will use Steam's response (if possible) to fill the right name. For any kind of keys tagging, we recommend that you name your keys yourself, as packages being redeemed on Steam do not have to follow logic of games that they're activating, so depending on what the developer has put, you may see correct game names, custom package names (e.g. Humble Indie Bundle 18) or outright wrong and potentially even malicious ones (e.g. Half-Life 4).

    ABCDE-EFGHJ-IJKLM
    12345-67890-ZXCVB
    POIUY-KJHGD-QWERT
    ZXCVB-ASDFG-QWERT
    

Regardless which format you've decided to stick with, ASF will import your `keys` file, either on bot startup, or later during execution. Na het succesvol verwerken van je bestand en eventuele weglating van ongeldige vermeldingen, worden alle correct gedetecteerde spellen toegevoegd aan de achtergrondwachtrij en wordt het `BotNaam.keys` bestand verwijderd uit de `config` map.

### IPC

In addition to using keys file mentioned above, ASF also exposes `GamesToRedeemInBackground` **[ASF API endpoint](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/IPC#asf-api)** which can be executed by any IPC tool, including our ASF-ui. Using IPC could be more powerful, as you can do appropriate parsing yourself, such as using a custom delimiter instead of being forced to a tab character, or even introducing your entirely own customized keys structure.

* * *

## Wachtrij

Zodra de spellen geïmporteerd zijn, zijn ze toegevoegd aan de wachtrij. ASF doorloopt automatisch de achtergrondwachtrij zolang de bot is verbonden met het Steam-netwerk en de wachtrij niet leeg is. Een productcode, waarbij is geprobeerd die te activeren en niet resulteerde in `RateLimited`, wordt verwijderd uit de wachtrij. De status wordt dan correct weggeschreven naar een bestand in de `config` map. Als een code werd gebruikt in het proces, wordt die opgeslagen in een `BotNaam.keys.used` bestand (bijv. `NoDetail`, `BadActivationCode`, `DuplicateActivationCode`). Als een code niet werd gebruikt, wordt die opgeslagen in een `BotNaam.keys.unused` bestand. ASF gebruikt opzettelijk de door jou opgegeven naam van het spel, omdat de productcode niet altijd een betekenisvolle naam krijgt van het Steam-netwerk - dus op die manier kun je de productcode zelfs je eigen naam geven als dat nodig/gewenst is.

Als tijdens het proces het account de `RateLimited` status krijgt, wordt de wachtrij tijdelijk voor een uur onderbroken om de cooldown af te wachten. Daarna gaat het proces verder waar het gebleven is, totdat de wachtrij leeg is.

* * *

## Voorbeeld

Stel je hebt een lijst met 100 codes. Eerst moet je een nieuw `BotNaam.keys.new` bestand maken in de ASF `config` map. We hebben een `.new` extensie toegevoegd om ASF te laten weten dat het dit bestand niet onmiddellijk moet lezen op het moment dat het gemaakt werd (aangezien het een nieuw leeg bestand is, wat nog niet gereed is om te importeren).

Je kunt nu het nieuwe bestand openen en de lijst met de 100 codes kopiëren en plakken - en indien nodig de opmaak corrigeren. Na de benodigde correcties heeft het `BotNaam.keys.new` bestand precies 100 regels (of 101, met de laatste nieuwe regel). Elke regel heeft een structuur met `GameNaam\tproductcode\n`, waarbij`\t` het tabteken is en`\n` de regeleinde aanduidt.

Je bent nu klaar om dit bestand te hernoemen van `BotNaam.keys.new` naar `BotNaam.keys` om ASF te laten weten dat het gereed is om te worden verwerkt. Wanneer je dit doet, zal ASF automatisch het bestand importeren (een herstart is niet nodig) en daarna verwijderen, waarbij wordt bevestigd dat al je spellen worden verwerkt en aan de wachtrij worden toegevoegd.

In plaats van het `BotName.keys` bestand te gebruiken, kun je ook het IPC API-eindpunt gebruiken of zelfs beide combineren als je dat wilt.

After some time, `BotName.keys.used` and `BotName.keys.unused` files will be generated. Deze bestanden bevatten de resultaten van het activeringsproces. Je kunt bijvoorbeeld `BotNaam.keys.unused` naar een `BotNaam2.keys` bestand hernoemen om de ongebruikte productcodes te gebruiken voor een andere bot, aangezien de vorige bot de codes niet heeft gebruikt. Of je kunt de ongebruikte codes naar een ander bestand kopiëren en ze bewaren voor later gebruik. Hou er rekening mee dat ASF tijdens het afwerken van de wachtrij nieuwe vermeldingen toevoegt aan de `used` en `unused` bestanden. Daardoor is het aanbevolen om te wachten totdat de wachtrij volledig is geleegd voordat je deze bestanden gebruikt. Als je deze bestanden toch wilt openen voordat de wachtrij volledig is geleegd, moet je eerst het uitvoerbestand dat je wilt openen **verplaatsen** naar een andere map **en dan** laten verwerken. De reden daarvoor is dat ASF nieuwe resultaten kan toevoegen terwijl je het bestand opent, wat mogelijk kan leiden tot verlies van productcodes. Als je bijvoorbeeld een bestand opent met 3 codes en het vervolgens verwijdert, negeer je het feit dat ASF ondertussen nog eens 4 andere codes toegevoegd kan hebben aan dat bestand. Als je toegang wilt tot deze bestanden, zorg er dan voor dat je ze uit de ASF `config` map verwijdert voordat je ze opent, bijvoorbeeld door ze te hernoemen.

Het is ook mogelijk om extra spellen te importeren terwijl er al spellen in de wachtrij staan, door alle bovenstaande stappen te herhalen. ASF zal de extra vermeldingen op de juiste manier aan de lopende wachtrij toevoegen en ze afwerken.

* * *

## Opmerkingen

Productcode-activering op de achtergrond gebruikt een `OrderedDictionary` onder de motorkap, wat betekent dat de productcodes de volgorde aanhouden zoals ze zijn ingevoerd in het bestand (of IPC API-verzoek). Dit betekent dat je een lijst kunt (en zou moeten geven) waarbij bepaalde productcodes alleen directe afhankelijkheden kan hebben van de productcodes daarboven, maar niet van daaronder. Als je bijvoorbeeld een DLC `D` hebt waarbij game `G` eerst moet worden geactiveerd, dan moet de productcode van game `G` **altijd** worden toegevoegd vóór de productcode van de DLC `D`. Hetzelfde geldt als `D` afhankelijk is van `A`, `B` en `C`, dan moeten deze 3 codes eerst worden ingevoerd (in willekeurige volgorde, tenzij deze weer afhankelijk van elkaar zijn).

Als je het bovenstaande schema niet volgt, wordt je DLC niet geactiveerd met `DoesNotOwnRequiredApp`, zelfs als je account in aanmerking zou komen om deze te activeren na het afwerken van de volledige wachtrij. Om dit te voorkomen zorg dat je DLC altijd onder het basisspel in je wachtrij staat.