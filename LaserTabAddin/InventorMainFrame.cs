using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserTabAddin
{
    internal class InventorMainFrame : System.Windows.Forms.IWin32Window
    {
        public InventorMainFrame(long hWnd) { m_hWnd = hWnd; }
        public System.IntPtr Handle
        {
            get { return (System.IntPtr)m_hWnd; }
        }
        private long m_hWnd;
    }
}
