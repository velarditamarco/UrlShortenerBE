using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using UrlShortener.DB;
using UrlShortener.Helper;
using UrlShortener.Interfaces;
using UrlShortener.Models;

namespace UrlShortener.Services
{
    public class UrlShortenerService : IUrlShortenerService
    {
        private readonly ILogger<UrlShortenerService> _log;

        private DataContext _data; 

        public UrlShortenerService(ILogger<UrlShortenerService> logger, DataContext data)
        {
            _log = logger;
            _data = data;
        }

        public ResponseHandler Create(UrlShortenerInput input)
        {
            var itemDb = GetBy(input.ShortLink);

            if (itemDb != null)
                return new ResponseHandler(400, "ShortLink already exist");

            UrlShortenerModel newItem = new UrlShortenerModel()
            {
                Id = Guid.NewGuid(),
                ShortLink = input.ShortLink,
                URL = input.URL,
                CreationDate = DateTime.Now
            };

            _data.urlLinks.Add(newItem);

            return new ResponseHandler(200, "Item created");
        }

        public ResponseHandler Delete(Guid id)
        {
            var item = _data.urlLinks.FirstOrDefault(x => x.Id == id);

            if (item == null)
                return new ResponseHandler(404, "Item not found");

            _data.urlLinks.Remove(item);

            return new ResponseHandler(200, "Item deleted");
        }

        public ResponseHandler Edit(Guid id, UrlShortenerInput input)
        {
            var item = _data.urlLinks.FirstOrDefault(x => x.Id == id);

            if (item == null)
                return new ResponseHandler(404, "Item not found");

            var itemDb = GetBy(input.ShortLink);

            if (itemDb != null)
                return new ResponseHandler(400, "ShortLink already exist");

           
            item.ShortLink = input.ShortLink;
            item.URL = input.URL;
            item.UpdateDate = DateTime.Now;

            return new ResponseHandler(200, "Item updated");
        }

        public List<UrlShortenerModel> Get() =>
            _data.urlLinks;

        public UrlShortenerModel GetBy(string shortLink) =>
            _data.urlLinks.FirstOrDefault(x => x.ShortLink.Trim().ToLower() == shortLink.Trim().ToLower());
          
    }
}
