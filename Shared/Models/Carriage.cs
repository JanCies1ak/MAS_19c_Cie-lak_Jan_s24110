using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Builders;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAS_19c_Cieślak_Jan_s24110.Shared.Models
{
    public abstract class Carriage
    {
        public int Id { get; set; }
        public abstract void ShowDetails();
    }
}
