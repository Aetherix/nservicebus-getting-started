using NServiceBus;
using NLog;

namespace Server
{
    public class AuthenticationHandler : IHandleMessages<IMessage>
    {
        private static readonly Logger s_log = LogManager.GetCurrentClassLogger();

        public IBus Bus { get; set; }

        public void Handle(IMessage message)
        {
            var token = Bus.GetMessageHeader(message, "access_token");

            if (token == "my_little_secret")
            {
                s_log.Info("User authenticated");
            }
            else
            {
                s_log.Info("User not authenticated");
                Bus.DoNotContinueDispatchingCurrentMessageToHandlers();
            }
        }
    }
}
