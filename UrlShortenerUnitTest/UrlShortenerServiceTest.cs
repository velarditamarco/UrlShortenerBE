using Castle.Core.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Policy;
using UrlShortener.Controllers;
using UrlShortener.DB;
using UrlShortener.Interfaces;
using UrlShortener.Models;
using UrlShortener.Services;

namespace UrlShortenerUnitTest
{
    public class UrlShortenerServiceTest
    {

        private readonly IUrlShortenerService _service;

        public UrlShortenerServiceTest()
        {
            var loggerMock = new Mock<ILogger<UrlShortenerService>>();
            _service = new UrlShortenerService(loggerMock.Object, new DataContext());
        }

        [SetUp]
        public void Setup()
        {
        }

        private UrlShortenerInput Input = new UrlShortenerInput()
        {
            URL = "http://www.google.com",
            ShortLink = "Google"
        };

        private int UrlLinksCount = 0;

        private bool IsItemCreated = false;

        [Test, Order(0)]
        public void CheckList()
        {
            var list = _service.Get();
            bool isEmptyList = IsEmpty(list);

            if (IsItemCreated)
                Assert.IsFalse(isEmptyList);
            else
                Assert.IsTrue(isEmptyList);

            Assert.AreEqual(this.UrlLinksCount, list.Count);
        }

        [Test, Order(1)]
        public void CreateItemList()
        {
            var response = _service.Create(this.Input);

            Assert.AreEqual(200, response.StatusCode);

            this.IsItemCreated = true;

            this.UrlLinksCount++;
            CheckList();
        }

        [Test, Order(2)]
        public void FindItem()
        {
            var item = _service.GetBy("google");

            Assert.AreEqual(item.ShortLink, "Google");
            Assert.AreEqual(item.URL, "http://www.google.com");
        }

        [Test, Order(3)]
        public void EditItem()
        {

            var list = _service.Get();

            if (!IsEmpty(list))
            {
                // retriving guid of the item to edit
                Guid itemGuid = list[0].Id;

                UrlShortenerInput newItem = new UrlShortenerInput()
                {
                    URL = "http://localhost/",
                    ShortLink = "My app"
                };

                var response = _service.Edit(itemGuid, newItem);

                Assert.AreNotEqual(list[0].URL, Input.URL);
                Assert.AreNotEqual(list[0].ShortLink, Input.ShortLink);

            }
        }


        [Test, Order(4)]
        public void ItemNotFound()
        {
            // insert wrong guid to catch error
            var editResponse = _service.Edit(Guid.NewGuid(), this.Input);
            var deleteResponse = _service.Delete(Guid.NewGuid());

            Assert.AreEqual(404, editResponse.StatusCode);
            Assert.AreEqual(404, deleteResponse.StatusCode);

        }

        [Test, Order(5)]
        public void DeleteItem()
        {
            var list = _service.Get();

            if (!IsEmpty(list))
            {
                // retriving data to edit
                Guid itemGuid = list[0].Id;

                var response = _service.Delete(itemGuid);

                Assert.AreEqual(200, response.StatusCode);


                this.UrlLinksCount--;
                this.IsItemCreated = false;
                CheckList();
            }
        }

        private bool IsEmpty(List<UrlShortenerModel> list) =>
            list.IsNullOrEmpty();

    }
}