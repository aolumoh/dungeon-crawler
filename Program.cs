namespace Dungeon_Crawler {
    internal class Program {
        private static bool gameOver = false;
        private static int[] pathOptions;
        private static Player player = new Player();

        static void Main(string[] args) {
            player.OnMonsterEncounter += HandleMonsterEncounter;
            player.OnBattle += HandleBattle;
            player.OnTrapTriggered += HandleTrap;
            player.OnTreasureFound += HandleTreasure;

            while (!gameOver) {
                PresentDirections();
                ProcessEvents();
            }
        }

        public static void PresentDirections() {
            bool validInput = false;
            pathOptions = GetPathOptions(); // Generate path options

            while (!validInput) {

                Console.WriteLine("\nYou find yourself at a junction in the dungeon.");

                // if we are at a dead end
                if (pathOptions.Length == 1 && (PathOptions)pathOptions[0] == PathOptions.DeadEnd) {
                    Console.WriteLine("You've hit a dead end! You'll need to turn back.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    pathOptions = GetPathOptions(); // This will regenerate new path options
                    continue; 
                }

                Console.WriteLine("Which of the following directions do you want to go?\n");

                // Print the path options and number them
                for (int i = 0; i < pathOptions.Length; i++) {
                    Console.WriteLine($"{i + 1}. {(PathOptions)pathOptions[i]}");
                }

                // if the user correctly entered an integer and it corresponds with one of the options
                Console.Write(">>> ");
                if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= pathOptions.Length) {
                    validInput = true;
                } else {
                    Console.WriteLine($"\"{choice}\" is not a valid option. Please enter a number from the list shown.");
                }
            }
        }


        public static void ProcessEvents() {
            int randomizedEvent = TriggerEvent();   // determine which event we will trigger

            switch (randomizedEvent) {
                case 1:
                    player.CallMonster();
                    break;
                case 2:
                    player.CallTrap();
                    break;
                case 3:
                    player.CallTreasure();
                    break;
                case 4:
                    Console.WriteLine("You progressed safely!"); // uneventful
                    break;
                default:
                    Console.WriteLine("Error processing event");
                    break;
            }

            CheckHealth(); // check the player's health to see if the event caused them to lose all hp
        }

        // Event handlers
        // this handler is called when a monster is encountered
        public static void HandleMonsterEncounter() {
            bool validInput = false;

            Console.Write("\nYou encountered a monster! ");
            while (!validInput) { // loop the prompt until user gives a valid input
                Console.WriteLine("Do you want to flee or fight?");
                Console.WriteLine("1. Flee\n2. Fight");
                Console.Write(">>> ");
                int.TryParse(Console.ReadLine(), out int input);

                if (input == 2) {
                    player.CallBattle();
                    validInput = true;
                } else if (input == 1) {
                    validInput = true;
                    Console.WriteLine("\nYou ran and escaped the clutches of the pizza monster!");
                } else {
                    Console.WriteLine($"\n\"{input}\" is not a valid input");
                }
            }
            Console.WriteLine($"Current Health: {player.Health} hp\nCurrent Score: {player.Score} points");
        }

        // this handler in called if the player decided to fight the monster
        public static void HandleBattle() {
            Random random = new Random();
            bool defeatedMonster = random.Next(0, 2) == 1; // 50% chance of defeating monster

            if (defeatedMonster) {
                player.Score += 20;
                player.Health -= 5;
                Console.WriteLine("You won the battle! You gained 20 points and lost only 5 hp");
            } else {
                player.Health -= 20;
                Console.WriteLine("You were defeated by the monster, You lost 20 hp");
            }
        }

        public static void HandleTrap() {
            Random random = new Random();
            bool avoidedTrap = random.Next(0, 2) == 1; // 50% chance of avoiding the trap

            if (avoidedTrap) {
                Console.WriteLine("You avoided the trap, awesome reflexes! Keep going");
            } else {
                player.Health -= 10;
                Console.WriteLine("You were hurt by the trap, You lost 10 hp");
            }
            Console.WriteLine($"Current Health: {player.Health} hp\nCurrent Score: {player.Score} points");
        }

        public static void HandleTreasure() {
            Random random = new Random();
            int value = random.Next(5, 51); // score increases by a random value between 5 and 50 inclusive
            player.Score += value;
            Console.WriteLine($"You found some gold and elixirs, you also gained {value} points!");
            Console.WriteLine($"Current Health: {player.Health} hp\nCurrent Score: {player.Score} points");
        }

        // Other Methods
        public static void CheckHealth() {
            if (player.Health <= 0) {
                gameOver = true;
                Console.WriteLine("--GAME OVER--");
                Console.WriteLine($"You finished with a total score of {player.Score} points!");
            }
        }



        // Provided Code
        public enum PathOptions {
            Left = 1,
            Right = 2,
            Straight = 3,
            DeadEnd = 4
        }

        public enum EventOptions {
            Monster = 1,
            Trap = 2,
            Treasure = 3,
            Nothing = 4
        }

        public static int[] GetPathOptions() {
            Random random = new Random();
            // Check if it's a dead end
            bool isDeadEnd = random.Next(0, 4) == 0; // 25% chance for a dead end

            if (isDeadEnd) {
                return new int[] { (int)PathOptions.DeadEnd };
            } else {
                // Randomize available directions (left, right, straight)
                List<int> options = new List<int>();

                if (random.Next(0, 2) == 1) options.Add((int)PathOptions.Left);
                if (random.Next(0, 2) == 1) options.Add((int)PathOptions.Right);
                if (random.Next(0, 2) == 1) options.Add((int)PathOptions.Straight);

                // If no options were randomly selected, return all options (to avoid a lockout)
                if (options.Count == 0) {
                    options.AddRange(new int[] { (int)PathOptions.Left, (int)PathOptions.Right, (int)PathOptions.Straight });
                }

                return options.ToArray();
            }
        }

        public static int TriggerEvent() {
            Random random = new Random();
            // Randomly select an event (Monster, Trap, Treasure, Nothing)
            return random.Next(1, 5); // This returns an int between 1 and 4 inclusive
        }
    }
}

/*
 * make deadend do something
 * future improvements:
 * make a list of monster names and randomize each time a monster is encountered and display the name to the user when they flee or fight.
 * make sure player can never lose more health than they have?
 */