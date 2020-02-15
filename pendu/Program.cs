using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pendu
{
    /// <summary>
    /// Classe de base d'un programme
    /// </summary>
    class Program
    {
        /// <summary>
        /// Générateur d'aléatoire
        /// </summary>
        private static Random _Random = new Random();
        /// <summary>
        /// Méthode d'exécution principal
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)

        {
            //mot générer pour le mot cacher
            string wordToFindString = null;
            char[] wordToFind = null;
            //mot vu par l'utilisateur
            char[] wordToShow = null;
            //vie de l'utilisateur
            int lifeCount = 6;
            //mot remplacé par le mot généré aléatirement
            wordToFindString = GetRandomWord();
            wordToFind = wordToFindString.ToArray();
            //mot remplacé par des étoiles
            wordToShow = new string('*', wordToFind.Length).ToArray();
            bool isFinished = false;
            string userInput = null;
            string[] dessin = File.ReadAllText("..\\..\\..\\Data\\Dessin.txt").Replace("\r", "").Split('\n');
            do
            {
                //vérifions que l'utilisateur n'a pas saisir plusieurs char
                //On affiche son nombre d vie restant et le mot caché par des *
                do
                {
                    Console.Clear();
                    switch (lifeCount)
                    {
                        case 1:
                            for (int i = 35; i < 42; i++)
                            {
                                Console.WriteLine(GetDessin(i));
                            }
                            break;
                        case 2:
                            for (int i = 28; i < 35; i++)
                            {
                                Console.WriteLine(GetDessin(i));
                            }
                            break;
                        case 3:
                            for (int i = 21; i < 28; i++)
                            {
                                Console.WriteLine(GetDessin(i));
                            }
                            break;
                        case 4:
                            for (int i = 14; i < 21; i++)
                            {
                                Console.WriteLine(GetDessin(i));
                            }
                            break;
                        case 5:
                            for (int i = 7; i < 14; i++)
                            {
                                Console.WriteLine(GetDessin(i));
                            }
                            break;
                        default:
                            for (int i = 0; i < 7; i++)
                            {
                                Console.WriteLine(GetDessin(i));
                            }
                            break;
                    }
                    Console.WriteLine("Nombre de vie restant : " + lifeCount + Environment.NewLine + "Essayer de trouver une lettre" + Environment.NewLine + new string(wordToShow));
                    userInput = Console.ReadLine();
                } while (userInput.Length != 1 || userInput.All(char.IsLetter) == false);
                //Si notre mot contient le caractère, on traire
                if (wordToFindString.ToString().Contains(userInput[0]))
                {
                    //Modification du mot à trouver,remplace les étoiles par des lettres
                    for (int i = 0; i < wordToFindString.Length; i++)
                    {
                        //on remplace les étoile par la lettre de l'utilisateur si elle est est dans le mot
                        if (wordToFind[i] == userInput[0])
                        {
                            wordToShow[i] = userInput[0];
                        }
                    }
                    Console.WriteLine(wordToShow);
                }
                //Sinon il perd une vie 
                else
                {
                    lifeCount--;
                }
                //si l'utilisateur n'as plus de vie et qu'il n'a pas trouver il a perdu et lui donne le mot caché
                if (lifeCount == 0)
                {
                    isFinished = true;
                    Console.Clear();
                    for (int i = 42; i < 49; i++)
                    {
                        Console.WriteLine(GetDessin(i));
                    }
                    Console.WriteLine("Plus de vie tu as perdu");
                    Console.WriteLine("Le mot était : " + wordToFindString);

                }
                //Si l'utilisateur a trouver le mot il a gagné 
                if (new string(wordToShow) == wordToFindString)
                {
                    isFinished = true;
                    Console.Clear();
                    Console.WriteLine("Nombre de vie restant : " + lifeCount);
                    Console.WriteLine("Le mot que tu as trouvé : " + wordToFindString);
                    Console.WriteLine("Tu as gagné BRAVO !");
                }
                if (isFinished == true)
                {
                    Console.WriteLine("Recommencer une partie (y/n) ");
                    string restart = Console.ReadLine();
                    switch (restart)
                    {
                        case "y":
                            isFinished = false;
                            break;
                        case "n":
                            isFinished = true;
                            break;
                    }
                }
            } while (!isFinished);

            Console.ReadKey();
        }
        /// <summary>
        /// Retourne un mot aléatoire du fichier Dictionnaire.txt
        /// </summary>
        /// <returns></returns>
        static string GetRandomWord()
        {
            string[] words = File.ReadAllText("..\\..\\..\\Data\\Dictionnaire.txt").Replace("\r", "").Split('\n');
            return words[_Random.Next(0, words.Length)];
        }
        /// <summary>
        /// Retourne le dessin correspondant au nombre de vie
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        static string GetDessin(int position)
        {
            string[] dessin = File.ReadAllText("..\\..\\..\\Data\\Dessin.txt").Replace("\r", "").Split('\n');
            return dessin[position];
        }

    }
}
