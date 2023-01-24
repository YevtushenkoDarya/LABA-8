using System.Timers;

namespace Laboratory
{
    public class DataFromFile
    {
        // Создаём класс для хранения информации из файла
        public int firstTime { get; set; }
        public int secondTime { get; set; }
        public string position { get; set; }
        public string color { get; set; }
        public string word { get; set; }
    }

    public class Programm
    {
        static int width = 150;
        static int height = 50;
        static System.Timers.Timer aTimer;
        static int time = 0;
        static List<DataFromFile> subtitles = new List<DataFromFile>();
        static string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        private static void Timer()
        {
            aTimer = new System.Timers.Timer(1000);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }
        static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            foreach (var subtitle in subtitles)
            {
                if (subtitle.firstTime == time) WriteWord(subtitle);
                if (subtitle.secondTime == time) DeleteWord(subtitle);
            }
            time++;
        }
        static void Draw()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (i == 0 || i == height - 1) Console.Write("*");
                    else if (j == 0 || j == width - 1) Console.Write("|");
                    else Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
        static void Color(string color)
                                                                                                                                                                                                {
            if (color != null) color = color.Replace(" ", "");
            if (color == "Red") Console.ForegroundColor = ConsoleColor.Red;
            if (color == "Green") Console.ForegroundColor = ConsoleColor.Green;
            if (color == "White") Console.ForegroundColor = ConsoleColor.White;
            if (color == "Blue") Console.ForegroundColor = ConsoleColor.Blue;
            if (color == "Yellow") Console.ForegroundColor = ConsoleColor.Yellow;
            switch(color)
            {
                case "Red": Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "Green":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                default: Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }
        public static void WriteWord(DataFromFile subtitle)
        {
            SetPosition(subtitle.position, subtitle.word.Length);
            Color(subtitle.color);
            Console.WriteLine(subtitle.word);
        }
        public static void SetPosition(string position, int lengthOfWord)
        {
            switch (position)
            {
                case "Top":
                    Console.SetCursorPosition((width - 2 - lengthOfWord) / 2, 1);
                    break;
                case "Bottom":
                    Console.SetCursorPosition((width - 2 - lengthOfWord) / 2, height - 2);
                    break;
                case "Right":
                    Console.SetCursorPosition(width - 1 - lengthOfWord, height / 2 - 1);
                    break;
                case "Left":
                    Console.SetCursorPosition(1, height / 2 - 1);
                    break;
                default:
                    Console.SetCursorPosition((width - lengthOfWord) / 2, height / 2);
                    break;
            }
        }
        public static void DeleteWord(DataFromFile subtitle)
        {
            SetPosition(subtitle.position, subtitle.word.Length);
            for (int i = 0; i < subtitle.word.Length; i++)
            {
                Console.Write(" ");
            }
        }
        public static void PutDataToList(string[] appartedData)
        {
            //Метод расскладывает информацию в class DataFromFile
            DataFromFile dataFromFile = new DataFromFile();
            dataFromFile.firstTime = int.Parse(appartedData[0].Replace(":", "")) % 100;
            if (appartedData.Length == 2)
            {
                dataFromFile.secondTime = int.Parse(appartedData[1].Substring(0, 5).Replace(":", ""));
                dataFromFile.word = appartedData[1].Substring(6);
            }
            else
            {
                dataFromFile.secondTime = int.Parse(appartedData[1].Replace(":", "")) % 100;
                dataFromFile.position = appartedData[2];
                dataFromFile.color = appartedData[3];
                dataFromFile.word = appartedData[4];
            }
            subtitles.Add(dataFromFile);
        }
        static void ReadFile()
        {
            // Метод считывает информацию из файла и вызывает метод, который заполняет class           

            foreach (var str in File.ReadLines(path + "\\text.txt"))
            {
                if (str.Contains('[')) str.Replace(" ", "");
                string[] splittedData = str.Split('-', ',', ']', '[');
                PutDataToList(splittedData);
            }
        }
        static void Main()
        {
            ReadFile();
            Draw();
            Timer();
            Console.ReadKey();
        }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           
    }
}
