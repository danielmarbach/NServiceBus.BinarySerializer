using System;
using NServiceBus.MessageInterfaces;
using NServiceBus.Serialization;
using NServiceBus.Settings;

namespace NServiceBus
{
    /// <summary>
    /// Enables Binary serialization.
    /// </summary>
    public class BinaryFormatterSerializer : SerializationDefinition
    {
        /// <summary>
        /// Provides a factory method for building a message serializer.
        /// </summary>
        public override Func<IMessageMapper, IMessageSerializer> Configure(ReadOnlySettings settings)
        {
            return mapper => new BinaryMessageSerializer();
        }
    }
}