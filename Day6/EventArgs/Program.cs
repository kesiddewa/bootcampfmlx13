using System;

namespace EventHandlerExample
{
    public class TemperatureEventArgs : EventArgs
    {
        public double Temperature { get; }
        public TemperatureEventArgs(double temperature)
        {
            Temperature = temperature;
        }
    }

    public class Sensor
    {
        public event EventHandler<TemperatureEventArgs> TemperatureExceeded;

        private double _threshold;

        public Sensor(double threshold)
        {
            _threshold = threshold;
        }

        public void CheckTemperature(double currentTemp)
        {
            Console.WriteLine($"Current temperature: {currentTemp}°C");

            if (currentTemp > _threshold)
            {
                OnTemperatureExceeded(new TemperatureEventArgs(currentTemp));
            }
        }

        protected virtual void OnTemperatureExceeded(TemperatureEventArgs e)
        {
            TemperatureExceeded?.Invoke(this, e);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Sensor sensor = new Sensor(30.0);

            sensor.TemperatureExceeded += HandleTemperatureExceeded;

            sensor.CheckTemperature(25.0);
            sensor.CheckTemperature(32.5);
            sensor.CheckTemperature(28.0);
            sensor.CheckTemperature(35.0); 

            Console.WriteLine("Selesai.");
        }

        static void HandleTemperatureExceeded(object sender, TemperatureEventArgs e)
        {
            Console.WriteLine($"Temperature exceeded: {e.Temperature}°C");
        }
    }
}