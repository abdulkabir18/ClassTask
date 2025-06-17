using ClassTask.Dtos;

namespace ClassTask.Services.Interfaces
{
    public interface IMediaUserService
    {
        Task<ResponseDto<string>> RegisterUser(SignUpRequestDto model);
        Task<ResponseDto<MediaUserReponseDto>> UserProfileById(Guid id);
        Task<ResponseDto<MediaUserReponseDto>> UserProfileByEmail(string email);
        Task<ResponseDto<MediaUserReponseDto>> UserProfileByPhoneNumber(string phoneNumber);
        Task<ResponseDto<ICollection<MediaUsersReponseDto>>> Search(string keyword);
        Task<ResponseDto<ICollection<MediaUsersReponseDto>>> GetAllUsers();
        Task<ResponseDto<MediaUserReponseDto>> UpdateDetails(DetailsUpdateRequestDto model);
        Task<ResponseDto<string>> DeleteAccount(DeleteAccountRequestDto model);

    }
}
