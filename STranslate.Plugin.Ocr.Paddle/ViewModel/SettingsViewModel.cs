using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace STranslate.Plugin.Ocr.Paddle.ViewModel;

public partial class SettingsViewModel(IPluginContext context, Settings settings) : ObservableObject
{
    [ObservableProperty] public partial string ModelsDirectory { get; set; } = settings.ModelsDirectory;

    public List<string> ModelSizes { get; } = ["Tiny", "Small", "Medium"];

    [ObservableProperty] public partial string SelectedModelSize { get; set; } = settings.ModelSize;

    partial void OnModelsDirectoryChanged(string value)
    {
        settings.ModelsDirectory = value;
        context.SaveSettingStorage<Settings>();
    }

    partial void OnSelectedModelSizeChanged(string value)
    {
        settings.ModelSize = value;
        context.SaveSettingStorage<Settings>();
    }

    [RelayCommand]
    private void SelectFolder()
    {
        var dialot = new FolderBrowserDialog
        {
            Multiselect = false,
            RootFolder = Environment.SpecialFolder.DesktopDirectory,
        };
        if (dialot.ShowDialog() != DialogResult.OK)
            return;

        ModelsDirectory = dialot.SelectedPath;
    }
}