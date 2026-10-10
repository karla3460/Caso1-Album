namespace Abstracciones.Modelos
{
    public class AlbumBase
    {
        public string Titulo { get; set; } = string.Empty;
        public string Artista { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
        public int AnioLanzamiento { get; set; }
        public int CantidadCanciones { get; set; }
    }

    public class AlbumRequest : AlbumBase
    {
    }

    public class AlbumResponse : AlbumBase
    {
        public Guid Id { get; set; }
    }
}