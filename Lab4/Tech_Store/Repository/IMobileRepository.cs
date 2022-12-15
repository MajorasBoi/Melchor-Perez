using Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IMobileRepository : IRepository
    {
       
        Mobile_Phone CreateMobile(EBrand brand, float price, DateTime date, EMobileDataGen mdg, short cr, EMobileStorage ms);
        
        void UpdateMobile(Mobile_Phone mobile);
       
        void DeleteMobile(Mobile_Phone mobile);
        
        Mobile_Phone FindMobile(EBrand brand);
        
        List<Mobile_Phone> GetMobiles();
    }
}
