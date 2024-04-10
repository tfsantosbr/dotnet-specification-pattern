using Companies.Application.Base;

namespace Companies.Application.Entities;

public static class RestaurantErrors
{
    public static Notification NoOperationalAdministrator => new()
    {
        Code = nameof(NoOperationalAdministrator),
        Description = "There is no operational administrator."
    };

    public static Notification NoOperationalManager => new()
    {
        Code = nameof(NoOperationalManager),
        Description = "There is no operational manager."
    };

    public static Notification NoOperationalAttendant => new()
    {
        Code = nameof(NoOperationalAttendant),
        Description = "There is no operational attendant."
    };

    public static Notification NoApprovedAddressProofDocument => new()
    {
        Code = nameof(NoApprovedAddressProofDocument),
        Description = "There is no approved address proof document."
    };

    public static Notification NotLocatedInSaoPaulo => new()
    {
        Code = nameof(NotLocatedInSaoPaulo),
        Description = "The restaurant must be located in São Paulo."
    };
}
