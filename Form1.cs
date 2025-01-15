using System.Net;
using Newtonsoft.Json.Linq;

namespace TemperatureSensor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            const string api = "ca88e8c853d9c9e518c3bfe1f4ff3cae";

            string url = $"https://api.openweathermap.org/data/2.5/weather?q=Perm&appid={api}&units=metric";
            try
            {
                using (WebClient client = new WebClient())
                {
                    string response = client.DownloadString(url);
                    
                    // Парсим json
                    var json = JObject.Parse(response);
                    string temp = json["main"]["temp"].ToString();
                    label3.Text = temp;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);                
                throw;
            }
            
        }
    }
}
