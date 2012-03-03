using System.Collections.Generic;
using System.Data.Odbc;

namespace The_Apocalypse
{
	class dataBase
    {
	    //Les variables de la BD ici.
		string driver; //Le nom du driver ODBC utilisé pour la connexion à la BD. Ce driver est installé dans le poste client.
		string server; //L'adresse vers le serveur de la BD.
		string login; //Le nom d'utilisateur.
		string password; //Le mot de passe de l'utilisateur.
		string schema; //Le schéma dans lequel on fera les requêtes.
        OdbcConnection DB; //L'objet OdbcConnection qui sera utilisée dans les fonctions de cette classe.
        bool connected = false; //Cette variable permet aux fonctions de savoir si la connexion est faite ou non.
		
        //Ce constructeur s'applique si on ne spécifie pas de driver quand on instancie la classe BD.
		public dataBase (string server, string login, string password, string schema)
		{
			this.driver = "SQL Server"; //Il utilise donc ce driver par défaut.
			this.server = server;
			this.login = login;
			this.password = password;
			this.schema = schema;
		}

        //Ce constructeur s'applique si on spécifie un driver quand on instancie la classe BD. C'est surtout utile pour mon application de test.
        public dataBase (string driver, string server, string login, string password, string schema)
		{
			this.driver = driver;
			this.server = server;
			this.login = login;
			this.password = password;
			this.schema = schema;
		}
        //On utilise cette fonction pour établir une connexion avec la base de données.
        public void connect()
        {
            if (!connected) //Si la connexion n'est pas déjà faite...
            {
                DB = new OdbcConnection("Driver={"+this.driver+"};Server="+this.server+";Database="+this.schema+";Uid="+this.login+";Pwd="+this.password+";Trusted_connection=no"); //La connexion vers la BD est créée ici.
                DB.Open(); //La connexion est faite.
                connected = true; //On confirme la connexion grâce à cette variable.
            }
        }
		
        //On utilise cette fonction lorsqu'on veut exécuter une requête qui n'est pas un SELECT.
		public void query(string query)
        {
            if (connected) //Si la connexion est faite...
            {
                OdbcCommand command = new OdbcCommand(query, DB); //La requête vers la BD est créée ici.
                command.ExecuteNonQuery(); //La requête est exécutée.
            }
        }

        /*On utilise cette fonction lorsqu'on veut exécuter une requête SELECT. Elle retourne une liste de tables d'objets.
         *Un élément de la liste représente une ligne dans les résultats de la requête, et une cellule dans le tableau représente une colonne.
         */
        public List<object[]> readQuery(string query)
        {
            if (connected) //Si la connexion est faite...
            {
                List<object[]> retour = new List<object[]>(); //La liste de tableaux d'objets est créée ici.
                OdbcCommand command = new OdbcCommand(query, DB); //La requête vers la BD est créée ici.
                OdbcDataReader resultat = command.ExecuteReader(); //Les résultats de la requête sont mits ici.
                while (resultat.Read()) //Cette boucle remplit la liste de tableaux d'objets "retour", selon le nombre de lignes dans les résultats.
                {
                    object[] oo = new object[resultat.FieldCount]; //Un nouveau tableau d'objets de la longueur du nombre de cellules dans la ligne est créé ici.
                    resultat.GetValues(oo); //On envoit les colonnes d'une ligne dans le tableau d'objets.
                    retour.Add(oo); //On ajoute le tableau d'objets dans la liste.
                }
                return retour; //La Liste de tableaux d'objets est envoyée.
            }
            else
                return null; //On retourne rien dans ce cas-ci.
        }

        //On utilise cette fonction pour se déconnecter de la base de données.
        public void disconnect()
        {
            if (connected) //Si la connexion est faite...
            {
                DB.Close(); //On la défait.
                connected = false; //Et on met la variable de confirmation à false.
            }
        }
        
        //On utilise cette fonction pour déterminer l'état de connexion n'importe où dans le programme.
        public bool isConnected()
        {
            return connected; //Il suffit d'envoyer la variable connected.
        }
	}
}