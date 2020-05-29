# Sécurité

## SteamPassword

ASF prend actuellement en charge 4 types de mots de passe - `PlainText`, `AES`, `ProtectedDataForCurrentUser` et None (`null` / `""`). 

Afin d’utiliser le mot de passe crypté, vous devez d’abord vous connecter à Steam comme d’habitude avec `Texte brut`, puis générer des mots de passe cryptés à l’aide de la **[commande](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands)** de `password`. Choisissez la méthode de cryptage, que vous préférez, puis mettez le mot de passe crypté obtenu en tant que propriété de config pour le bot `SteamPassword` et enfin n’oubliez pas de changer `PasswordFormat` à celui qui correspond à votre méthode de chiffrement choisie.

* * *

### PlainText

C’est le moyen plus simple et le moins sécurisé de stocker le mot de passe, défini comme `PasswordFormat` de `0`. ASF s'attend à ce que la propriété ` SteamPassword </ 0> soit sous forme de texte brut - le mot de passe est utilisé pour se connecter à Steam sous sa forme directe. C’est le plus facile à utiliser et 100 % compatible avec tous les réglages, il est donc par défaut.</p>

<hr />

<h3>AES</h3>

<p>Considéré comme sûr par les normes actuelles, le <strong><a href="https://en.wikipedia.org/wiki/Advanced_Encryption_Standard">AES</a></strong> moyen de stockage du mot de passe est défini comme <code>PasswordFormat` sur `1`. ASF s'attend à ce que la fonctionnalité `SteamPassword`</a></strong> soit une séquence de caractères **base64-encoded</1>, générant un tableau d'octets chiffré au format AES après traduction, qui doit ensuite être déchiffré à l'aide du **[](https://en.wikipedia.org/wiki/Initialization_vector)**vecteur d'initialisation<2> inclus et la clé de chiffrement ASF.</p> 

La méthode ci-dessus garantit la sécurité tant que l'attaquant ne connaît pas la clé de cryptage ASF intégrée utilisée pour le décryptage ainsi que le cryptage des mots de passe. ASF vous permet de spécifier la clé via `--cryptkey` **[command-line argument](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Command-Line-Arguments)**, que vous devez utiliser pour une sécurité maximale. Si vous décidez de l'omettre, ASF utilisera sa propre clé **connue** et codée en dur dans l'application, ce qui signifie que tout le monde peut inverser le cryptage ASF et obtenir un mot de passe déchiffré. Cela demande toujours quelques efforts et n’est pas si facile à faire, mais c’est possible, c’est pourquoi vous devriez presque toujours utiliser le cryptage `AES` avec votre propre `--cryptkey` qui est gardé secret. La méthode AES utilisée dans ASF fournit une sécurité qui devrait être satisfaisante. C'est un équilibre entre la simplicité de `PlainText`` et la complexité de <code>ProtectedDataForCurrentUser`, mais il est vivement recommandé de l’utiliser avec la <0>--cryptkey</code> personnalisée.

* * *

### ProtectedDataForCurrentUser

Actuellement, le moyen le plus sûr de stocker le mot de passe proposé par ASF, et bien plus sûr que la `AES` décrite ci-dessus, est défini comme `PasswordFormat` de `2`. Le principal avantage de cette méthode est en même temps le principal inconvénient: au lieu d'utiliser une clé de cryptage (comme dans `AES`), les données sont cryptées à l'aide des informations de connexion de l'utilisateur actuellement connecté, ce qui signifie qu'il est possible pour déchiffrer les données **uniquement** sur la machine sur laquelle elles ont été cryptées, et en plus de cela, **uniquement** par l'utilisateur qui a émis le cryptage. Cela garantit que même si vous envoyez votre `Bot.json` au complet, il ne sera pas en mesure de déchiffrer le mot de passe sans accéder à votre PC. C’est une excellente mesure de sécurité, mais qui présente en même temps l’inconvénient majeur d’être moins compatible, car le mot de passe crypté à l’aide de cette méthode sera incompatible avec tout autre utilisateur ainsi qu’avec une machine, y compris ** la votre </ 0>, si vous souhaitez, par exemple réinstallez votre système d'exploitation. Néanmoins, c’est l’une des meilleures méthodes de stockage des mots de passe. Si vous êtes inquiet pour la sécurité de `PlainText`, vous ne souhaitez pas mettre un mot de passe à chaque fois avec l’option `None`. , c’est votre meilleur choix tant que vous n’avez pas à accéder à vos configurations à partir d’une autre machine que la vôtre.</p> 

**Veuillez noter que cette option est disponible uniquement pour les machines fonctionnant sous Windows.**

* * *

### None

La seule façon de garantir à 100% la sécurité et d’assurer que personne ne puisse voler votre mot de passe Steam. Pour pouvoir utiliser cette option, il suffit de définir votre `SteamPassword` sur une chaîne vide (`""`) ou une valeur `null`. ASF vous demandera votre mot de passe Steam lorsque cela sera nécessaire et ne le sauvegardera nulle part, mais gardera en mémoire le processus en cours d'exécution jusqu'à la fermeture du processus. Bien qu’il s’agisse de la méthode la plus sécurisée pour gérer les mots de passe, c’est aussi la plus gênante, car vous devez entrer votre mot de passe manuellement à chaque exécution du fichier ASF (le cas échéant). Si cela ne vous pose pas de problème, c’est votre meilleur choix en matière de sécurité.

* * *

## Recommandation

Si la compatibilité ne vous pose pas problème et que la méthode ` ProtectedDataForCurrentUser </ 0> vous convient, c’est l’option <strong> recommandée </ 1> de stockage du mot de passe dans ASF, car celle-ci fournit la meilleure sécurité. La méthode <code> AES </ 0> est un bon choix pour les personnes qui souhaitent continuer à utiliser leurs configurations sur la machine de leur choix, tandis que <code> PlainText </ 0> est le moyen le plus simple de stocker le mot de passe, si cela ne vous dérange pas que quelqu'un puisse voir le fichier de configuration JSON.</p>

<p>Veuillez garder à l'esprit que toutes ces 3 méthodes sont considérées comme <strong> non sécurisées </ 0> si l'attaquant a accès à votre PC. ASF doit pouvoir déchiffrer le mot de passe chiffré. Si le programme exécuté sur votre machine est capable de le faire, tout autre programme exécuté sur le même ordinateur le fera également. <code> ProtectedDataForCurrentUser </ 0> est la variante la plus sécurisée car <strong> même un autre utilisateur utilisant le même PC ne pourra pas le déchiffrer </ 1>, mais il est toujours possible de déchiffrer les données si quelqu'un est capable de voler. vos identifiants de connexion et vos informations de machine en plus du fichier de configuration ASF.</p>

<p>Pour les personnes qui lancent rarement ASF ou pour celles qui ne se gênent pas pour entrer le mot de passe à chaque démarrage d’ASF, <code>None` est le plus sûr, car le mot de passe Steam n’est enregistré nulle part.

* * *

# Déchiffrement

ASF ne prend en charge aucun moyen de déchiffrer des mots de passe déjà chiffrés, car les méthodes de déchiffrement ne sont utilisées qu'en interne pour accéder aux données dans le processus. Si vous souhaitez rétablir la procédure de chiffrement, par exemple, pour déplacer ASF sur une autre machine lors de l’utilisation de  ProtectedDataForCurrentUser </ 0>, il vous suffit alors de remplacer votre <code> PasswordFormat </ 0> par <code> 0 </ 0> (PlainText) et de renseigner <code> SteamPassword < / 0> de manière appropriée. Vous pouvez ensuite lancer ASF comme d’habitude et répéter la procédure depuis le début.</p>