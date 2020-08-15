using Compar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
namespace Compar
{
    public class CompareService : ICompareService
    {
        private HttpClient Client { get; }

        public CompareService()
        {
            Client = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(1020)
            };
        }

        public const string BaseUrlString = "https://725a065c-8a29-47bc-92d9-b30864f9f852.mock.pstmn.io/";
        public async Task<GenericResponse<Login>> AdminLogin(Login Input)
        {
            try
            {
                var address = BaseUrlString + "Login";

                var json = JsonConvert.SerializeObject(Input);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                using (var response = await Client.PostAsync(address, content))
                {
                    var strResult = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        
                        var e= JsonConvert.DeserializeObject<GenericResponse<Login>>(strResult);
                        e.Data.Email = Input.Email;
                        e.Data.Password = Input.Password;

                        return e;
                    }
                    else
                    {
                        return null;
                    }

                }
            }
            catch (Exception e)
            {
                return new GenericResponse<Login>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false

                };
            }
        }

        public async Task<GenericResponse<Compare>> CompareInput(Compare Input)
        {
            try
            {
                var address = BaseUrlString + "Compare";

                var json = JsonConvert.SerializeObject(Input);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                using (var response = await Client.PostAsync(address, content))
                {
                    var strResult = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {

                        var e = JsonConvert.DeserializeObject<GenericResponse<Compare>>(strResult);
                        e.Data.FirstStudentName = Input.FirstStudentName;
                        e.Data.FirstStudentFile = Input.FirstStudentFile;
                        e.Data.SecondStudentName = Input.SecondStudentName;
                        e.Data.SecondStudentFile = Input.SecondStudentFile;
                        return e;
                    }
                    else
                    {
                        return null;
                    }

                }
            }
            catch (Exception e)
            {
                return new GenericResponse<Compare>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false

                };
            }
        }
    }
}
