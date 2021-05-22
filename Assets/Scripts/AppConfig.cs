using UnityEngine;

public static class AppConfig
{
	public static string PackageName { get; private set; }
	public static string MarketURL { get; private set; }
	public static string PublisherURL { get; private set; }

	public static string AdAppID { get; private set; }
	public static string AdTestDeviceID1 { get; private set; }
	public static string AdTestDeviceID2 { get; private set; }
	public static string AdBannerID { get; private set; }
	public static string AdInterstitialID { get; private set; }

	public static string GameCenterLeaderboardID { get; private set; }
}
