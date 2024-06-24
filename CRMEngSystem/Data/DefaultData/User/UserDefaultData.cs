using CRMEngSystem.Data.Entities.User;
using CRMEngSystem.Data.Enums;


namespace CRMEngSystem.Data.DefaultData.User
{
    public static class UserDefaultData
    {
        public static List<UserEntity> Users = new()
        {
            new()
            {
                UserName = "Maksym",
                NormalizedUserName = "MAKSYM",
                PasswordHash = "AQAAAAIAAYagAAAAEKC3o+5uAHxP8FrvZwO03/HeS9j7AQDRjSEhD/seG+wJgtNU8RoVkum08efUyesL6w==",
                ContactId = 1,
                AccessLevel = AccessLevel.High
            }
        };
    }
}
