namespace MC.eSIS.Core.Classes
{
    public class GeneralError
    {
        public string Message { get; set; }

        public GeneralError(string message)
        {
            this.Message = message;
        }
    }
}