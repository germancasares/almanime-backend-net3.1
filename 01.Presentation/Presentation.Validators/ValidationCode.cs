namespace Presentation.Validators
{
    public enum ValidationCode
    {
        NotEmpty = 1,
        Unique = 2,

        // Account
        ValidEmailAddress = 1001,
        IdentifierExists = 1002,
        MinimumLength = 1003,
        HasDigit = 1004,
        HasLowerCase = 1005,
        HasNonAlphanumeric = 1006,
        HasUpperCase = 1007
    }
}
