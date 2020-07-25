using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShortener.Models;

namespace UrlShortener.DB
{
    public class DataContext
    {
        public List<UrlShortenerModel> urlLinks = new List<UrlShortenerModel>(); 
    }
}
