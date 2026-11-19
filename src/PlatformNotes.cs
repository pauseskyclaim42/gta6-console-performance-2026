namespace GTA6ConsoleCompanion;

/// <summary>
/// Platform notes for the GTA 6 console performance companion.
/// Produces the comparison between supported consoles.
/// </summary>
public class PlatformNotes
{
    /// <summary>
    /// Returns the performance comparison for the known consoles.
    /// </summary>
    public List<PlatformComparison> GetComparison()
    {
        List<PlatformComparison> comparisons = new List<PlatformComparison>
        {
            new PlatformComparison
            {
                Name = "PlayStation 5",
                Resolution = 1920,
                TargetFps = 60,
                DipSeverity = "Low",
                Notes = "Reference console from the public testing notes."
            },
            new PlatformComparison
            {
                Name = "PlayStation 5 Pro",
                Resolution = 2520,
                TargetFps = 60,
                DipSeverity = "Medium",
                Notes = "Higher resolution target with a reduced dip risk on demanding scenes."
            },
            new PlatformComparison
            {
                Name = "Xbox Series X",
                Resolution = 2560,
                TargetFps = 60,
                DipSeverity = "Low",
                Notes = "High performance console variant with a strong performance profile."
            },
            new PlatformComparison
            {
                Name = "Xbox Series S",
                Resolution = 1280,
                TargetFps = 30,
                DipSeverity = "High",
                Notes = "Lower tier console with a reduced target frame rate."
            }
        };

        return comparisons;
    }
}
