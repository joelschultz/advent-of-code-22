namespace Days {
    public class Day7 : DaySolverBase {
        public override string Example1 =>
            @"$ cd /
$ ls
dir a
14848514 b.txt
8504156 c.dat
dir d
$ cd a
$ ls
dir e
29116 f
2557 g
62596 h.lst
$ cd e
$ ls
584 i
$ cd ..
$ cd ..
$ cd d
$ ls
4060174 j
8033020 d.log
5626152 d.ext
7214296 k";

        struct Directory {
            public Dictionary<string, Directory> directories;
            public Dictionary<string, int> files;
            public string parentDirectoryName;
        }

        
        
        Directory Transform(string raw) {

            Directory outermostDirectory = new Directory {
                directories = new Dictionary<string, Directory>(),
                files = new Dictionary<string, int>()
            };

            List<Directory> directoryNest = new List<Directory> { outermostDirectory };

            List<string> inputRows = raw.Split("\n").ToList();

            for(int currentInputRowIndex = 1; currentInputRowIndex < inputRows.Count; currentInputRowIndex++) {
                string currentInputRow = inputRows[currentInputRowIndex];
                if(currentInputRow[0] == '$') {
                    //Execute command
                    if(currentInputRow.Contains("cd")) {
                        string directoryNameToMoveTo = currentInputRow.Substring(5);
                        if(directoryNameToMoveTo == "..") {
                            directoryNest.RemoveAt(directoryNest.Count - 1);
                        }
                        else {
                            directoryNest.Add(directoryNest[^1].directories[directoryNameToMoveTo]);
                        }
                    }
                    else if(currentInputRow.Contains("ls")) {
                        int endIndexForNonCommand = currentInputRowIndex;
                        for(int j = currentInputRowIndex + 1; j < inputRows.Count; j++) {
                            if(inputRows[j][0] == '$') {
                                break;
                            }
                            if(inputRows[j].Contains("dir")) {
                                //Add directory
                                string directoryName = inputRows[j].Substring(4);
                                if(!directoryNest[^1].directories.TryGetValue(directoryName, out Directory existingDirectory)) {
                                    Directory newDirectory = new Directory {
                                        files = new Dictionary<string, int>(),
                                        directories = new Dictionary<string, Directory>()
                                    };
                                    directoryNest[^1].directories.Add(directoryName, newDirectory);
                                }
                            }
                            else {
                                //Add file
                                List<string> nameAndSizeOfFile = inputRows[j].Split(" ").ToList();

                                int.TryParse(nameAndSizeOfFile[0], out int fileSize);
                                string fileName = nameAndSizeOfFile[1];

                                directoryNest[^1].files.Add(fileName, fileSize);
                            }
                        }
                    }
                }
                
            }
            
            return outermostDirectory;
        }

        public override object Solve1(string raw) {
            var outermostDirectory = Transform(raw);

            foreach(KeyValuePair<string, Directory> directory in outermostDirectory.directories) {
                Console.WriteLine(directory.Key);
                int totalFileSize = 0;
                foreach(var file in directory.Value.files) {
                    totalFileSize += file.Value;
                }
                foreach(var childDirectory in directory.Value.directories) {
                    foreach(var file in childDirectory.Value.files) {
                        totalFileSize += file.Value;
                    }
                }
            }
            return outermostDirectory;
        }

        public override object Solve2(string raw) {
            var input = Transform(raw);

            return -1;
        }
    }
}