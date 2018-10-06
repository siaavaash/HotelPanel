using Service.ServiceModel.DOTWModels;
using Service.Suppliers;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Logic.BusinessObjects
{
    public class DOTWBusiness
    {
        public bool GetInternalCodes(Command command, string path)
        {
            try
            {
                if (DOTWAccess.GetInternalCodes(command, out string result))
                {
                    File.WriteAllText(path, result);
                    return true;
                }
                return false;
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        public bool GetHotelsByCity(int code, string path)
        {
            try
            {
                if (DOTWAccess.SearchHotelByCity(code, out string result))
                {
                    File.WriteAllText(path, result);
                    return true;
                }
                return false;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public List<SearchHotelByCityViewModel> GetHotelsByAllCities(string inPath, string outPath)
        {
            try
            {
                var result = new List<SearchHotelByCityViewModel>();
                XmlReader reader = XmlReader.Create(inPath);
                XmlSerializer serializer = new XmlSerializer(typeof(result));
                if (serializer.CanDeserialize(reader))
                {
                    var response = (result)serializer.Deserialize(reader);
                    if (response.successful == "FALSE") return new List<SearchHotelByCityViewModel>();
                    var cities = ((result)serializer.Deserialize(reader)).cities.city;
                    Parallel.ForEach(cities, city =>
                    {
                        var success = GetHotelsByCity(city.code, outPath + city.name + ".xml");
                        result.Add(new SearchHotelByCityViewModel
                        {
                            CityCode = city.code.ToString(),
                            Success = success,
                        });
                    });
                }
                else
                    throw new System.Exception("The File is incorrect format.");
                return result;

            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
