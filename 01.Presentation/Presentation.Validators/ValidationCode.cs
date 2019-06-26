namespace Presentation.Validators
{
    public enum ValidationCode
    {
        NotEmpty = 1,
        Unique = 2,
        MinimumLength = 3,
        MaximumLength = 4,
        ContentTypeNotValid = 5,
        ImageAspectRatio = 6,
        ImageResolution = 7,

        // Account
        ValidEmailAddress = 1001,
        IdentifierExists = 1002,
        HasDigit = 1003,
        HasLowerCase = 1004,
        HasNonAlphanumeric = 105,
        HasUpperCase = 1006,
    }
}
