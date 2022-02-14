#nullable enable
using System;
namespace UniversityProject.Services
{
    public class MyEmailSenderException
        : ApplicationException
    {
        private const string StandardERRORMESSAGE
            = "Something went wrong while sending mail";

        public MyEmailSenderException()
            : base(StandardERRORMESSAGE)
        {

        }

        public MyEmailSenderException(string message)
            :base(message)
        {

        }
        public MyEmailSenderException(string message,Exception innerexception)
            :base(message, innerexception)
        {

        }

    }
}
