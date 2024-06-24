using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBankApp.Infrastructure.Persistance
{
    public class DBSettings
    {
        public const string SectionName = "DBSettings";
        public string? Provider { get; set; }
        public string? ConnectionString { get; set; }
    }
}
