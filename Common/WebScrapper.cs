using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Common
{
    public class WebScrapper : ApplicationContext
    {
        private string url;
        public Thread thread;
        public WebBrowser WebBrowser { get; private set; }
        private AutoResetEvent resetEvent;
        private WebBrowserDocumentCompletedEventHandler documentCompletedEventHandler;
        public WebScrapper(string url, WebBrowserDocumentCompletedEventHandler documentCompletedEventHandler, AutoResetEvent resetEvent)
        {
            this.url = url;
            this.documentCompletedEventHandler = documentCompletedEventHandler;
            this.resetEvent = resetEvent;
            thread = new Thread(new ThreadStart(() =>
            {
                Init();
                Application.Run(this);
            }));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private void Init()
        {
            WebBrowser = new WebBrowser();
            WebBrowser.AllowNavigation = true;
            WebBrowser.DocumentCompleted += documentCompletedEventHandler;
            WebBrowser.Navigate(url);
        }
        protected override void Dispose(bool disposing)
        {
            Application.Exit();
            if (thread != null)
            {
                thread.Abort();
                thread = null;
                return;
            }

            System.Runtime.InteropServices.Marshal.Release(WebBrowser.Handle);
            WebBrowser.Dispose();
            base.Dispose(disposing);
        }
    }
}
