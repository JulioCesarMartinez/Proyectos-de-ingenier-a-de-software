using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Actividad1
{
    class Program
    {
        private const string Activity1 = @"C:\Users\ricardo.sanchez.SIEENA\Documents\Tecmilenio\Proyectos de ingenería de software\Actividad 1\a1_equipo1.txt";
        private const string Activity2 = @"C:\Users\ricardo.sanchez.SIEENA\Documents\Tecmilenio\Proyectos de ingenería de software\Actividad 1\a2_equipo1.txt";
        private const string FolderFilePath = @"C:\Users\ricardo.sanchez.SIEENA\Documents\Tecmilenio\Proyectos de ingenería de software\Actividad 1\CS13309_Archivos_HTML\Files\";
        private static readonly string[] FilesPaths = Directory.GetFiles(FolderFilePath);
        static void Main(string[] args)
        {
            /*
             * In the whole activity we are using absolute paths,
             * in order to test it the paths should be change.
            */
            var activityOneStart = DateTime.Now;
            DateTime endProgram;
            double timeFiles = 0;
            double totalTimeProgramTime;
            string totalTimeProgramString;
            string totalTimeOpenFiles;

            try
            {
                //Path of the activity log.
                
                //Opens or creates the file
                using(var sourceStream = File.Open(Activity1, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    foreach (var filePath in FilesPaths)
                    {
                        //Opens the file and calculates the time it took.
                        double timeTookOpen;
                        var timeToOpen = OpenFile(filePath, out timeTookOpen);
                        timeFiles += timeTookOpen;
                        AddText(sourceStream, timeToOpen);
                        JumpLine(sourceStream);
                    }
                    //Writes the total time that took to open the files.
                    totalTimeOpenFiles = $"Tiempo total en abrir los archivos: {timeFiles} segundos";
                    AddText(sourceStream, totalTimeOpenFiles);
                    JumpLine(sourceStream);
                    //The total time the program took to run for activity 1.
                    endProgram = DateTime.Now;
                    totalTimeProgramTime = endProgram.Subtract(activityOneStart).TotalSeconds;
                    totalTimeProgramString = $"Tiempo total de ejecución: {totalTimeProgramTime} segundos";
                    AddText(sourceStream, totalTimeProgramString);
                }
                var activityTwoStart = DateTime.Now;
                using (var sourceStream = File.Open(Activity2, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    foreach (var filePath in FilesPaths)
                    {
                        //Opens the file and calculates the time it took.
                        double timeTookOpen;
                        var timeToOpen = RemoveTags(filePath, out timeTookOpen);
                        timeFiles += timeTookOpen;
                        AddText(sourceStream, timeToOpen);
                        JumpLine(sourceStream);
                    }
                    //Writes the total time that took to remove the tags from the files.
                    totalTimeOpenFiles = $"Tiempo total en eliminar etiquetas HTML: {timeFiles} segundos";
                    AddText(sourceStream, totalTimeOpenFiles);
                    JumpLine(sourceStream);
                    //The total time the program took to run for activity 2.
                    endProgram = DateTime.Now;
                    totalTimeProgramTime = endProgram.Subtract(activityTwoStart).TotalSeconds;
                    totalTimeProgramString = $"Tiempo total de ejecución: {totalTimeProgramTime} segundos";
                    AddText(sourceStream, totalTimeProgramString);
                }
            }
            catch(Exception ex)
            {
                //We didn't know what to do with the exception, so we just put it in a console log.
                Console.WriteLine(ex);
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Write a new line to a Filestream
        /// </summary>
        /// <param name="fs">Filestream</param>
        /// <param name="text">The text to add</param>
        private static void AddText(FileStream fs, string text)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(text);
            fs.Write(info, 0, info.Length);
        }

        /// <summary>
        /// Just a simple method to write a new line
        /// </summary>
        /// <param name="fs"></param>
        private static void JumpLine(FileStream fs)
        {
            byte[] newline = Encoding.ASCII.GetBytes(Environment.NewLine);
            fs.Write(newline, 0, newline.Length);
        }

        /// <summary>
        /// Opens a file and calculates the time it took to open it.
        /// </summary>
        /// <param name="path">The path of the file you want to open.</param>
        /// <param name="timeToOpen">Gives it the value of the time it took.</param>
        /// <returns>Returns a string with the name of the path and the time it took</returns>
        private static string OpenFile(string path, out double timeToOpen)
        {
            var startOpen = DateTime.Now;
            using (var sourceStream = File.Open(path, FileMode.Open))
            {   
            }
            var completeOpen = DateTime.Now;
            timeToOpen = completeOpen.Subtract(startOpen).TotalMilliseconds;
            return $"{path}    {timeToOpen}";
        }

        /// <summary>
        /// Opens a file, remove the HTML tags and calculates the time it took to open it.
        /// </summary>
        /// <param name="path">The path of the file you want to open.</param>
        /// <param name="timeToOpen">Gives it the value of the time it took.</param>
        /// <returns>Returns a string with the name of the path and the time it took</returns>
        private static string RemoveTags(string path, out double timeToOpen)
        {
            var startOpen = DateTime.Now;
            using (var sourceStream = File.Open(path, FileMode.Open))
            {
                string newText;
                using (var reader = new StreamReader(sourceStream))
                {
                    var fileText = reader.ReadToEnd();
                    newText = Regex.Replace(fileText, "<.*?>", String.Empty);
                    sourceStream.SetLength(0);
                    AddText(sourceStream, newText);
                }

            }
            var completeOpen = DateTime.Now;
            timeToOpen = completeOpen.Subtract(startOpen).TotalMilliseconds;
            return $"{path}    {timeToOpen}";
        }
    }
}
