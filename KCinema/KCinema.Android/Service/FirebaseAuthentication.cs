using System;
using System.Threading.Tasks;
using Firebase.Auth;
using KCinema.Android;
using KCinema.repo;


[assembly: Xamarin.Forms.Dependency(typeof(FirebaseAuthentication))]
namespace KCinema.Android
{
    public class FirebaseAuthentication : IFirebaseAuthentication
    {
        public async Task<string> SignInWithEmailAndPassword(string email, string password)
        {
   
            try
            {
                var task = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
                var token = task.User;

                return token != null ? (string) token.GetIdToken(false) : string.Empty;
            }
            catch (FirebaseAuthInvalidUserException e)
            {
                Console.WriteLine(e);
                return string.Empty;
            }
            catch (FirebaseAuthInvalidCredentialsException e)
            {
                e.PrintStackTrace();
                return string.Empty;
            } 
        }

        public async Task<string> SignUpWithEmailAndPassword(string email, string password)
        {
            try
            {
                var newUser = await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
                var token = newUser.User;

                return token != null ? (string) token.GetIdToken(false) : string.Empty;
            }
            catch (FirebaseAuthInvalidUserException e)
            {
                Console.WriteLine(e);
                return string.Empty;
            }
            catch (FirebaseAuthInvalidCredentialsException e)
            {
                e.PrintStackTrace();
                return string.Empty;
            } 
        }

        public bool SignOut()
        {
            try
            {
               FirebaseAuth.Instance.SignOut();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            return true;
        }

        public bool IsSignIn()
        {
            var user = FirebaseAuth.Instance.CurrentUser;
            return user != null;
        }
    }
}