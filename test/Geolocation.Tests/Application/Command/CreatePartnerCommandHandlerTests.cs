using Geolocalization.Application.Command.Commands;
using Geolocalization.Application.Command.Handlers;
using Geolocalization.Domain.Entities;
using Geolocalization.Domain.Repositories;
using MongoDB.Bson;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Geolocation.Tests.Application.Command
{
    public class CreatePartnerCommandHandlerTests
    {
        private readonly IPartnersRepository _repository;
        private readonly CreatePartnerCommandHandler _handler;

        public CreatePartnerCommandHandlerTests()
        {
            _repository = Substitute.For<IPartnersRepository>();
            _handler = new CreatePartnerCommandHandler(_repository);
        }

        [Fact]
        public async Task Handle_ShouldCreatePartner()
        {
            // Arrange
            var command = new CreatePartnerCommand()
            {
                Address = new Geolocalization.Application.Command.Commands.Point(),
                CoverageArea = new Geolocalization.Application.Command.Commands.MultiPolygon()
            };
            var generatedId = ObjectId.GenerateNewId().ToString();
            _repository.Create(Arg.Any<Partner>()).Returns(generatedId);

            // Act
            var response = await _handler.Handle(command, default(CancellationToken));

            // Assert
            await _repository.Received(1).Create(Arg.Any<Partner>());

        }
    }
}
