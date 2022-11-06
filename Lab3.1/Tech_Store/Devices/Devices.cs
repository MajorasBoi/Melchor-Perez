using System.ComponentModel.DataAnnotations.Schema;

namespace Devices
{
    public enum EBrand
    {
        Samsung,
        LG,
        Apple,
        Xiaomi,
        Huawei,
    }

    public enum EMobileDataGen
    {
        E,
        _3G,
        LTE,
    }

    public enum EMobileStorage
    {
        _16gB,
        _32gB,
        _64gB,
        _128gB,
        _256gB,
    }

    public enum ELaptopStorage
    {
        _256gB,
        _512gB,
        _1024gB,
        _2048gB,
    }

    public enum EGPU
    {
        Dedicated,
        Integrated,
    }

    public enum ECPU
    {
        i3,
        i5,
        i7,
    }

    public abstract class Devices
    {
        #region Attributes

        
        /// <summary>
        /// Refers to the brand of the device
        /// </summary>
        public EBrand Brand { set; get; }

        /// <summary>
        /// Refers to the price of the device
        /// </summary>
        public float Price { set; get; }

        /// <summary>
        /// Refers to the day of shipping.
        /// </summary>
        public DateTime Date { set; get; }

        #endregion
        #region Constructor


        /// <summary>
        /// </summary>
        /// <param name="brand">
        /// Device's brand.
        /// </param>
        /// <param name="price">
        /// Device's price.
        /// </param>
        /// <param name="date">
        /// Device's year of release.
        /// </param>
        public Devices(EBrand brand, float price, DateTime date)
        {
            Brand = brand;
            Price = price;
            Date = date;
        }

        /// <summary>
        /// Creates an object <see cref="Devices"/>.
        /// </summary>
        /// <param name="device">
        /// A <see cref="Devices"/> type.
        /// </param>
        public Devices (Devices device)
        {
            Brand = device.Brand;
            Price = device.Price;
            Date = device.Date;
        }

        #endregion
        #region Method

        /// <summary>
        /// This function returns the device's shipping day.
        /// </summary>
        /// <returns>Type of data of the object</returns>
        public abstract bool IsEconomic();

        #endregion
    }

    public class Mobile_Phone : Devices
    {
        #region Attributes


        
        /// <summary>
        /// Refers to the mobile data generation of the mobile phone. 
        /// </summary>
        public EMobileDataGen MobileDataGen { set; get; }


        
        /// <summary>
        /// Refers to the camera resolution of the mobile phone.
        /// </summary>
        public short CameraRes { set; get; }


        
        /// <summary>
        /// Refers to the amount of storage of the mobile phone.
        /// </summary>
        public EMobileStorage Storage { set; get; }

        #endregion
        #region Constructor

        /// <summary>
        /// Creates an object <see cref="Mobile_Phone"/>.
        /// </summary>
        /// <param name="brand"></param>
        /// <param name="price"></param>
        /// <param name="date"></param>
        /// <param name="mobile_data_gen">
        /// Mobile data generation of the mobile phone.
        /// </param>
        /// <param name="camera_res">
        /// Camera resolution of the mobile phone.
        /// </param>
        /// <param name="storage">
        /// Amount of storage of the mobile phone.
        /// </param>
        public Mobile_Phone(EBrand brand, float price, DateTime date, EMobileDataGen mobile_data_gen, short camera_res, EMobileStorage storage) : base(brand, price, date)
        {
            MobileDataGen = mobile_data_gen;
            CameraRes = camera_res;
            Storage = storage;
        }

        /// <summary>
        /// Creates an object <see cref="Mobile_Phone"/>.
        /// </summary>
        /// <param name="mobile">
        /// A <see cref="Mobile_Phone"/> type.
        /// </param>
        public Mobile_Phone(Mobile_Phone mobile): base(mobile.Brand, mobile.Price, mobile.Date)
        {
            MobileDataGen = mobile.MobileDataGen;
            CameraRes = mobile.CameraRes;
            Storage = mobile.Storage;
        }

        #endregion
        #region Method


        public override bool IsEconomic()
        {
            if(Price > 300)
                return false;
            return true;
        }

        #endregion
    }

    public class Laptop : Devices
    {
        #region Attributes


        /// <summary>
        /// Refers to the dimension of the laptop.
        /// </summary>
        public double Dimension { set; get; }

        /// <summary>
        /// Refers to the type of GPU of the laptop.
        /// </summary>
        public EGPU GPU { set; get; }

        /// <summary>
        /// Refers to the type of CPU of the laptop.
        /// </summary>
        public ECPU CPU { set; get; }

        /// <summary>
        /// Refers to the amount of storage of the laptop.
        /// </summary>
        public ELaptopStorage Storage { set; get; }

        #endregion
        #region Constructor

        /// <summary>
        /// Creates an object <see cref="Laptop"/>.
        /// </summary>
        /// <param name="brand"></param>
        /// <param name="price"></param>
        /// <param name="date"></param>
        /// <param name="dimension">
        /// Dimension of the laptop.
        /// </param>
        /// <param name="gpu">
        /// Type of GPU.
        /// </param>
        /// <param name="cpu">
        /// Type of CPU
        /// </param>
        /// <param name="storage">
        /// Amount of storage of the laptop.
        /// </param>
        public Laptop(EBrand brand, float price, DateTime date, double dimension, EGPU gpu, ECPU cpu, ELaptopStorage storage) : base(brand, price, date)
        {
            Dimension = dimension;
            GPU = gpu;
            CPU = cpu;
            Storage = storage;
        }

        /// <summary>
        /// Creates an object <see cref="Laptop"/>.
        /// </summary>
        /// <param name="laptop">
        /// A <see cref="Laptop"/> type.
        /// </param>
        public Laptop(Laptop laptop): base(laptop.Brand, laptop.Price, laptop.Date)
        {
            Dimension = laptop.Dimension;
            GPU = laptop.GPU;
            CPU = laptop.CPU;
            Storage = laptop.Storage;
        }

        #endregion
        #region Method


        public override bool IsEconomic()
        {
            if (Price > 500)
                return false;
            return true;
        }

        #endregion
    }
}