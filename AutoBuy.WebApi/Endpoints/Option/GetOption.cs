using Data.Contracts;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace AutoBuy;

public record GetOptionRequest
{
    public Guid Id { get; set; }
}

public class GetOption : Endpoint<GetOptionRequest, OptionDto>
{
    private readonly IAutoBuyDbContext _context;

    public GetOption(IAutoBuyDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Get("/options/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetOptionRequest req, CancellationToken ct)
    {
        var option = await _context.Options.FirstOrDefaultAsync(o => o.Id == req.Id, ct);

        if (option == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        var response = new OptionDto
        {
            Id = option.Id,
            Name = option.Name,
            Values = option.Values,
            ProductEntityId = option.ProductEntityId
        };

        await Send.OkAsync(response, ct);
    }
}