using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace CommerceApp.Models;

public class Product
{
[Key]
public int Id { get; set; }
[Required]
public string? Title { get; set; }
public string? Description {get; set;}
[Required]
public string? ISBN {get; set;}
public string? Author {get; set;}
[Required]
[Display(Name ="List Price")]
[Range(1,10000)]
public int ListPrice {get; set;}
[Required]
[Display(Name ="Price for 1-50")]
[Range(1,10000)]
public int Price  {get; set;}
[Required]
[Display(Name ="Price for 50+")]
[Range(1,10000)]
public int Price50 {get; set;}
[Required]
[Display(Name ="Price for 100+")]
[Range(1,10000)]
public int  Price100 {get; set;}

public int CategoryId { get; set; }
[ForeignKey("CategoryId")]
public Category? Category { get; set; }
public string? ImageURL { get; set; }
}
