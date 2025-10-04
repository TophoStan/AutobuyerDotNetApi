using Data.Contracts;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace AutoBuy;

public record GetOptionsResponse
{
    public List<OptionDto> Options { get; set; } = [];
}

public class GetOptions : EndpointWithoutRequest<GetOptionsResponse>
{
    private readonly IAutoBuyDbContext _context;

    public GetOptions(IAutoBuyDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Get("/options");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var options = await _context.Options.ToListAsync(ct);

        var response = options.Select(o => new OptionDto
        {
            Id = o.Id,
            Name = o.Name,
            Values = o.Values,
            ProductEntityId = o.ProductEntityId
        }).ToList();

        await Send.OkAsync(new GetOptionsResponse { Options = response }, ct);
    }
}
