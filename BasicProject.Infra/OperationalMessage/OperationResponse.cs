using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace BasicProject.Infra.OperationalMessage
{
    public class OperationResponse<T>
    {
        public OperationResponse()
        {
            if (typeof(T) == typeof(string))
                Data = default(T);
            else
                Data = System.Activator.CreateInstance<T>();

            Messages = new List<OperationMessage>();
        }

        public bool IsSucceed { get { return Messages.All(p => p.Type != OperationMessageTypes.Error); } }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public T Data { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<OperationMessage> Messages { get; set; }

        public int Count { get; set; }

        public OperationResponse<T> AddMessage(IList<OperationMessage> messages)
        {
            Messages.AddRange(messages);
            return this;
        }

        public OperationResponse<T> AddMessage(OperationMessage message)
        {
            Messages.Add(message);
            return this;
        }

        public OperationResponse<T> AddMessage(OperationMessageTypes type, string description, params object[] descriptionParams)
        {
            Messages.Add(new OperationMessage { Type = type, Description = description });

            foreach (var item in descriptionParams)
            {
                Messages.Add(new OperationMessage { Type = type, Description = item as string });
            }
            return this;
        }

        public OperationResponse<T> ClearRepeatedMessages()
        {
            Messages = GroupBy().ToList();
            return this;
        }

        private IEnumerable<OperationMessage> GroupBy()
        {
            var queryMessage = (from message in Messages
                                group message by message.Description
                                  into newMessage
                                orderby newMessage.Key
                                select newMessage);
            foreach (var item in queryMessage)
            {
                yield return Messages.FirstOrDefault(p => p.Description == item.Key);
            }
        }
    }
}
