using System.ComponentModel.DataAnnotations;

namespace ClassTask.Dtos
{
    public record MediaUserReponseDto
    {
        public Guid Id { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public string UserName { get; set; } = string.Empty;
        public required string FullName { get; set; } 
        public required DateOnly DateOfBirth { get; set; }
        public string Address { get; set; } = string.Empty;
        public DateTime DateAdded { get; set; }
    }

    public record MediaUsersReponseDto
    {
        public Guid Id { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string FullName { get; set; }
        public required DateOnly DateOfBirth { get; set; }
        public DateTime DateAdded { get; set; }
    }

    public record SignUpRequestDto
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [Phone]
        public required string PhoneNumber { get; set; }

        [StringLength(10,MinimumLength = 5)]
        [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Username can only contain letters, numbers, and underscores.")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [StringLength(18)]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(23)]
        public required string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public required DateOnly DateOfBirth { get; set; }

        [StringLength(90)]
        public string Address { get; set; } = string.Empty;
    }

    public record DetailsUpdateRequestDto
    {
        [Required]
        [EmailAddress]
        public required string YourEmail { get; set; }

        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [StringLength(10, MinimumLength = 5)]
        [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Username can only contain letters, numbers, and underscores.")]
        public string UserName { get; set; } = string.Empty;

        [StringLength(18)]
        public required string FirstName { get; set; }

        [StringLength(23)]
        public string LastName { get; set; } = string.Empty;

        [StringLength(90)]
        public string Address { get; set; } = string.Empty;
    }

    public record DeleteAccountRequestDto
    {
        [Required]
        public required Guid UserId { get; set; }

        [Required]
        [EmailAddress]
        public required string UserEmail { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public required DateOnly UserDateOfBirth { get; set; }
    }
}