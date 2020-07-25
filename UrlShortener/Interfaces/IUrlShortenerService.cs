using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShortener.Helper;
using UrlShortener.Models;

namespace UrlShortener.Interfaces
{
    public interface IUrlShortenerService
    {
        List<UrlShortenerModel> Get();

        UrlShortenerModel GetBy(string shortLink);

        ResponseHandler Create(UrlShortenerInput input);

        ResponseHandler Edit(Guid id, UrlShortenerInput input);

        ResponseHandler Delete(Guid id);
    }
}
