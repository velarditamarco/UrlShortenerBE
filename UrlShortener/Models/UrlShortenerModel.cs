using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShortener.Models
{
    public class UrlShortenerInput
    {
        [Required ( ErrorMessage = "short link required")]
        [StringLength(maximumLength: 20, ErrorMessage = "You can insert between 3 and 20 characters", MinimumLength = 3)]
        public string ShortLink { get; set; }

        [Required(ErrorMessage = "url required")]
        [Url(ErrorMessage = "url not valid")]
        public string URL { get; set; }

    }

    public class UrlShortenerModel : UrlShortenerInput
    {
        public Guid Id { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}
