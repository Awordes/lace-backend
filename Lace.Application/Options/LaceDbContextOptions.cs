namespace Lace.Application.Options;

public class LaceDbContextOptions
{
    public static string SectionName = nameof(LaceDbContextOptions);

    public string ConnectionString { get; set; }
}