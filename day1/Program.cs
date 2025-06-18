
public interface IObserver
{
    void Update(string weather);
}

public interface ISubject
{
    void AddObserver(IObserver observer);
    void RemoveObserver(IObserver observer);
    void NotifyObservers();
}

public class WeatherStation : ISubject
{
    private List<IObserver> _observers = new List<IObserver>();
    private string _weather;

    public void AddObserver(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (var observer in _observers)
        {
            observer.Update(_weather);
        }
    }

    public void SetWeather(string newWeather)
    {
        _weather = newWeather;
        NotifyObservers();
    }
}

public class PhoneDisplay : IObserver
{
    private string _weather;

    public void Update(string weather)
    {
        _weather = weather;
        Display();
    }

    private void Display()
    {
        Console.WriteLine($"Phone Display: Current weather is {_weather}");
    }
}

public class TVDisplay : IObserver
{
    private string _weather;

    public void Update(string weather)
    {
        _weather = weather;
        Display();
    }

    private void Display()
    {
        Console.WriteLine($"TV Display: Weather forecast: {_weather}");
    }
}

namespace WithoutObserverPattern
{
    public class WeatherStation
    {
        private PhoneDisplay _phoneDisplay;
        private TVDisplay _tvDisplay;
        private string _weather;

        public WeatherStation(PhoneDisplay phoneDisplay, TVDisplay tvDisplay)
        {
            _phoneDisplay = phoneDisplay;
            _tvDisplay = tvDisplay;
        }

        public void SetWeather(string newWeather)
        {
            _weather = newWeather;

            _phoneDisplay.Update(_weather);
            _tvDisplay.Update(_weather);
        }
    }

    public class PhoneDisplay
    {
        private string _weather;

        public void Update(string weather)
        {
            _weather = weather;
            Display();
        }

        private void Display()
        {
            Console.WriteLine($"Phone Display: Current weather is {_weather}");
        }
    }

    public class TVDisplay
    {
        private string _weather;

        public void Update(string weather)
        {
            _weather = weather;
            Display();
        }

        private void Display()
        {
            Console.WriteLine($"TV Display: Weather forecast: {_weather}");
        }
    }
}