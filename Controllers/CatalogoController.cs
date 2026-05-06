using Catalogo.Models;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo.Controllers
{
    public class CatalogoController : Controller
    {
        // Lista estática: Se mantiene en memoria mientras la app esté corriendo
        private static List<Item> _items = new()
        {
            new Item {
                Id = 1,
                Titulo = "Devil May Cry",
                Genero = "Hack and Slash",
                Ano = 2001,
                Consola = "PlayStation 2",
                Descripcion = "Videojuego que trata de un cazador de demonios llamado Dante."
            },
            new Item {
                Id = 2,
                Titulo = "Castlevania: Symphony of the Night",
                Genero = "Metroidvania",
                Ano = 1997,
                Consola = "PlayStation 1",
                Descripcion = "Alucard explora el castillo de Drácula en esta obra maestra lateral."
            },
            new Item {
                Id = 3,
                Titulo = "NieR: Automata",
                Genero = "Action RPG",
                Ano = 2017,
                Consola = "PlayStation 4",
                Descripcion = "Una guerra entre androides y máquinas en un futuro distópico."
            },
            new Item {
                Id = 4,
                Titulo = "Hollow Knight",
                Genero = "Metroidvania",
                Ano = 2017,
                Consola = "PC / Nintendo Switch",
                Descripcion = "Explora un reino de insectos en ruinas lleno de secretos y jefes desafiantes."
            },
            new Item {
                Id = 5,
                Titulo = "God of War",
                Genero = "Acción-Aventura",
                Ano = 2018,
                Consola = "PlayStation 4",
                Descripcion = "Kratos y su hijo Atreus viajan por las tierras de la mitología nórdica."
            }
        };

        // GET: Catalogo/Index
        public IActionResult Index(string? genero)
        {
            // Filtrado eficiente
            var resultado = string.IsNullOrEmpty(genero)
                ? _items
                : _items.Where(i => i.Genero != null && i.Genero.Equals(genero, StringComparison.OrdinalIgnoreCase)).ToList();

            // Pasamos la lista de géneros únicos para los botones del filtro
            ViewBag.Generos = _items.Where(i => !string.IsNullOrEmpty(i.Genero))
                                    .Select(i => i.Genero)
                                    .Distinct()
                                    .ToList();

            ViewBag.GeneroActual = genero;

            return View(resultado);
        }

        // GET: Catalogo/Detalle/5
        public IActionResult Detalle(int id)
        {
            var item = _items.FirstOrDefault(i => i.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Catalogo/Agregar
        public IActionResult Agregar()
        {
            return View();
        }

        // POST: Catalogo/Agregar
        [HttpPost]
        [ValidateAntiForgeryToken] // Seguridad básica contra ataques XSRF
        public IActionResult Agregar(Item item)
        {
            if (ModelState.IsValid)
            {
                // Generar ID autoincremental seguro
                item.Id = _items.Any() ? _items.Max(i => i.Id) + 1 : 1;

                // Si la descripción está vacía, ponemos una por defecto
                if (string.IsNullOrWhiteSpace(item.Descripcion))
                {
                    item.Descripcion = "Sin descripción disponible.";
                }

                _items.Add(item);
                return RedirectToAction(nameof(Index));
            }

            // Si el modelo no es válido, regresamos a la vista con los errores
            return View(item);
        }
    }
}