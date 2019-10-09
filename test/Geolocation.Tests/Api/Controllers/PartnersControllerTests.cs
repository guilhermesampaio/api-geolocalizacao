using FluentAssertions;
using Geolocalization.Api.Controllers;
using Geolocalization.Application.Command.Commands;
using Geolocalization.Application.Query.Queries;
using Geolocalization.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace Geolocation.Tests.Api.Controllers
{
    public class PartnersControllerTests
    {
        private readonly IMediator _mediator;
        private readonly PartnersController _controller;
        private const string TradingName = "Some name";
        private const string OwnerName = "Some Owner Name";
        private const string Document = "Some Document";
        private const Geolocalization.Domain.Entities.MultiPolygon CoverageArea = default(Geolocalization.Domain.Entities.MultiPolygon);
        private const Geolocalization.Domain.Entities.Point Address = default(Geolocalization.Domain.Entities.Point);

        public PartnersControllerTests()
        {
            _mediator = Substitute.For<IMediator>();
            _controller = new PartnersController(_mediator);
        }

        [Fact]
        public async Task Create_WhenRequestInvalid_ShouldReturnBadRequest()
        {
            // Arrange
            var request = new CreatePartnerCommand();
            _controller.ModelState.AddModelError("any error", "any message");

            // Act
            var response = await _controller.Create(request);

            // Assert
            response.Should().NotBeNull().And.BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Create_WhenRequestIsValid_ShouldReturnCreatedAtActionResult()
        {
            // Arrange
            var request = new CreatePartnerCommand();
            Partner partner = CreatePartner();
            _mediator.Send(Arg.Any<CreatePartnerCommand>()).Returns(partner);

            // Act
            var response = await _controller.Create(request);

            // Assert
            response.Should().NotBeNull().And.BeOfType<CreatedAtActionResult>();
            var createdAtActionResponse = response as CreatedAtActionResult;
            createdAtActionResponse.ActionName.Should().Be(nameof(_controller.GetById));
        }

        private static Partner CreatePartner()
        {
            return new Partner(TradingName, OwnerName, Document, CoverageArea, Address);
        }

        [Fact]
        public async Task GetById_WhenPartnerDoesNotExists_ShouldReturnNotFound()
        {
            // Arrange
            var id = ObjectId.GenerateNewId().ToString();
            _mediator.Send(Arg.Any<GetPartnerByIdQuery>()).Returns(default(Partner));

            // Act
            var response = await _controller.GetById(id);

            // Assert
            response.Should().NotBeNull().And.BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task GetById_WhenPartnerExists_ShouldReturnOk()
        {
            // Arrange
            var id = ObjectId.GenerateNewId().ToString();
            var partner = CreatePartner();
            _mediator.Send(Arg.Any<GetPartnerByIdQuery>()).Returns(partner);

            // Act
            var response = await _controller.GetById(id);

            // Assert
            response.Should().NotBeNull().And.BeOfType<OkObjectResult>();
            var okObjectResult = response as OkObjectResult;
            okObjectResult.Value.Should().BeOfType<Partner>();
            var partnerResult = okObjectResult.Value as Partner;
            partnerResult.TradingName.Should().Be(TradingName);
        }


        [Fact]
        public async Task GetByCoordinates_WhenPartnerDoesNotExists_ShouldReturnNotFound()
        {
            // Arrange
            var request = new CoordinatesRequest() { Latitude = 10, Longitude = 30 };
            _mediator.Send(Arg.Any<GetPartnerByCoordinatesQuery>()).Returns(default(Partner));

            // Act
            var response = await _controller.GetByCoordinates(request);

            // Assert
            response.Should().NotBeNull().And.BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task GetByCoordinates_WhenPartnerExists_ShouldReturnOk()
        {
            // Arrange            
            var partner = CreatePartner();
            var request = new CoordinatesRequest() { Latitude = 10, Longitude = 30 };
            _mediator.Send(Arg.Any<GetPartnerByCoordinatesQuery>()).Returns(partner);

            // Act
            var response = await _controller.GetByCoordinates(request);

            // Assert
            response.Should().NotBeNull().And.BeOfType<OkObjectResult>();
            var okObjectResult = response as OkObjectResult;
            okObjectResult.Value.Should().BeOfType<Partner>();
            var partnerResult = okObjectResult.Value as Partner;
            partnerResult.TradingName.Should().Be(TradingName);
        }
    }
}
