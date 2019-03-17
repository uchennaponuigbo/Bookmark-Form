using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Windows.Forms;

namespace Practice_2__Local_Web_Bookmark
{
    public static class WebValidator
    {
        public static bool URLExists(string url)
        {
            bool result = false;
            WebRequest webrequest = WebRequest.Create(url);
            webrequest.Timeout = 1200; //in milliseconds
            webrequest.Method = "HEAD";


            HttpWebResponse response = null;

            try
            {
                response = (HttpWebResponse)webrequest.GetResponse();
                result = true;
            }
            catch (WebException webException)
            {
                MessageBox.Show(url + "does not exist:" + webException.Message);

            }
            finally
            {
                if (response != null)
                    response.Close();
            }
            return result;
        }
        public static bool URLIsValid(string url)
        {

            Stream sStream;
            HttpWebRequest webRequest;
            HttpWebResponse webResponse;
            try
            {

                webRequest = (HttpWebRequest)WebRequest.Create(url);
                webResponse = (HttpWebResponse)webRequest.GetResponse();
                sStream = webResponse.GetResponseStream();
                string read = new StreamReader(sStream).ReadToEnd();

                sStream.Close();

                //Uri www;
                //return Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out www);
                return true;               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "URL error");
                return false;
            }                        
        }       
    }
}
