using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7
{
    internal class Dir
    {
        public string Name { get; set; }
        public Dictionary<string, int> Files { get; set; }
        public List<Dir> SubDirs { get; set; }
        public Dir Parent { get; set; }

        public int Size()
        {
            int total = 0;

            foreach (var size in Files.Values)
            {
                total += size;
            }

            foreach (var dir in SubDirs)
            {
                total += dir.Size();
            }

            return total;
        }
        public Dir(string name) {
            this.Name = name;
            this.Files = new Dictionary<string, int>();
            this.SubDirs = new List<Dir>();
            this.Parent = this;
        }

        public Dir(string name, Dir parent) : this(name)
        {
            this.Parent = parent;
        }

        public int Part1()
        {
            int total = 0;

            foreach (var dir in SubDirs)
            {
                total += dir.Part1();
            }

            if (this.Size() <= 100000)
            {
                total += this.Size();
            }

            return total;
        }

        public void Part2(List<int> sizes ) {
            foreach(var dir in SubDirs)
            {
                sizes.Add(dir.Size());
                dir.Part2(sizes);
            }
        }
    }
}
