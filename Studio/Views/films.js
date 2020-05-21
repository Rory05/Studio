const urif = 'api/Films';
let films = [];

function getFilms() {
    fetch(urif)
        .then(response => response.json())
        .then(data => _displayFilms(data))
        .catch(error => console.error('Unable to get films.', error));
}
function getFilm(id) {
    fetch(`${urif}/${id}`, {
        method: 'GET'
    })
        .then(() => getFilms())
        .catch(error => console.error('Unable to delete genre.', error));
}

function _displayFilms(data) {
    const tBody = document.getElementById('films');
    tBody.innerHTML = '';

    let tr1 = tBody.insertRow();
    let td1 = tr1.insertCell(0);
    let textNode = document.createTextNode(films.name);
    td1.appendChild(textNode);

    data.forEach(film => {

        

        let tr2 = tBody.insertRow();
        let td1 = tr2.insertCell(0);
        let textNode = document.createTextNode(film.year);
        td1.appendChild(textNode);

        let tr3 = tBody.insertRow();
        let td1 = tr3.insertCell(0);
        let textNode = document.createTextNode(film.duration);
        td1.appendChild(textNode);

    });

    genres = data;
}
