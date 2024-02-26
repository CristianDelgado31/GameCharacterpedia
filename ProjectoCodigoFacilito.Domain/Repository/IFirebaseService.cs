namespace ProjectoCodigoFacilito.Domain.Repository;

public interface IFirebaseService
{
    Task<string> UploadStorage(string imageName, Stream stream);
}