namespace GooglePlacesApi.Contracts;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class PlacesResponseDto
{
        public List<PredictionDto> Predictions { get; set; } = new();

        public string Status { get; set; } = string.Empty;
}

public class PredictionDto
{
        public string Description { get; set; } = string.Empty;

        public List<MatchedSubstringDto> MatchedSubstrings { get; set; } = new();

        [JsonPropertyName("place_id")]
        public string PlaceId { get; set; } = string.Empty;

        public string Reference { get; set; } = string.Empty;

        public StructuredFormattingDto StructuredFormatting { get; set; } = new();

        public List<TermDto> Terms { get; set; } = new();

        public List<string> Types { get; set; } = new();
}

public class MatchedSubstringDto
{
        public int Length { get; set; }

        public int Offset { get; set; }
}

public class TermDto
{
        public int Offset { get; set; }

        public string Value { get; set; } = string.Empty;
}

public class StructuredFormattingDto
{
        public string MainText { get; set; } = string.Empty;

        public List<MatchedSubstringDto> MainTextMatchedSubstrings { get; set; } = new();

        public string SecondaryText { get; set; } = string.Empty;
}
