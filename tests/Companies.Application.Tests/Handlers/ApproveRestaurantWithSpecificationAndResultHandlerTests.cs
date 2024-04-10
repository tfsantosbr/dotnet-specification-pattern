using Companies.Application.Base;
using Companies.Application.Entities;
using Companies.Application.Handlers;
using Companies.Application.Specifications;
using NSubstitute;

namespace Companies.Application.Tests;

public class ApproveRestaurantWithSpecificationAndResultHandlerTests
{
    private readonly IRestaurantEligibleToBeApprovedWithResultSpec<Restaurant> _specification;
    private readonly ApproveRestaurantWithSpecificationAndResultHandler _handler;

    public ApproveRestaurantWithSpecificationAndResultHandlerTests()
    {
        _specification = Substitute.For<IRestaurantEligibleToBeApprovedWithResultSpec<Restaurant>>();
        _handler = new ApproveRestaurantWithSpecificationAndResultHandler(_specification);
    }

    [Fact]
    public void Handle_RestaurantNotEligible_StatusNotApproved()
    {
        // arrange
        var restaurant = new Restaurant { Status = RestaurantStatus.Pending };
        _specification.IsSatisfiedBy(restaurant).Returns(Result.Failure(new Notification()));

        // act
        var result = _handler.Handle(restaurant);

        // assert
        Assert.False(result.IsSuccess);
        Assert.Equal(RestaurantStatus.Pending, restaurant.Status);
    }

    [Fact]
    public void Handle_RestaurantEligible_StatusApproved()
    {
        // arrange
        var restaurant = new Restaurant { Status = RestaurantStatus.Pending };
        _specification.IsSatisfiedBy(restaurant).Returns(Result.Success());

        // act
        var result = _handler.Handle(restaurant);

        // assert
        Assert.True(result.IsSuccess);
        Assert.Equal(RestaurantStatus.Approved, restaurant.Status);
    }
}
