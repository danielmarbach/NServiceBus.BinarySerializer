using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Linq;
using NServiceBus.Serialization;

namespace NServiceBus
{
    class BinaryMessageSerializer : IMessageSerializer
    {
        public BinaryMessageSerializer()
        {
            var surrogateSelector = new SurrogateSelector();
            surrogateSelector.AddSurrogate(typeof(XDocument), new StreamingContext(StreamingContextStates.All), new XContainerSurrogate());
            surrogateSelector.AddSurrogate(typeof(XElement), new StreamingContext(StreamingContextStates.All), new XElementSurrogate());

            binaryFormatter.SurrogateSelector = surrogateSelector;
        }

        public void Serialize(object message, Stream stream)
        {
            binaryFormatter.Serialize(stream, new List<object> { message });
        }

        /// <summary>
        /// Deserializes from the given stream a set of messages.
        /// </summary>
        /// <param name="stream">Stream that contains messages.</param>
        /// <param name="messageTypes">The list of message types to deserialize. If null the types must be inferred from the serialized data.</param>
        /// <returns>Deserialized messages.</returns>
        public object[] Deserialize(Stream stream, IList<Type> messageTypes = null)
        {
            if (stream == null)
                return null;

            var body = binaryFormatter.Deserialize(stream) as List<object>;

            if (body == null)
                return null;

            var result = new object[body.Count];

            var i = 0;
            foreach (var m in body)
                result[i++] = m;

            return result;
        }

        /// <summary>
        /// Gets the content type into which this serializer serializes the content to 
        /// </summary>
        public string ContentType { get { return "application/binary"; } }

        readonly BinaryFormatter binaryFormatter = new BinaryFormatter();
    }
}