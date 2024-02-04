using System.IO; 
public class File {

    public string fileName;

    public void Save(string fileName, List<Entry> entries) {
        using (StreamWriter outputFile = new StreamWriter(fileName)) {
            foreach (Entry entry in entries) {
                outputFile.WriteLine($"{entry.date}***{entry.specialDate}***{entry.prompt}***{entry.promptResponse}");
            }
        }
    }
    public Journal Load(string fileName) {
        string[] lines = System.IO.File.ReadAllLines(fileName);
        Journal newJournal = new Journal();
        newJournal.entries = new List<Entry>();
        foreach (string line in lines) {
            string[] parts = line.Split("***");
            Entry newEntry = new Entry(parts[0], parts[1], parts[2], parts[3]);
            newJournal.entries.Add(newEntry);
        }
        return newJournal;
    }
}