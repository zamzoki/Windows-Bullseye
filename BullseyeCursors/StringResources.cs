
internal static class StringResources
{
    private const string ShootInstruction = "Press [Space] to shoot.";

    private const string RetryInstruction = "Press [R] to retry.";
    
    private const string ExitInstruction = "Press [Esc] to exit.";
    
    private const string AttemptsLabel = "Attempts: ";

    private const string PointsLabel = "Points";

    private const string MinusOne = "-1";

    public static string GetInstructions()
        => $"{ShootInstruction}\n{RetryInstruction}\n{ExitInstruction}";

    public static string GetPointsText(int noOfPoints)
        => $"{PointsLabel}: {noOfPoints}";
    
    public static string GetPointsWithPointsToAddText(int noOfPoints, int noOfPointsToAdd)
        => $"{PointsLabel}: {noOfPoints}+{noOfPointsToAdd}"; 

    public static string GetAttemptsText(int noOfAttempts)
        => $"{AttemptsLabel}: {noOfAttempts}";

    public static string GetAttemptsWithMinusOneText(int noOfAttempts)
        => $"{AttemptsLabel}: {noOfAttempts}{MinusOne}";
}