using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace GTA6ConsoleCompanion;

/// <summary>
/// Main window of the GTA 6 console performance companion.
/// Hosts the "Performance" tab with scene presets and the
/// "Stutter Log" tab with the recorded stutter events.
/// </summary>
public partial class MainWindow : Window
{
    private readonly PresetManager _presetManager = new PresetManager();

    private readonly StutterLogger _stutterLogger = new StutterLogger();

    public MainWindow()
    {
        InitializeComponent();

        SceneComboBox.ItemsSource = GetScenes();
        ConsoleComboBox.ItemsSource = GetConsoles();
    }

    private static List<SceneType> GetScenes()
    {
        List<SceneType> scenes = new List<SceneType>();
        scenes.Add(SceneType.FreeRoam);
        scenes.Add(SceneType.Combat);
        scenes.Add(SceneType.Chase);
        scenes.Add(SceneType.Cutscene);
        return scenes;
    }

    private static List<ConsoleType> GetConsoles()
    {
        List<ConsoleType> consoles = new List<ConsoleType>();
        consoles.Add(ConsoleType.Ps5);
        consoles.Add(ConsoleType.Ps5Pro);
        consoles.Add(ConsoleType.SeriesX);
        consoles.Add(ConsoleType.SeriesS);
        return consoles;
    }

    private void GetRecommendationsButton_Click(object sender, RoutedEventArgs e)
    {
        SceneType scene = (SceneType)SceneComboBox.SelectedItem;
        ConsoleType console = (ConsoleType)ConsoleComboBox.SelectedItem;

        Preset preset = _presetManager.GetPreset(scene, console);

        RecommendationsTextBlock.Text = preset.Notes;

        foreach (KeyValuePair<string, string> setting in preset.Settings)
        {
            RecommendationsTextBlock.Text = RecommendationsTextBlock.Text + Environment.NewLine + setting.Key + ": " + setting.Value;
        }
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        RefreshLogTable();
    }

    private void Window_Closed(object sender, EventArgs e)
    {
    }

    private void RefreshLogTable()
    {
        List<StutterEntry> entries = new List<StutterEntry>(_stutterLogger.GetEntries());

        if (entries.Count > 0)
        {
            StutterLogDataGrid.ItemsSource = entries;
        }
        else
        {
            StutterLogDataGrid.ItemsSource = null;
        }
    }
}
