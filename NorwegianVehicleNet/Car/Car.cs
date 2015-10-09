using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorwegianVehicleNet.Car
{
    class Car
    {
        public enum FuelTypes { Gasoline, Diesel, Electricity };

        public string Merke { get; set; }
        public string Modell { get; set; }
        public string Brukstypegruppe { get; set; }
        public string Brukstypeikon { get; set; }
        public string Farge { get; set; }
        public string Kjoretoysklasse { get; set; }
        public int? Seter { get; set; }
        public int? Staaplasser { get; set; }
        public FuelTypes? DrivstoffType { get; set; }
        public Engine Motor { get; set; }
        public int? AkslerMedDrift { get; set; }
        public int? EgenvektMedForer { get; set; }
        public int? TilhengervektMedBrems { get; set; }
        public int? TilhengervektUtenBrems { get; set; }
        public int? TilhengervektKopling { get; set; }
        public int? Vogntogvekt { get; set; }
        public int? Taklast { get; set; }
        public RegistrationNumber Registreringsnummer { get; set; }
        public string Understellsnummer { get; set; }
		public int? Registreringsaar { get; set; }
		public DateTime? ForsteGangsRegistrering { get; set; }
		public DateTime? RegistrertEierDato { get; set; }
		public string RegistrertDistrikt { get; set; }
		public DateTime? AvregistrertDato { get; set; }
		public int? Egenvekt { get; set; }
		public int? Totalvekt { get; set; }
		public int? Lengde { get; set; }
		public int? Bredde { get; set; }
		public int? Nyttelast { get; set; }
		public int? AkseltrykkForan { get; set; }
		public int? AkseltrykkBak { get; set; }
		public string DekkdimensjonForan { get; set; }
		public string DekkdimensjonBak { get; set; }
		public string HastighetsindeksForan { get; set; }
		public string HastighetsindeksBak { get; set; }
		public int? LastindeksForan { get; set; }
		public int? LastindeksBak { get; set; }
		public string InnpressForan { get; set; }
		public string InnpressBak { get; set; }
		public int? AntallAksler { get; set; }
		public DateTime? EuKontrollfrist { get; set; }
		public DateTime? EuKontrollSist { get; set; }
	}
}
