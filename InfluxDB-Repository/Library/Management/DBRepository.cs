using System;
using Newtonsoft.Json;
using Microsoft.CSharp;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Core.Flux.Domain;
using InfluxDB.Client.Writes;
using Models;
using Mappers;

namespace Management
{
    public class DBRepository: IRepository
    {
        public async Task CreateBucket()
        {
            const string url = "http://localhost:8086";
            const string token = "BcLOyY7HHsDVbRCPvJpAmLVGKLL1Rb4Dg67OJ20Pzoc51DRFo0_TW6FNIPt0gCrS_ENdQwoId20SYqJBFhJ6nw==";
            const string org = "Development";
            const string bucket_name = "Data Center";
            var options = new InfluxDBClientOptions(url)
            {
                Token = token,
                Org = org,
                Bucket = bucket_name
            };

            using var client = new InfluxDBClient(url, token);

            // Find ID of Organization with specified name (PermissionAPI requires ID of Organization).
            var orgId = (await client.GetOrganizationsApi().FindOrganizationsAsync(org: org)).First().Id;

            //
            // Create bucket "Final_project" with no data retention
            //
            var retention = new BucketRetentionRules(BucketRetentionRules.TypeEnum.Expire, 0);

            var bucket = await client.GetBucketsApi().CreateBucketAsync(bucket_name, retention, orgId);

            //
            // Create access token to "Final_project"
            //
            var resource = new PermissionResource(PermissionResource.TypeBuckets, bucket.Id, null,
                orgId);

            // Read permission
            var read = new Permission(Permission.ActionEnum.Read, resource);

            // Write permission
            var write = new Permission(Permission.ActionEnum.Write, resource);

            var authorization = await client.GetAuthorizationsApi()
                .CreateAuthorizationAsync(orgId, new List<Permission> { read, write });

            //
            // Created token that can be use for writes to "iot_bucket"
            //
            Console.WriteLine($"Authorized token to write into Data Center: {authorization.Token}");
        }

        public async Task WriteData(Sensor sensor)
        {
            const string url = "http://localhost:8086";
            const string token = "BcLOyY7HHsDVbRCPvJpAmLVGKLL1Rb4Dg67OJ20Pzoc51DRFo0_TW6FNIPt0gCrS_ENdQwoId20SYqJBFhJ6nw==";
            const string org = "Development";
            const string bucket_name = "Data Center";
            var options = new InfluxDBClientOptions(url)
            {
                Token = token,
                Org = org,
                Bucket = bucket_name
            };

            using var client = new InfluxDBClient(options);
            var converter = new DomainEntityConverter();

            await client.GetWriteApiAsync(converter).WritePointAsync(converter.ConvertToPointData(sensor, WritePrecision.S), bucket_name, org);

            Console.WriteLine("Successfully written point!");

        }

        public void QueryAll()
        {
            const string url = "http://localhost:8086";
            const string token = "BcLOyY7HHsDVbRCPvJpAmLVGKLL1Rb4Dg67OJ20Pzoc51DRFo0_TW6FNIPt0gCrS_ENdQwoId20SYqJBFhJ6nw==";
            const string org = "Development";
            const string bucket_name = "Data Center";
            var options = new InfluxDBClientOptions(url)
            {
                Token = token,
                Org = org,
                Bucket = bucket_name
            };

            using var client = new InfluxDBClient(options);

            var flux = $"from(bucket:\"{bucket_name}\") |> range(start: 0)";

            var converter = new DomainEntityConverter();

            var queryApi = client.GetQueryApi();

            var tables =  queryApi.QueryAsync(flux, "Development");
            
            foreach(var table in tables.Result)
            {
                foreach(var record in table.Records)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(converter.ConvertToEntity<Sensor>(record)));
                }
            }
        }

        public void QueryByMeasurement(string measurement)
        {
            const string url = "http://localhost:8086";
            const string token = "BcLOyY7HHsDVbRCPvJpAmLVGKLL1Rb4Dg67OJ20Pzoc51DRFo0_TW6FNIPt0gCrS_ENdQwoId20SYqJBFhJ6nw==";
            const string org = "Development";
            const string bucket_name = "Data Center";
            var options = new InfluxDBClientOptions(url)
            {
                Token = token,
                Org = org,
                Bucket = bucket_name
            };

            using var client = new InfluxDBClient(options);

            var flux = $"from(bucket:\"{bucket_name}\") " +
                $"|> range(start: 0) " +
                $"|> filter(fn:(r) => r[\"_measurement\"] == \"{measurement}\")";

            var queryApi = client.GetQueryApi();

            var converter = new DomainEntityConverter();

            var tables = queryApi.QueryAsync(flux, "Development");

            foreach (var table in tables.Result)
            {
                foreach (var record in table.Records)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(converter.ConvertToEntity<Sensor>(record)));
                }
            }
        }

        public void QueryByTimeLapse(int minutes, string measurement)
        {
            const string url = "http://localhost:8086";
            const string token = "BcLOyY7HHsDVbRCPvJpAmLVGKLL1Rb4Dg67OJ20Pzoc51DRFo0_TW6FNIPt0gCrS_ENdQwoId20SYqJBFhJ6nw==";
            const string org = "Development";
            const string bucket_name = "Data Center";
            var options = new InfluxDBClientOptions(url)
            {
                Token = token,
                Org = org,
                Bucket = bucket_name
            };

            using var client = new InfluxDBClient(options);

            var flux = $"from(bucket:\"{bucket_name}\") " +
                $"|> range(start: {minutes*-1}m) " +
                $"|> filter(fn:(r) => r[\"_measurement\"] == \"{measurement}\")";

            var queryApi = client.GetQueryApi();

            var converter = new DomainEntityConverter();

            var tables = queryApi.QueryAsync(flux, "Development");

            double max = 0;
            double min = 100000000;

            foreach (var table in tables.Result)
            {
                foreach (var record in table.Records)
                {
                    if(converter.ConvertToEntity<Sensor>(record).Value > max)
                    {
                        max = converter.ConvertToEntity<Sensor>(record).Value;
                    }
                    if(converter.ConvertToEntity<Sensor>(record).Value < min)
                    {
                        min = converter.ConvertToEntity<Sensor>(record).Value;
                    }
                }
            }

            Console.WriteLine(measurement);
            Console.WriteLine("Max: " + max);
            Console.WriteLine("Min: " + min);
        }

        public void QueryById(int id)
        {
            const string url = "http://localhost:8086";
            const string token = "BcLOyY7HHsDVbRCPvJpAmLVGKLL1Rb4Dg67OJ20Pzoc51DRFo0_TW6FNIPt0gCrS_ENdQwoId20SYqJBFhJ6nw==";
            const string org = "Development";
            const string bucket_name = "Data Center";
            var options = new InfluxDBClientOptions(url)
            {
                Token = token,
                Org = org,
                Bucket = bucket_name
            };

            using var client = new InfluxDBClient(options);

            var flux = $"from(bucket:\"{bucket_name}\") " +
                $"|> range(start: 0) " +
                $"|> filter(fn:(r) => r[\"id\"] == \"{id}\")";

            var queryApi = client.GetQueryApi();

            var converter = new DomainEntityConverter();

            var tables = queryApi.QueryAsync(flux, "Development");

            foreach (var table in tables.Result)
            {
                foreach (var record in table.Records)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(converter.ConvertToEntity<Sensor>(record)));
                }
            }
        }

        public double GetAverage(int hours, string measurement)
        {
            const string url = "http://localhost:8086";
            const string token = "BcLOyY7HHsDVbRCPvJpAmLVGKLL1Rb4Dg67OJ20Pzoc51DRFo0_TW6FNIPt0gCrS_ENdQwoId20SYqJBFhJ6nw==";
            const string org = "Development";
            const string bucket_name = "Data Center";
            var options = new InfluxDBClientOptions(url)
            {
                Token = token,
                Org = org,
                Bucket = bucket_name
            };

            int i = 0;
            double avg = 0;

            using var client = new InfluxDBClient(options);

            var flux = $"from(bucket:\"{bucket_name}\") " +
                $"|> range(start: {hours*-1}h) " +
                $"|> filter(fn:(r) => r[\"_measurement\"] == \"{measurement}\")";

            var queryApi = client.GetQueryApi();

            var converter = new DomainEntityConverter();

            var tables = queryApi.QueryAsync(flux, "Development");

            foreach (var table in tables.Result)
            {
                foreach (var record in table.Records)
                {
                    ++i;
                    Sensor temp = new Sensor(converter.ConvertToEntity<Sensor>(record));
                    avg += temp.Value;
                }
            }

            avg = avg / i;

            if (measurement == "Temperature")
                Console.WriteLine(measurement + " average is: " + avg + "°C from " + hours + " hours ago until now.");
            else
                Console.WriteLine(measurement + " average percentage is: " + avg + "% from " + hours + " hours ago until now.");
            
            
            return avg;
        }

        public double GetMedian(int hours, string measurement)
        {
            const string url = "http://localhost:8086";
            const string token = "BcLOyY7HHsDVbRCPvJpAmLVGKLL1Rb4Dg67OJ20Pzoc51DRFo0_TW6FNIPt0gCrS_ENdQwoId20SYqJBFhJ6nw==";
            const string org = "Development";
            const string bucket_name = "Data Center";
            var options = new InfluxDBClientOptions(url)
            {
                Token = token,
                Org = org,
                Bucket = bucket_name
            };

            List<double> list = new List<double>();

            using var client = new InfluxDBClient(options);

            var flux = $"from(bucket:\"{bucket_name}\") " +
                $"|> range(start: {hours * -1}h) " +
                $"|> filter(fn:(r) => r[\"_measurement\"] == \"{measurement}\")";

            var queryApi = client.GetQueryApi();

            var converter = new DomainEntityConverter();

            var tables = queryApi.QueryAsync(flux, "Development");

            foreach (var table in tables.Result)
            {
                foreach (var record in table.Records)
                {
                    list.Add(converter.ConvertToEntity<Sensor>(record).Value);
                }
            }

            double[] arr = list.ToArray();
            int i = arr.Length;
            if (i % 2 == 1)
            {
                i = i / 2;
                return list.ElementAt<double>(--i);
            }
            i = i / 2;
            double temp1 = list.ElementAt<double>(i - 1);
            double temp2 = list.ElementAt<double>(i);

            return ((temp1 + temp2) / 2);
        }

        public void DeleteData(string bucket, string organization, int days)
        {
            string Token = "BcLOyY7HHsDVbRCPvJpAmLVGKLL1Rb4Dg67OJ20Pzoc51DRFo0_TW6FNIPt0gCrS_ENdQwoId20SYqJBFhJ6nw==";
            InfluxDBClient client = new InfluxDBClient("http://localhost:8086", Token);
            client.GetDeleteApi().Delete(DateTime.UtcNow.AddDays(days * (-1)), DateTime.UtcNow, "", bucket, organization);

            Console.WriteLine("Successfully deleted from " + days + " days until current time.");
        }
    }
}
    
