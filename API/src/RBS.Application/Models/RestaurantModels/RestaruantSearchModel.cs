﻿using RBS.Domain.Restaurants;

namespace RBS.Application.Models.RestaurantModels
{
    public class RestaruantSearchModel
    {
        public int Id { get; set; }
        public bool IsNew { get; set; }
        public bool IsOpen { get; set; }
        public string Name { get; set; }
        public AddressModel Address { get; set; }
        public string ImgSrc { get; set; }
        public string ImgAlt { get; set; }
        public decimal? AverageRate { get; set; }
        public string? MainType { get; set; }
        public int? ReviewerCount { get; set; }
        public string GoogleMapUrl { get; set; }
        public List<DateTime> FreeTime { get; set; }

        public RestaruantSearchModel(Restaurant restaurant)
        {
            Id = restaurant.Id;
            Name = restaurant.Name;
            Address = new AddressModel(restaurant.Address);
            var restaurantImg = restaurant.Images.FirstOrDefault(x => x.IsTop == true);
            ImgSrc = restaurantImg?.Src;
            ImgAlt = restaurantImg?.Alt;
            AverageRate = restaurant.Reviews?.Average(x => x.OverallRate).GetValueOrDefault();
            ReviewerCount = restaurant.Reviews?.Count;
            MainType = restaurant.RSTypes?.FirstOrDefault(x => x.IsMain)?.Name;
            GoogleMapUrl = restaurant.GoogleMapUrl;
            IsNew = (DateTime.Now - restaurant.PublishDate).Days < 7;
            IsOpen = DateTime.Now.Hour > restaurant.OpentTime.Hour && DateTime.Now.Hour < restaurant.CloseTime.Hour;
            FreeTime = new List<DateTime>();

            var fromTime = restaurant.OpentTime.TimeOfDay > DateTime.Now.TimeOfDay ? restaurant.OpentTime : DateTime.Now;
            for (var dt = fromTime.TimeOfDay; dt <= restaurant.CloseTime.TimeOfDay; dt = dt.Add(new TimeSpan(0, 30, 0)))
            {
                FreeTime.Add(new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, DateTime.Now.Date.Day, dt.Hours, dt.Minutes, dt.Seconds));
            }
        }
    }
}
