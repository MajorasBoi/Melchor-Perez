using Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public partial class DBRepository : IMobileRepository
    {
       
        public Mobile_Phone CreateMobile(EBrand brand, float price, DateTime date, EMobileDataGen mdg, short cr, EMobileStorage ms)
        {
            var mobile = new Mobile_Phone(brand, price, date, mdg, cr, ms);
            Add(mobile);
            return mobile;
        }
        
        public void UpdateMobile(Mobile_Phone mobile)
        {
            Update(mobile);
        }
        
        public void DeleteMobile(Mobile_Phone mobile)
        {
            Delete(mobile);
        }
        
        public Mobile_Phone FindMobile(EBrand brand)
        {
            return Get<Mobile_Phone>(w => w.Brand == brand).First();
        }
        
        public List<Mobile_Phone> GetMobiles()
        {
            return Get<Mobile_Phone>();
        }
    }
}
