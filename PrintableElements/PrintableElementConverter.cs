using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlakietkUJ.PrintableElements
{
    public class PrintableElementConverter : JsonConverter<PrintableElement>
    {
        public override PrintableElement ReadJson(JsonReader reader, Type objectType, PrintableElement existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);

            // Check the type property in the JSON object
            if (jsonObject.TryGetValue("Type", out JToken typeToken))
            {
                string elementType = typeToken.Value<string>();
                switch (elementType)
                {
                    case "TextField":
                        return jsonObject.ToObject<TextField>(serializer);
                    case "ImageElement":
                        return jsonObject.ToObject<ImageElement>(serializer);
                    // Add more cases for other derived types if needed
                }
            }

            // Default to PrintableElement if type information is missing
            return jsonObject.ToObject<PrintableElement>(serializer);
        }

        public override void WriteJson(JsonWriter writer, PrintableElement value, JsonSerializer serializer)
        {
            throw new NotImplementedException(); // Optional: Implement this if needed for serialization
        }
    }
}
