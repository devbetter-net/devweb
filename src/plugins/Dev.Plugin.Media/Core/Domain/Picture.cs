namespace Dev.Plugin.Media.Core.Domain;
public class Picture
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string MimeType { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string Alt { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
}
