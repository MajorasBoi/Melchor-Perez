using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Management
{
    public interface IRepository
    {
        public Task CreateBucket();

        public Task WriteData(Sensor sensor);

        public void QueryAll();

        public void QueryByMeasurement(string measurement);

        public void QueryById(int id);

        public void DeleteData(string bucket, string organization);
    }
}
