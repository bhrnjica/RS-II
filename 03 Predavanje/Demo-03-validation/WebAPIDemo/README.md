# Implementacija Validacije podataka - Web API sa APS.NET Core 

Osnovna ideja MVC dizajna jeste DRY (don't repeat yourself). MVC ima ugrađeni mehanizam prilikom validacije 
podataka te se često postavljaju u obliku atributa na clasu i njene članove.

Primjer validacije će se implementirati preko klase `movie`.
- Kao prvo potrebno je `StudentControlles` preimenovati u `MovieController`
- Kada je implementiran MoviewKontrollers potrebno je implementirati `Movie model`.
- Napraviti novi folder u projektu `Movie`
- Implemntirati sljedeću klasu:

```cs
public class Movie
{
    public int Id { get; set; }

    [StringLength(60, MinimumLength = 3)]
    [Required]
    public string Title { get; set; }

    [Display(Name = "Release Date")]
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }

    [Range(1, 100)]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
    [Required]
    [StringLength(30)]
    public string Genre { get; set; }

    [StringLength(5)]
    [Required]
    public string Rating { get; set; }
}
```





