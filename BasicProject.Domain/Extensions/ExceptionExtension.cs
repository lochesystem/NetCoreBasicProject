using System;

namespace BasicProject.Domain.Extensions
{
    public class ExceptionExtensions
    {

        public static string PrepareExceptionString(Exception ex)
        {

            string exceptionMessage = string.Format("{0}\r\n\t{1}\r\n---------------------------\r\n", ex.Message, ex);

            if (ex.InnerException != null)
                exceptionMessage += string.Format("\t{0}", PrepareExceptionString(ex.InnerException));

            return exceptionMessage;

        }

    }
}
