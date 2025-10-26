namespace GooglePlacesApi.Contracts;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public class PlaceDetailsResponseDto
{
        [JsonPropertyName("html_attributions")]
        public List<object> HtmlAttributions { get; set; } = new();

        [JsonPropertyName("result")]
        public PlaceResultDto Result { get; set; } = new();

        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
}

public class PlaceResultDto
{
        [JsonPropertyName("address_components")]
        public List<AddressComponentDto> AddressComponents { get; set; } = new();
}

public class AddressComponentDto
{
        [JsonPropertyName("long_name")]
        public string LongName { get; set; } = string.Empty;

        [JsonPropertyName("short_name")]
        public string ShortName { get; set; } = string.Empty;

        public List<string> Types { get; set; } = new();
}
