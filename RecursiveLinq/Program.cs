namespace RecursiveLinq
{
    public class Program
    {
        public static void Main()
        {
            var inputCharacters = new List<char> { 'a', 'b', 'c' };
            Program p = new Program();

            var allSequences = p.GetAllSequences(inputCharacters.AsEnumerable());

            foreach (var sequence in allSequences)
            {
                Console.WriteLine(sequence);
            }
        }

        IEnumerable<string> GetAllSequences(IEnumerable<char> chars)
        {
            return GetAllSequencesRec(chars.ToList(), new List<string>());
        }

        public static IEnumerable<string> GetAllSequencesRec(IEnumerable<char> remainedCharacters, IEnumerable<string> resultedCombinations)
        {
            if (!remainedCharacters.Any()) // Символы закончились, завершение
            {
                return resultedCombinations;
            }
            else // Продолжить добавление
            {
                var newCombinations = new List<string>();

                foreach (var character in remainedCharacters)
                {
                    var tempCombinations = new List<string>(resultedCombinations);

                    if (!tempCombinations.Any()) // Новая комбинация начиная с этого символа
                    {
                        tempCombinations.Add(character.ToString());
                    }
                    else // Добавление символа в конец каждой созданной комбинации
                    {
                        for (int i = 0; i < resultedCombinations.Count(); i++)
                        {
                            tempCombinations[i] += character;
                        }
                    }

                    // Новые комбинации без добавления этого символа
                    var appended = GetAllSequencesRec(remainedCharacters.Where(c => c != character), tempCombinations);

                    // Добавление полученных комбинация в результат
                    newCombinations.AddRange(appended);
                }

                return newCombinations;
            }
        }
    }
}