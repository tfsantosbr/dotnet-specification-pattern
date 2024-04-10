using Companies.Application.Entities;
using Companies.Application.Specifications;

namespace Companies.Application.Handlers;

public class ApproveRestaurantWithSpecificationHandler(
    IRestaurantEligibleToBeApprovedSpec<Restaurant> restaurantEligibleToBeApprovedSpec)
{
    public Restaurant Handle(Restaurant restaurant)
    {
        if(!restaurantEligibleToBeApprovedSpec.IsSatisfiedBy(restaurant))
            throw new InvalidOperationException("The restaurant is not eligible to be approved.");

        restaurant.Status = RestaurantStatus.Approved;

        return restaurant;
    }
}
