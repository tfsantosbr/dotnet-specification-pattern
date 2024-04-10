using Companies.Application.Base;
using Companies.Application.Entities;

namespace Companies.Application.Specifications;

public interface IRestaurantEligibleToBeApprovedWithResultSpec<Restaurant>
    : ISpecificationWithResult<Restaurant>;

public class RestaurantEligibleToBeApprovedWithResultSpec 
    : IRestaurantEligibleToBeApprovedWithResultSpec<Restaurant>
{
    public Result IsSatisfiedBy(Restaurant restaurant)
    {
        if (ThereIsNoOperationalAdministrator(restaurant))
            return Result.Failure(RestaurantErrors.NoOperationalAdministrator);

        if (ThereIsNoOperationalManager(restaurant))
            return Result.Failure(RestaurantErrors.NoOperationalManager);

        if (ThereIsNoOperationalAttendant(restaurant))
            return Result.Failure(RestaurantErrors.NoOperationalAttendant);

        if (TheIsNoApprovedAddressProofDocument(restaurant))
            return Result.Failure(RestaurantErrors.NoApprovedAddressProofDocument);

        if (NotBetLocatedInSaoPaulo(restaurant))
            return Result.Failure(RestaurantErrors.NotLocatedInSaoPaulo);

        return Result.Success();
    }

    private static bool NotBetLocatedInSaoPaulo(Restaurant restaurant)
    {
        return restaurant.Address.City != "São Paulo";
    }

    private static bool TheIsNoApprovedAddressProofDocument(Restaurant restaurant)
    {
        return !restaurant.Documents.Any(d =>
                    d.Type == DocumentType.AddressProof &&
                    d.Status == DocumentStatus.Approved);
    }

    private static bool ThereIsNoOperationalAttendant(Restaurant restaurant)
    {
        return !restaurant.Collaborators.Any(c =>
                    c.Type == CollaboratorType.Attendant &&
                    c.IsOperational);
    }

    private static bool ThereIsNoOperationalManager(Restaurant restaurant)
    {
        return !restaurant.Collaborators.Any(c =>
                    c.Type == CollaboratorType.Manager &&
                    c.IsOperational);
    }

    private static bool ThereIsNoOperationalAdministrator(Restaurant restaurant)
    {
        return !restaurant.Collaborators.Any(c =>
                    c.Type == CollaboratorType.Administrator &&
                    c.IsOperational);
    }
}