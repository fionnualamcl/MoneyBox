using System;
using System.Collections.Generic;
using System.Text;

namespace Moneybox.App.Domain
{
    public class Address
    {
        private string _AddressLine1;
        private string _AddressLine2;
        private string _AddressLine3;
        private string _City;
        private string _Region;
        private string _Country;
        private string _PostCode;

        public string AddressLine1 { get { return this._AddressLine1; } }

        public string AddressLine2 { get { return this._AddressLine2; } }

        public string AddressLine3 { get { return this._AddressLine3; } }

        public string City { get { return this._City; } }

        public string Region { get { return this._Region; } }

        public string Country { get { return this._Country; } }

        public string PostCode { get { return this._PostCode; } }

        public Address(string addressLine1, string addressLine2, string addressLine3,
                        string city, string region, string country, string postCode)
        {
            this._AddressLine1 = addressLine1;
            this._AddressLine2 = addressLine2;
            this._AddressLine3 = addressLine3;
            this._City = city;
            this._Region = region;
            this._Country = country;
            this._PostCode = postCode;
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(this._AddressLine1) ||
                string.IsNullOrWhiteSpace(this._AddressLine2) ||
                string.IsNullOrWhiteSpace(this._AddressLine3) ||
                string.IsNullOrWhiteSpace(this._City) ||
                string.IsNullOrWhiteSpace(this._Region) ||
                string.IsNullOrWhiteSpace(this._Country) ||
                string.IsNullOrWhiteSpace(this._PostCode))
            {
                throw new InvalidOperationException("Invalid address");
            }
        }
    }
}
