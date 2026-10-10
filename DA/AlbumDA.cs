using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;

namespace DA
{
    public class AlbumDA : IAlbumDA
    {
        // static: la lista sobrevive entre peticiones aunque el DA sea Scoped
        private static readonly List<AlbumResponse> _albumes = new()
        {
            new AlbumResponse
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Titulo = "Thriller", Artista = "Michael Jackson", Genero = "Pop",
                AnioLanzamiento = 1982, CantidadCanciones = 9
            },
            new AlbumResponse
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Titulo = "Abbey Road", Artista = "The Beatles", Genero = "Rock",
                AnioLanzamiento = 1969, CantidadCanciones = 17
            },
            new AlbumResponse
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Titulo = "Un Verano Sin Ti", Artista = "Bad Bunny", Genero = "Reggaetón",
                AnioLanzamiento = 2022, CantidadCanciones = 23
            }
        };

        public Task<IEnumerable<AlbumResponse>> Obtener(string? artista)
        {
            IEnumerable<AlbumResponse> resultado = string.IsNullOrWhiteSpace(artista)
                ? _albumes.ToList()
                : _albumes.Where(a => a.Artista.Contains(artista, StringComparison.OrdinalIgnoreCase)).ToList();

            return Task.FromResult(resultado);
        }

        public Task<AlbumResponse?> Obtener(Guid Id)
        {
            var album = _albumes.FirstOrDefault(a => a.Id == Id);
            return Task.FromResult(album);
        }

        public Task<Guid> Agregar(AlbumRequest album)
        {
            var nuevo = new AlbumResponse
            {
                Id = Guid.NewGuid(),
                Titulo = album.Titulo,
                Artista = album.Artista,
                Genero = album.Genero,
                AnioLanzamiento = album.AnioLanzamiento,
                CantidadCanciones = album.CantidadCanciones
            };

            _albumes.Add(nuevo);
            return Task.FromResult(nuevo.Id);
        }

        public Task<bool> Editar(Guid Id, AlbumRequest album)
        {
            var existente = _albumes.FirstOrDefault(a => a.Id == Id);
            if (existente is null)
                return Task.FromResult(false);

            existente.Titulo = album.Titulo;
            existente.Artista = album.Artista;
            existente.Genero = album.Genero;
            existente.AnioLanzamiento = album.AnioLanzamiento;
            existente.CantidadCanciones = album.CantidadCanciones;

            return Task.FromResult(true);
        }

        public Task<bool> Eliminar(Guid Id)
        {
            var existente = _albumes.FirstOrDefault(a => a.Id == Id);
            if (existente is null)
                return Task.FromResult(false);

            _albumes.Remove(existente);
            return Task.FromResult(true);
        }
    }
}