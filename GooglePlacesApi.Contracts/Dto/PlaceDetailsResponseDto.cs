namespace GooglePlacesApi.Contracts;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public class PlaceDetailsResponseDto
{
        public List<object> HtmlAttributions { get; set; } = new();

        public PlaceResultDto Result { get; set; } = new();

        public string Status { get; set; } = string.Empty;
}

public class PlaceResultDto
{
        public List<AddressComponentDto> AddressComponents { get; set; } = new();
}

public class AddressComponentDto
{
        public string LongName { get; set; } = string.Empty;

        public string ShortName { get; set; } = string.Empty;

        public List<string> Types { get; set; } = new();
}
