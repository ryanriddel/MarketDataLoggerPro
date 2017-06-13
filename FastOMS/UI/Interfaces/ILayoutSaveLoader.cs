namespace FastOMS.UI.Interfaces
{
    public interface ILayoutSaveLoader
    {
        string GetLayoutXML();
        void LoadLayoutXML(string xml);
    }
}
