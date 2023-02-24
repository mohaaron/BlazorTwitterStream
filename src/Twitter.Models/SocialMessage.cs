using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Models
{
    public record SocialMessage(string Message)
    {
        public List<Hashtag> Hashtags { get; set; } = new();
    }
}
