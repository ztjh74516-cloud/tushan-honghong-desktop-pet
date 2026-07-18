using System.IO;
using System.Text.Json;
using TushanHonghong.DesktopPet.Domain;

namespace TushanHonghong.DesktopPet.Services;

public sealed class SettingsService
{
    private const string FileName = "settings.json";
    private readonly string _directory;

    public SettingsService(string? directory = null)
    {
        _directory = directory ?? Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "TushanHonghongDesktopPet");
    }

    public async Task<PetSettings> LoadAsync()
    {
        var path = Path.Combine(_directory, FileName);
        if (!File.Exists(path))
        {
            return PetSettings.Default;
        }

        try
        {
            await using var stream = File.OpenRead(path);
            return await JsonSerializer.DeserializeAsync<PetSettings>(stream) ?? PetSettings.Default;
        }
        catch (JsonException)
        {
            return PetSettings.Default;
        }
        catch (IOException)
        {
            return PetSettings.Default;
        }
    }

    public async Task SaveAsync(PetSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);
        Directory.CreateDirectory(_directory);
        var path = Path.Combine(_directory, FileName);
        await using var stream = File.Create(path);
        await JsonSerializer.SerializeAsync(stream, settings);
    }
}
