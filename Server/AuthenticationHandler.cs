using System;
using NServiceBus;

namespace Server
{
    public class AuthenticationHandler : IHandleMessages<IMessage>
    {
        public IBus Bus { get; set; }

        public void Handle(IMessage message)
        {
            var token = Bus.GetMessageHeader(message, "access_token");

            if (token == "my_little_secret")
            {
                Console.WriteLine("User authenticated");
            }
            else
            {
                Console.WriteLine("User not authenticated");
                Bus.DoNotContinueDispatchingCurrentMessageToHandlers();
            }
        }
    }
}
