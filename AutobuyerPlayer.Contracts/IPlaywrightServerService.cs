namespace AutobuyerPlayer.Contracts;

public interface IPlaywrightServerService
{
    public Task<ExecutePlaywrightResponse?> Execute(ExecutePlaywrightRequest request);
}