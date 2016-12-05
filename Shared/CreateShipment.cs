using NServiceBus;
using System;

namespace Shared
{
    public class CreateShipment : ICommand
    {
        public Guid OrderId { get; set; }
    }
}
