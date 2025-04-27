using Il2CppScheduleOne.DevUtilities;
using Il2CppScheduleOne.Persistence;
using MelonLoader;
using System.IO.Compression;

[assembly: MelonInfo(typeof(SaveFileSharing.SaveFileSharingMod), SaveFileSharing.BuildInfo.Name, SaveFileSharing.BuildInfo.Version, SaveFileSharing.BuildInfo.Author, SaveFileSharing.BuildInfo.DownloadLink)]
[assembly: MelonColor()]
[assembly: MelonGame("TVGS", "Schedule I")]

namespace SaveFileSharing
{
    public static class BuildInfo
    {
        public const string Name = "SaveFileSharingMod";
        public const string Description = "Share and get save files from your friends so you dont need one person to always be on to play!";
        public const string Author = "Sour420";
        public const string Company = null;
        public const string Version = "1.0";
        public const string DownloadLink = null;
    }

    public class SaveFileSharingMod : MelonMod
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private static string downloadUrl;
        private static string uploadUrl;
        private static string apiKey;
        private static int saveSlotNumber;
        private static string saveFolderPath;
        private static string zipFilePath;

        public override void OnApplicationStart()
        {
            var category = MelonPreferences.CreateCategory("SaveFileSharingMod");
            category.SetFilePath("UserData/SaveFileSharing.cfg");

            var slotPref = category.CreateEntry("SaveSlotNumber", 1);
            var downloadPref = category.CreateEntry("DownloadUrl", $"https://[ID].supabase.co/storage/v1/object/public/save-sync/SaveGame_");
            var uploadPref = category.CreateEntry("UploadUrl", $"https://[ID].supabase.co/storage/v1/object/save-sync/SaveGame_");
            var apiKeyPref = category.CreateEntry("ApiKey", "");
            var steamIDPref = category.CreateEntry("SteamID", "");

            category.SaveToFile();

            if (string.IsNullOrEmpty(steamIDPref.Value) || !steamIDPref.Value.All(char.IsDigit))
            {
                MelonLogger.Error("[SaveFileSharingMod] SteamID is invalid! Please make sure it only contains numbers.");
                return;
            }

            string _path = $"Saves\\{steamIDPref.Value}";
            string Savepath = Path.Combine(UnityEngine.Application.persistentDataPath, _path);

            saveSlotNumber = slotPref.Value;
            downloadUrl = $"{downloadPref.Value}{saveSlotNumber}.zip";
            uploadUrl = $"{uploadPref.Value}{saveSlotNumber}.zip";
            apiKey = apiKeyPref.Value;

            saveFolderPath = Path.Combine(Savepath, $"SaveGame_{saveSlotNumber}");
            zipFilePath = Path.Combine(Savepath, $"SaveGame_{saveSlotNumber}.zip");

            MelonLogger.Msg($"SaveFileMod initialized for SaveGame_{saveSlotNumber}");
            MelonLogger.Msg($"Save Path: {saveFolderPath}");

            Task.Run(DownloadAndExtractSave).Wait();

        }

        public override void OnApplicationQuit()
        {
            Task.Run(ZipAndUploadSave).Wait();
        }

        private async Task DownloadAndExtractSave()
        {
            try
            {
                MelonLogger.Msg("Downloading save file...");
                byte[] zipData = await httpClient.GetByteArrayAsync(downloadUrl);
                File.WriteAllBytes(zipFilePath, zipData);

                if (Directory.Exists(saveFolderPath))
                    Directory.Delete(saveFolderPath, true);

                ZipFile.ExtractToDirectory(zipFilePath, saveFolderPath);
                MelonLogger.Msg("Save file downloaded and extracted successfully!");


            }
            catch (Exception ex)
            {
                MelonLogger.Warning($"Downloading save file failed: {ex.Message}");
            }
        }

        private async Task ZipAndUploadSave()
        {
            try
            {
                MelonLogger.Msg("Zipping save folder...");

                if (Directory.Exists(saveFolderPath))
                {
                    if (File.Exists(zipFilePath))
                        File.Delete(zipFilePath);

                    using (var zipArchive = ZipFile.Open(zipFilePath, ZipArchiveMode.Create))
                    {
                        var files = Directory.GetFiles(saveFolderPath, "*", SearchOption.AllDirectories);

                        foreach (var file in files)
                        {

                            var relativePath = file.Substring(saveFolderPath.Length + 1);
                            zipArchive.CreateEntryFromFile(file, relativePath);
                        }
                    }

                    MelonLogger.Msg("Zip created! Uploading to Supabase...");

                    var fileBytes = File.ReadAllBytes(zipFilePath);
                    var request = new HttpRequestMessage(HttpMethod.Put, uploadUrl)
                    {
                        Content = new ByteArrayContent(fileBytes)
                    };

                    request.Headers.Add("Authorization", $"Bearer {apiKey}");
                    request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/zip");

                    var response = await httpClient.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        MelonLogger.Msg("Save file uploaded successfully to Supabase!");
                    }
                    else
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        MelonLogger.Warning($"Upload failed: {response.StatusCode}. Response: {responseContent}");
                    }
                }
                else
                {
                    MelonLogger.Warning("Save folder does not exist, aborting zip operation.");
                }
            }
            catch (Exception ex)
            {
                MelonLogger.Warning($"Upload failed: {ex.Message}");
            }
        }

    }
}
