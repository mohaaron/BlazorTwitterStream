using Jha.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jha.Services.Events
{
    public class TweetPublishedEventArgs : EventArgs
    {
        public Tweet Tweet { get; set; } = default!;
    }
}
