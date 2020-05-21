const uri = 'api/Genres';
const uriF = 'api/Films';
const uriFG = 'api/FilmGenres'
let genres = [];
let films = [];
let filmgenres = [];

function getGenres() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayGenres(data))
        .catch(error => console.error('Unable to get genres.', error));
}

function addGenre() {
    const addNameTextbox = document.getElementById('add-name');

    const genre = {
        name: addNameTextbox.value.trim(),
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(genre)
    })
        .then(response => response.json())
        .then(() => {
            getGenre();
            addNameTextbox.value = '';
        })
        .catch(error => console.error('Unable to add genre.', error));
}

function deleteGenre(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getGenres())
        .catch(error => console.error('Unable to delete genre.', error));
}

function displayEditForm(id) {
    const genre = genres.find(genre => genre.id === id);

    document.getElementById('edit-id').value = genre.id;
    document.getElementById('edit-name').value = genre.name;
    document.getElementById('editForm').style.display = 'block';
}

function updateGenre() {
    const genreId = document.getElementById('edit-id').value;
    const genre = {
        id: parseInt(genreId, 10),
        name: document.getElementById('edit-name').value.trim()
    };

    fetch(`${uri}/${genreId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(genre)
    })
        .then(() => getGenres())
        .catch(error => console.error('Unable to update genre.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}


function _displayGenres(data) {
    var gen = document.getElementById("selectgenre");
    data.forEach(genre => {
        var genr = document.createElement('option');
        genr.appendChild(document.createTextNode(genre.name));
        gen.appendChild(genr);
    });

    genres = data;
    //const tBody = document.getElementById('genres');
    
    //data.forEach(genre => {
    //    tBody.innerHTML += `<tr><td>${genre.name}</td><td><button onclick='deleteGenre(${genre.id})'>Delete </button></td></tr>`;
    //});

    //genres = data;
}

function getFilmGenres() {
    var genreName = document.getElementById('selectgenre').value;
    fetch(uriF + `?genreName=${genreName}`)
        .then(response => log(response.text()))
        .then(response => response.json())
        .then(data => _displayFilms(data))
        .catch(error => console.error('Unable to get genres.', error));
}
function getFilms() {
    fetch(uriF)
        .then(response => response.json())
        .then(data => _displayFilms(data))
        .catch(error => console.error('Unable to get films.', error));
}

function _displayFilms(data) {
    const tBody = document.getElementById('films');
    data.forEach(film => {
        tBody.innerHTML += `<tr><td>${film.name}</td></tr>`;
        films = data;
    })
}
//function _displayFilms(data) {
//    const tBody = document.getElementById('films');

//    data.forEach(film => {
//        tBody.innerHTML += `<tr><td>${film.name}</td></tr>`;
//    });

//    films = data;
//}
