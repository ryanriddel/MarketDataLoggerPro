using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Google.Cloud.Storage.V1;
using Google.Apis.Upload;
using Google.Apis.Storage.v1;

namespace FastOMS
{
    public class CloudManager
    {
        char _dataDelimiter = ',';

        string _fileTypeSuffix = ".txt";
        string _marketDataStorageBucketName = "scalp_market_data_storage";
        string _projectID = "marketdatastorage";

        StreamReader _sReader;
        StreamWriter _sWriter;

        public StorageClient _storageClient;

        public CloudManager()
        {
            _storageClient = StorageClient.Create();
        }
        public async void DownloadMarketDataFile(string googleFilePathname, string newFilePathname)
        {
            FileStream stream = File.OpenWrite(newFilePathname);
            CloudDownloadProgress newProgress = new CloudDownloadProgress(googleFilePathname);

            await _storageClient.DownloadObjectAsync(_marketDataStorageBucketName, googleFilePathname,
                stream, null, default(System.Threading.CancellationToken), newProgress);
        }

        public async Task UploadMarketDataFile(string filePathname)
        {

            //file name should be in this format: {"QUOTE" or "TRADE"}{delimiter}{Symbol}{delimiter}{Datestamp}
            try
            {
                string fileName = Path.GetFileName(filePathname);
                string directoryName = Path.GetDirectoryName(filePathname);

                DateTime fileCreationDateTime = File.GetCreationTime(filePathname);
                DateTime fileLastWriteTime = File.GetLastWriteTime(filePathname);

                bool isFileOldStyle = false;


                char _filePartsDelimiter = '_';



                string[] fileNameParts = fileName.Split(new char[] { _filePartsDelimiter });

                if (fileNameParts.Length != 4)
                {
                    Console.WriteLine("Invalid file: " + fileName);
                    return;
                }

                bool isQuote = fileNameParts[0] == "QUOTE" ? true : false;

                string symbol = fileNameParts[1];
                string folderName = fileNameParts[2] + fileNameParts[3].Split(new char[] { '.' })[0];

                byte[] bytes = File.ReadAllBytes(filePathname);


                

                string fileOldStyleModifier = "";

                //string folderName = fileCreationDateTime.Month.ToString("00") + "_" + fileCreationDateTime.Day.ToString("00") + "_" + fileCreationDateTime.Year.ToString();

                string newFileName = symbol + "_" + fileNameParts[2] + "_" + fileNameParts[3];

                string newFilePath = (isQuote ? "QuoteLogs" : "TradeLogs") + "/" + fileOldStyleModifier +
                    folderName + "/" + newFileName;



                CloudUploadProgress newProgress = new CloudUploadProgress(bytes.Length);
                newProgress._systemFilePath = filePathname;
                newProgress._googleFilePath = newFilePath;
                _cloudUploadProgressList.Add(newProgress);


                Object result = await _storageClient.UploadObjectAsync(_marketDataStorageBucketName, newFilePath,
                    "text/plain", new MemoryStream(bytes), null, default(System.Threading.CancellationToken), newProgress);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cloud write failed: " + e.ToString());
            }

            return;
        }

        List<CloudUploadProgress> _cloudUploadProgressList = new List<CloudUploadProgress>();

        public class CloudUploadProgress : IProgress<Google.Apis.Upload.IUploadProgress>
        {
            public string _systemFilePath;
            public string _googleFilePath;
            public long _totalFileBytes;

            public CloudUploadProgress(long totalFileBytes)
            {
                _totalFileBytes = totalFileBytes;
            }

            public void Report(IUploadProgress value)
            {
                try
                {
                    if (value.Status == UploadStatus.Starting)
                        Console.WriteLine(_googleFilePath + " upload starting.");
                    else if (value.Status == UploadStatus.Failed)
                        Console.WriteLine(_googleFilePath + " upload failed.");
                    else if (value.Status == UploadStatus.Uploading)
                        Console.WriteLine(_googleFilePath + " upload " + (100 * value.BytesSent / _totalFileBytes).ToString() + "% complete.");
                    else if (value.Status == UploadStatus.Completed)
                        Console.WriteLine(_googleFilePath + " upload complete!");
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.ToString());
                }
            }
        }

        List<CloudDownloadProgress> _cloudDownloadProgressList = new List<CloudDownloadProgress>();

        public class CloudDownloadProgress : IProgress<Google.Apis.Download.IDownloadProgress>
        {
            public string _systemFilePath;
            public string _googleFilePath;

            public CloudDownloadProgress(string _googlePath)
            {
                _googleFilePath = _googlePath;
            }

            public void Report(Google.Apis.Download.IDownloadProgress value)
            {
                if (value.Status == Google.Apis.Download.DownloadStatus.NotStarted)
                    Console.WriteLine(_googleFilePath + " download waiting.");
                else if (value.Status == Google.Apis.Download.DownloadStatus.Failed)
                    Console.WriteLine(_googleFilePath + " download failed.");
                else if (value.Status == Google.Apis.Download.DownloadStatus.Downloading)
                    Console.WriteLine(_googleFilePath + " downloaded " + value.BytesDownloaded + " bytes.");
                else if (value.Status == Google.Apis.Download.DownloadStatus.Completed)
                    Console.WriteLine(_googleFilePath + " download complete!");
            }
        }

        //this is so dumb
        public bool IsOldStyleHistoricalDataFile(string filename)
        {
            if (filename.Contains('_'))
                return true;
            else
                return false;
        }

    }
}
