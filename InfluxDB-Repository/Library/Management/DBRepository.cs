using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Core.Flux.Domain;
using InfluxDB.Client.Writes;

namespace Management
{
    public class DBRepository: IRepository
    {
        public async Task CreateBucket()
        {
            const string url = "http://localhost:8086";
            const string token = "BcLOyY7HHsDVbRCPvJpAmLVGKLL1Rb4Dg67OJ20Pzoc51DRFo0_TW6FNIPt0gCrS_ENdQwoId20SYqJBFhJ6nw==";
            const string org = "Development";

            using var client = new InfluxDBClient(url, token);

            // Find ID of Organization with specified name (PermissionAPI requires ID of Organization).
            var orgId = (await client.GetOrganizationsApi().FindOrganizationsAsync(org: org)).First().Id;

            //
            // Create bucket "Final_project" with no data retention
            //
            var retention = new BucketRetentionRules(BucketRetentionRules.TypeEnum.Expire, 0);

            var bucket = await client.GetBucketsApi().CreateBucketAsync("Final_project", retention, orgId);

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
    }
}
