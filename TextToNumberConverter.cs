using System.Text.RegularExpressions;
using TextToNumberNLP.Models;

namespace TextToNumberNLP
{
    public static class TextToNumberConverter
    {
        private static Dictionary<string, int> numberWords = new()
        {
            { "sıfır", 0 },
            { "bir", 1 },
            { "iki", 2 },
            { "üç", 3 },
            { "dört", 4 },
            { "beş", 5 },
            { "altı", 6 },
            { "yedi", 7 },
            { "sekiz", 8 },
            { "dokuz", 9 },
            { "on", 10 },
            { "yirmi", 20 },
            { "otuz", 30 },
            { "kırk", 40 },
            { "elli", 50 },
            { "altmış", 60 },
            { "yetmiş", 70 },
            { "seksen", 80 },
            { "doksan", 90 },
            { "yüz", 100 },
            { "bin", 1_000 },
            { "milyon", 1_000_000 },
            { "milyar", 1_000_000_000 }
        };

        public static UserOutputModel Convert(string input)
        {

            string pattern = @"(?i)(sıfır|bir|iki|üç|dört|beş|altı|yedi|sekiz|dokuz|on|yirmi|otuz|kırk|elli|altmış|yetmiş|seksen|doksan|yüz|bin|milyon|milyar+)|(\d+)";
            string output = String.Empty;
            long sum = 0;
            long digitGroupSum = 0;

            try
            {
                // match and tokenize input, remove whitespace artefacts from regex splitting
                var split = Regex.Split(input.Trim(), pattern).Where(t => !String.IsNullOrWhiteSpace(t)).ToArray();

                foreach (string token in split)
                {
                    // enumerate on tokenized word/numbers
                    if (!Regex.Match(token, pattern).Success)
                    {
                        // not digits or number word, just other text
                        // don't touch, add to output as is and continue

                        if (digitGroupSum > 0) sum += digitGroupSum;
                        if (sum > 0) output += sum.ToString("N0");
                        output += token;
                        sum = digitGroupSum = 0;
                        continue;
                    }

                    // token is a pattern matched & valid number as either digits or word
                    // get int value or parse as int and start adding
                    if (int.TryParse(token, out int num) || numberWords.TryGetValue(token.ToLower(), out num))
                    {
                        if (digitGroupSum > 0)
                        {
                            switch (num)
                            {
                                case 1_000_000_000:
                                    digitGroupSum *= 1_000_000_000;
                                    break;
                                case 1_000_000:
                                    digitGroupSum *= 1_000_000;
                                    break;
                                case 1_000:
                                    digitGroupSum *= 1_000;
                                    break;
                                case 100:
                                    digitGroupSum *= 100;
                                    break;
                                default:
                                    digitGroupSum += num;
                                    continue;
                            }
                        }
                        else
                        {
                            digitGroupSum += num;
                            continue;
                        }

                    }

                    if (digitGroupSum > 0) sum += digitGroupSum;
                    digitGroupSum = 0;
                }

                // input ends with number
                if (digitGroupSum > 0) sum += digitGroupSum;
                if (sum > 0) output += sum.ToString("N0");
            }
            catch (ArgumentNullException)
            {
                return new UserOutputModel(output, "Argument Null");
            }
            catch (NullReferenceException)
            {
                return new UserOutputModel(output, "Null Reference");
            }

            return new UserOutputModel(output);
        }
    }
}
