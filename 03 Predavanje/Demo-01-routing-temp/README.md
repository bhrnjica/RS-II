# Implementacija routing šablona - Web API sa APS.NET Core 

Demo primjer prikazuje na koji način se mogu koristiti ruting šabloni da se Web API
 učuni pristupačnijim, sigurnijim i pouzdanijim.

- Ovaj primjer prikazuje implementaciju različitih ruting sablona na primjeru `Movie` klase.
- U svrhu implementacije potrebno je generirati `Movie` i `MoviewController` klase.
- `Movie` model clasa implementira se u folderu `Models`, sa implementacijom kao na sljedećem primjeru.
- U `Startup` klasi potrebno je dodati MVC komponentu, kao prethodno.

```cs
public class Movie
{
    public int Id { get; set; }

    public string Title { get; set; }

    public DateTime ReleaseDate { get; set; }

    public decimal Price { get; set; }

    public string Genre { get; set; }

    public string Rating { get; set; }
}
```
- `MoviewController` klasa generira se u folderu `Controllers`, na sličan način kako je to prethodno objašnjeno sa `Student` kontrolerom.
- Kada se generira klasa, potrebno je implemntirati listu moview objekata na sljedeći način:

```cs
static List<Movie> m_Movies = generateMovies();

//generiranje početne baze filmova
private static List<Movie> generateMovies()
{
    var lst = new List<Movie>()
    {
        new Movie(){ Id=1, Genre="Action", Price=12.50m, Rating="****", ReleaseDate= new DateTime(1984, 12,12), Title="Terminator "},
        new Movie(){ Id=2, Genre="Comedy", Price=11.50m, Rating="*****", ReleaseDate= new DateTime(1984, 12,12), Title="Glup glupllji "},
        new Movie(){ Id=3, Genre="Sci", Price=12.50m, Rating="****", ReleaseDate= new DateTime(1999, 12,12), Title="Matrix "},
        new Movie(){ Id=4, Genre="Domestic", Price=12.50m, Rating="****", ReleaseDate= new DateTime(1984, 12,12), Title="Ničija zemlja"},
    };

    return lst;
}
```
Implementacija GET, POST, PU i DELETE akcija:

```cs
[HttpGet]
public IEnumerable<Movie> Get()
{
    return m_Movies;
}
public Movie Get(int id)
{
    return m_Movies.Where(x=>x.Id==id).FirstOrDefault();
}
[HttpPost]
public void Post([FromBody] Movie movie)
{
    m_Movies.Add(movie);
}

[HttpPut("{id}")]
public void Put(int id, [FromBody] Movie moview)
{
    var m  =  m_Movies.Where(x => x.Id == id).FirstOrDefault();

    if (m != null)
    {
        m.Genre = moview.Genre;
        m.Price = moview.Price;
        m.Rating = moview.Rating;
        m.ReleaseDate = moview.ReleaseDate;
        m.Title = moview.Title;
    }
}

[HttpDelete("{id}")]
public void Delete(int id)
{
    m_Movies.RemoveAll(x=>x.Id==id);
}
```
Osnovna implementacija kontrolera, daje nam mogućnost prikaza liste filmova, prikaz selektovanog fila, kao i dodavanje novog, promjena postojeceg i brisanje filma u listi. 
Pomoću šablona ruta moguće je prošititi osnovne akcije, sa većom mogućnošću prikaza, filtriranja, i td.

