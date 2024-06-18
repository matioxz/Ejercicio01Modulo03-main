using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EjercicioClase1Modulo3.Controllers
{
    [Route("v1/libros")]
    [ApiController]
    public class BookController : ControllerBase
    {
        //Books contiene una lista de libros. Esta información viene del archivo libros.json ubicado dentro de la carpeta Data.
        public List<Book> Books { get; set; }

        //filePath contiene la ubicación del archivo libros.json. No mover el archivo libros.json de esa carpeta.
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data\libros.json");

        public BookController()
        {
            //Instanciación e inicialización de la lista de libros deserializando el archivo libros.json
            Books = JsonSerializer.Deserialize<List<Book>>(System.IO.File.ReadAllText(filePath));
        }

        #region Ejercicio 1
        // Crear un endpoint que liste todos los libros
        [HttpGet]
        public ActionResult<List<Book>> GetBooks()
        {
            return Ok(Books);
        }
        #endregion

        #region Ejercicio 2
        // Crear un endpoint para Obtener un libro por su número de id usando route parameters
        [HttpGet("{id}")]
        public ActionResult<Book> GetBookById(int id)
        {
            var book = Books.FirstOrDefault(b => b.id == id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }
        #endregion

        #region Ejercicio 3
        // Crear un endpoint para listar todos libros de un género en particular usando route parameters
        [HttpGet("genero/{genero}")]
        public ActionResult<List<Book>> GetBooksByGenero(string genero)
        {
            var books = Books.Where(b => b.genero.Equals(genero, StringComparison.OrdinalIgnoreCase)).ToList();
            if (books.Count == 0)
            {
                return NotFound();
            }
            return Ok(books);
        }
        #endregion

        #region Ejercicio 4
        // Crear un endpoint para listar todos los libros de un autor usando query parameters
        [HttpGet("autor")]
        public ActionResult<List<Book>> GetBooksByAuthor([FromQuery] string autor)
        {
            var booksByAuthor = Books.Where(b => b.autor.ToLower().Contains(autor.ToLower())).ToList();
            if (booksByAuthor.Count == 0)
            {
                return NotFound();
            }
            return Ok(booksByAuthor);
        }
        #endregion

        #region Ejercicio 5
        // Crear un endpoint para listar unicamente todos los géneros de libros disponibles
        [HttpGet("generos")]
        public ActionResult<List<string>> GetGeneros()
        {
            var generos = Books.Select(b => b.genero).Distinct().ToList();
            return Ok(generos);
        }
        #endregion

        #region Ejercicio 6
        // Crear un endpoint para listar todos los libros implementando paginación usando route parameters
        [HttpGet("pagina")]
        public ActionResult<List<Book>> GetBooksPaginated([FromQuery] int pagina, [FromQuery] int cantidad)
        {
            var paginatedBooks = Books.Skip((pagina - 1) * cantidad).Take(cantidad).ToList();
            if (paginatedBooks.Count == 0)
            {
                return NotFound();
            }
            return Ok(paginatedBooks);
        }
        #endregion
    }
}

