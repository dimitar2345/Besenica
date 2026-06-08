using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.ConstrainedExecution;


namespace MyApp
{
    internal class Program
    {
        static List<History> histories = new List<History>();

        static void Main(string[] args)
        {



            while (true)
            {
                PrintCaption();
                int responce = PrintOptions();

                switch (responce)
                {
                    case 1:
                        Game();
                        break;
                    case 2:
                        PrintHistory();
                        break;
                    case 3:
                        PrintRules();
                        break;
                    case 4:
                        //Exit
                        Console.WriteLine("Have a great day!!!");
                        return;
                    default:
                        Console.WriteLine("Invalid input");
                        break;

                }
                Console.Clear();
            }


        }
        static void PrintHistory()
        {
            if(histories.Count()<=0||histories == null)
            {
                Console.WriteLine("The history is empthy");
                Console.WriteLine("Press enter to continiue...");
                Console.ReadLine();
                return;
            }
            int gameCount = 1;

            foreach (var temp in histories)
            {
                Console.Write($"Игра {gameCount}: ПРОГРАМА | ");
                if (temp.win)
                {
                    Console.Write("Победа |");
                }
                else
                {
                    Console.Write("Загуба |");
                }
                Console.WriteLine($" Грешки: {temp.mistakes}");
                Console.Write("Използвани букви: ");
                for (int i = 0; i < temp.usedLetters.Count(); i++)
                {
                    if (i == temp.usedLetters.Count() - 1)
                    {
                        Console.Write(temp.usedLetters[i]);

                    }
                    else
                    {
                        Console.Write(temp.usedLetters[i] + ",");
                    }
                }
                Console.WriteLine();
                Console.WriteLine();
                gameCount++;
            }
            Console.WriteLine("Press enter to continiue...");
            Console.ReadLine();
            Console.Clear();
            return;

        }
        static void AddHistory(bool win, int mistakes, Dictionary<string, List<char>> usedChars)
        {
            List<char> used = new List<char>();
            foreach (List<char> Result in usedChars.Values)
            {
                foreach (char letter in Result)
                {
                    used.Add(letter);
                }
            }
            histories.Add(new History(win, mistakes, used));
            return;
        }
        static void Game()
        {
            PrintCaption();
            Dictionary<string, List<char>> usedChars = new Dictionary<string, List<char>>()
            {
                {
                    "Used", new List<char>()
                },
                {
                    "Mistakes", new List<char>()
                }
            };
            List<string> chosenWord = WordSelect();
            chosenWord[0] = chosenWord[0].ToUpper();
            Console.Clear();
            int mistakes = 0;
            char[] firsLastLetter = new char[2] { chosenWord[0][0], chosenWord[0][chosenWord[0].Length - 1] };
            while (true)
            {
                bool isFinished = true;
                char inputLetter;
                PrintCaption();
                Console.WriteLine($"Category: {chosenWord[1]}");
                Console.WriteLine($"Hint: {chosenWord[2]}");
                DrawHangman(mistakes);


                Console.Write("Word: ");
                foreach (char n in chosenWord[0])
                {
                    if (firsLastLetter.Contains(n) || usedChars["Used"].Contains(n))
                    {
                        Console.Write(n + " ");

                    }
                    else
                    {
                        Console.Write("_ ");
                        isFinished = false;

                    }
                }
                Console.WriteLine();


                Console.Write("Used letters: ");
                for (int i = 0; i < usedChars["Used"].Count(); i++)
                {
                    if (i == usedChars["Used"].Count() - 1)
                    {
                        Console.Write(usedChars["Used"][i]);

                    }
                    else
                    {
                        Console.Write(usedChars["Used"][i] + ",");
                    }

                }
                Console.WriteLine();


                Console.Write("Wrong letters: ");
                for (int i = 0; i < usedChars["Mistakes"].Count(); i++)
                {
                    if (i == usedChars["Mistakes"].Count() - 1)
                    {
                        Console.Write(usedChars["Mistakes"][i]);
                        break;
                    }
                    else
                    {
                        Console.Write(usedChars["Mistakes"][i] + ",");
                    }

                }
                Console.WriteLine();


                Console.WriteLine("Mistakes left: " + (6 - mistakes));
                if (mistakes == 6)
                {
                    Console.Clear();
                    Console.WriteLine("Game over!");
                    Console.WriteLine("The word was: "+chosenWord[0]);
                    AddHistory(false, mistakes, usedChars);
                    Console.WriteLine("Press enter to continiue...");
                    Console.ReadLine();
                    Console.Clear();
                    return;
                }
                if (isFinished)
                {
                    
                    Console.WriteLine("You win!");
                    AddHistory(true, mistakes, usedChars);
                    Console.WriteLine("Press enter to continiue...");
                    Console.ReadLine();
                    Console.Clear();
                    return;
                }


                while (true)
                {
                    string input = Console.ReadLine();

                    if (char.TryParse(input, out inputLetter))
                    {
                        inputLetter = char.ToUpper(char.Parse(input));
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input");
                    }
                }
                if (chosenWord[0].Contains(inputLetter))
                {
                    usedChars["Used"].Add(inputLetter);
                }
                else
                {
                    usedChars["Mistakes"].Add(inputLetter);
                    mistakes++;
                }
                Console.Clear();
            }

        }
        static void PrintCaption()
        {
            Console.WriteLine("/------------------------------\\");
            Console.WriteLine("| WELLCOME TO A GAME OF HANGMAN|");
            Console.WriteLine("\\------------------------------/");
        }
        static int PrintOptions()
        {
            while (true)
            {
                Console.WriteLine("1.New game");
                Console.WriteLine("2.Game history");
                Console.WriteLine("3.Ruls");
                Console.WriteLine("4.Exit");

                string input = Console.ReadLine();
                int responce = 0;
                if (int.TryParse(input, out responce))
                {
                    return responce;
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
            }
        }
        static void DrawHangman(int mistakes)
        {
            Console.WriteLine(" +---+");
            Console.WriteLine(" |   |");

            if (mistakes >= 1)
                Console.WriteLine(" O   |");
            else
                Console.WriteLine("     |");

            if (mistakes == 2)
                Console.WriteLine(" |   |");
            else if (mistakes == 3)
                Console.WriteLine("/|   |");
            else if (mistakes >= 4)
                Console.WriteLine("/|\\  |");
            else
                Console.WriteLine("     |");

            if (mistakes == 5)
                Console.WriteLine("/    |");
            else if (mistakes >= 6)
                Console.WriteLine("/ \\  |");
            else
                Console.WriteLine("     |");

            Console.WriteLine("     |");
            Console.WriteLine("=========");
        }
        static void PrintRules()
        {
            Console.WriteLine(@"Hangman Rules (copyable text)

Objective:
Guess the hidden word before the hangman drawing is completed.

Setup:

1. One player thinks of a word and keeps it secret.
2. They draw a blank line (_) for each letter in the word.

   Example:
   Word: APPLE
   Display: _ _ _ _ _

How to Play:

1. The guessing player chooses one letter.
2. If the letter is in the word:

   * Reveal every occurrence of that letter in the correct positions.
3. If the letter is not in the word:

   * Add one part to the hangman drawing.
4. Continue guessing letters until the word is guessed or the drawing is completed.

Typical Hangman Drawing Order:

1. Head
2. Body
3. Left arm
4. Right arm
5. Left leg
6. Right leg

Winning:

* The guesser wins if they reveal all the letters before the hangman is completed.
* The host wins if the hangman is completed before the word is guessed.

Optional Rule:

* A player may try to guess the entire word instead of a single letter on their turn.

Example:

Secret word: CAT

Start:

_ _ _

Guess A:
_ A _

Guess C:
C A _

Guess T:
C A T

Result:
The guesser wins.");
            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }
        static List<string> WordSelect()
        {
            int choice = 0;
        var categories = new Dictionary<string, Dictionary<string, string>>()
        {
            {
                "Животни",
                new Dictionary<string,string>()
                {
                    { "Куче", "Домашен любимец, лае" },
                    { "Котка", "Домашен любимец, мърка" },
                    { "Лъв", "Наричан цар на животните" },
                    { "Слон", "Най-голямото сухоземно животно" },
                    { "Мишка", "Малък гризач" },
                    { "Тигър", "Голяма раирана котка" },
                    { "Костенурка", "Има твърда черупка" },
                    { "Динозавър", "Праисторическо животно" },
                    { "Пингвин", "Птица, която не може да лети" },
                    { "Змия", "Влечуго без крака" },
                    { "Папагал", "Птица, която може да повтаря думи" }
                }
            },

            {
                "Държави",
                new Dictionary<string,string>()
                {
                    { "България", "Столицата е София" },
                    { "Франция", "Столицата е Париж" },
                    { "Япония", "Страна на изгряващото слънце" },
                    { "Канада", "Втората по площ държава в света" },
                    { "Бразилия", "Най-голямата държава в Южна Америка" },
                    { "Германия", "Столицата е Берлин" },
                    { "Русия", "Най-голямата държава по площ" },
                    { "Швейцария", "Известна с часовници и банки" },
                    { "Индия", "Една от най-населените държави" },
                    { "Италия", "Столицата е Рим" },
                    { "Аржентина", "Известна с тангото" }
                }
            },

            {
                "Компютърни термини",
                new Dictionary<string,string>()
                {
                    { "Програма", "Изпълнява задачи на компютър" },
                    { "Алгоритъм", "Последователност от стъпки за решаване на проблем" },
                    { "Браузър", "Използва се за разглеждане на уеб сайтове" },
                    { "Файл", "Съдържа данни, записани на устройство" },
                    { "Сървър", "Предоставя услуги на други компютри" }
                }
            },

            {
                "Герои в лига",
                new Dictionary<string,string>()
                {
                    { "Aatrox", "Darkin воин с огромен меч" },
                    { "Ahri", "Деветоопашата лисица магьосница" },
                    { "Akali", "Нинджа убиец от Kinkou" },
                    { "Akshan", "Стрелец, който може да възкресява съюзници" },
                    { "Alistar", "Минотавър танк" },
                    { "Ambessa", "Ноксианска военачалничка" },
                    { "Amumu", "Тъжната мумия" },
                    { "Anivia", "Леден феникс" },
                    { "Annie", "Момиче с мечето Tibbers" },
                    { "Aphelios", "Стрелец с пет оръжия" },
                    { "Ashe", "Ледена стрелкиня от Freljord" },
                    { "Aurelion Sol", "Космически дракон, създател на звезди" },
                    { "Aurora", "Магьосница, свързана с духовния свят" },
                    { "Azir", "Императорът на Shurima" },

                    { "Bard", "Космически пазител, събиращ камбанки" },
                    { "Bel'Veth", "Императрицата на Void" },
                    { "Blitzcrank", "Паранен робот с кука" },
                    { "Brand", "Жив огън" },
                    { "Braum", "Герой с огромен щит" },
                    { "Briar", "Вампирка с неудържим глад" },

                    { "Caitlyn", "Шерифът на Piltover" },
                    { "Camille", "Агент с остриета вместо крака" },
                    { "Cassiopeia", "Жена-змия" },
                    { "Cho'Gath", "Чудовище от Void, което расте при хранене" },
                    { "Corki", "Йордъл пилот" },

                    { "Darius", "Ръката на Noxus" },
                    { "Diana", "Воин на луната" },
                    { "Dr. Mundo", "Лудият доктор" },
                    { "Draven", "Екзекуторът на Noxus с въртящи се брадви" },
                    { "Ekko", "Момче, което манипулира времето" },
                    { "Elise", "Жена-паяк" },
                    { "Evelynn", "Демон, който примамва жертвите си" },
                    { "Ezreal", "Приключенец с магическа ръкавица" },

                    { "Fiddlesticks", "Древно плашило на страха" },
                    { "Fiora", "Майсторка на дуелите" },
                    { "Fizz", "Амфибиен измамник с тризъбец" },

                    { "Galio", "Оживяла каменна статуя" },
                    { "Gangplank", "Безмилостен пират" },
                    { "Garen", "Могъщ воин от Demacia" },
                    { "Gnar", "Малък йордъл, който се превръща в звяр" },
                    { "Gragas", "Любител на бирата и битките" },
                    { "Graves", "Стрелец с огромна пушка" },
                    { "Gwen", "Оживяла кукла с ножици" },

                    { "Hecarim", "Призрачен конник" },
                    { "Heimerdinger", "Йордъл учен и изобретател" },
                    { "Hwei", "Художник магьосник" },
                    { "Illaoi", "Жрица на Nagakabouros" },
                    { "Irelia", "Танцуваща с остриета воин" },
                    { "Ivern", "Добрият зелен пазител на гората" },

                    { "Janna", "Повелителка на вятъра" },
                    { "Jarvan IV", "Принцът на Demacia" },
                    { "Jax", "Майстор на оръжията" },
                    { "Jayce", "Изобретател с трансформиращ се чук" },
                    { "Jhin", "Убиец, обсебен от числото четири" },
                    { "Jinx", "Хаотична стрелкиня от Zaun" },

                    { "K'Sante", "Ловец на чудовища от Nazumah" },
                    { "Kai'Sa", "Оцеляла във Void" },
                    { "Kalista", "Духът на отмъщението" },
                    { "Karma", "Духовният водач на Ionia" },
                    { "Karthus", "Певецът на смъртта" },
                    { "Kassadin", "Скитникът между световете" },
                    { "Katarina", "Убийца с кинжали" },
                    { "Kayle", "Праведната ангелска воин" },
                    { "Kayn", "Воин, борещ се с Darkin оръжие" },
                    { "Kennen", "Мълниеносен нинджа йордъл" },
                    { "Kha'Zix", "Хищник от Void" },
                    { "Kindred", "Олицетворение на смъртта" },
                    { "Kled", "Луд йордъл ездач" },
                    { "Kog'Maw", "Ненаситно създание от Void" },

                    { "LeBlanc", "Мистериозна магьосница на Noxus" },
                    { "Lee Sin", "Сляп монах и майстор на бойните изкуства" },
                    { "Leona", "Воин на слънцето" },
                    { "Lillia", "Срамежлива еленка от сънищата" },
                    { "Lissandra", "Ледената вещица" },
                    { "Lucian", "Ловец на духове" },
                    { "Lulu", "Фея магьосница" },
                    { "Lux", "Магьосница на светлината" },
                    { "Malphite", "Живо същество от камък" },
                    { "Malzahar", "Пророкът на Void" },
                    { "Maokai", "Оживяло дърво" },
                    { "Master Yi", "Майстор на стила Wuju" },
                    { "Mel", "Политик и магьосница от Noxus" },
                    { "Milio", "Млад лечител с огнени духчета" },
                    { "Miss Fortune", "Ловец на глави и пиратка" },
                    { "Mordekaiser", "Железният ревенант" },
                    { "Morgana", "Паднал ангел" },

                    { "Naafiri", "Darkin, затворен в глутница кучета" },
                    { "Nami", "Русалка от дълбините" },
                    { "Nasus", "Възнесен пазител с чакалска глава" },
                    { "Nautilus", "Гигант в брониран водолазен костюм" },
                    { "Neeko", "Хамелеон, който копира външността на други" },
                    { "Nidalee", "Ловджийка, която се превръща в пума" },
                    { "Nilah", "Воин, използващ силата на радостта" },
                    { "Nocturne", "Жив кошмар" },
                    { "Nunu & Willump", "Момче и снежен йети" },

                    { "Olaf", "Берсерк викинг" },
                    { "Orianna", "Механично момиче с магическа сфера" },
                    { "Ornn", "Бог ковач на Freljord" },

                    { "Pantheon", "Непреклонен воин със копие и щит" },
                    { "Poppy", "Йордъл с огромен чук" },
                    { "Pyke", "Призрачен убиец от Bilgewater" },

                    { "Qiyana", "Императрица на елементите" },
                    { "Quinn", "Разузнавачка с орел на име Valor" },
                }

            }
        };

            Random random = new Random();



            while (true)
            {
                Console.WriteLine("Изберете категория:");
                Console.WriteLine("1 - Животни");
                Console.WriteLine("2 - Държави");
                Console.WriteLine("3 - Компютърни термини");

                string input = Console.ReadLine();

                if (int.TryParse(input, out choice))
                {
                    choice = int.Parse(input);
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
            }
            string category = "";
            string selectedWord = "";
            if (choice == 745748379)
            {
                Dictionary<string,string> words = categories["Герои в лига"];
                selectedWord = words.Keys.ElementAt(random.Next(words.Count()));
                category = "Герои в лига";
            }
            else if (choice >= 1 && choice <= 3)
            {
                category = choice == 1 ? "Животни" :
                                  choice == 2 ? "Държави" :
                                  "Компютърни термини";

                Dictionary<string,string> words = categories[category];
                selectedWord = words.Keys.ElementAt(random.Next(words.Count()));
            }
            else
            {
                Console.WriteLine("Choosing random");
                Dictionary<string,string> allWords = new Dictionary<string,string>();

                foreach (var category_names in categories.Values)
                {
                    if (category_names.ToString() == "Герои в лига")
                    {
                        continue;
                    }
                    foreach(var word in category_names)
                    {
                        allWords.Add(word.Value,word.Key);
                    }
                    
                }

                selectedWord = allWords.Keys.ElementAt(random.Next(allWords.Count()));
                category = allWords[selectedWord];
            }
            List<string> answer = new List<string>(){selectedWord,category,categories[category][selectedWord]};
            return answer;
        }



    }
    class History
    {
        public bool win;
        public int mistakes;
        public List<char> usedLetters = new List<char>();

        public History(bool Win, int Mistakes, List<char> UsedLetters)
        {
            win = Win;
            mistakes = Mistakes;
            usedLetters = UsedLetters;
        }
    }

}