namespace Dev.Plugin.Media.Core.Domain;

public class Video
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string VideoUrl { get; set; } = string.Empty;
}
