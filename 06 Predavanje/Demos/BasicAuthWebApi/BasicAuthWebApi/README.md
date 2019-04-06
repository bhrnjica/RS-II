# Primjer implementacije zaštite Web API sa BasicAuthentication

Primjer predstavlja klasican pristup zaštite Web API koristeći username, password.
Primjer se sastoji od dva kontrolera:

- ``UnSecureController`` - koji predstavlja kontroler bez zaštite i kojem mogu pristupitu svi korisnici.
- ``SecureController`` - predstavlja kontroler sa implementacijom zaštite koja se bazira na ``BasicAuthentication`

``SecureController`` - sadrži akcije kojim mogu pristupati korisnici različitih prava ``roles``. 

## Zaštita kontrolera

Zaštita kontrolera može se realizirati na nekoliko načina, a u svim slučajevim to radimo pomoću atributa. Zaštita se provodi na:

1. nivou kontrolera (klase)
2. nivou akcije (metode)

Da bi se implementirala zaštita na nivou kontrolera, potrebno je nad klasom kontrolera postaviti atribut ``[Authorize]``. Kada je postavljen ovaj atribut
kontroleru mogu prstupati samo autorizovani (logirani) korisnici. U slučaju da u zaštićenom kontroleru potrebno obezbjediti 
da odredjena akcija bude dostupa i anonimnim korisnicima, u tom slučaju takvu metodu je potrebno dekorirati atributom ``[AllowAnonymous]``.

Ukoliko u ne zaštićenom kontroleru trebamo zaštiti metodu, istu možemo zaštiti ako postavimo atribut ``[Authorize]`` iznad metoda. 

# Implementacija 'BasicAuthentication' u ASP.NET Core Web API
Implementacija zaštite započenje sa dodavanjem ``services.AddAuthentication`` i ``app.UseAuthentication`` u startup datoteci. 
Prije same implementacije potrebno je definisati sljedeće tipove:

 - ``User`` - klasa koja sadrži informacije o korisniku (login, password, roles, .....)
 - ``Role`` - klasa koja sadrži informacije o rolama odnosno nivoima zaštite. Za ovaj primjer implementirana su 
dva nivoa zaštite: ``Admin`` i ``User``. 
 Kada želimo da nekoj akciji (metodi) u kontroleru pristupa samo korisnik sa specifičnom rolom, 
potrebno je akciju (metodu) deklarisati sa ``[Authorize(Roles = Role.Admin)]``
- ``IUserService`` - interfejs koji deklariše process autentifikacije,
- ``UserService`` - klasa koja implementira interfejs odnosno proces autentifikacije u kojoj su pohranjeni korisnici i njihove lozinke i role. Ovo obično se definise u nekoj bazi podataka sa ekriptovanom lozinkom.
- ``BasicAuthenticationHandler`` -klasa koja je izvedena iz osnovne APS.NET core klase ``AuthenticationHandler<AuthenticationSchemeOptions>`` a definiše proces autentifikacije

# Implementacija SecureController
- Sada kada imamo implementirate gornje komponente, implementacija zaštite na kontroleru jednostavno se implementira pomoću atributa kojim dekorišemo klasu i metode.

## ASP.NET Core Basic Authentication Handler
"Basic authentication handler" je asp.net core middleware preko kojeg se upravlja sa zahtjevima za autentifikaciju, a izveden je iz asp.net core AuthenticationHandler bazne klase i preklapanjem HandleAuthenticateAsync() metode.
Logika za "Basic authentication" je implementirana unutar HandleAuthenticateAsync() metode preko verifikacije username i password koji su dobijeni preko ``HTTP Authorization`` hedera, a verifikacija se završava pozivom _userService.Authenticate(username, password).
U slučaju uspješne autentifikacije vraća se ``AuthenticateResult.Success(ticket)`` koji predstavlja autentifikovani zahtjev i postavlja 
``HttpContext.User`` kao tekućeg logiranog korisnika. "Basic authentication middleware" se konfiguriše u aplikaciji unutar ``ConfigureServices(IServiceCollection services)`` metode.



