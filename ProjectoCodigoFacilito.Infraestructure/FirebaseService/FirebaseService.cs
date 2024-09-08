using Firebase.Storage;
using ProjectoCodigoFacilito.Domain.Repository;
using Firebase.Auth;

namespace ProjectoCodigoFacilito.Infraestructure.FireBaseService;


public class FirebaseService : IFirebaseService
{
    private static readonly string route = "projectcodigofacilito.appspot.com";
    private static readonly string apiKey = "API-KEY example";
    private static readonly string email = "example@gmail.com";
    private static readonly string password = "PASSWORD EXAMPLE";

    public async Task<string> UploadStorage(string imageName, Stream stream)
    {
        var auth = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
        var a = await auth.SignInWithEmailAndPasswordAsync(email, password);

        var cancellation = new CancellationTokenSource();

        var task = new FirebaseStorage(
            route,
            new FirebaseStorageOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                ThrowOnCancel = true
            })
            .Child("images")
            .Child(imageName)
            .PutAsync(stream, cancellation.Token);

        var link = await task;

        return link;
    }
}


