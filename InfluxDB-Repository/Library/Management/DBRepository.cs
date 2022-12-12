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

namespace Management
{
    
    public class DBRepository: IRepository
    {
        public async Task CreateBucket()
        {
            const string url = "http://localhost:8086";
            const string token = "BcLOyY7HHsDVbRCPvJpAmLVGKLL1Rb4Dg67OJ20Pzoc51DRFo0_TW6FNIPt0gCrS_ENdQwoId20SYqJBFhJ6nw==";
            const string org = "Development";
            const string bucket_name = "Final_project";
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
            Console.WriteLine($"Authorized token to write into Final_project: {authorization.Token}");
        }

        public async Task WriteData(Documents doc)
        {
            var point = doc.ConvertToPointData(WritePrecision.S);

            const string url = "http://localhost:8086";
            const string token = "BcLOyY7HHsDVbRCPvJpAmLVGKLL1Rb4Dg67OJ20Pzoc51DRFo0_TW6FNIPt0gCrS_ENdQwoId20SYqJBFhJ6nw==";
            const string org = "Development";
            const string bucket_name = "Final_project";
            var options = new InfluxDBClientOptions(url)
            {
                Token = token,
                Org = org,
                Bucket = bucket_name
            };

            using var client = new InfluxDBClient(options);

            using (var writeApi = client.GetWriteApi())
            {
                writeApi.WritePoint(point, bucket_name, org);
            }

        }

        public void QueryAll()
        {
            const string url = "http://localhost:8086";
            const string token = "BcLOyY7HHsDVbRCPvJpAmLVGKLL1Rb4Dg67OJ20Pzoc51DRFo0_TW6FNIPt0gCrS_ENdQwoId20SYqJBFhJ6nw==";
            const string org = "Development";
            const string bucket_name = "Final_project";
            var options = new InfluxDBClientOptions(url)
            {
                Token = token,
                Org = org,
                Bucket = bucket_name
            };

            using var client = new InfluxDBClient(options);

            var flux = "from(bucket:\"Final_project\") |> range(start: 0)";

            var queryApi = client.GetQueryApi();

            var tables =  queryApi.QueryAsync(flux, "Development");
            
            foreach(var table in tables.Result)
            {
                foreach(var record in table.Records)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(record.Values));

                    //Console.WriteLine($"{record.GetTime()}: {record.GetField()}: {record.GetValueByKey("_value")}");
                }
            }
        }
    }
}
