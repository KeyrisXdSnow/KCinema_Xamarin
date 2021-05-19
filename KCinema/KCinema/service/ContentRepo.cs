using System;
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
           return (await _firebaseClient.Child(FirebaseConfig.Root).OnceAsync<Content>()).Select(item =>
                {
                    ContentType type; 
                    Enum.TryParse("Active", out  type);

                    return new Content(
                        id: item.Key,
                        name: item.Object.Name,
                        type: type,
                        genres: item.Object.Genres,
                        rating: item.Object.Rating,
                        country: item.Object.Country,
                        watchTime: item.Object.WatchTime,
                        releaseDate: item.Object.ReleaseDate,
                        posterUrl: item.Object.PosterUrl,
                        trailerUrl: item.Object.TrailerUrl,
                        description: item.Object.Description,
                        gallery: item.Object.Gallery
                    );
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