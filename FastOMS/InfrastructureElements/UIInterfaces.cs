using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MktSrvcAPI;

namespace FastOMS
{
    
    public enum WindowType
    {
        Level2,
        Spread,
        OrderViewer
    }

    
    public abstract class WindowUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        public static Dictionary<Guid, WindowUserControl> windowUserControlDict = new Dictionary<Guid, WindowUserControl>();

        public event WindowUserControlDataReceivedEventHandler WindowUserControlDataReceived = delegate { };

        Window _parent;

        Guid _windowUserControlID = Guid.NewGuid();

        public WindowUserControl(Window parent)
        {
            _parent = parent;
            WindowUserControlDataReceived += DataReceivedFromWindowUserControl;
            windowUserControlDict[_windowUserControlID] = this;
        }

        public WindowUserControl()
        {
            throw new Exception("This should only be used when visual studio forces user controls to have parameterless constructors");
        }

        private void SendDataToWindowUserControl(object data, WindowUserControl windowUserControl)
        {
            windowUserControl.WindowUserControlDataReceived(data, windowUserControl);
        }

        protected abstract void DataReceivedFromWindowUserControl(object data, WindowUserControl windowUserControl);


    }


    //This should be an abstract class but visual studio's designer cannot render forms which
    //inherit from abstract classes 
    public class Window : DevExpress.XtraEditors.XtraForm
    {
        public static List<Window> windowList = new List<Window>();
        public List<WindowUserControl> userControlList = new List<WindowUserControl>();
        

        Guid _windowID = Guid.NewGuid();
        WindowType _windowType;
        public InstrInfo[] instruments;

        public Window(WindowType windowType)
        {
            _windowType = windowType;
        }

        public Window()
        {

        }


        private void SendDataToWindow(object data, Window window)
        {
            //window.WindowDataReceived(data, this);

        }

        //protected abstract void DataReceivedFromWindow(object data, Window window);

        private void SendDataToWindowUserControl(object data, WindowUserControl control)
        {
        }

        //protected abstract void DataReceivedFromWindowUserControl(object data, WindowUserControl control);
    }



    public delegate void WindowDataReceivedEventHandler(object data, Window sender);
    public delegate void WindowUserControlDataReceivedEventHandler(object data, WindowUserControl sender);

}
