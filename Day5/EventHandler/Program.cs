class WeatherStation {
    public delegate void TemperatureAlertHandler(string message);
    public event TemperatureAlertHandler OnTemperatureAlert;

    public void CheckTemperature(double temperature) {
        Console.WriteLine($"Current temperature is: {temperature}");
        if (temperature > 30) {
            OnTemperatureAlert?.Invoke("Temperature too high...");
        }
    }
    public void DisplayDevice(string msg) => Console.WriteLine($"Display shows alert: {msg}");
    public void CoolingSystem(string msg) => Console.WriteLine($"Cooling system activated: {msg}");
}

class Program {
    static void Main() {
        WeatherStation station = new WeatherStation();

        station.OnTemperatureAlert += station.DisplayDevice;
        station.OnTemperatureAlert += station.CoolingSystem;

        station.CheckTemperature(35.5);
    }

}