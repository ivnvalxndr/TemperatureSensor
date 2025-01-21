# Temperature Sensor FTP File Handler

Этот проект представляет собой приложение на C#, которое взаимодействует с FTP-сервером, скачивает файлы с определенным шаблоном имени (например, начинающиеся с "sound" и заканчивающиеся на ".txt"), удаляет их с сервера после загрузки и выводит содержимое этих файлов на экран.

## Описание

Приложение реализует функциональность, которая позволяет выполнять следующие операции:
1. Подключение к FTP-серверу с использованием учетных данных.
2. Получение списка файлов с заданным шаблоном имени (например, `sound*.txt`).
3. Загрузка найденных файлов на локальную машину.
4. Удаление загруженных файлов с FTP-сервера.
5. Чтение содержимого загруженных файлов и вывод их строк на экран.

### Примечания:
- Код работает в цикле, постоянно проверяя FTP-сервер на наличие новых файлов, соответствующих шаблону.
- Если файл найден, он скачивается, удаляется с сервера и его содержимое выводится через `MessageBox`.

## Установка

1. Скачайте проект и откройте его в **Visual Studio**.
2. Убедитесь, что у вас установлены все необходимые зависимости (например, для работы с FTP).
3. Откройте `Form1.cs` и настройте параметры подключения к FTP-серверу:
    - Замените значения переменных:
        - `ftpServer` — адрес FTP-сервера.
        - `remoteDirectory` — папка на сервере, где находятся файлы.
        - `remoteFilePattern` — шаблон для поиска файлов.
        - `localDirectory` — локальная папка для сохранения файлов.
        - `userName` — имя пользователя для FTP.
        - `password` — пароль для FTP.
4. Постоянно проверяйте сервер на наличие новых файлов с именами, которые соответствуют шаблону (`sound*.txt`).

## Как использовать

1. Запустите программу.
2. После запуска приложение будет работать в фоновом режиме, регулярно проверяя FTP-сервер на наличие новых файлов.
3. Когда приложение находит файл, который соответствует шаблону, оно скачивает его и удаляет с сервера.
4. После загрузки файла содержимое файла будет выведено в виде уведомлений через `MessageBox`.

## Пример кода

Пример использования FTP для скачивания и удаления файла:

```csharp
// Пример скачивания файла с FTP-сервера
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
