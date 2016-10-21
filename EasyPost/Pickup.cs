﻿using RestSharp;

using System;
using System.Collections.Generic;

namespace EasyPost {
    public class Pickup : Resource {
        public string id { get; set; }
        public string mode { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public string status { get; set; }
        public string name { get; set; }
        public string reference { get; set; }
        public DateTime min_datetime { get; set; }
        public DateTime max_datetime { get; set; }
        public bool is_account_address { get; set; }
        public string instructions { get; set; }
        public List<string> messages { get; set; }
        public string confirmation { get; set; }
        public Address address { get; set; }
        public List<CarrierAccount> carrier_accounts { get; set; }
        public List<Rate> pickup_rates { get; set; }

        /// <summary>
        /// Retrieve a Pickup from its id.
        /// </summary>
        /// <param name="id">String representing a Pickup. Starts with "pickup_".</param>
        /// <returns>EasyPost.Pickup instance.</returns>
        public static Pickup Retrieve(string id,ClientConfiguration clientConfiguration =null) {
            Request request = new Request("pickups/{id}", clientConfiguration);
            request.AddUrlSegment("id", id);

            return request.Execute<Pickup>();
        }
        /// <summary>
        /// Create a Pickup.
        /// </summary>
        /// <param name="parameters">
        /// Optional dictionary containing parameters to create the batch with. Valid pairs:
        ///   * {"is_account_address", bool}
        ///   * {"min_datetime", DateTime}
        ///   * {"max_datetime", DateTime}
        ///   * {"reference", string}
        ///   * {"instructions", string}
        ///   * {"carrier_accounts", List<CarrierAccount>}
        ///   * {"address", Address}
        ///   * {"shipment", Shipment}
        ///   * {"batch", Batch}
        /// All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Pickup instance.</returns>
        public static Pickup Create(Dictionary<string, object> parameters = null,ClientConfiguration clientConfiguration = null) {
            return sendCreate(parameters ?? new Dictionary<string, object>(), clientConfiguration);
        }

        /// <summary>
        /// Create this Pickup.
        /// </summary>
        /// <exception cref="ResourceAlreadyCreated">Pickup already has an id.</exception>
        public void Create() {
            if (id != null)
                throw new ResourceAlreadyCreated();
            Merge(sendCreate(this.AsDictionary(), clientConfiguration));
        }

        private static Pickup sendCreate(Dictionary<string, object> parameters, ClientConfiguration clientConfiguration) {
            Request request = new Request("pickups", clientConfiguration,Method.POST);
            request.AddBody(parameters, "pickup");

            return request.Execute<Pickup>();
        }

        /// <summary>
        /// Purchase this pickup.
        /// </summary>
        /// <param name="carrier">The name of the carrier to purchase with.</param>
        /// <param name="service">The name of the service to purchase.</param>
        public void Buy(string carrier, string service) {
            Request request = new Request("pickups/{id}/buy", clientConfiguration, Method.POST);
            request.AddUrlSegment("id", id);
            request.AddBody(new List<Tuple<string, string>>() {
                new Tuple<string, string>("carrier", carrier),
                new Tuple<string, string>("service", service)
            });

            Merge(request.Execute<Pickup>());
        }

        /// <summary>
        /// Cancel this pickup.
        /// </summary>
        public void Cancel() {
            Request request = new Request("pickups/{id}/cancel", clientConfiguration, Method.POST);
            request.AddUrlSegment("id", id);

            Merge(request.Execute<Pickup>());
        }
    }
}
