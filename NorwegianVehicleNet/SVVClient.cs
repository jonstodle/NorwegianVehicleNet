using NorwegianVehicleNet.Vehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NorwegianVehicleNet
{
	public sealed class SvvClient
	{
		private const string SvvBaseUriString = "http://www.vegvesen.no/system/mobilapi?registreringsnummer=";

		public async Task<Car> GetCarAsync(RegistrationNumber registrationNumber)
		{
			var xDoc = await GetXmlDocumentAsync(registrationNumber);

			var returnCar = new Car()
			{
				Registreringsnummer = registrationNumber,
				Motor = new Engine()
			};
			foreach (var i in xDoc.Descendants())
			{
				switch (i.Name.LocalName)
				{
					case "tidspunkt":
						var tidspunktString = i.Value.Split('.');
						if(tidspunktString.Length == 3) returnCar.Tidspunkt = new DateTime(int.Parse(tidspunktString[2]), int.Parse(tidspunktString[1]), int.Parse(tidspunktString[0]));
						break;
					case "nesteOppdatering":
						var nesteOppdateringString = i.Value.Split('.');
						if (nesteOppdateringString.Length == 3) returnCar.NesteOppdatering = new DateTime(int.Parse(nesteOppdateringString[2]), int.Parse(nesteOppdateringString[1]), int.Parse(nesteOppdateringString[0]));
						break;
					case "merke":
						returnCar.Merke = i.Value;
						break;
					case "modell":
						returnCar.Modell = i.Value;
						break;
					case "type":
						returnCar.Type = i.Value;
						break;
					case "gruppe":
						returnCar.Brukstypegruppe = i.Value;
						break;
					case "ikon":
						returnCar.Brukstypeikon = i.Value.Split(',').Last();
						break;
					case "farge":
						returnCar.Farge = i.Value;
						break;
					case "kanBeregnes":
						returnCar.Kjoretoysklasse = Regex.Match(i.Elements().First().Name.LocalName, "[A-Z]+").Value;
						break;
					case "seter":
						int seterCount;
						if (int.TryParse(i.Value, out seterCount)) returnCar.Seter = seterCount;
						break;
					case "staaplasser":
						int staaplasserCount;
						if (int.TryParse(i.Value, out staaplasserCount)) returnCar.Staaplasser = staaplasserCount;
						break;
					case "drivstofftype":
						returnCar.DrivstoffType = i.Value;
						break;
					case "slagvolum":
						var slagvolumElements = i.Elements();
						int slagvolumCount, literCount;
						if(int.TryParse(slagvolumElements.First().Value, out slagvolumCount)) returnCar.Motor.SlagVolum = slagvolumCount;
						if(int.TryParse(slagvolumElements.Last().Value, out literCount)) returnCar.Motor.Liter = literCount;
						break;
					case "motorytelse":
						foreach (var j in i.Elements())
						{
							switch (j.Name.LocalName)
							{
								case "oppgitt":
									int motorYtelseCount;
                                    if(int.TryParse(j.Value, out motorYtelseCount)) returnCar.Motor.MotorYtelse = motorYtelseCount;
									break;
								case "oppgittBenevning":
									returnCar.Motor.MotorYtelseBenevning = j.Value;
									break;
								case "hestekrefter":
									int hesteKrefterCount;
                                    if(int.TryParse(j.Value, out hesteKrefterCount)) returnCar.Motor.HesteKrefter = hesteKrefterCount;
									break;
								default:
									break;
							}
						}
						break;
					case "akslerMedDrift":
						int akslerMedDriftCount;
                        if (int.TryParse(i.Value, out akslerMedDriftCount)) returnCar.AkslerMedDrift = akslerMedDriftCount;
						break;
					case "egenvektMedForer":
						int egenvektMedForerCount;
                        if (int.TryParse(i.Value, out egenvektMedForerCount)) returnCar.EgenvektMedForer = egenvektMedForerCount;
						break;
					case "tilhengervektMBrems":
						int tilhengervektMedBremsCount;
						if (int.TryParse(i.Value, out tilhengervektMedBremsCount)) returnCar.TilhengervektMedBrems = tilhengervektMedBremsCount;
						break;
					case "tilhengervektUBrems":
						int tilhengervektUtenBremsCount;
                        if (int.TryParse(i.Value, out tilhengervektUtenBremsCount)) returnCar.TilhengervektUtenBrems = tilhengervektUtenBremsCount;
						break;
					case "tilhengervektKopl":
						int tilhengervektKoplingCount;
						if (int.TryParse(i.Value, out tilhengervektKoplingCount)) returnCar.TilhengervektKopling = tilhengervektKoplingCount;
						break;
					case "vogntogvekt":
						int vogntogvektCount;
						if (int.TryParse(i.Value, out vogntogvektCount)) returnCar.Vogntogvekt = vogntogvektCount;
						break;
					case "taklast":
						int taklastCount;
						if (int.TryParse(i.Value, out taklastCount)) returnCar.Taklast = taklastCount;
						break;
					case "understellsnr":
						returnCar.Understellsnummer = i.Value;
						break;
					case "registreringsaar":
						int registreringsaarCount;
						if (int.TryParse(i.Value, out registreringsaarCount)) returnCar.Registreringsaar = registreringsaarCount;
						break;
					case "forstegangsreg":
						var forstegangsregString = i.Value.Split('.');
						if (forstegangsregString.Length == 3) returnCar.ForsteGangsRegistrering = new DateTime(int.Parse(forstegangsregString[2]), int.Parse(forstegangsregString[1]), int.Parse(forstegangsregString[0]));
						break;
					case "registrertEierDato":
						var registrertEierDatoString = i.Value.Split('.');
						if (registrertEierDatoString.Length == 3) returnCar.ForsteGangsRegistrering = new DateTime(int.Parse(registrertEierDatoString[2]), int.Parse(registrertEierDatoString[1]), int.Parse(registrertEierDatoString[0]));
						break;
					case "registrertDistrikt":
						returnCar.RegistrertDistrikt = i.Value;
						break;
					case "avregistrertDato":
						var avregistrertDatoString = i.Value.Split('.');
						if (avregistrertDatoString.Length == 3) returnCar.ForsteGangsRegistrering = new DateTime(int.Parse(avregistrertDatoString[2]), int.Parse(avregistrertDatoString[1]), int.Parse(avregistrertDatoString[0]));
						break;
					case "egenvekt":
						int egenvektCount;
						if (int.TryParse(i.Value, out egenvektCount)) returnCar.Egenvekt = egenvektCount;
						break;
					case "totalvekt":
						int totalvektCount;
						if (int.TryParse(i.Value, out totalvektCount)) returnCar.Totalvekt = totalvektCount;
						break;
					case "lengde":
						int lengdeCount;
						if (int.TryParse(i.Value, out lengdeCount)) returnCar.Lengde = lengdeCount;
						break;
					case "bredde":
						int breddeCount;
						if (int.TryParse(i.Value, out breddeCount)) returnCar.Bredde = breddeCount;
						break;
					case "nyttelast":
						int nyttelastCount;
						if (int.TryParse(i.Value, out nyttelastCount)) returnCar.Nyttelast = nyttelastCount;
						break;
					case "akseltrykkForan":
						int akseltrykkForanCount;
						if (int.TryParse(i.Value, out akseltrykkForanCount)) returnCar.AkseltrykkForan = akseltrykkForanCount;
						break;
					case "akseltrykkBak":
						int akseltrykkBakCount;
						if (int.TryParse(i.Value, out akseltrykkBakCount)) returnCar.AkseltrykkBak = akseltrykkBakCount;
						break;
					case "dekkdimensjonForan":
						returnCar.DekkdimensjonForan = i.Value;
						break;
					case "dekkdimensjonBak":
						returnCar.DekkdimensjonBak = i.Value;
						break;
					case "hastighetsindeksForan":
						returnCar.HastighetsindeksForan = i.Value;
						break;
					case "hastighetsindeksBak":
						returnCar.HastighetsindeksBak = i.Value;
						break;
					case "lastindeksForan":
						int lastindeksForanCount;
						if (int.TryParse(i.Value, out lastindeksForanCount)) returnCar.LastindeksForan = lastindeksForanCount;
						break;
					case "lastindeksBak":
						int lastindeksBakCount;
						if (int.TryParse(i.Value, out lastindeksBakCount)) returnCar.LastindeksBak = lastindeksBakCount;
						break;
					case "innpressForan":
						returnCar.InnpressForan = i.Value;
						break;
					case "innpressBak":
						returnCar.InnpressBak = i.Value;
						break;
					case "antallAksler":
						int antallAkslerCount;
						if (int.TryParse(i.Value, out antallAkslerCount)) returnCar.AntallAksler = antallAkslerCount;
						break;
					case "eukontrollfrist":
						var eukontrollfristString = i.Value.Split('.');
						if (eukontrollfristString.Length == 3) returnCar.EuKontrollfrist = new DateTime(int.Parse(eukontrollfristString[2]), int.Parse(eukontrollfristString[1]), int.Parse(eukontrollfristString[0]));
						break;
					case "eukontrollSist":
						var eukontrollsistString = i.Value.Split('.');
						if (eukontrollsistString.Length == 3) returnCar.EuKontrollSist = new DateTime(int.Parse(eukontrollsistString[2]), int.Parse(eukontrollsistString[1]), int.Parse(eukontrollsistString[0]));
						break;
                    default:
						break;
				}
			}
			return returnCar;
		}

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
