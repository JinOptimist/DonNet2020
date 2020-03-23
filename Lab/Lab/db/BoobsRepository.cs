using Lab.dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Lab.db
{
    public class BoobsRepository : BaseRepository<Boobs>
    {
        public override string FileName { get; } = "Boobs.txt";

        public Boobs SaveBoobs(BoobsSize size)
        {
            var all = GetAll();
            var maxId = all.Any() 
                ? all.Max(x => x.Id)
                : 1;
            var boobs = new Boobs();
            boobs.Color = ConsoleColor.Red;
            boobs.Size = size;
            boobs.Id = maxId + 1;

            var json = new JavaScriptSerializer().Serialize(boobs);
            using (var file = File.Open(FileName, FileMode.Append))
            {
                using (var sw = new StreamWriter(file))
                {
                    sw.WriteLine(json);
                }
            }

            return boobs;
        }
    }
}
