using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkRemoteAdmin
{
    static class ErrorHandler
    {
        public static SharpRaven.RavenClient Raven = new SharpRaven.RavenClient("https://a3f74c99e80d4414a6044094349ebac5:ac050aab41b14ae697cc64687becec9c@app.getsentry.com/48642");

        public static void CaptureException(Exception ex)
        {
            Raven.CaptureException(ex);
        }

        public static void CaptureUnhandledException(Exception ex)
        {
            Raven.CaptureException(ex, null, SharpRaven.Data.ErrorLevel.Fatal);
        }
    }
}
