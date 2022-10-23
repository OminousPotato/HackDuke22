namespace SustainabotMVC.Models;

public class Review 
{
    public int Id {get; set;}
    public string? Company {get; set;}
    public string? Product {get; set;}
    public int Rating {get; set;}
    public string? Summary {get; set;}

    public string? ImgPath {get; set;}

}