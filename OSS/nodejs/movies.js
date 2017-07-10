var bodyParser = require('body-parser');
var express = require('express');
var router = express.Router();
router.use(bodyParser.json());
router.use(bodyParser.urlencoded());

var movies = require('./movies.json')

router.get('/', function (req, res) {
    res.json(movies);
});

router.get('/:id', function (req, res) {
    var id = +req.params.id;

    var movie = movies.filter(m => m.id === id).pop();

    res.json(movie);
});

router.post('/', function (req, res) {
    var movie = req.body;
    movie.id = movie.id || Date.now();
    movies.unshift(movie);

    var fullUrl = req.protocol + '://' + req.get('host') + req.originalUrl;
    res.location(fullUrl + '/' + movie.id);

    res.statusCode = 201;
    res.end();
});

router.put('/:id', function (req, res) {
    var newMovie = req.body;
    var id = +req.params.id;

    var oldMovie = movies.filter(m => m.id === id).pop();
    Object.assign(oldMovie, newMovie);

    res.statusCode = 204;
    res.end();
});

router.delete('/:id', function (req, res) {
    var id = +req.params.id;

    movies = movies.filter(m => m.id !== id);

    res.statusCode = 204;
    res.end();
});

module.exports = router;