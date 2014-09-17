namespace StoreOnLine.Models
{
    public interface IProgressBar
    {
        string GetProgressBar(bool isstriped=false);
        string Getdanger();
        string GetSuccess();
        string GetInfo();
        string GetWarning();

    }
}