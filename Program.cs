namespace Dungeon_Crawler {
    internal class Program {
        private static bool gameOver = false;

        static void Main(string[] args) {
            while (!gameOver) {
                PresentDirections();
                ProcessDirection();
            }
        }

        public static void PresentDirections() {
            Console.WriteLine("Which of the following directions do you want to go?\n");
            int[] pathOptions = GetPathOptions();
            for(int i = 0; i < pathOptions.Length; i++) {
                Console.WriteLine($"{i + 1}. {(PathOptions)pathOptions[i]}");
            }
            Console.WriteLine();
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
