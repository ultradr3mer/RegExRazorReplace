using Newtonsoft.Json;
using RegExRazorReplace.Data;
using System.IO;

namespace RegExRazorReplace.Services
{
  internal class SaveToHardDriveService
  {
    #region Fields

    private const string SettingsFileName = "settings.json";

    #endregion Fields

    #region Methods

    internal SaveData Load()
    {
      if (!File.Exists(SettingsFileName))
      {
        return null;
      }

      string json = File.ReadAllText(SettingsFileName);
      return JsonConvert.DeserializeObject<SaveData>(json);
    }

    internal void Save(SaveData saveData)
    {
      string json = JsonConvert.SerializeObject(saveData, Formatting.Indented);
      File.WriteAllText(SettingsFileName, json);
    }

    #endregion Methods
  }
}