using DataAccessLayer.IService;
using DataAccessLayer.Model.Model;
using DataAccessLayer.Model.Troubles;
using DataAccessLayer.Service;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject1.SampleTestData;

namespace TestProject1.DataAccessLayerTest
{
    internal class ImageServiceTest
    {
        private ImageService _service;
        private ImageData _data;
        [SetUp]
        public void SetUp()
        {
            IConfiguration config = new ConfigurationBuilder()
                                    .AddJsonFile("appsettings.json")
                                    .AddEnvironmentVariables()
                                    .Build();
            _service = new ImageService(config);
            _data = new ImageData(config);
        }
        
    }
}
