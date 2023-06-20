using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShop.OrderAPI.Model;

public class BaseEntity
{
    [Key]
    [Column("id")]
    public long Id { get; set; }
}