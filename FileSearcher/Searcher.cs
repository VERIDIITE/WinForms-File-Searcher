using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearcher
{

    delegate void FileFound(string path);
    class Searcher
    {
        private string term;
        private string dir;

        public event FileFound OnFileFound;



        public Searcher(string dir, string term)
        {
            this.dir = dir;
            this.term = term;
        }

        private void Scan(String dir)
        {
            string[] files = System.IO.Directory.GetFiles(dir);
            string[] dirs = System.IO.Directory.GetDirectories(dir);

            List<string> allFiles = new List<string>();
            allFiles.AddRange(files);
            allFiles.AddRange(dirs);

            foreach(string s in allFiles)
            {
                string _s = s.ToLower();
                string _term = this.term.ToLower();

                if (Directory.Exists(s) && s != ".." && s != "..")
                {
                    Scan(s);
                    continue;
                }

                /// Check if the file contains MATCHES
                if (_s.Contains(_term))
                {
                    OnFileFound(s);
                }
            }

        }

        public void Start()
        {
            Scan(this.dir);
        }

        public string Term
        {
            set { term = value; }
            get { return this.term; }
        }

        public string Dir
        {
            set { dir = value; }
            get { return this.dir; }
        }
    }
}
