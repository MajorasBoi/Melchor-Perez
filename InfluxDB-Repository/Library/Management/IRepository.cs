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

        public Task WriteData(Documents doc);

        public void QueryAll();

        public void QueryByMeasurement(string measurement);
    }
}
