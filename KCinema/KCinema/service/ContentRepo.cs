using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using KCinema.config;
using KCinema.model;
using KCinema.repo;

namespace KCinema.service
{
    public class ContentRepo : IContentRepo
    {
        
        private readonly FirebaseClient _firebaseClient
            = new FirebaseClient(FirebaseConfig.FirebaseUrl);
        
        public async Task<List<Content>> GetAllContent()
        {
            return (await _firebaseClient
                    .Child(FirebaseConfig.FirebaseUrl)
                    .OnceAsync<Content>())
                .Select(item =>
                    new Content
                    {
                        Name = item.Object.Name,
                        Type = item.Object.Type,
                        Genres = item.Object. Genres,
                        Rating = item.Object.Rating,
                        Country = item.Object.Country,
                        WatchTime = item.Object.WatchTime,
                        ReleaseDate = item.Object.ReleaseDate,
                        PosterUrl = item.Object.PosterUrl,
                        TrailerUrl = item.Object.TrailerUrl,
                        Description = item.Object.Description,
                        Gallery = item.Object.Gallery,
                        Id = item.Key
                    }
                ).ToList();
        }

        public async Task DeleteContent(Content content)
        {
            await _firebaseClient
                .Child(FirebaseConfig.Root)
                .Child(content.Id)
                .DeleteAsync();
        }

        public async Task UpdateContent(Content content)
        {
            await _firebaseClient  
                .Child(FirebaseConfig.Root)  
                .Child(content.Id)  
                .PutAsync(content);
        }

        public async Task SaveContent(Content content)
        {
            await _firebaseClient.Child(FirebaseConfig.Root).PostAsync(content);
        }
    }
}