using System.Net;

namespace TemperatureSensor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // получаем данные по API (не используется)
        /*private void button1_Click(object sender, EventArgs e)
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

        }*/

        private void button2_Click(object sender, EventArgs e)
        {
            string ftpServer = "hidden";  // FTP сервер
            string remoteDirectory = "/test/";  // Папка на сервере, где находятся файлы
            string remoteFilePattern = "sound*.txt";  // Маска для файлов на FTP
            string localDirectory = @"D:\dt\";  // Папка на локальной машине для сохранения файлов
            string userName = "hidden";
            string password = "hidden";

            // Запускаем цикл, который будет работать бесконечно
            while (true)
            {
                try
                {
                    // Получаем список файлов в директории
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpServer + remoteDirectory);
                    request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;  // Запрос на получение списка файлов
                    request.Credentials = new NetworkCredential(userName, password);
                    request.UsePassive = true;  // Включение пассивного режима FTP

                    FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            // Проверяем, что имя файла начинается с "sound" и заканчивается на ".txt"
                            if (line.Contains("sound") && line.EndsWith(".txt"))
                            {
                                string fileName = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Last();
                                string remoteFilePath = ftpServer + remoteDirectory + fileName;  // Полный путь к файлу на сервере
                                string localFilePath = localDirectory + fileName;  // Путь для сохранения на локальной машине

                                // Скачиваем файл с FTP
                                FtpWebRequest downloadRequest = (FtpWebRequest)WebRequest.Create(remoteFilePath);
                                downloadRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                                downloadRequest.Credentials = new NetworkCredential(userName, password);
                                downloadRequest.UsePassive = true;

                                using (FtpWebResponse downloadResponse = (FtpWebResponse)downloadRequest.GetResponse())
                                using (Stream responseStream = downloadResponse.GetResponseStream())
                                using (FileStream localFileStream = new FileStream(localFilePath, FileMode.Create))
                                {
                                    byte[] buffer = new byte[4096];
                                    int bytesRead;
                                    while ((bytesRead = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                                    {
                                        localFileStream.Write(buffer, 0, bytesRead);
                                    }
                                }

                                // После успешного скачивания удаляем файл с сервера
                                FtpWebRequest deleteRequest = (FtpWebRequest)WebRequest.Create(remoteFilePath);
                                deleteRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                                deleteRequest.Credentials = new NetworkCredential(userName, password);
                                deleteRequest.UsePassive = true;

                                using (FtpWebResponse deleteResponse = (FtpWebResponse)deleteRequest.GetResponse())
                                {
                                    //MessageBox.Show($"Файл {fileName} успешно удалён с FTP-сервера.");
                                }

                                // Чтение локально сохраненного файла
                                using (StreamReader localReader = new StreamReader(localFilePath))
                                {
                                    string lineFromFile;
                                    while ((lineFromFile = localReader.ReadLine()) != null)
                                    {
                                        MessageBox.Show(lineFromFile);  // Выводим строки из файла
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке файла: " + ex.Message);
                }

                // Пауза перед следующей проверкой (например, 5 секунд)
                Thread.Sleep(5000);
            }
        }
    }
}

