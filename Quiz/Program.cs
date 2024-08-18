using Newtonsoft.Json;

namespace Quiz {
    class Program {
        static void Main(string[] args) {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string jsonFilePath = "C:\\Users\\sebas\\source\\repos\\Quiz\\quiz.json";
            string jsonData = File.ReadAllText(jsonFilePath, System.Text.Encoding.UTF8);

            Quiz quiz = JsonConvert.DeserializeObject<Quiz>(jsonData);

            RunQuiz(quiz);
        }

        static void RunQuiz(Quiz quiz) {
            int score = 0;
            int questionNumber = 1;

            foreach (var question in quiz.Questions) {
                Console.WriteLine($"Question {questionNumber}: {question.QuestionText}");
                for (int i = 0; i < question.Options.Length; i++) {
                    Console.WriteLine($"{i + 1}. {question.Options[i]}");
                }

                Console.Write("Dit svar (1-3): ");
                int userAnswer;
                while (!int.TryParse(Console.ReadLine(), out userAnswer) || userAnswer < 1 || userAnswer > question.Options.Length) {
                    Console.Write("Invalid input. Skriv et tal imellem 1 og 3: ");
                }

                if (userAnswer - 1 == question.CorrectOption) {
                    Console.WriteLine("Rigtigt!");
                    score++;
                }
                else {
                    Console.WriteLine("Forkert!");
                }
                Console.WriteLine();
                questionNumber++;
            }

            Console.WriteLine($"Din score er: {score}/{quiz.Questions.Count}");
        }
    }

    class Quiz {
        [JsonProperty("questions")]
        public List<Question> Questions { get; set; }
    }

    class Question {
        [JsonProperty("question")]
        public string QuestionText { get; set; }

        [JsonProperty("options")]
        public string[] Options { get; set; }

        [JsonProperty("correctOption")]
        public int CorrectOption { get; set; }
    }
}
