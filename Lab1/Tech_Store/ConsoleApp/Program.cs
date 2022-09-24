using Tech_Store;

DateTime d = DateTime.Now;
Laptop l = new ((EBrand)1, 12, d, 23.5, (EGPU)0, (ECPU)0,(ELaptopStorage)0);
l.ShowAllProps();
System.Console.WriteLine("Introduzca la marca: ");

try
{

    l.Brand = (EBrand)int.Parse(System.Console.ReadLine());

}catch(FormatException e)
{

    System.Console.WriteLine($"Excepcion: {e.Message.ToString()}");

}

l.ShowAllProps();

