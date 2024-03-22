public class Reference {

    private string book;
    private string chapter;
    private List<int> verses;
    private string verseRange;
    private List<Scripture> text;

    public Reference(string book, string chapter, List<int> verses, string text) {
        this.book = book;
        this.chapter = chapter;
        this.verses = verses;
        this.text = new List<Scripture>([new Scripture(text)]);
    }
    public Reference(string book, string chapter, string verseRange, List<int> verses, string text) {
        this.book = book;
        this.chapter = chapter;
        this.verseRange = verseRange;
        this.verses = verses;
        this.text = [];
        string toSplit = text;
        for (int i = 0; i <= verses.Count - 1; i++) {
            string splitChar = verses[i].ToString() + " ";
            string[] eachVerse = toSplit.Split(splitChar);
            if (i > 0) {
                Scripture verse = new Scripture(eachVerse[0]);
                this.text.Add(verse);
            }
            toSplit = eachVerse[1];
            if (i == verses.Count - 1) {
                this.text.Add(new Scripture(eachVerse[1]));
            }
        }
    }
    public override string ToString() {
        string printThis;
        if (verses.Count() > 1) {
            printThis = $"{this.book} {this.chapter}:{this.verseRange}";
            for (int i = 0; i <= verses.Count - 1; i++) {
                printThis = printThis + $"\n\n{verses[i]}. {this.text[i]}";
            }
        }
        else {
            printThis = $"{this.book} {this.chapter}:{this.verses[0]}\n\n{this.text[0]}";
        }
        return printThis;
        }
        
    public void HideWords(int numToHide = 2) {
        foreach (Scripture scripture in text) {
            scripture.HideWords(numToHide);
        }
    }
    public void PrintPartialScripture() {
        for (int i = 0; i <= verses.Count - 1; i++) {
            Console.WriteLine($"\n{verses[i]}. " + this.text[i]);
        }
    }
}