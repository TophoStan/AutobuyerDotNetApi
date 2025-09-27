namespace AutobuyerPlayer.Contracts;

public record ExecutePlaywrightRequest
{
    public required ICollection<string> Lines = [];
}