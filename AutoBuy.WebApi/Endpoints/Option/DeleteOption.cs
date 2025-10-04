using Data.Contracts;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace AutoBuy;

public record DeleteOptionRequest
{
    public Guid Id { get; set; }
}

public class DeleteOption : Endpoint<DeleteOptionRequest>
{
    private readonly IAutoBuyDbContext _context;

    public DeleteOption(IAutoBuyDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Delete("/options/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteOptionRequest req, CancellationToken ct)
    {
        var option = await _context.Options.FirstOrDefaultAsync(o => o.Id == req.Id, ct);

        if (option == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        _context.Options.Remove(option);
        await _context.SaveChangesAsync(ct);

        await Send.NoContentAsync(ct);
    }
}