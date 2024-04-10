using Companies.Application.Entities;
using Companies.Application.Handlers;

namespace Companies.Application.Tests;

public class ApproveRestaurantHandlerTests
{
    private ApproveRestaurantHandler _handler;

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
}
