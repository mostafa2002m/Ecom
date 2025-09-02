using System.Net;
using System.Net.Http;
using System.Net.Http.Json;

namespace Application.Services
{
    public interface IService<T> where T : class
    {
        Task<List<T>> GetAllAsync(string apiName);
        Task<T> AddAsync(string apiName, T entity);
        Task<T> UpdateAsync(string apiName, T entity);
        Task DeleteAsync (string apiName);
        Task<T> GetAsync(string apiName);
    }

    public class Service<T>(HttpClient httpClient) : IService<T> where T : class
    {
        private readonly HttpClient httpClient = httpClient;

        public async Task<T> AddAsync(string apiName, T entity)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync<T>(apiName, entity);
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        return default(T);
                    }

                    return await response.Content.ReadFromJsonAsync<T>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status code: {response.StatusCode} message: {message}");
                }
            }
            catch (Exception ex)
            {
               throw new Exception($"Error : {ex.Message}");


            }
        }

        //public async Task<T> DeleteAsync(string apiName)
        //{
        //     try
        //    {
        //        var response = await httpClient.DeleteAsync(apiName);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            if (response.StatusCode == HttpStatusCode.NoContent)
        //            {
        //                return default(T);
        //            }

        //            return await response.Content.ReadFromJsonAsync<T>();
        //        }
        //        else
        //        {
        //            var message = await response.Content.ReadAsStringAsync();
        //            throw new Exception($"Http status code: {response.StatusCode} message: {message}");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"Error : {ex.Message}");


        //    }
        //}

        public async Task DeleteAsync(string apiName)
        {
            await httpClient.DeleteAsync(apiName);
        }

        public async Task<List<T>> GetAllAsync(string apiName)
        {

            try
            {
                var response = await httpClient.GetAsync(apiName);
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return default(List<T>);
                    }

                    return await response.Content.ReadFromJsonAsync<List<T>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status code: {response.StatusCode} message: {message}");
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error : {ex.Message}");

                throw;
            }
        }




        public async Task<T> GetAsync(string apiName)
        {
            try
            {
                var response = await httpClient.GetAsync(apiName);

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(T);
                    }

                    return await response.Content.ReadFromJsonAsync<T>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status code: {response.StatusCode} message: {message}");
                }

            }
            catch (Exception ex)
            {

                throw new Exception($"Error : {ex.Message}");


            }

        }

        public async Task<T> UpdateAsync(string apiName, T entity)
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync<T>(apiName, entity);
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        return default(T);
                    }

                    return await response.Content.ReadFromJsonAsync<T>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status code: {response.StatusCode} message: {message}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error : {ex.Message}");


            }
        }
    }
}
