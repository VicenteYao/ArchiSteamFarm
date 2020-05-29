# Désapprobation

Depuis ASF V3.1.2.2, nous suivons une politique de désapprobation cohérente afin de rendre le développement et l’utilisation beaucoup plus cohérents.

* * *

## Qu'est ce qu'une désapprobation ?

La désapprobation est un processus consistant à effectuer des modifications majeures plus ou moins importantes rendant obsolètes les options, arguments, fonctionnalités ou fonctions précédemment utilisés. Une désapprobation signifie généralement que quelque chose a été simplement réécrit d'une autre manière, et vous devez vous assurer que vous y passerez dans les meilleurs délais. Dans ce cas, il s'agit simplement de déplacer une fonctionnalité vers un emplacement plus approprié.

ASF change rapidement et cherche toujours à devenir meilleur. This sadly means that we may change or move some existing functionality into another segment of the program in order for it to benefit from new features, compatibility or stability. Grâce à cela, nous n’avons pas besoin de nous en tenir aux décisions de développement obsolètes ou tout simplement douloureusement erronées que nous avons prises il y a des années. Nous essayons toujours de fournir un remplacement raisonnable qui corresponde à l'utilisation attendue des fonctionnalités précédemment disponibles. C'est pourquoi la désapprobation est généralement sans danger et nécessite de petites corrections par rapport à l'utilisation précédente.

* * *

## Stage de désapprobation

ASF suivra 2 étapes de désapprobation, rendant la transition beaucoup plus facile et moins gênante.

### Stage 1

Le stage 1 se produit une fois que la fonctionnalité donnée est devenue obsolète, avec la disponibilité immédiate d'une autre solution (ou aucune si aucune réinstallation n'est envisagée).

Au cours de cette étape, ASF imprimera l’avertissement approprié lorsque la fonction obsolète est utilisée. Dans la mesure du possible, ASF tentera d'imiter l'ancien comportement et restera compatible avec celui-ci. ASF continuera d’être au stage 1 en ce qui concerne cette fonctionnalité au moins jusqu’à la prochaine version stable. C’est le moment où, sans rompre la compatibilité, vous pouvez procéder à la commutation appropriée de tous vos outils et modèles pour satisfaire un nouveau comportement. Vous pouvez confirmer si vous avez effectué toutes les modifications appropriées et de ne plus voir l'avertissement de désapprobation.

### Stage 2

le stage 2 est planifié après la phase 1 expliquée ci-dessus et est publiée dans une version stable. Cette étape introduit la suppression complète de l'existence de fonctionnalités obsolètes, ce qui signifie qu'ASF ne reconnaît même pas que vous tentez d'utiliser une fonctionnalité obsolète, encore moins de la respecter, car elle n'existe tout simplement pas dans le code actuel. ASF ne donnera plus aucun avertissement, car il ne reconnaît plus ce que vous essayez de faire.

* * *

## Résumé

Vous avez plus ou moins un ** mois complet </ 0> pour pouvoir effectuer le basculement approprié, ce qui devrait être amplement suffisant même si vous êtes un utilisateur occasionnel de ASF. Après cette période, ASF ne garantit plus que les anciens paramètres n’auront aucun effet (stage 2), faisant en sorte que certaines fonctions cesseront de fonctionner sans que vous le remarquiez. Si vous lancez ASF après plus d'un mois d'inactivité, il est recommandé de ** recommencer à zéro </ 0> ou de lire tous les journaux des modifications manqués et d'adapter manuellement votre utilisation à la version actuelle.</p> 

In most cases, disregarding deprecation warning will not render general ASF functionality unusable, but rather falling back to default behaviour (which may or may not match your personal preferences).

* * *

## Exemple

Nous avons déplacé : <-1> argument de ligne de commande </ 1> antérieur à la V3.1.2.2 ` sur le serveur <code> dans <0> IPC </ 0> <strong><a href="https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration#global-config">de la configuration globale</ 2>.</p>

<h3>Stage 1</h3>

<p>L'étape 1 s'est déroulée dans la version V3.1.2.2 où nous avons ajouté l'avertissement approprié à l'utilisation du <code>--server`. L'argument `--server` a été automatiquement mappé dans la propriété de configuration globale ` IPC: true `, agissant exactement de la même manière que l'ancien commutateur <0 --server</code>. A partir de maintenant. Cela a permis à tout le monde de faire les changements appropriés avant qu'ASF ne cesse d'accepter l'ancien argument.

### Stage 2

Le stage 2 s'est déroulée dans la version V3.1.3.0, juste après la version V3.1.2.9 stable et le stage 1 expliquée ci-dessus. Le stage 2 a obligé ASF à ne plus reconnaître l'argument `--server` et à le traiter comme tout autre argument non valide transmis, ce qui n'a plus d'effet sur le programme. 177/5000 Pour les personnes qui n'ont toujours pas modifié leur utilisation du `--server` en ` IPC: true `, IPC a complètement cessé de fonctionner, ASF n'effectuant plus le mappage approprié.