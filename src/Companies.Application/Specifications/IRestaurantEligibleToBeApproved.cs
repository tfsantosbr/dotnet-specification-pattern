using Companies.Application.Entities;

namespace Companies.Application.Specifications;

public interface IRestaurantEligibleToBeApprovedSpec<Restaurant>
    : ISpecification<Restaurant>;

public class RestaurantEligibleToBeApprovedSpec : IRestaurantEligibleToBeApprovedSpec<Restaurant>
{
    public bool IsSatisfiedBy(Restaurant restaurant)
    {
        if (ThereIsNoOperationalAdministrator(restaurant))
            throw new InvalidOperationException("There is no operational administrator.");

        if (ThereIsNoOperationalManager(restaurant))
            throw new InvalidOperationException("There is no operational manager.");

        if (ThereIsNoOperationalAttendant(restaurant))
            throw new InvalidOperationException("There is no operational attendant.");

        if (TheIsNoApprovedAddressProofDocument(restaurant))
            throw new InvalidOperationException("There is no approved address proof document.");

        if (NotBetLocatedInSaoPaulo(restaurant))
            throw new InvalidOperationException("The restaurant must be located in São Paulo.");

        return true;
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