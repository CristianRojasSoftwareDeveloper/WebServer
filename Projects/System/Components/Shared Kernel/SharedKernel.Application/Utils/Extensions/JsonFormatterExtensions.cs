using System.Text.Json;
using System.Text.Json.Serialization;

namespace SharedKernel.Application.Utils.Extensions {

    public static class JsonFormatterExtensions {

        public static string Serialize (this object complexObject) {

            var serializerOptions = new JsonSerializerOptions {
                WriteIndented = true,
                ReferenceHandler = ReferenceHandler.Preserve
            };

            string serializedComplexObject = JsonSerializer.Serialize(complexObject, serializerOptions);

            return serializedComplexObject;

        }

    }

}