namespace Tech_Store
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

    public abstract class Device
    {
        #region Attributes

        private EBrand _brand;
        private float _price;
        private DateTime _year;

        #endregion
        #region Access Descriptors 

        public EBrand Brand
        {
            get {
                return _brand;
            }
            set { 
                _brand = value;
            }
        }
        public float Price
        {
            get
            {
                return _price;
            }
            set { _price = value; }
        }
        public DateTime Year
        {
            get { return _year; }
            set { _year = value; }
        }

        #endregion
        #region Constructor

        public Device(EBrand brand, float price, DateTime year)
        {
            _brand = brand;
            _price = price;
            _year = year;
        }

        #endregion
        #region Method
        /// <summary>
        /// This function shows on console every attribute of the object
        /// </summary>
        public abstract void ShowAllProps();
        #endregion
    }

    
    public class Mobile_Phone: Device
    {
        #region Attributes

        private EMobileDataGen _mobile_data_gen;
        private short _camera_res;
        private EMobileStorage _storage;

        #endregion
        #region Constructor
        public Mobile_Phone(EBrand brand, float price, DateTime year, EMobileDataGen mobile_data_gen, short camera_res, EMobileStorage storage): base( brand, price, year)
        {
            _mobile_data_gen = mobile_data_gen;
            _camera_res = camera_res;
            _storage = storage;
        }

        #endregion
        #region Access Descriptors

        public EMobileDataGen MobileData_gen
        {
            get { return _mobile_data_gen; }
            set { _mobile_data_gen = value; }
        }
        public short CameraRes
        {
            get { return _camera_res; }
            set { _camera_res = value; }
        }
        public EMobileStorage Storage
        {
            get { return _storage; }
            set { _storage = value; }
        }

        #endregion
        #region Method

        public override void ShowAllProps()
        {
            System.Console.WriteLine("Brand: " + Brand);
            System.Console.WriteLine("Price: " + Price + " USD");
            System.Console.WriteLine("Release Year: " + Year);
            System.Console.WriteLine("Mobile Data Gen: " + MobileData_gen);
            System.Console.WriteLine("Camera Resolution: " + CameraRes + "Mpx");
            System.Console.WriteLine("Storage: " + Storage);
        }

        #endregion
    }


    public class Laptop: Device
    {
        #region Attributes

        private double _dimension;
        private EGPU _gpu;
        private ECPU _cpu;
        private ELaptopStorage _storage;

        #endregion
        #region Constructor

        public Laptop(EBrand brand, float price, DateTime year, double dimension, EGPU gpu, ECPU cpu, ELaptopStorage storage): base(brand, price, year)
        {
            _dimension = dimension;
            _gpu = gpu;
            _cpu = cpu;
            _storage = storage;
        }

        #endregion
        #region Access Description

        public double Dimension
        {
            get { return _dimension; }
            set { _dimension = value; }
        }
        public EGPU GPU
        {
            get { return _gpu; }
            set { _gpu = value; }
        }
        public ECPU CPU
        {
            get { return _cpu; }
            set { _cpu = value; }
        }
        public ELaptopStorage Storage
        {
            get { return _storage; }
            set { _storage = value; }
        }

        #endregion
        #region Method

        public override void ShowAllProps()
        {
            System.Console.WriteLine("Brand: " + Brand);
            System.Console.WriteLine("Price: " + Price + " USD");
            System.Console.WriteLine("Release Year: " + Year);
            System.Console.WriteLine("Dimension: " + Dimension + "in");
            System.Console.WriteLine("GPU: " + GPU);
            System.Console.WriteLine("CPU: " + CPU);
            System.Console.WriteLine("Storage: " + Storage);
        }

        #endregion


    }























}