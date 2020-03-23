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
    public abstract class BaseRepository<T> where T : BaseDto
    {
        public abstract string FileName { get; }

        public BaseRepository()
        {
            if (!File.Exists(FileName))
            {
                using (File.Create(FileName))
                {
                }
            }
        }

        public List<T> GetAll()
        {
            var jss = new JavaScriptSerializer();
            var lines = File.ReadAllLines(FileName);
            var usersFast =
                lines.Select(line => jss.Deserialize<T>(line))
                .ToList();

            return usersFast;
        }

        public void Delete(int id)
        {
            var objsFromDb = GetAll();
            objsFromDb = objsFromDb.Where(x => x.Id != id).ToList();

            var jss = new JavaScriptSerializer();

            using (var file = File.Create(FileName))
            {
                using (var sw = new StreamWriter(file))
                {
                    objsFromDb
                        .ForEach(user =>
                            sw.WriteLine(
                                jss.Serialize(user)
                            )
                        );
                }
            }
        }
    }
}
