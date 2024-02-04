public class Journal {

    public List<Entry> entries;

    public void Display(List<Entry> entries) {
        foreach (Entry entry in entries) {
            entry.Display(entry.date, entry.specialDate, entry.prompt, entry.promptResponse);
        }
    }
}