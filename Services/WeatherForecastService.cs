using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using WeatherForecast.Models;
using Microsoft.EntityFrameworkCore;

namespace WeatherForecast.Services
{
    public interface IWeatherForecastService
    {
        Task<Weather> GetWeatherAPIAsync(string City);
        Task<Weather> GetWeatherAsync(string City, Guid userId);
        Task<Weather> SetWeatherAsync(Weather weather);
        Task SetUserWeatherAsync(UserWeather userWeather);
    }
    public class WeatherForecastService : IWeatherForecastService
    {
        public readonly HttpClient _httpClient;
        public readonly WeatherForecastContext _context;
        private static string _apiUri { get; set; }
        private static string _apiKey { get; set; }
        public IConfiguration _configuration { get; }

        public WeatherForecastService(HttpClient httpClient, IConfiguration configuration, WeatherForecastContext context)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _context = context;
            _apiUri = _configuration.GetSection("WeatherAPIUri").Value;
            _apiKey = _configuration.GetSection("WeatherAPIKey").Value;
        }
        public async Task<Weather> GetWeatherAPIAsync(string City)
        {
            try
            {
                var json = await _httpClient.GetStringAsync(string.Format("{0}weather?q={1}&appid={2}&units=metric", _apiUri, City, _apiKey));

                return JsonConvert.DeserializeObject<Weather>(json);
            }
            catch (Exception ex)
            {
                return null;
            }


        }
               
        public async Task<Weather> GetWeatherAsync(string City, Guid userId)
        {
            var data = (from uw in _context.UserWeathers
                        join w in _context.Weathers on uw.WeatherId equals w.Id
                        join m in _context.Mains on w.Id equals m.WeatherId

                        where uw.UserId == userId
                        && w.City.StartsWith(City)

                        select new Weather()
                        {
                            Id = w.Id,
                            Description = w.Description,
                            Icon = w.Icon,
                            City = w.City,
                            Main = m
                        }
                        );

            return await data.FirstOrDefaultAsync();


        }

        public async Task<Weather> SetWeatherAsync(Weather weather)
        {
            _context.Weathers.Add(weather);
            await _context.SaveChangesAsync();
            return weather;
        }

        public async Task SetUserWeatherAsync(UserWeather userWeather)
        {
            _context.UserWeathers.Add(userWeather);
            await _context.SaveChangesAsync();
        }
    }
}
