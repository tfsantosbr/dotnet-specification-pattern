using Companies.Application.Entities;
using Companies.Application.Specifications;
using Xunit;

namespace Companies.Application.Tests
{
    public class RestaurantEligibleToBeApprovedSpecTests
    {
        private readonly RestaurantEligibleToBeApprovedSpec _spec;

        public RestaurantEligibleToBeApprovedSpecTests()
        {
            _spec = new RestaurantEligibleToBeApprovedSpec();
        }

        [Fact]
        public void IsSatisfiedBy_NoOperationalAdministrator_ThrowsException()
        {
            var restaurant = new Restaurant
            {
                Collaborators =
                [
                    new () { Type = CollaboratorType.Administrator, EmailConfirmed = false },
                    new () { Type = CollaboratorType.Manager, EmailConfirmed = true },
                    new () { Type = CollaboratorType.Attendant, EmailConfirmed = true }
                ],
                Documents =
                [
                    new() { Type = DocumentType.AddressProof, Status = DocumentStatus.Approved }
                ],
                Address = new Address { City = "São Paulo" }
            };

            var exception = Assert.Throws<InvalidOperationException>(() => _spec.IsSatisfiedBy(restaurant));
            Assert.Equal("There is no operational administrator.", exception.Message);
        }

        [Fact]
        public void IsSatisfiedBy_NoOperationalManager_ThrowsException()
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
                    new() { Type = DocumentType.AddressProof, Status = DocumentStatus.Approved }
                ],
                Address = new Address { City = "São Paulo" }
            };

            var exception = Assert.Throws<InvalidOperationException>(() => _spec.IsSatisfiedBy(restaurant));
            Assert.Equal("There is no operational manager.", exception.Message);
        }

        [Fact]
        public void IsSatisfiedBy_NoOperationalAttendant_ThrowsException()
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
                    new() { Type = DocumentType.AddressProof, Status = DocumentStatus.Approved }
                ],
                Address = new Address { City = "São Paulo" }
            };

            var exception = Assert.Throws<InvalidOperationException>(() => _spec.IsSatisfiedBy(restaurant));
            Assert.Equal("There is no operational attendant.", exception.Message);
        }

        [Fact]
        public void IsSatisfiedBy_NoApprovedAddressProofDocument_ThrowsException()
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
                    new() { Type = DocumentType.AddressProof, Status = DocumentStatus.Rejected }
                ],
                Address = new Address { City = "São Paulo" }
            };

            var exception = Assert.Throws<InvalidOperationException>(() => _spec.IsSatisfiedBy(restaurant));
            Assert.Equal("There is no approved address proof document.", exception.Message);
        }

        [Fact]
        public void IsSatisfiedBy_RestaurantNotLocatedInSaoPaulo_ThrowsException()
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
                    new() { Type = DocumentType.AddressProof, Status = DocumentStatus.Approved }
                ],
                Address = new Address { City = "Rio de Janeiro" }
            };

            var exception = Assert.Throws<InvalidOperationException>(() => _spec.IsSatisfiedBy(restaurant));
            Assert.Equal("The restaurant must be located in São Paulo.", exception.Message);
        }

        [Fact]
        public void IsSatisfiedBy_AllConditionsSatisfied_ReturnsTrue()
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
                    new() { Type = DocumentType.AddressProof, Status = DocumentStatus.Approved }
                ],
                Address = new Address { City = "São Paulo" }
            };

            var result = _spec.IsSatisfiedBy(restaurant);
            Assert.True(result);
        }
    }
}
