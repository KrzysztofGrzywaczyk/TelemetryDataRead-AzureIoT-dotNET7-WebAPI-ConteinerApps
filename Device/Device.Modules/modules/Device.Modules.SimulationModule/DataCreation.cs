namespace Device.Modules.SimulationModule;

public class DataCreation
{
    private static readonly Random Rnd = new Random();

    public double TempMin { get; private set; }

    public double TempMax { get; private set; }

    public int Frequency { get; private set; }

    public static double CalculateData(double currentData)
    {
        if (currentData <= currentData + Rnd.NextDouble())
        {
            currentData = Math.Round(currentData + (Rnd.NextDouble() * 2), 2);
        }
        else
        {
            currentData = Math.Round(currentData - (Rnd.NextDouble() * 2), 2);
        }

        return currentData;
    }
}