using System.Diagnostics.CodeAnalysis;

namespace JwtWebAPITutorial.Entities_SubModel.User.SubModel
{
    public class UserFilter
    {
        [AllowNull]
        public string? Name { get; set; }

        [AllowNull]
        public string? Email { get; set; }

        [AllowNull]
        public string? PhoneNumber { get; set; }
    }
}
