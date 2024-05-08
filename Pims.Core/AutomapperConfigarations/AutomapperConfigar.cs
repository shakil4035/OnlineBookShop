using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pims.Core.AutomapperConfigarations
{
   public class AutomapperConfigar
    {
        public static void configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<MappingsProfile>();
 
            });
        }
    }
}
