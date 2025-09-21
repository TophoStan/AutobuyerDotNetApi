using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Data.Contracts;

public abstract class BaseEntity
{
    [Key]
    public Guid Id { get; set; }
}