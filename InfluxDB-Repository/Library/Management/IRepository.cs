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

        public void QueryByTimeLapse(int time, string measurement);

        public void QueryById(int id);

        public double GetAverage(int hours, string measurement);

        public double GetMedian(int hours, string measurement);

        public void DeleteData(string bucket, string organization, int days);
    }
}
