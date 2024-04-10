using Companies.Application.Base;
using Companies.Application.Entities;
using Companies.Application.Specifications;

namespace Companies.Application.Handlers;

public class ApproveRestaurantWithSpecificationAndResultHandler(
    IRestaurantEligibleToBeApprovedWithResultSpec<Restaurant> restaurantEligibleToBeApprovedSpec)
{
    public Result Handle(Restaurant restaurant)
    {
        var result = restaurantEligibleToBeApprovedSpec.IsSatisfiedBy(restaurant);

        if(result.HasNotifications)
            return result;

        restaurant.Status = RestaurantStatus.Approved;

        return Result.Success();
    }
}
