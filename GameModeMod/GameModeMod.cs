using UnityEngine;

namespace GameModeMod
{
    public class GameModeMod : Mod
    {
        private void Start()
        {
            LogInfo("Mod was loaded successfully!");
            FollowUpLog(
                "You can now change your game mode with the following command: <i>gamemode <Normal|Hardcore|Creative|Easy|Peaceful></i>");
        }

        public void OnModUnload()
        {
            LogInfo("Mod has been unloaded.");
        }

        [ConsoleCommand("gamemode", "Change your game mode.")]
        // ReSharper disable once UnusedMember.Local
        private static void ChangeGameMode(string[] arguments)
        {
            if (arguments.Length == 0)
            {
                LogInfo("Current game mode: " + GameModeValueManager.GetCurrentGameModeValue().gameMode);
                FollowUpLog("You can change your game mode with the following command: " +
                            "<i>gamemode <Normal|Hardcore|Creative|Easy|Peaceful></i>");
            }
            else if (arguments.Length == 1)
            {
                var mode = GetGameModeFromString(arguments[0]);
                if (mode == GameMode.None)
                {
                    LogError("'" + arguments[0] + "' is not a valid game mode.");
                }
                else
                {
                    GameModeValueManager.SelectCurrentGameMode(mode);
                    LogInfo("The game mode was changed to " + mode + ".");
                }
            }
            else
            {
                LogError("Usage: <i>gamemode <Normal|Hardcore|Creative|Easy|Peaceful></i>");
            }
        }

        /// <summary>
        /// Finds the corresponding gamemode from a string input by name or abbreviated name (first letter). This method
        /// is case-insensitive.
        /// </summary>
        /// <param name="input">the input should contain the name of the game mode to find.</param>
        /// <returns>the corresponding <see cref="GameMode"/> value or <see cref="GameMode.None"/> if the input string
        /// could not be resolved to a valid game mode.</returns>
        private static GameMode GetGameModeFromString(string input)
        {
            switch (input.ToLower())
            {
                case "normal":
                case "n":
                    return GameMode.Normal;
                case "hardcore":
                case "h":
                    return GameMode.Hardcore;
                case "creative":
                case "c":
                    return GameMode.Creative;
                case "easy":
                case "e":
                    return GameMode.Easy;
                case "peaceful":
                case "p":
                    return GameMode.Peaceful;
                default:
                    return GameMode.None;
            }
        }

        private static void LogInfo(string message)
        {
            Debug.Log("<color=#3498db>[info]</color>\t<b>traxam's GameMode mod:</b> " + message);
        }

        private static void FollowUpLog(string message)
        {
            Debug.Log("\t" + message);
        }

        private static void LogError(string message)
        {
            Debug.LogError("<color=#e74c3c>[error]</color>\t<b>traxam's GameMode mod:</b> " + message);
        }
    }
}