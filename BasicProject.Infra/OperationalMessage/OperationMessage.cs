using Newtonsoft.Json;

namespace BasicProject.Infra.OperationalMessage
{
    public class OperationMessage
    {
        public OperationMessage()
        {

        }

        public OperationMessage(OperationMessageTypes type, string description)
        {
            this.Description = description;
            this.Type = type;
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string StackTrace { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public OperationMessageTypes Type { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object Data { get; set; }
    }
}
