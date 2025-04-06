namespace Services;

public class ContainerDesc
{
    public double Mass { get; set; }

    public double Temperature { get; set; }
    public double Pressure { get; set; }

    public double GasConstant => 8.314;

    public double Volume => 1;
}

public class ContainerLimits
{
    public double lowerLimit => 90000;
    public double upperLimit => 95000;
    public double implosionLimit => 85000;
    public double explosionLimit => 100000;
}

/// <summary>
/// Service contract.
/// </summary>
public interface IContainerService
{
    /// <summary>
    /// Limits for the Logic class to access
    /// </summary>
    /// <returns>container limits</returns>
    ContainerLimits GetContainerLimits();

    /// <summary>
    /// variables for the Logic class to access
    /// </summary>
    /// <returns>container variables</returns>
    ContainerDesc ContainerInfo();

    /// <summary>
    /// allows clients to update mass in the server
    /// </summary>
    /// <param name="mass">the mass update value</param>
    void SetMass(double mass);

    /// <summary>
    /// Gets the container's control state.
    /// </summary>
    /// <returns>returns 1 for input component to work and 2 for output component to work</returns>
    int ActiveClient();
}
