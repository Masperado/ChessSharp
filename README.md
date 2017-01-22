# FerChess

Ovaj projekt je rađen za vještinu "Razvoj aplikacija u programskom jeziku C#" koja se održavala u zimskog semestru akademske godine 2016./2017. na FER-u. Radi se o web stranici koja će se upotrebljavati za potrebe šahovske sekcije na FER-u. Stranica predstavlja implementaciju igranja dopisnog šaha. Korisnici mogu otvoriti svoj profil, izazvati bilo kog od korisnika koji su registrirani na sustav, s njima odigrati po volji mnogo partija koje ostaju zapisane u bazi podataka te imaju svoj elo rejting koji raste, odnosno pada zavisno o perfomansu u odigranim partijama.

## Osnovni podatci

Stranica se nalazi na: http://ferchess.azurewebsites.net/ . Stranicu su izradili Josip Paulik (0036492647) i Josip Torić (0036491099).

### Zahtjevi za pregledavanje web stranice

Da biste uspješno otvorili web stranicu trebate imati web preglednik noviji od IE 8. (Chrome, Firefox, Edge, etc.)

### Ulazak na stranicu

Kada dođete na početnu stranicu vidjet ćete šahovsku ploču na kojoj možete pomicati figure i analizirati razne pozicije. U gornjem desnom kutu vam piše Register i Log in. Kada kliknete na Register otvori vam se forma gdje možete napraviti novi račun, potreban vam je samo e-mail. Ako uđete na Log in, možete se logirati u već postojeći račun ili ući na stranicu pomoću Facebooka ili Googlea.

### Vlastiti profil

Kada uđete na vlastiti profil možete vidjeti 5 kućica. U prvoj se nalazi popis svih korisnika na stranici kojima možete poslati zahtjev da igrate kao bijeli ili crni, u drugoj popis svih zahtjeva koji su vam poslali drugi korisnici, u trećoj popis svih vaših poslanih zahtjeva, u četvrtoj popis svih vaših aktivnih igara, a u petoj svih vaših završenih igara. Kada prihvatite zahtjev koji vam je poslao novi korisnik stvara se nova igra koja vam se prikaže kao novi redak u kućici aktivnih igara. U kućici sa aktivnim igrama možete stisnuti na play button koji vas vodi na stranicu gdje se održava dotična igra. U kućici sa završenim igrama možete kliknuti na View te vam se otvara stranica gdje se održavala dotična igra. Za potrebe testiranja slobodno napravite dva profila da možete sami sebi poslati zahtjev i odigrati partiju sami sa sobom da vidite kako radi stranica same igre.

### Stranica igre

Na stranici igre možete igrati samu partiju. Kada je vaš potez možete pomaknuti figuru na ploči. Nakon što pomaknete figuru morate kliknuti na Sumbit button da biste poslali potez u bazu podataka i dali potez vašem protivniku. Možete kliknuti i na Cancel button da bi ste poništili potez prije nego što kliknete Sumbit i odigrati novi potez. Vašem protivniku možete ponuditi remi kojeg protivnik može prihvatiti ili odbiti. Zahtjev za remi možete i sami ponišiti u bilo kojem trenutku. Dok traje prvi potez, možete napustiti igru bez da vam utječe na rejting. Nakon što prođe prvi potez(nakon što crni povuče svoj prvi potez) partiju možete predati u bilo kojem trenutku, naravno to se računa kao poraz. U donjem lijevom kutu piše čiji je porez, lista poteza, jeli ponuđen remi te status partije. 

### Osposobljavanje web stranice za rad

Da bi ste pokrenuli ovu web stranicu nakon što skinete cjelokupan kod trebate podesiti vlasitu bazu podataka prema modelima ove web stranice koje se nalaze u folderu "Models".

### Korištene tehnologije

* [ASP.Net Core](https://www.asp.net/core) - Korišteni web framework
* [Chessboard.js](http://chessboardjs.com/) - Implementacija šahovske ploče
* [Bootstrap](http://getbootstrap.com/) - Korišten css za dizajn stranice

## Autori

* **Josip Torić** - *Ideja web stranice i front end* - [Masperado](https://github.com/Masperado)
* **Josip Paulik** - *Back end i dizajn baze podatka* - [josip-paulik](https://github.com/josip-paulik)
