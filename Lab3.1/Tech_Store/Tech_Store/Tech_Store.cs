using Devices;

namespace Tech_Store
{
        
    public class Store
    {
        #region Fields

        private List<Device> _device;

        #endregion
        #region Constructors

        public Store()
        {
            _device = new List<Device>();
        }

        public Store(List<Device> device)
        {
            _device = device;
        }

        #endregion
        #region Methods


        /// <summary>
        /// Adds a mobile phone to the devices list.
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
        /// <param name="mobile_data_gen">
        /// Mobile data generation of the mobile phone.
        /// </param>
        /// <param name="camera_res">
        /// Camera resolution of the mobile phone.
        /// </param>
        /// <param name="storage">
        /// Amount of storage of the mobile phone.
        /// </param>
        public void AddMobilePhone(EBrand brand, float price, DateTime date, EMobileDataGen mobile_data_gen, short camera_res, EMobileStorage storage)
        {
            var mobile = new Mobile_Phone(brand, price, date, mobile_data_gen, camera_res, storage);
            _device.Add(mobile);
        }

        public void AddMobilePhone(Mobile_Phone mobile)
        {
            _device.Add(mobile);
        }

        /// <summary>
        /// Adds a laptop to the devices list.
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
        public void AddLaptop(EBrand brand, float price, DateTime date, double dimension, EGPU gpu, ECPU cpu, ELaptopStorage storage)
        {
            var laptop = new Laptop(brand, price, date, dimension, gpu, cpu, storage);
            _device.Add(laptop);
        }

        public void AddLaptop(Laptop laptop)
        {
            _device.Add(laptop);
        }

        /// <summary>
        /// Returns the device at the position especified.
        /// </summary>
        /// <param name="index">
        /// Index of the device.
        /// </param>
        /// <returns>
        /// A <see cref="Device"/> type.
        /// </returns>
        public Device GetElementAt(int index)
        {
            return _device[index];
        }

        #endregion

    }
}