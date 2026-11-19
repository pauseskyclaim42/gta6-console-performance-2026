using System;
using System.Collections.Generic;

namespace GTA6ConsoleCompanion;

/// <summary>
/// Supported scene types for the performance companion.
/// </summary>
public enum SceneType
{
    FreeRoam,
    Combat,
    Chase,
    Cutscene
}

/// <summary>
/// Supported console families for the performance companion.
/// </summary>
public enum ConsoleType
{
    Ps5,
    Ps5Pro,
    SeriesX,
    SeriesS
}

/// <summary>
/// Performance preset for one scene and one console.
/// </summary>
public class Preset
{
    public Dictionary<string, string> Settings { get; set; } = new Dictionary<string, string>();

    public string Notes { get; set; } = string.Empty;
}

/// <summary>
/// Single stutter event recorded for a scene.
/// </summary>
public class StutterEntry
{
    public DateTime Time { get; set; }

    public SceneType Scene { get; set; }

    public int DurationMs { get; set; }
}

/// <summary>
/// Aggregated stutter statistics for the tracked sessions.
/// </summary>
public class StutterStatistics
{
    public List<SceneStat> WorstScenes { get; set; } = new List<SceneStat>();

    public double AverageDurationMs { get; set; }

    public Dictionary<SceneType, int> ClusterCounts { get; set; } = new Dictionary<SceneType, int>();
}

/// <summary>
/// Per scene stutter statistic.
/// </summary>
public class SceneStat
{
    public SceneType Scene { get; set; }

    public int Count { get; set; }

    public double TotalDurationMs { get; set; }

    public double AverageDurationMs { get; set; }
}

/// <summary>
/// Platform comparison record for the performance companion.
/// </summary>
public class PlatformComparison
{
    public string Name { get; set; } = string.Empty;

    public int Resolution { get; set; }

    public int TargetFps { get; set; }

    public string DipSeverity { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;
}
