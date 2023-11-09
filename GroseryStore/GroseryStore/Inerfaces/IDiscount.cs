using GroseryStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroseryStore.Inerfaces
{
    public interface IDiscount
    {
        public void ApplyDiscount(ref List<GroseryItem> scannedItems, ref List<SpecialDeals> specialDeals,ref int sum);
    }
}
