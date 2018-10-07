using Ionic.Zip;
using Service.ServiceModel.DOTWModels;
using Service.Suppliers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Logic.BusinessObjects
{
    public class DOTWBusiness
    {
        public MyFile GetInternalCodes(Command command)
        {
            try
            {
                var response = DOTWAccess.GetInternalCodes(command);
                if (response != null)
                {
                    return new MyFile
                    {
                        Name = command.ToString(),
                        Extention = ".xml",
                        Contents = Encoding.UTF8.GetBytes(response)
                    };
                }
                return null;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public resultCitiesCity[] GetAllCities()
        {
            try
            {
                var file = GetInternalCodes(Command.getallcities);
                XmlSerializer serializer = new XmlSerializer(typeof(result));
                StringReader reader = new StringReader(Encoding.UTF8.GetString(file.Contents));
                var model = (result)serializer.Deserialize(reader);
                return model.cities.city;
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        public MyFile GetHotelsByCity(int code, string name)
        {
            try
            {
                var response = DOTWAccess.SearchHotelByCity(code);
                if (response != null)
                {
                    return new MyFile
                    {
                        Name = name,
                        Extention = ".xml",
                        Contents = Encoding.UTF8.GetBytes(response)
                    };
                }
                return null;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public (byte[] outputFile, List<SearchHotelByCityViewModel> logResult) GetHotelsByAllCities()
        {
            try
            {
                var output = new MemoryStream();
                var result = new ConcurrentBag<SearchHotelByCityViewModel>();
                //var cities = GetAllCities();
                var testCity = new resultCitiesCity[]
                {
                    new resultCitiesCity {name = "Dubai",code=364 },
                    //new resultCitiesCity {name = "ABANT",code=14104},
                    //new resultCitiesCity {name = "Abashiri",code=194748},
                    //new resultCitiesCity {name = "ABBARETZ",code=272465},
                    //new resultCitiesCity {name = "ABBERLEY",code=179868},
                };
                using (var zip = new ZipFile())
                {
                    Parallel.ForEach(testCity, city =>
                    {
                        try
                        {
                            var file = GetHotelsByCity(city.code, city.name);
                            zip.AddEntry($"{file.Name}{file.Extention}", file.Contents);
                            result.Add(new SearchHotelByCityViewModel
                            {
                                CityCode = city.name,
                                Success = true,
                            });
                        }
                        catch (System.Exception ex)
                        {
                            result.Add(new SearchHotelByCityViewModel
                            {
                                Message = ex.Message,
                                CityCode = city.name,
                                Success = false,
                            });
                        }
                    });
                    zip.Save(output);
                }
                return (output.ToArray(), result.ToList());
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        public byte[] GetZipFile(List<MyFile> files)
        {
            try
            {
                var output = new MemoryStream();
                using (var zip = new ZipFile())
                {
                    Parallel.ForEach(files, file =>
                    {
                        zip.AddEntry($"{file.Name}{file.Extention}", file.Contents);
                    });
                    zip.Save(output);
                }
                return output.ToArray();
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
