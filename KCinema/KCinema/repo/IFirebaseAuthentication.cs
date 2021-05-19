using System.Threading.Tasks;

namespace KCinema.repo
{
    public interface IFirebaseAuthentication
    {
        Task<string> SignInWithEmailAndPassword(string email, string password);
        Task<string> SignUpWithEmailAndPassword(string email, string password);
        
        bool SignOut();
        bool IsSignIn();
    }
}