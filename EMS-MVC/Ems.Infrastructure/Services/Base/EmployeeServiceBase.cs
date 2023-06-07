using EMS.Application.Contract;
using EMS.Application.ViewModels;
using EMS.Core.Common;
using EMS.Repository.EF;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Infrastructure.Services.Base
{
    public class EmployeeServiceBase
    {
        private static HttpClient _httpClient;

        static EmployeeServiceBase()
        {
            EmployeeServiceBase._httpClient = new HttpClient
            {
                BaseAddress = new Uri(CommonSettings.EmployeeBaseUrl),
                //Timeout = TimeSpan.FromSeconds(15)
            };
        }

        public async Task<Response<List<EmployeeApiVM>>> GetAllEmployees()
        {

            var empList = new Response<List<EmployeeApiVM>>();
            var json = string.Empty;
            try
            {
                var url = string.Empty;
                url = $"GetAllEmployees";

                var results = await EmployeeServiceBase._httpClient.GetAsync(url).ConfigureAwait(false);

                if (results.IsSuccessStatusCode)
                {
                    json = await results.Content.ReadAsStringAsync().ConfigureAwait(false);
                    empList = JsonConvert.DeserializeObject<Response<List<EmployeeApiVM>>>(json);
                }
            }
            catch (Exception e)
            {
                return null;
            }

            return empList ?? new Response<List<EmployeeApiVM>>();

        }


        public async Task<Response<EmployeeApiVM>> GetEmployeeByid(long Employeeid)
        {
            var empData = new Response<EmployeeApiVM>();
            var json = string.Empty;
            try
            {
                var url = string.Empty;
                url = $"GetEmpById/{Employeeid}";
                var results = await EmployeeServiceBase._httpClient.GetAsync(url).ConfigureAwait(false);

                if (results.IsSuccessStatusCode)
                {
                    json = await results.Content.ReadAsStringAsync().ConfigureAwait(false);
                    empData = JsonConvert.DeserializeObject<Response<EmployeeApiVM>>(json);
                }
            }
            catch (Exception e)
            {
                return null;
            }

            return empData ?? new Response<EmployeeApiVM>();

        }

        public async Task<bool> UpdateEmployee(Response<EmployeeApiVM> model, long EmployeeId)
        {
            var employeeData = model.Content;
            var json = string.Empty;
            try
            {
                var url = string.Empty;
                url = $"UpdateEmp/{EmployeeId}";
                json = JsonConvert.SerializeObject(employeeData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await EmployeeServiceBase._httpClient.PutAsync(url, content).ConfigureAwait(false);


                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public async Task<Response<StatusCodeViewModel>> DeleyeEmployeeByid(long Employeeid)
        {
            var empData = new Response<StatusCodeViewModel>();
            var json = string.Empty;
            try
            {
                var url = string.Empty;
                url = $"DeleteEmp/{Employeeid}";
                var results = await EmployeeServiceBase._httpClient.DeleteAsync(url).ConfigureAwait(false);

                if (results.IsSuccessStatusCode)
                {
                    json = await results.Content.ReadAsStringAsync().ConfigureAwait(false);
                    empData = JsonConvert.DeserializeObject<Response<StatusCodeViewModel>>(json);
                }
            }
            catch (Exception e)
            {
                return null;
            }

            return empData ?? new Response<StatusCodeViewModel>();

        }

        public async Task<bool> CreateEmpApi(CreateEmpViewModel model)
        {
            var employeeData = model;
            var json = string.Empty;
            try
            {
                var url = string.Empty;
                url = $"CreateEmp";
                json = JsonConvert.SerializeObject(employeeData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await EmployeeServiceBase._httpClient.PostAsync(url, content).ConfigureAwait(false);


                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }

        }

    }
}
