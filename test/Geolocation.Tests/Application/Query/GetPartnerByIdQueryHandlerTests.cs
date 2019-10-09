using Geolocalization.Application.Query.Handlers;
using Geolocalization.Application.Query.Queries;
using Geolocalization.Domain.Entities;
using Geolocalization.Domain.Repositories;
using MongoDB.Bson;
using NSubstitute;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Geolocation.Tests.Application.Query
{
    public class GetPartnerByIdQueryHandlerTests
    {
        private readonly IPartnersRepository _repository;
        private readonly GetPartnerByIdQueryHandler _handler;

        public GetPartnerByIdQueryHandlerTests()
        {
            _repository = Substitute.For<IPartnersRepository>();
            _handler = new GetPartnerByIdQueryHandler(_repository);
        }

        [Fact]
        public async Task Handle_ShouldGetPartner()
        {
            // Arrange
            var id = ObjectId.GenerateNewId().ToString();
            var command = new GetPartnerByIdQuery(id);
            
            _repository.Get(id).Returns(default(Partner));

            // Act
            var response = await _handler.Handle(command, default(CancellationToken));

            // Assert
            await _repository.Received(1).Get(id);

        }
    }
}
