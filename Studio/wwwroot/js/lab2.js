const uri = 'api/Genres';
const uriF = 'api/Films';
const uriA = 'api/Actors'
const uriFG = 'api/FilmGenres'
let genres = [];
let films = [];
let actors = [];
let filmgenres = [];


///// список жанров
function getGenres() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayGenres(data))
        .catch(error => console.error('Unable to get genres.', error));
}
function _displayGenres(data) {
    var gen = document.getElementById("selectgenre");
    data.forEach(genre => {
        var genr = document.createElement('option');
        genr.appendChild(document.createTextNode(genre.name));
        gen.appendChild(genr);
    });

    genres = data;
}

function getFilmGenres() {
    var genreName = document.getElementById('selectgenre').value;
    fetch(uriF + `?genreName=${genreName}`)
        .then(response => response.json())
        .then(data => _displayFilms(data))
        .catch(error => console.error('Unable to get films.', error));
}

////// список актеров
function getActors() {
    fetch(uriA)
        .then(response => response.json())
        .then(data => _displayActors(data))
        .catch(error => console.error('Unable to get actors.', error));
}
function _displayActors(data) {
    var ac = document.getElementById("selectactor");
    //ac.appendChild(document.createElement('option').appendChild(document.createTextNode("Выберите жанр")));
    data.forEach(actor => {
        var act = document.createElement('option');
        act.appendChild(document.createTextNode(actor.name));
        ac.appendChild(act);
    });

    actors = data;
}
function _displayActor(data) {
    const tBody = document.getElementById('actor');
    tBody.innerHTML = '';
    data.forEach(actor => {

        tBody.innerHTML += `<div class="divform1"><dl><dt class="name">${actor.name}</dt>
                      <img src="img/${actor.img}" class="img1" >
                      <a class="alltext">Возраст:   ${2020- actor.date} лет</a><br/>
                      <a class="alltext">Количество фильмов с участием:   ${actor.filmNumber}</a><br/>
                      <a class="alltext">Первый фильм:   ${actor.firstFilm}</a><dl></div>`;


    });

    actors = data;
}
function getFilmActors() {
    var actorName = document.getElementById('selectactor').value;
    fetch(uriF + `?actorName=${actorName}`)
        .then(response => response.json())
        .then(data => _displayFilms(data))
        .catch(error => console.error('Unable to get films.', error));
    fetch(uriA + `?actorName=${actorName}`)
        .then(response => response.json())
        .then(data => _displayActor(data))
        .catch(error => console.error('Unable to get actor', error));
}
/////// вывод фильмов
function getFilms() {
    fetch(uriF)
        .then(response => response.json())
        .then(data => _displayFilms(data))
        .catch(error => console.error('Unable to get films.', error));
}

function _displayFilms(data) {
    const tBody = document.getElementById('films');
    tBody.innerHTML = '';
    data.forEach(film => {
        
        var text = `<div class="divform"><dl><dt class="name">${film.name}</dt>
                    <img src="img/${film.img}" class="img" >
                    <div><a class="alltext">Длительность:   ${film.duration}</a><br/>
                    <a class="alltext">Жанр(ы):   `;

        film.genres.forEach(genre => {
            text += genre + ', ';
        });
        text = text.slice(0, -2);
        text += '</a><br/>';
        text += '<a class="alltext">Акртёр(ы):   ';
        film.actors.forEach(actor => {
            text += actor + ', ';
        });
        text = text.slice(0, -2);

        text += `</a><br/>
                  <a class="alltext">Год премьеры:   ${film.year}</a><br/>
                  <a class="alltext">Возраст:   ${film.age}+</a>            </div><dl></div>`;
        tBody.innerHTML += text;

        
    });
    films = data;
}

function clearActor() {
    const tBody1 = document.getElementById('actor');
    tBody1.innerHTML = '';
}

function getGenresForFilms() {
    var id = document.getElementById('selectgenre').value;
    fetch(uriG + `?id=${id}&shoto=darova`)
        .then(response => response.json())
        .then(data => _displayGenresForFilms(data))
        .catch(error => console.error('Unable to get genres.', error));
}
function _displayGenresForFilms(data) {
    const a = document.getElementById("selectgenre");
    //a +=`<a class="alltext">Genre:   </a>`
    data.forEach(genre => {
        a += `  ${genre.name} `;
    });
    a += `</br>`;

    genres = data;
}


//Get the button:
mybutton = document.getElementById("myBtn");

// When the user scrolls down 20px from the top of the document, show the button
window.onscroll = function () { scrollFunction() };

function scrollFunction() {
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
        mybutton.style.display = "block";
    } else {
        mybutton.style.display = "none";
    }
}

function topFunction() {
    document.documentElement.style.transition = 7;
    document.documentElement.scrollTop = 0; 
}