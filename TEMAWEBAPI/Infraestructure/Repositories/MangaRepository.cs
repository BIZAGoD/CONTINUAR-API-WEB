using System.Text.Json;
using System.Text.Json.Serialization;

public class MangaRepository
{
    private List<Manga> _mangas;
    private string _filePath;

    public MangaRepository(IConfiguration configuration)
    {
        _filePath = configuration.GetValue<string>("dataBank") ?? string.Empty;
        _mangas = LoadData();
    }

    private string GetCurrentFilePath()
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var currentFilePath = Path.Combine(currentDirectory, _filePath);

        return currentFilePath;
    }

    private List<Manga> LoadData()
    {
        var currentFilePath = GetCurrentFilePath();

        if (File.Exists(currentFilePath))
        {
            var jsonData = File.ReadAllText(currentFilePath);
            return JsonSerializer.Deserialize<List<Manga>>(jsonData)!;
        }

        return new List<Manga>();
    }


    public IEnumerable<Manga> GetAll()
    {
        return _mangas;
    }

    public Manga GetById(int id)
    {
        return _mangas.FirstOrDefault(Manga => Manga.id == id)
                ?? new Manga
                {
                    Title = string.Empty,
                    Author = string.Empty
                };

    }


    public void Add(Manga manga)
    {
        var currentFilePath = GetCurrentFilePath();
        if (!File.Exists(currentFilePath))
        return;


        _mangas.Add(manga);
        File.WriteAllText(_filePath, JsonSerializer.Serialize(_mangas));
    }


    public void Update(Manga updatedManga)
    {
        var currentFilePath = GetCurrentFilePath();
        if (!File.Exists(currentFilePath))
        return;

        var index = _mangas.FindIndex(m => m.id == updatedManga.id);

        if (index != -1)
        {
            _mangas[index] = updatedManga;
            File.WriteAllText(_filePath, JsonSerializer.Serialize(_mangas));
        }
    }

    public void Delete(int id)
    {
        var currentFilePath = GetCurrentFilePath();
        if (!File.Exists(currentFilePath))
        return;

        _mangas.RemoveAll(m => m.id == id);
        File.WriteAllText(_filePath, JsonSerializer.Serialize(_mangas));
    }
}