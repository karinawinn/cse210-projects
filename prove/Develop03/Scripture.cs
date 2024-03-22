public class Scripture {

  private List<Word> words;

  public Scripture(string text) {
      this.words = text.Split(' ').Select((word,index) => new Word(index,word)).ToList();
  }

  public void Display() {}
  public void HideWords(int numToHide = 2) {
    Random number = new Random();
    int wordsLeft = this.words.Count(word => !word.IsHidden());
    numToHide = Math.Min(numToHide, wordsLeft);
    for (int i = 0; i < numToHide; i++) {
      int index = number.Next(0, this.words.Count);
      while (this.words[index].IsHidden()) {
        index = number.Next(0, this.words.Count);
      }
      this.words[index].Hide();
    }
  }
  public override string ToString() {
    string toPrint = "";
    foreach (Word word in this.words) {
      toPrint = toPrint + $"{word}" + " ";
    }
    return toPrint;
  }
}