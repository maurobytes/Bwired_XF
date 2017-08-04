using Bwired.Models;
using System;

namespace Bwired.Helpers
{
    public interface ITweetStore
    {
        void Save(System.Collections.Generic.List<Tweet> tweets);
    }
}