using MotorComponent;
namespace Motor;

public class Motorcycle {
    public Engine engine;
    public Tire tire;
    public Motorcycle(Engine engine, Tire tire) {
        this.engine = engine;
        this.tire = tire;
    }
}