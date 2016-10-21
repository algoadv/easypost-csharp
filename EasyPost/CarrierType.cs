using System.Collections.Generic;

namespace EasyPost {
    public class CarrierType : Resource {
        public string type { get; set; }
        public string readable { get; set; }
        public string logo { get; set; }
        public Dictionary<string, object> fields { get; set; }

        public static List<CarrierType> All(ClientConfiguration clientConfiguration = null) {
            Request request = new Request("carrier_types", clientConfiguration);
            return request.Execute<List<CarrierType>>();
        }
    }
}