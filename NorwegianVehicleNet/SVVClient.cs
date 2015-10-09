using NorwegianVehicleNet.Car;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NorwegianVehicleNet
{
    public sealed class SVVClient
    {
        private const string SvvBaseUriString = "http://www.vegvesen.no/system/mobilapi?registreringsnummer=";

        /// <summary>
        /// Gets the xml representation of a car
        /// </summary>
        /// <param name="registrationNumber">The registration number of the car to get</param>
        /// <returns>An XDocument representing the car</returns>
        private async Task<XDocument> GetXmlDocumentAsync(RegistrationNumber registrationNumber)
        {
            var client = new HttpClient();
            var xmlString = await client.GetStringAsync(SvvBaseUriString + registrationNumber);
            return XDocument.Parse(xmlString);
        }
    }
}
