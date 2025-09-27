namespace AutobuyerPlayer.Contracts;

public record ExecutePlaywrightResponse
{
    public required string Url { get; set; }
}