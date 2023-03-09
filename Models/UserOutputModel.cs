namespace TextToNumberNLP.Models
{
    public class UserOutputModel
    {
        public bool Success { get; set; }
        public string Output { get; set; }
        public string? ErrorMessage { get; set; }

        public UserOutputModel(string successfulOutput)
        {
            Success = true;
            Output = successfulOutput;
        }

        public UserOutputModel(string failedOutput, string errorMessage)
        {
            Success = false;
            Output = failedOutput;
            ErrorMessage = errorMessage;
        }
    }
}