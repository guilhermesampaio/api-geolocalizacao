using FluentValidation;
using Geolocalization.Application.Command.Commands;
using System;
using System.Linq;

namespace Geolocalization.Api.Validators
{
    public class PartnerValidator : AbstractValidator<CreatePartnerCommand>
    {
        public PartnerValidator()
        {
            RuleFor(x => x.Document).NotEmpty();
            RuleFor(x => x.OwnerName).NotEmpty();
            RuleFor(x => x.TradingName).NotEmpty();
            RuleFor(x => x.CoverageArea).NotNull().Must(it => ValidateCoverageArea(it));
            RuleFor(x => x.Address).NotNull().Must(it => ValidateAddress(it));
        }

        private static bool ValidateAddress(Point point)
        {
            return string.Compare(point.Type, "point", StringComparison.InvariantCultureIgnoreCase) == 0 && point.Coordinates.Any();
        }

        private static bool ValidateCoverageArea(MultiPolygon multipolygon)
        {
            return string.Compare(multipolygon.Type, "multipolygon", StringComparison.InvariantCultureIgnoreCase) == 0 && multipolygon.Coordinates.Any();
        }
    }
}
