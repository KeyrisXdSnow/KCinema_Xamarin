using System.Collections.Generic;
using Newtonsoft.Json;

namespace KCinema.model
{
    public class Content
    {
        
        public string Id { get; set; }
        
        public string Name { get; set; }
        public ContentType Type { get; set; }
        
        public string Genres { get; set; }
        public float Rating { get; set; }
        
        public string Country { get; set; }
        public int WatchTime { get; set; }
        public string ReleaseDate { get; set; }
        
        public string PosterUrl { get; set; }
        public string TrailerUrl { get; set; }
        public string Description { get; set; }
        
        public List<string> Gallery { get; set; }

        public Content()
        {
        }

        public Content(string id, string name, ContentType type, string genres, float rating, string country, int watchTime, string releaseDate, string posterUrl, string trailerUrl, string description, List<string> gallery)
        {
            Id = id;
            Name = name;
            Type = type;
            Genres = genres;
            Rating = rating;
            Country = country;
            WatchTime = watchTime;
            ReleaseDate = releaseDate;
            PosterUrl = posterUrl;
            TrailerUrl = trailerUrl;
            Description = description;
            Gallery = gallery;
        }

        public Content(string name, ContentType type)
        {
            Name = name;
            Type = type;
        }
    }
    
    
    
    
    public enum ContentType
    {
        Film,
        Series,
        Cartoon,
        AnimatedSeries,
        Anime
   
    }
}