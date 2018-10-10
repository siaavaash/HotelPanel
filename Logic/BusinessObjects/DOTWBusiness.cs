using Ionic.Zip;
using Service.ServiceModel.DOTWModels;
using Service.Suppliers;
using System;
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
        private static resultCitiesCity[] allCities;
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
        public async Task<MyFile> GetHotelsByCity(int code, string name)
        {
            try
            {
                var response = await DOTWAccess.SearchHotelByCityAsync(code);
                if (response != null)
                {
                    return new MyFile
                    {
                        Name = $"{name}_{code}",
                        Extention = ".xml",
                        Contents = Encoding.UTF8.GetBytes(response)
                    };
                }
                return new MyFile
                {
                    Name = $"{name}_{code}",
                    Extention = ".xml",
                    Contents = new byte[0],
                };
            }
            catch (Exception)
            {
                try
                {
                    var response = await DOTWAccess.SearchHotelByCityAsync(code);
                    if (response != null)
                    {
                        return new MyFile
                        {
                            Name = $"{name}_{code}",
                            Extention = ".xml",
                            Contents = Encoding.UTF8.GetBytes(response)
                        };
                    }
                    return new MyFile
                    {
                        Name = $"{name}_{code}",
                        Extention = ".xml",
                        Contents = new byte[0],
                    };
                }
                catch (Exception ex)
                {
                    return new MyFile
                    {
                        Name = $"{name}_{code}",
                        Extention = ".txt",
                        Contents = Encoding.UTF8.GetBytes(ex.Message),
                    };
                }
            }
        }

        public async Task<byte[]> GetHotelsByAllCitiesAsync(byte part)
        {
            try
            {
                var output = new MemoryStream();
                if (allCities == null) allCities = GetAllCities();
                int size = Convert.ToInt32(Math.Ceiling(allCities.Count() / 10d));
                //var cities = new List<resultCitiesCity>
                //{
                //    new resultCitiesCity
                //    {
                //        name = "city",
                //        code=87976
                //    },
                //    new resultCitiesCity
                //    {
                //        name = "city2",
                //        code=68314
                //    },
                //    new resultCitiesCity
                //    {
                //        name = "city3",
                //        code=253208
                //    },
                //    new resultCitiesCity
                //    {
                //        name = "city4",
                //        code=258338
                //    },
                //};

                var cities = allCities.OrderBy(x => x.code).Skip((part - 1) * size).Take(size).ToArray();
                using (var zip = new ZipFile())
                {
                    var tasks = cities.Select(city => GetHotelsByCity(city.code, city.name));
                    var files = await Task.WhenAll(tasks);
                    foreach (var file in files)
                        zip.AddEntry($"{file.Name}{file.Extention}", file.Contents);
                    zip.Save(output);
                }
                return output.ToArray();
            }
            catch (Exception)
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
