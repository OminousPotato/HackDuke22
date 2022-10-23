using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SustainabotMVC.Models;

public class ReviewImgFile 
{
    public int Id {get; set;}
    public string? Company {get; set;}
    public string? Product {get; set;}
    [Range(1, 5)]
    public int Rating {get; set;}
    public string? Summary {get; set;}

    [NotMapped]
    public IFormFile ImgFile { get; set; }  

}