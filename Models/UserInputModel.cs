namespace TextToNumberNLP.Models
{
    public class UserInputModel
    {
        public string UserText { get; set; }

        public UserInputModel(string userText)
        {
            UserText = userText;
        }
    }
}