using System.ComponentModel.DataAnnotations;

namespace FMA.Entities.Dto
{
    public class UserDto : LoginUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public List<string> Roles { get; set; }
    }
}
