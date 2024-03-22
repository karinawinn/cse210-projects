using System.Dynamic;
using System.Runtime.CompilerServices;
public class Word {
    private string text;
    private bool hidden;
    private int index;
    public Word(string text) {
        this.text = text;
    }
    public Word(int index, string text) {
        this.index = index;
        this.text = text;
    }
    public int GetIndex() {
        return this.index;
    }
    public void Hide() {
        this.hidden = true;
    }
    public bool IsHidden() {
        return this.hidden;
    }
    public override string ToString()
    {
        if (!hidden) {
            return this.text;
        }
        else {
            int len = this.text.Length;
            string underscores = "";
            while (len > 0) {
                underscores = underscores + "_";
                len -= 1;
            } 
            return underscores;
        } 
    }
}