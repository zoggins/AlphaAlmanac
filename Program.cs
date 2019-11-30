using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaAlmanac
{

    struct data
    {
        public string title;
        public string old_dir;

    };

    class Program
    {
        static void Main(string[] args)
        {
            var directory_data = new List<data>();

            Directory.SetCurrentDirectory("..");
            var directories = Directory.GetDirectories(Directory.GetCurrentDirectory());
            foreach (var dir in directories)
            {
                if (Path.GetFileName(dir) != "01")
                {
                    var files = Directory.GetFiles(dir);
                    bool foundTitle = false;
                    foreach (var file in files)
                    {
                        if (Path.GetFileName(file) == "title.txt")
                        {
                            var title = File.ReadAllText(Path.Combine(dir, file));
                            var item = new data() ;
                            item.title = title;
                            Directory.Move(dir, dir + ".temp");
                            item.old_dir = dir + ".temp";
                            directory_data.Add(item);
                            foundTitle = true;
                        }
                    }

                    if (!foundTitle)
                    {
                        var title = Path.GetFileName(dir);
                        System.IO.File.WriteAllText(Path.Combine(dir, "title.txt"), title);
                        var item = new data();
                        item.title = title;
                        Directory.Move(dir, dir + ".temp");
                        item.old_dir = dir + ".temp";
                        directory_data.Add(item);
                    }
                }
            }

            directory_data.Sort((s1, s2) => s1.title.ToLower().CompareTo(s2.title.ToLower()));

            int i = 2;
            foreach(var item in directory_data)
            {
                Directory.Move(item.old_dir, Path.Combine(Directory.GetCurrentDirectory(), i < 100 ? i.ToString("00") : i.ToString("000")));
                ++i;
            }
        }
    }
}
