using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ExpenseManagement.Web.Utility
{
    public class JsonUtility
    {
        public JsonSerializerSettings ConfigureJSon()
        {
            var toReturn = new JsonSerializerSettings();
            toReturn.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            toReturn.ContractResolver = new DefaultContractResolver()
            {
                IgnoreSerializableInterface = true,
                IgnoreSerializableAttribute = true
            };
            return toReturn;
        }
    }
}
