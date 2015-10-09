using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorwegianVehicleNet.Car
{
    class Car
    {
        public enum FuelTypes { Unknown, Gasoline, Diesel, Electricity };

        public string Merke { get; set; }
        public string Modell { get; set; }
        public string Brukstypegruppe { get; set; }
        public string Brukstypeikon { get; set; }
        public string Farge { get; set; }
        public string Kjoretoysklasse { get; set; }
        public int Seter { get; set; }
        public int Staaplasser { get; set; }
        public FuelTypes DrivstoffType { get; set; }
        public Engine Motor { get; set; }
        public int AkslerMedDrift { get; set; }
        public int EgenvektMedForer { get; set; }
        public int TilhengervektMedBrems { get; set; }
        public int TilhengervektUtenBrems { get; set; }
        public int TilhengervektKopling { get; set; }
        public int Vogntogvekt { get; set; }
        public int Taklast { get; set; }
        public RegistrationNumber Registreringsnummer { get; set; }
        public string Understellsnummer { get; set; }
    }
}
