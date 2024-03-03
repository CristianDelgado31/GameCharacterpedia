using ProjectoCodigoFacilito.Client.Models.ReferenceModel;

namespace ProjectoCodigoFacilito.Client.Services.Interfaces
{
    public interface IReferenceIdService
    {
        Task<ReferenceIdModel> CreateReferenceId(ReferenceIdModel reference);

        Task<ReferenceIdModel> DeleteReferenceId(ReferenceIdModel reference);

        void ModifyReferenceId(ReferenceIdModel reference);
        Task<ReferenceIdModel> GetReferenceId(ReferenceIdModel reference);
    }
}
