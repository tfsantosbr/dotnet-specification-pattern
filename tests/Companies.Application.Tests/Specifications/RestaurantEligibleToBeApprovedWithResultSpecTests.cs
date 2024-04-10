using Companies.Application.Entities;
using Companies.Application.Specifications;
using Xunit;

namespace Companies.Application.Tests
{
    public class RestaurantEligibleToBeApprovedWithResultSpecTests
    {
        private readonly RestaurantEligibleToBeApprovedWithResultSpec _spec;

        public RestaurantEligibleToBeApprovedWithResultSpecTests()
        {
            _spec = new RestaurantEligibleToBeApprovedWithResultSpec();
        }

        [Fact]
        public void IsSatisfiedBy_NoOperationalAdministrator_ReturnsFailure()
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
                    new() { Type = DocumentType.AddressProof, Status = DocumentStatus.Approved }
                ],
                Address = new Address { City = "São Paulo" }
            };

            var result = _spec.IsSatisfiedBy(restaurant);
            Assert.False(result.IsSuccess);
            Assert.Contains(RestaurantErrors.NoOperationalAdministrator, result.Notifications);
        }

        [Fact]
        public void IsSatisfiedBy_NoOperationalManager_ReturnsFailure()
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

            var result = _spec.IsSatisfiedBy(restaurant);
            Assert.False(result.IsSuccess);
            Assert.Contains(RestaurantErrors.NoOperationalManager, result.Notifications);
        }

        [Fact]
        public void IsSatisfiedBy_NoOperationalAttendant_ReturnsFailure()
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

            var result = _spec.IsSatisfiedBy(restaurant);
            Assert.False(result.IsSuccess);
            Assert.Contains(RestaurantErrors.NoOperationalAttendant, result.Notifications);
        }

        [Fact]
        public void IsSatisfiedBy_NoApprovedAddressProofDocument_ReturnsFailure()
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

            var result = _spec.IsSatisfiedBy(restaurant);

            Assert.False(result.IsSuccess);
            Assert.Contains(RestaurantErrors.NoApprovedAddressProofDocument, result.Notifications);
        }

        [Fact]
        public void IsSatisfiedBy_RestaurantNotLocatedInSaoPaulo_ReturnsFailure()
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

            var result = _spec.IsSatisfiedBy(restaurant);
            Assert.False(result.IsSuccess);
            Assert.Contains(RestaurantErrors.NotLocatedInSaoPaulo, result.Notifications);
        }

        [Fact]
        public void IsSatisfiedBy_AllConditionsSatisfied_ReturnsSuccess()
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
            Assert.True(result.IsSuccess);
        }
    }
}
