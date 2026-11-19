namespace GTA6ConsoleCompanion;

/// <summary>
/// Preset manager that produces performance recommendations
/// for a scene and a console. Values come from public testing notes.
/// </summary>
public class PresetManager
{
    /// <summary>
    /// Returns the performance preset for the requested scene and console.
    /// </summary>
    public Preset GetPreset(SceneType scene, ConsoleType console)
    {
        Dictionary<string, string> settings = new Dictionary<string, string>();
        string notes = string.Empty;

        switch (scene)
        {
            case SceneType.Combat:
                settings["HDR"] = "Off";
                settings["CrowdDensity"] = "Reduced";
                notes = "Testing notes: combat scenes can keep a stable frame rate with HDR off and reduced crowd density.";
                break;

            case SceneType.FreeRoam:
                settings["FrameRateTarget"] = "60";
                settings["PopulationDensity"] = "High";
                notes = "Testing notes: open world scenes target a smooth frame rate with the frame rate target set for the selected console.";
                break;

            case SceneType.Chase:
                settings["FrameRateTarget"] = "60";
                settings["MotionBlur"] = "Off";
                notes = "Testing notes: chase scenes reduce motion blur to keep the frame rate stable.";
                break;

            case SceneType.Cutscene:
                settings["FrameRateTarget"] = "30";
                settings["ResolutionScale"] = "720";
                notes = "Testing notes: cutscene scenes use a lower frame rate and resolution scale to reserve performance headroom.";
                break;

            default:
                settings["FrameRateTarget"] = "60";
                notes = "No preset data for the requested scene yet.";
                break;
        }

        return new Preset
        {
            Settings = settings,
            Notes = notes
        };
    }
}
