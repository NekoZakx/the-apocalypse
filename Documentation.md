# Introduction #

Ce projet est réalisé par des étudiants pour une majeur en conception de jeux vidéos à l'Université du Québec À Chicoutimi (UQAC).<br>
La date d'échéance de ce projet est avant le <b>25 avril 2012</b>.<br>
<br>
<br>
<h1>Description du Projet</h1>

Comme projet, nous avons pensé à réaliser un jeu vidéo de tir multidirectionnel où le joueur doit  survivre à une invasion de morts-vivants. Le but du jeu est d’éliminer tous les morts-vivants dans un niveau fixe. Plus le joueur réussit de niveaux, plus le niveau de difficulté augmente. Par exemple, lors de la progression, il y a de nouveaux morts-vivants avec des pouvoirs différents et il y en a davantage par niveau. Le joueur peut acquérir de nouvelles armes pour augmenter ses chances de survie dans chaque niveau. De plus, nous avons pensé à intégrer la possibilité de jouer en coopération avec des amis dans notre jeu en réseau, cependant le jeu étant d’une grande ampleur, il est possible que cette fonctionnalité ne soit pas réalisé.<br>
<br>
<h1>Hello World... Tout à un commencement</h1>

Puisque nous commençons dans l'univers du jeu vidéo, il va de soit que des tests seront effectués. Tout d'abord, nos premiers tests constituent un élément de familiarisation avec XNA Game Studio.<br>
<br />
Voici les tests qui ont été effectué:<br>
<ul><li>Faire afficher une chaîne de caractère à l'écran et l'animé pour que ce ne soit pas que du texte brute;<br>
</li><li>Faire affichier des sprites et les faire bouger sur la fenêtre de jeu;<br>
</li><li>Prochaine étape: Animé les sprites à l'aide de SpriteSheet.</li></ul>

<h1>La structure du programme</h1>

Pour le bon fonctionnement du programme, une bonne analyse doit être faite. Avec le peu de développement que nous avons effectués, nous avons réalisé le diagramme de classe. Ce diagramme à été conçu avec le peu d'expérience dans XNA que l'équipe dispose. Cependant, les données à conservé au niveau des objets est du pareil au même peu importe le langage de programmation.<br>
<br />
Le diagramme de classe est disponible à l'adresse suivante: <a href='http://dl.dropbox.com/u/15342998/The_Apocalypse.vsd'>lien</a>

<h1>L'envoie de projectile</h1>

Le principe d'envoyer un projectile dans tout jeux vidéo est exprimé selon des formules mathématiques. Puisque la balle est vu comme étant un point sur un plan cartésien où le X et le Y sont strictement positif. Soit la balle dessine une formule de degrée 1 (Une Droite de forme Y = MX + B). Cependant, nous ne la traiterons pas dans cette optique de degrée 1. Nous allons plutôt faire de la trigonométrie et une rapport d'angle pour calculer le tout.<br>
<br>
Simplement faire incrémenter le X pour faire avancé la balle ne donnera pas de bon résultat. Il va de soit pour couvrir la totalité d'un cercle il faut utilisé la trigonométrie. Si l'on connait l'angle en radian de notre droite. Il ne suffit que de faire le COS et le SIN de celui-ci pour obtenir ses coordonnées cartésienne. l'incrémentation du X et du Y se fera selon un ratio établit grâce à l'angle calculer.<br>
<br>
Le calcul de l'angle se fait à la tangante de la division entre 2 points de la droite.<br>
<br>
<img src='http://dl.dropbox.com/u/15342998/trigo.png' />

<h1>Animation du texte</h1>

L'animation d'un texte avec XNA est fort simple. Tout d'abord vous avez besoin de deux variables. L'une composé du texte souhaité et l'une contenant la chaîne de caractère à affiché. Le but est d'affiché les lettres une par une tant que la phrase souhaité n'est pas complètement affiché. Nous allons donc se servir des 2 chaînes de caractère et transférer à un interval régulier chacun des caractères de la variable contenant la phrase complète vers celle qui sera affiché.<br>
<br>
<h1>Les paramètres</h1>

Les paramètres sont l'endroit oû toutes les variables pouvant être modifiées sont regroupées. Cette fonctionnalité doit être disponible à tout moment et sert par la même occasion de menu pause. Le jeu est complètement interrompu lorsque cette fenêtre est activé. Les paramètres retrouvés sont:<br>
<ul><li>Le nom du joueur(15 caractère maximum)<br>
</li><li>Le volume du jeu(0% à 100%)<br>
</li><li>La balance(-100% à 100%)<br>
</li><li>Le pitch(-100% à 100%) Ce paramètre est un supplément facultatif et amusant.<br>
</li><li>La contraste (0 à 255)<br>
</li><li>La luminosité (50 à 255)<br>
</li><li>Activer/Désactiver le mode plein écran</li></ul>

<h1>Intelligence Artificiel</h1>

Cette étape est crucial au projet, c'est grâce à elle que le jeu base son univers de jeu de survie. L'intelligence artificiel est ce qui rend les mort-vivants intelligents. Par intelligent, on sous-entend faire ce qu'un mort-vivant doit faire, soit courir après un être vivants doté d'un cerveau fonctionnel, il est clair qu'un mort-vivant n'a rien dans le crâne. Le but du patron de conception Observateur est justement de faire en sorte que le mort-vivant entreprenne son seul et unique travail, c'est-à-dire poursuivre le joueur jusqu'à sa mort.<br>
<br />
<h3>Comment est-ce que le patron de conception fonctionne?</h3>
Très simple, il suffit d'avoir un sujet et un observateur. Où le sujet dans notre cas est le joueur et l'observateur le zombie. Dès qu'une action qui doit faire réagir les morts-vivants est entreprise par le joueur, ceux-ci doivent réagir à cette action. Dans le cas présent, dès que le joueur bouge, ne serait-ce que d'un nanomètre, le mort-vivant doit en être informé pour se redirigé vers ca cible. Cette information est transmise via une fonction appelé Notify pour Notification qui elle se charge d'appelé une fonction dans chacun des morts-vivants pour mettre à jour les données de positionnement du joueur.<br>
<br>
<h3>Comment le joueur peut parler directement aux mort-vivants?</h3>
Bonne question, c'est grâce à la fonction Attach(Observer) qui en d'autre terme signifie, attaché un observateur. Le joueur à une liste de monstre dans sa classe. La fonction Detach(Observer), retire de la liste l'observateur souhaité. Cependant, ce n'est pas l'objet lui même qui est transférer et stocké dans le joueur. Ce n'est qu'un référencement pour appeler des méthodes sans aucune variable. Le monstre à lui aussi un joueur pour savoir le positionnement.<br>
<br>
<h3>Concrètement, que se passe-t-il?</h3>
Si le joueur, ne bouge pas la fonction Notify ne sera pas appelé et les morts-vivants ne changeront pas leur cible, qui est au coordonné déjà enregistré. Cependant, dès que le joueur bougera, la fonction Notify va être appelé et celle-ci va faire appel à tous les morts-vivants pour leurs dirent, en quelque sorte, de changer de trajectoire puisqu'elle n'est plus valide.<br>
<br>
<h3>Pourquoi ne pas juste transférer les valeurs par le niveau?</h3>
Les ressources! Si l'on fait cela, on ne sait jamais quand le joueur va bouger sauf si l'on garde des coordonnés en variable dans la classe. Si nous suivons un principe OO (Orienté Objet), ce n'est pas à la classe Niveau de prendre le positionnement du joueur en variable. De plus faire cela, reviendrais à dupliquer les valeurs et prendre davantage d'espace mémoire. Avec le principe d'Observateur, le programme fait seulement les calculs lorsque s'en est vraiment nécessaire. Ainsi, nous gagnons en puissance et plus de monstres peuvent être généré pour la cause.<br>
<br />
Pour comprendre l'observateur voici un lien l'expliquant:<br>
<a href='http://www.dofactory.com/Patterns/PatternObserver.aspx'>Observateur (Anglais)</a>

<h2>Le Pathfinding</h2>

Plutôt que de gèrer la collision entre chacun des monstres, les monstres assigneront chacun leur position à une grille de taille égal au nombre de pixel du jeu. La grille aura comme état à chacun de ses carrés, un booléen indiquant la possibilité de passé ou non. Implémenter le pathfinding à ce moment est l'occassion parfaite de pofiner notre intelligence artificiel. Avec le principe de pathfinding, il sera inutile de geré les colisions puisque les monstres ne pouront passé par dessus les uns des autres.<br>
<br>
<h3>Qu'es-ce qu'un pathfinding?</h3>
Un pathfinding est un chemin tracé entre un point A et un point B en contournant les diverses obstacles mit sur le chemin de l'élément en mouvement, pour se rendre à destination.<br>
<br>
<h3>Comment faire pour élaborer un chemin?</h3>
C'est une étape de calcul très complexe. Tout d'abord, prennons un point A qui est situé dans l'une des cases de notre grille. Le type de chemin que nous allons créer est de type étoile. C'est-à-dire que nous allons vérifier chacune des cases autour de notre personnage et vérifier autour des cases que nous avons vérifier excluant celle déjà vérifier. Cela pourrait vous semblez être un algorithme récursif, et bien vous avez raison! Une fois la cible atteinte, la recherche récursive ayant été la plus rapide sera remonté, ainsi révèllant le chemin à parcourir le plus simple pour se rendre à la cible.<br>
<br>
<h3>Qu'es-ce qui arrive si la cible est hors d'atteinte?</h3>
La recherche récursive retournera aucun résultat et le monstres resteras sur place. C'est pourquoi dans ce cas le zombie se promènera aléatoirement sur la carte cherchant une cible. Notamment cette situation sera rare, puisque les monstres vont mourir peu à peu et que le personnage se déplace.<br>
<br>
<h3>Le personnage peut se déplacer au travers des zombies?</h3>
Évidemment que c'est pas possible, il doit se frayer un chemin à travers les positions non prise par la grille du pathfinding.<br>
<br>
<h3>Le pathfinding est alors accessible à tous les personnages?</h3>
Certainement, il sera initializer lors de la création du niveau, en tant que singleton et sera redistribuer à chacun des membres parcourant le plateau de jeu!<br>
<br>
<br>
<h1>Attaque au corps à corps</h1>

Le corps à corps est un peu complexe. Dans la situation que ce n'est qu'un fragment du cercle trigonométrique orienté dans la direction du joueur. Le cercle sera divisé en 2 pour cette partie. L'attaque se fera donc sur 180 degrée ou Pi en radian. Le plus compliquer n'est pas de définir la zone d'attaque, mais plutôt d'y faire les gestions de collision. Si un monstre entre dans cette zone, il sera automatique touché par l'attaque au nombre de seconde qu'elle fait ses dommages.<br>
<br>
Il y a 3 étapes à la résolution de ce problème:<br>
<ul><li>Étape 1: Calculer si le point est dans le cercle<br>
</li><li>Étape 2: Calculer la pente perpendiculaire à celle du joueur et de la souris<br>
</li><li>Étape 3: Définir si le point est au-dessus ou en-dessous de cette droite</li></ul>

<h3>Étape 1 : Le cercle</h3>
La formule du cercle est (x-h)² + (y+k)² = r² , où (h,k) est la position en x et y du joueur, r la zone d'attaque et (x,y) le point a vérifier. Lors d'une vérification à l'intérieur d'un cercle il va de soit que ce n'est pas une égalité, mais plutôt une innégalité. La formule de vient donc (x-h)² + (y+k)² <= r² . Maintenant que la condition peut être évaluer, on passe à l'étape suivante.<br>
<br>
<h3>Étape 2 : La droite perpendiculaire</h3>
La droite perpendiculaire est celle qui découpera notre cercle en 2. Le personnage ne peut pas attaquer sur 360 degrée lorsqu'il regarde dans une seul et unique direction. Puisque le champ de vision d'une personne n'exède pas les 180 degrée et que les bras on une certaine limite d'atteinte, le maximum sera donc la taille d'un demi-cercle. Pour couper le cercle de l'étape 1 en 2, il faut que se soit perpendiculaire à la direction qu'il regarde. Grâce au point de la souris et du joueur, nous sommes en mesure de trouver la pente initial à laquel nous trouverons sa perpendiculaire au point du joueur.<br>
<br>
Pour trouver la perpendiculaire, il faut tout d'abord trouver la pente de la droite première.<br>
<pre>
∆y    Position Y Souris - Position Y Joueur<br>
-- = ---------------------------------------<br>
∆x    Position X Souris - Position X Joueur<br>
</pre>

Nul besoin de trouver le B de la formule Y = MX+B, puisque l'on veut sa perpendiculaire le B est inutile pour l'instant. Maintenant que la pente est trouvé, il faut tourné la pente dans l'autre sens, soit en lui appliquant la formule suivante:<br>
<br>
<pre>
M┴ =    1<br>
-( --- )<br>
M<br>
</pre>

Maintenant que M┴(M perpendiculaire) est calculé, il faut lui appliqué la formule Y = MX + B, donc il faut isoler B avec la position X et Y du joueur.<br>
<br>
<pre>
Y = M┴X + B<br>
<br>
Y<br>
----- = B<br>
M┴X<br>
</pre>

Une fois que le B est isoler, il ne reste qu'à réunir tous les bouts pour avoir notre formule de droite perpendiculaire.<br>
<br>
Y = M┴X + B<br>
<br>
<h3>Étape 3 : La validation</h3>

Cette étape est la finale, dans le cas que la première étape à été validé. Maintenant que nous avons la droite qui coupe notre cercle en 2 et que notre point est dans notre cercle, il nous reste juste à savoir si elle est dans la bonne demi. Pour se faire nous devons savoir dans quel position, au dessus ou en dessous de la droite, la bonne demi se trouve. Généralement, la bonne demi est là où la souris était orienté. Il suffit de regarder si le point de la souris est en dessous ou au dessus de la droite. Ensuite nous comparons avec le point à vérifier s'il est dans la bonne demi que l'on vient de trouvé. S'il est en dessous ou au dessus tout comme le point de la souris, c'est qu'il est valide!<br>
<br>
<h3>Comment vérifier si un point est en dessous ou au dessus d'une droite?</h3>
En fait la notion de au-dessus et en-dessous n'existe pas vraiment dans notre contexte, c'est plutôt de regarder de quel côté est situé notre par rapport à la droite.<br>
<br>
Nous avons, Y = MX+B et un point dans le plan cartésien.<br>
<br>
Grâce à ses 2 éléments, nous sommes en mesure de dire si elle est au-dessus ou en dessous:<br>
<br>
Y reste tel quel, nous allons trouver la valeur du Y pour le X de notre point sur la droite. Donc, voici ce que vous devriez avoir:<br>
<br>
<pre>
Y = M┴(Coordonné X du point à évaluer) + B┴<br>
</pre>

Si le Y du point à évaluer est plus petit que le Y trouvé, ce point est donc en dessous de la droite. Le contraire est aussi vrai, c'est-à-dire que s'il est plus grand, il est situé au-dessus de la droite.<br>
<br>
Ce n'est pas tout! Puisque notre jeu accepte les droites de type Y=X. Dans ce cas, il faut savoir si elle est à droite ou à gauche. Le même principe est utilisé. Il ne suffit que d'isoler le X.<br>
<br>
<pre>
X =    Y<br>
( --- ) + B┴<br>
M┴<br>
</pre>

Il ne reste donc qu'à savoir si le X est plus petit ou plus grand. Dans le cas du plus petit, c'est que le point est situé à gauche de la droite et dans l'autre cas, il est situé à droite.<br>
<br>
<h3>Mise en garde</h3>
Dans une approche où le monstre veux utiliser les attaques au corps à corps il suffit de remplacer la position de souris par celle du joueur et celle du joueur par celle du monstre. De plus, si le point de la souris est le même que celle du joueur... Impossible de calculer la pente, elle sera nulle. Faire un nombre aléatoire pour définir une fause position de souris serait absurde dans ce cas. Il faut donc, créer un faux point dans la direction que le joueur regardait précédemment. À noter que les positions fictive sont les suivantes:<br>
<ul><li>Nord : (x_joueur, y_joueur-1)<br>
</li><li>Sud  : (x_joueur, y_joueur+1)<br>
</li><li>Est  : (X_joueur+1, y_joueur)<br>
</li><li>Ouest: (X_joueur-1, y_joueur)<br>
</li><li>Nord-Est: (X_joueur+1, y_joueur-1)<br>
</li><li>Nord-Ouest: (X_joueur-1, y_joueur-1)<br>
</li><li>Sud-Est: (X_joueur+1, y_joueur+1)<br>
</li><li>Sud-Ouest: (X_joueur-1, y_joueur+1)