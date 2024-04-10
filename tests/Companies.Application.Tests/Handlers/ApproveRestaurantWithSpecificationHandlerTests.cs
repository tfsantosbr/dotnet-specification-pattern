using Companies.Application.Entities;
using Companies.Application.Handlers;
using Companies.Application.Specifications;
using NSubstitute;

namespace Companies.Application.Tests;

public class ApproveRestaurantWithSpecificationHandlerTests
{
    private ApproveRestaurantWithSpecificationHandler _handler;
    private IRestaurantEligibleToBeApprovedSpec<Restaurant> _specification;

    public ApproveRestaurantWithSpecificationHandlerTests()
    {
        _specification = Substitute.For<IRestaurantEligibleToBeApprovedSpec<Restaurant>>();
        _handler = new ApproveRestaurantWithSpecificationHandler(_specification);
    }

    [Fact]
    public void Handle_RestaurantIneligible_ThrowsException()
    {
        // arrange
        var restaurant = new Restaurant();
        _specification.IsSatisfiedBy(Arg.Any<Restaurant>()).Returns(false);

        // act & assert
        var exception = Assert.Throws<InvalidOperationException>(() => _handler.Handle(restaurant));
        Assert.Equal("The restaurant is not eligible to be approved.", exception.Message);
    }

    [Fact]
    public void Handle_RestaurantEligible_NotThrowsException()
    {
        // arrange
        var restaurant = new Restaurant();
        _specification.IsSatisfiedBy(Arg.Any<Restaurant>()).Returns(true);

        // act
        var updatedRestaurant = _handler.Handle(restaurant);

        // assert
        Assert.Equal(RestaurantStatus.Approved, updatedRestaurant.Status);
    }
}
