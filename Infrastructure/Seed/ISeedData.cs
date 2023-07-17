using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seed
{
    public interface ISeedData
    {
        void EnsurePopulated(bool dropExisting = false);
    }
}
