using System.Collections.Generic;
using System.Threading.Tasks;
using KCinema.model;

namespace KCinema.repo
{
    public interface IContentRepo
    {
        Task<List<Content>> GetAllContent();

        Task DeleteContent(Content content);
        
        Task UpdateContent(Content content);

        Task SaveContent(Content content);
        
    }
    
}