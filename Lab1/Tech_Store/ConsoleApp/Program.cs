using Tech_Store;
using Devices;

EBrand brand = EBrand.LG;
float price = 200;
DateTime date = DateTime.Now;
EMobileDataGen mdg = EMobileDataGen.LTE;
short cr = 12;
EMobileStorage storage = EMobileStorage._256gB;

Mobile_Phone mobile1 = new Mobile_Phone(brand, price, date, mdg, cr, storage);
Store tech = new Store();
tech.AddMobilePhone(mobile1);

Mobile_Phone mobile = new Mobile_Phone((Mobile_Phone)tech.GetElementAt(0));

Console.WriteLine(mobile.Brand);
Console.WriteLine(mobile.Price);
Console.WriteLine(mobile.Date);
Console.WriteLine(mobile.MobileDataGen);
Console.WriteLine(mobile.CameraRes);
Console.WriteLine(mobile.Storage);





