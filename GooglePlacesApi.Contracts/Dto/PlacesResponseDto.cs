namespace GooglePlacesApi.Contracts;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class PlacesResponseDto
{
    [JsonPropertyName("predictions")]
    public List<PredictionDto> Predictions { get; set; } = new();

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;
}

public class PredictionDto
{
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("matched_substrings")]
    public List<MatchedSubstringDto> MatchedSubstrings { get; set; } = new();

    [JsonPropertyName("place_id")]
    public string PlaceId { get; set; } = string.Empty;

    [JsonPropertyName("reference")]
    public string Reference { get; set; } = string.Empty;

    [JsonPropertyName("structured_formatting")]
    public StructuredFormattingDto StructuredFormatting { get; set; } = new();

    [JsonPropertyName("terms")]
    public List<TermDto> Terms { get; set; } = new();

    [JsonPropertyName("types")]
    public List<string> Types { get; set; } = new();
}

public class MatchedSubstringDto
{
    [JsonPropertyName("length")]
    public int Length { get; set; }

    [JsonPropertyName("offset")]
    public int Offset { get; set; }
}

public class TermDto
{
    [JsonPropertyName("offset")]
    public int Offset { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;
}

public class StructuredFormattingDto
{
    [JsonPropertyName("main_text")]
    public string MainText { get; set; } = string.Empty;

    [JsonPropertyName("main_text_matched_substrings")]
    public List<MatchedSubstringDto> MainTextMatchedSubstrings { get; set; } = new();

    [JsonPropertyName("secondary_text")]
    public string SecondaryText { get; set; } = string.Empty;
}
