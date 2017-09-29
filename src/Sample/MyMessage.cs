using System;
using NServiceBus;

[Serializable]
class MyMessage:IMessage
{
    public DateTime DateSend { get; set; }
}
