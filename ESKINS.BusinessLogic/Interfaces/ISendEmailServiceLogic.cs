namespace ESKINS.BusinessLogic.Interfaces
{
    public interface ISendEmailServiceLogic
    {
        /// <summary>
        /// Sending the email to given parameters.
        /// </summary>
        /// <param name="recipients"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="cc"></param>
        /// <returns></returns>
        public bool SendMessage(string[] recipients, string subject, string body, string[] cc = null);
    }
}
