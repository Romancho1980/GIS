namespace WinForm_ForTests.Data
{
    public class DataTemplate
    {
        public Element GetData(Element el)
        {
            return new Element(el.X, el.Y);
        }

        public Element GetData_1(Element el)
        {
            return el;
        }
    }
}
