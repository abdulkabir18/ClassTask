using ClassTask.Dtos;
using ClassTask.Models;
using ClassTask.Repositories.Interfaces;
using ClassTask.Services.Interfaces;
using ClassTask.UnitOfWork.Interface;

namespace ClassTask.Services.Implementations
{
    public class MediaUserService : IMediaUserService
    {
        private readonly IMediaUserRepository _mediaUserRepository;
        private readonly IUnitOfWork _unitOfWork;
        public MediaUserService(IMediaUserRepository mediaUserRepository, IUnitOfWork unitOfWork)
        {
            _mediaUserRepository = mediaUserRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDto<string>> DeleteAccount(DeleteAccountRequestDto model)
        {
            var mediaUser = await _mediaUserRepository.GetAsync(model.UserId);
            if (mediaUser == null || mediaUser.Email != model.UserEmail || mediaUser.DateOfBirth != model.UserDateOfBirth) return ResponseDto<string>.Fail("No record found, Try again");

            _mediaUserRepository.Delete(mediaUser);
            return ResponseDto<string>.Success($"{mediaUser.Id}", "Record deleted ");
        }

        public async Task<ResponseDto<ICollection<MediaUsersReponseDto>>> GetAllUsers()
        {
            var mediaUsers = await _mediaUserRepository.GetAllAsync();

            if (!mediaUsers.Any()) return ResponseDto<ICollection<MediaUsersReponseDto>>.Fail("No users avaliable");

            var mediaUsersDto = mediaUsers.Select(x => new MediaUsersReponseDto
            {
                DateOfBirth = x.DateOfBirth,
                Email = x.Email,
                FullName = x.FirstName + " " + x.LastName,
                PhoneNumber = x.PhoneNumber,
                DateAdded = x.DateAdded,
                Id = x.Id
            }).ToList();

            return ResponseDto<ICollection<MediaUsersReponseDto>>.Success(mediaUsersDto, "Users record loading...");
        }

        public async Task<ResponseDto<string>> RegisterUser(SignUpRequestDto model)
        {
            bool checkEmail = await _mediaUserRepository.CheckAsync(m => m.Email == model.Email);
            if (checkEmail) return ResponseDto<string>.Fail("Email is associated with another account");

            bool checkUsername = await _mediaUserRepository.CheckAsync(m => m.UserName == model.UserName);
            if (checkUsername) return ResponseDto<string>.Fail("Username already exists");

            var mediaUser = new MediaUser
            {
                DateOfBirth = model.DateOfBirth,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                UserName = model.UserName
            };

            await _mediaUserRepository.AddAsync(mediaUser);
            await _unitOfWork.SaveChangesAsync();

            return ResponseDto<string>.Success(mediaUser.Id.ToString(), "Register Successfully");
        }

        public async Task<ResponseDto<ICollection<MediaUsersReponseDto>>> Search(string keyword)
        {
            var filter = await _mediaUserRepository.GetAllAsync(m => m.UserName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
            m.PhoneNumber.Contains(keyword, StringComparison.OrdinalIgnoreCase) || 
            m.FirstName.Contains(keyword, StringComparison.OrdinalIgnoreCase) || 
            m.LastName.Contains(keyword, StringComparison.OrdinalIgnoreCase) || 
            m.Email.Contains(keyword, StringComparison.OrdinalIgnoreCase));

            if (!filter.Any()) return ResponseDto<ICollection<MediaUsersReponseDto>>.Fail("No record found");

            var filtered = filter.Select(x => new MediaUsersReponseDto
            {
                DateOfBirth = x.DateOfBirth,
                Email = x.Email,
                FullName = x.FirstName + " " + x.LastName,
                PhoneNumber = x.PhoneNumber,
                DateAdded = x.DateAdded,
                Id = x.Id
            }).ToList();

            return ResponseDto<ICollection<MediaUsersReponseDto>>.Success(filtered, "Loading...");
        }

        public Task<ResponseDto<MediaUserReponseDto>> UpdateDetails(DetailsUpdateRequestDto model)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto<MediaUserReponseDto>> UserProfileByEmail(string email)
        {
            var mediaUser = await _mediaUserRepository.GetAsync(m => m.Email == email);
            if (mediaUser == null) return ResponseDto<MediaUserReponseDto>.Fail("Not found");

            var mediaUserDto = new MediaUserReponseDto
            { DateOfBirth = mediaUser.DateOfBirth, Email = mediaUser.Email, FullName = mediaUser.FirstName + " " + mediaUser.LastName, PhoneNumber = mediaUser.PhoneNumber, Address = mediaUser.Address, DateAdded = mediaUser.DateAdded, Id = mediaUser.Id, UserName = mediaUser.UserName };

            return ResponseDto<MediaUserReponseDto>.Success(mediaUserDto, "Found");
        }

        public async Task<ResponseDto<MediaUserReponseDto>> UserProfileById(Guid id)
        {
            var mediaUser = await _mediaUserRepository.GetAsync(id);
            if (mediaUser == null) return ResponseDto<MediaUserReponseDto>.Fail("Not found");

            var mediaUserDto = new MediaUserReponseDto
            { DateOfBirth = mediaUser.DateOfBirth, Email = mediaUser.Email, FullName = mediaUser.FirstName + " " + mediaUser.LastName, PhoneNumber = mediaUser.PhoneNumber, Address = mediaUser.Address, DateAdded = mediaUser.DateAdded, Id = mediaUser.Id, UserName = mediaUser.UserName };

            return ResponseDto<MediaUserReponseDto>.Success(mediaUserDto, "Found");
        }

        public async Task<ResponseDto<MediaUserReponseDto>> UserProfileByPhoneNumber(string phoneNumber)
        {
            var mediaUser = await _mediaUserRepository.GetAsync(m => m.PhoneNumber == phoneNumber);
            if (mediaUser == null) return ResponseDto<MediaUserReponseDto>.Fail("Not found");

            var mediaUserDto = new MediaUserReponseDto
            { DateOfBirth = mediaUser.DateOfBirth, Email = mediaUser.Email, FullName = mediaUser.FirstName + " " + mediaUser.LastName, PhoneNumber = mediaUser.PhoneNumber, Address = mediaUser.Address, DateAdded = mediaUser.DateAdded, Id = mediaUser.Id, UserName = mediaUser.UserName };

            return ResponseDto<MediaUserReponseDto>.Success(mediaUserDto, "Found");
        }
    }
}
