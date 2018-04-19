// Copyright © Microsoft Corporation. Alle Rechte vorbehalten.
// Dieser Code wird gemäß den Bedingungen der 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html) veröffentlicht.


using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace LaserTabAddin
{
    internal class PictureConverter : AxHost
    {
        private PictureConverter() : base("") { }

        static public stdole.IPictureDisp ImageToPictureDisp(Image image)
        {
            return (stdole.IPictureDisp)GetIPictureDispFromPicture(image);
        }
    }
}
