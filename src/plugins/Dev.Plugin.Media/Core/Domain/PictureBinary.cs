namespace Dev.Plugin.Media.Core.Domain;

public class PictureBinary
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid PictureId { get; set; }
    public required byte[] Binary { get; set; }
}
