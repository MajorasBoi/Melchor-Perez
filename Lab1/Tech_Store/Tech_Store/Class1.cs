namespace Tech_Store
{
    enum Brand
    {
        Samsung,
        LG,
        Apple,
        Xiaomi,
        Huawei,
    }

    enum MobileDataGen
    {
        _2G,
        _3G,
        LTE,
    }

    public abstract class Device
    {
        //Attributes
        private Brand _brand { get; set; }
        private float _price { get; set; }
        private DateTime _year { get; set; }

        //Methods
        public abstract void ShowAllProps();
    }

    public class Mobile_Phone: Device
    {
        //Attributes
        private MobileDataGen mobile_data_gen { get; set; }

        //Methods
        public override void ShowAllProps()
        {
            
        }
    }
























}