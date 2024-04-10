using Companies.Application.Entities;
using Companies.Application.Handlers;

namespace Companies.Application.Tests;

public class ApproveRestaurantHandlerTests
{
    private readonly ApproveRestaurantHandler _handler;

    public ApproveRestaurantHandlerTests()
    {
        _handler = new ApproveRestaurantHandler();
    }

    [Fact]
    public void Handle_NoOperationalAdministrator_ThrowsException()
    {
        var restaurant = new Restaurant
        {
            Collaborators =
            [
                new() { Type = CollaboratorType.Administrator, EmailConfirmed = false },
                new() { Type = CollaboratorType.Manager, EmailConfirmed = true },
                new() { Type = CollaboratorType.Attendant, EmailConfirmed = true }
            ],
            Documents =
            [
                new Document { Type = DocumentType.AddressProof, Status = DocumentStatus.Approved }
            ],
            Address = new Address { City = "São Paulo" }
        };

        var exception = Assert.Throws<InvalidOperationException>(() => _handler.Handle(restaurant));
        Assert.Equal("There is no operational administrator.", exception.Message);
    }

    [Fact]
    public void Handle_NoOperationalManager_ThrowsException()
    {
        var restaurant = new Restaurant
        {
            Collaborators =
            [
                new() { Type = CollaboratorType.Administrator, EmailConfirmed = true },
                new() { Type = CollaboratorType.Manager, EmailConfirmed = false },
                new() { Type = CollaboratorType.Attendant, EmailConfirmed = true }
            ],
            Documents =
            [
                new Document { Type = DocumentType.AddressProof, Status = DocumentStatus.Approved }
            ],
            Address = new Address { City = "São Paulo" }
        };

        var exception = Assert.Throws<InvalidOperationException>(() => _handler.Handle(restaurant));
        Assert.Equal("There is no operational manager.", exception.Message);
    }

    [Fact]
    public void Handle_NoOperationalAttendant_ThrowsException()
    {
        var restaurant = new Restaurant
        {
            Collaborators =
            [
                new() { Type = CollaboratorType.Administrator, EmailConfirmed = true },
                new() { Type = CollaboratorType.Manager, EmailConfirmed = true },
                new() { Type = CollaboratorType.Attendant, EmailConfirmed = false }
            ],
            Documents =
            [
                new Document { Type = DocumentType.AddressProof, Status = DocumentStatus.Approved }
            ],
            Address = new Address { City = "São Paulo" }
        };

        var exception = Assert.Throws<InvalidOperationException>(() => _handler.Handle(restaurant));
        Assert.Equal("There is no operational attendant.", exception.Message);
    }

    [Fact]
    public void Handle_NoApprovedAddressProofDocument_ThrowsException()
    {
        var restaurant = new Restaurant
        {
            Collaborators =
            [
                new() { Type = CollaboratorType.Administrator, EmailConfirmed = true },
                new() { Type = CollaboratorType.Manager, EmailConfirmed = true },
                new() { Type = CollaboratorType.Attendant, EmailConfirmed = true }
            ],
            Documents =
            [
                new Document { Type = DocumentType.AddressProof, Status = DocumentStatus.Pending }
            ],
            Address = new Address { City = "São Paulo" }
        };

        var exception = Assert.Throws<InvalidOperationException>(() => _handler.Handle(restaurant));
        Assert.Equal("There is no approved address proof document.", exception.Message);
    }

    [Fact]
    public void Handle_NotLocatedInSaoPaulo_ThrowsException()
    {
        var restaurant = new Restaurant
        {
            Collaborators =
            [
                new() { Type = CollaboratorType.Administrator, EmailConfirmed = true },
                new() { Type = CollaboratorType.Manager, EmailConfirmed = true },
                new() { Type = CollaboratorType.Attendant, EmailConfirmed = true }
            ],
            Documents =
            [
                new Document { Type = DocumentType.AddressProof, Status = DocumentStatus.Approved }
            ],
            Address = new Address { City = "Rio de Janeiro" }
        };

        var exception = Assert.Throws<InvalidOperationException>(() => _handler.Handle(restaurant));
        Assert.Equal("The restaurant must be located in São Paulo.", exception.Message);
    }

    [Fact]
    public void Handle_AllConditionsMet_RestaurantApproved()
    {
        // arrange
        var restaurant = new Restaurant
        {
            Collaborators =
            [
                new() { Type = CollaboratorType.Administrator, EmailConfirmed = true },
                new() { Type = CollaboratorType.Manager, EmailConfirmed = true },
                new() { Type = CollaboratorType.Attendant, EmailConfirmed = true }
            ],
            Documents =
            [
                new Document { Type = DocumentType.AddressProof, Status = DocumentStatus.Approved }
            ],
            Address = new Address { City = "São Paulo" }
        };

        // act
        var updatedRestaurant = _handler.Handle(restaurant);

        // assert
        Assert.Equal(RestaurantStatus.Approved, updatedRestaurant.Status);
    }
}
