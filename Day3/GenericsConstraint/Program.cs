public interface IPrintable {
    void Print();
}

public class Document : IPrintable {
    public required string Title {set; get;}
    public void Print() {
        System.Console.WriteLine($"{Title}");
    }
}

public class Printer<T> where T : IPrintable {
    public void PrintItem(T item) {
        item.Print();
    }
}

class Program {
    static void Main(string[] args)
    {
        Document doc = new Document { Title = "Laporan Bulanan" };
        Printer<Document> printer = new Printer<Document>();
        printer.PrintItem(doc);
    }
}