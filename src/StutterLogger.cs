using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GTA6ConsoleCompanion;

/// <summary>
/// Stutter logger for the performance companion.
/// Keeps stutter events and derives simple statistics from them.
/// </summary>
public class StutterLogger
{
    private readonly List<StutterEntry> _entries = new List<StutterEntry>();

    /// <summary>
    /// Adds a new stutter event for a scene.
    /// </summary>
    public void LogStutter(DateTime time, SceneType scene, int durationMs)
    {
        if (durationMs <= 0)
        {
            return;
        }

        _entries.Add(new StutterEntry
        {
            Time = time,
            Scene = scene,
            DurationMs = durationMs
        });
    }

    /// <summary>
    /// Returns the recorded events.
    /// </summary>
    public List<StutterEntry> GetEntries()
    {
        return new List<StutterEntry>(_entries);
    }

    /// <summary>
    /// Builds the statistics: worst scenes, average duration and clustering per scene.
    /// </summary>
    public StutterStatistics GetPatterns()
    {
        Dictionary<SceneType, List<int>> grouped = new Dictionary<SceneType, List<int>>();

        foreach (StutterEntry entry in _entries)
        {
            if (!grouped.ContainsKey(entry.Scene))
            {
                grouped[entry.Scene] = new List<int>();
            }

            grouped[entry.Scene].Add(entry.DurationMs);
        }

        List<SceneStat> worstScenes = new List<SceneStat>();

        foreach (KeyValuePair<SceneType, List<int>> pair in grouped)
        {
            SceneStat stat = new SceneStat
            {
                Scene = pair.Key,
                Count = pair.Value.Count,
                TotalDurationMs = pair.Value.Sum(duration => (double)duration),
                AverageDurationMs = pair.Value.Count > 0 ? pair.Value.Average(duration => (double)duration) : 0
            };

            worstScenes.Add(stat);
        }

        worstScenes.Sort((left, right) => right.AverageDurationMs.CompareTo(left.AverageDurationMs));

        List<SceneStat> worstScenesList = new List<SceneStat>();
        foreach (SceneStat stat in worstScenes)
        {
            if (stat.Count > 0)
            {
                worstScenesList.Add(stat);
            }
        }

        double averageDuration = _entries.Count > 0
            ? _entries.Average(entry => (double)entry.DurationMs)
            : 0;

        Dictionary<SceneType, int> clustering = new Dictionary<SceneType, int>();

        foreach (StutterEntry entry in _entries)
        {
            if (clustering.ContainsKey(entry.Scene))
            {
                clustering[entry.Scene] = clustering[entry.Scene] + 1;
            }
            else
            {
                clustering[entry.Scene] = 1;
            }
        }

        return new StutterStatistics
        {
            WorstScenes = worstScenesList,
            AverageDurationMs = averageDuration,
            ClusterCounts = clustering
        };
    }

    /// <summary>
    /// Exports the recorded events to a CSV file.
    /// </summary>
    public void ExportToCsv(string path)
    {
        using StreamWriter writer = new StreamWriter(path, false, new UTF8Encoding(false));

        writer.WriteLine("Time,Scene,DurationMs");

        foreach (StutterEntry entry in _entries)
        {
            string sceneText = entry.Scene.ToString();
            string timeText = entry.Time.ToString("yyyy-MM-dd HH:mm:ss");

            writer.WriteLine(timeText + "," + sceneText + "," + entry.DurationMs);
        }
    }
}
