using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Infrastructure.Services
{
    public class AmazonS3Bts
    {
        private readonly IConfiguration _config;
        public AmazonS3Bts(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateDownloadPreSignedURL(string bucket, string key, double duration = 1)
        {
            var s3Client = new AmazonS3Client
                (_config.GetSection("Cognito")["AccessKeyId"],
                _config.GetSection("Cognito")["AccessSecretKey"],
                RegionEndpoint.GetBySystemName(_config.GetSection("Cognito")["Region"]));
            var request = new GetPreSignedUrlRequest
            {
                BucketName = bucket,
                Key = key,
                Expires = DateTime.UtcNow.AddHours(duration)
            };

            string url = s3Client.GetPreSignedURL(request);
            return url;
        }

        public string GenerateUploadPreSignedURL(string bucket, string key, double duration = 1)
        {
            var s3Client = new AmazonS3Client
                (_config.GetSection("Cognito")["AccessKeyId"],
                _config.GetSection("Cognito")["AccessSecretKey"],
                RegionEndpoint.GetBySystemName(_config.GetSection("Cognito")["Region"]));
            var request = new GetPreSignedUrlRequest
            {
                BucketName = bucket,
                Key = key,
                Verb = HttpVerb.PUT,
                Expires = DateTime.UtcNow.AddHours(duration)
            };

            string url = s3Client.GetPreSignedURL(request);
            return url;
        }
    }
}
