using UnityEngine;
using Discord;

public class DiscordRPC : MonoBehaviour
{
    private void Awake()
    {
        if(GameObject.FindGameObjectsWithTag("DiscordManager").Length > 1)
        {
            if(instance != this)
            {
                Destroy(gameObject);
                return;
            }
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        try
        {
            discord = new Discord.Discord(applicationId, (ulong)Discord.CreateFlags.NoRequireDiscord);
        } catch
        {

        }

        startTime = System.DateTimeOffset.Now.ToUnixTimeMilliseconds();
    }

    private void Update()
    {
        try
        {
            discord.RunCallbacks();
        }

        catch
        {
            try
            {
                discord = new Discord.Discord(applicationId, (ulong)Discord.CreateFlags.NoRequireDiscord);
            } catch
            {

            }
            return;
        }

        updateRPC();
    }

    private void updateRPC()
    {
        try {
            ActivityManager activityManager = discord.GetActivityManager();
            Activity activity = new Discord.Activity
            {
                State = status,
                Assets =
                {
                    LargeImage = largeImage,
                    LargeText = largeText
                },

                Timestamps =
                {
                    Start = startTime
                }
            };

            activityManager.UpdateActivity(activity, (res) =>
            {
                if (res != Discord.Result.Ok)
                {
                    Debug.LogError("Failed to update Discord status");
                }
            });
        }
        catch
        {

        }
    }

    public void setStatus(string _status)
    {
        status = _status;
    }

    public static DiscordRPC instance { get; private set; }

    private Discord.Discord discord;

    private long startTime;

    const string largeImage = "image";

    [SerializeField] private long applicationId;
    [SerializeField] private string largeText;
    [SerializeField] private string status;
    
}
