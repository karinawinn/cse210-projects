public class Job {
    
    private string company;

    private string jobTitle;

    private int startYear;

    private int endYear;

// Constructor
    public Job(string companyName, string title, int start, int end) {
        company = companyName;
        jobTitle = title;
        startYear = start;
        endYear = end;
    }

    public void Display() {
        Console.WriteLine($"{jobTitle} {company} {startYear}-{endYear}");
    }

}