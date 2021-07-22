using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CompleX_Library
{
    /// <summary>
    /// Klasse mit statischen Utility-Methoden für das Dateisystem
    /// </summary>
    public static class FileSystemUtils
    {

        [Serializable]
        public struct ShellExecuteInfo
        {
            public int Size;
            public uint Mask;
            public IntPtr hwnd;
            public string Verb;
            public string File;
            public string Parameters;
            public string Directory;
            public uint Show;
            public IntPtr InstApp;
            public IntPtr IDList;
            public string Class;
            public IntPtr hkeyClass;
            public uint HotKey;
            public IntPtr Icon;
            public IntPtr Monitor;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public IntPtr iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }


        public const uint SW_NORMAL = 1;
        private const int SW_SHOW = 5;
        private const uint SEE_MASK_INVOKEIDLIST = 12;

        // Code For OpenWithDialog Box

        [DllImport("shell32.dll", SetLastError = true)]
        extern public static bool
               ShellExecuteEx(ref ShellExecuteInfo lpExecInfo);

        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes,
            ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

        /// <summary>
        /// Prüft, ob eine Datei ausfürbar ist (.exe, .bat, etc.)
        /// </summary>
        /// <param name="path">Pfad zu der Datei.</param>
        /// <returns>true, wenn die Datei ausführbar ist, ansonsten false.</returns>
        public static bool IsExecutable(string path)
        {
            SHFILEINFO fi = new SHFILEINFO();
            int SHGFI_EXETYPE = 0x000002000;     // return exe type

            IntPtr res = SHGetFileInfo(
                path,
                0,
                ref fi,
                (uint)System.Runtime.InteropServices.Marshal.SizeOf(fi),
                (uint) SHGFI_EXETYPE);

            return (res != IntPtr.Zero);
        }


        public static void ShowProperties(string path)
        {
            var fi = new FileInfo(path);

            var info = new ShellExecuteInfo();
            info.Size = Marshal.SizeOf(info);
            info.Verb = "properties";
            info.File = fi.Name;
            info.Directory = fi.DirectoryName;
            info.Show = SW_SHOW;
            info.Mask = SEE_MASK_INVOKEIDLIST;
            ShellExecuteEx(ref info);
        }



        public static void OpenAs(string file)
        {
            ShellExecuteInfo sei = new ShellExecuteInfo();
            sei.Size = Marshal.SizeOf(sei);
            sei.Verb = "openas";
            sei.File = file;
            sei.Show = SW_NORMAL;
            if (!ShellExecuteEx(ref sei))
                throw new System.ComponentModel.Win32Exception();
        }

        /// <summary>
        /// Deklaration der API-Funktion PathRelativePathTo
        /// </summary>
        [DllImport("shlwapi.dll", CharSet = CharSet.Auto)]
        static extern bool PathRelativePathTo(StringBuilder pszPath,
                                              string pszFrom, uint dwAttrFrom, string pszTo, uint dwAttrTo);

        /// <summary>
        /// Ermittelt den relativen Pfad eines absoluten Pfades
        /// </summary>
        /// <param name="absolutePath">Der absolute Pfad</param>
        /// <param name="absolutePathIsDirectory">Gibt an, ob es sich bei dem absoluten Pfad um ein Verzeichnis handelt (anderenfalls handelt es sich um eine Datei)</param>
        /// <param name="referencePath">Der Pfad, auf den sich der relative Pfad bezieht</param>
        /// <param name="referencePathIsDirectory">Gibt an, ob es sich bei dem Bezugs-Pfad um ein Verzeichnis handelt (anderenfalls handelt es sich um eine Datei)</param>
        /// <returns>Gibt den relativen Pfad zurück</returns>
        /// <exception cref="IOException">Wird geworfen falls der absolute und der Referenz-Pfad keine gemeinsame Basis besitzen</exception>
        public static string GetRelativePath(string absolutePath,
                                             bool absolutePathIsDirectory, string referencePath,
                                             bool referencePathIsDirectory)
        {
            // Die Pfadangaben normalisieren für den Fall, dass
            // ..- und .-Angaben enthalten sind
            absolutePath = Path.GetFullPath(absolutePath);
            referencePath = Path.GetFullPath(referencePath);

            const uint FILE_ATTRIBUTE_DIRECTORY = 0x10;
            const int MAX_PATH = 260;

            // PathRelativePathTo aufrufen
            StringBuilder relativePath = new StringBuilder(MAX_PATH);
            if (PathRelativePathTo(relativePath, referencePath,
                                   (referencePathIsDirectory ? FILE_ATTRIBUTE_DIRECTORY : 0),
                                   absolutePath,
                                   (absolutePathIsDirectory ? FILE_ATTRIBUTE_DIRECTORY : 0)))
            {
                return relativePath.ToString();
            }
            else
            {
                return absolutePath;
            }
        }

        /// <summary>
        /// Ermittelt für einen gegebenen relativen einen absoluten Pfad
        /// </summary>
        /// <param name="relativePath">Der relative Pfad</param>
        /// <param name="referencePath">Der Pfad, auf den sich der relative Pfad bezieht</param>
        /// <returns>Gibt den ermittelten absoluten Pfad zurück</returns>
        public static string GetAbsolutePath(string relativePath,
                                             string referencePath)
        {
            if (referencePath.EndsWith("\\") == false)
            {
                referencePath += "\\";
            }
            return Path.GetFullPath(referencePath + relativePath);
        }


        /// <summary>
        /// Kopiert einen Ordner per API-Funktion
        /// </summary>
        /// <param name="sourceFolderPath">Pfad zum Quellordner</param>
        /// <param name="destFolderPath">Pfad zum Zielordner</param>
        /// <param name="confirmOverwrites">Gibt an, ob das Überschreiben von 
        /// Ordnern oder Dateien vom Benutzer bestätigt werden soll</param>
        /// <exception cref="IOException">Wird geworfen, wenn der dem Zielordner
        /// übergeordnete Ordner nicht existiert, der Quellordner nicht 
        /// existiert oder beim Kopieren einer der (leider nicht dokumentierten)
        /// Fehler auftritt</exception>
        /// <remarks>
        /// <para>
        /// Bei der Anwendung dieser Methode müssen Sie beachten, 
        /// dass die Angabe des Ziel-Ordners für SHFileOperation der Ordner ist, 
        /// der den zu kopierenden Ordner aufnehmen soll. 
        /// Wenn Sie den Ordner C:\Codebook z.B. in den Ordner G:\Backup 
        /// als Unterordner 'Codebook' kopieren wollen, müssen Sie als Quelle
        /// 'C:\Codebook' und als Ziel 'G:\Backup' angeben.
        /// </para>
        /// </remarks>
        public static void CopyFolder(string sourceFolderPath,
                                      string destFolderPath, bool confirmOverwrites)
        {
            // Überprüfen, ob der Zielordner existiert, 
            // um zum einen das Problem zu vermeiden, dass SHFileOperation beim 
            // Kopieren auf ein nicht existierendes Laufwerk ohne Fehler 
            // ausgeführt wird und zum anderen den Fehler zu vermeiden, den
            // SHFileOperation meldet, wenn dieser Ordner nicht existiert.
            if (Directory.Exists(destFolderPath) == false)
            {
                // Ziel-Parent-Ordner existiert nicht: Ausnahme werfen
                throw new IOException("Der Ziel-Ordner " + destFolderPath +
                                      " existiert nicht");
            }

            // Überprüfen, ob der Quellordner existiert
            if (Directory.Exists(sourceFolderPath) == false)
            {
                throw new IOException("Der Quell-Ordner " + sourceFolderPath +
                                      " existiert nicht");
            }

            // Struktur für die Dateiinformationen erzeugen
            SHFILEOPSTRUCT fileOp = new SHFILEOPSTRUCT();

            // (Unter-)Funktion definieren (ShFileOperation kann auch Dateien und
            // Ordner löschen, verschieben oder umbenennen)
            fileOp.wFunc = FO_COPY;

            // Quelle und Ziel definieren. Dabei müssen mehrere Datei- oder
            // Ordnerangaben über 0-Zeichen getrennt werden. Am Ende muss ein
            // zusätzliches 0-Zeichen stehen
            fileOp.pFrom = sourceFolderPath + "\x0\x0";
            fileOp.pTo = destFolderPath + "\x0\x0";

            // Flags setzen, sodass ein Rückgängigmachen möglich ist und 
            // dass - je nach Argument confirmOverwrites - keine Nachfrage 
            // beim Überschreiben von Ordnern beim Anwender erfolgt
            if (confirmOverwrites)
            {
                fileOp.fFlags = FOF_ALLOWUNDO;
            }
            else
            {
                fileOp.fFlags = FOF_ALLOWUNDO | FOF_NOCONFIRMATION;
            }

            // ShFileOperation unter Übergabe der Struktur aufrufen
            int result = SHFileOperation(ref fileOp);

            // Erfolg auswerten. SHFileOperation liefert
            // 0 zurück, wenn kein Fehler aufgetreten ist, ansonsten einen
            // (leider undokumentierten) Wert ungleich 0.
            if (result != 0)
            {
                throw new IOException("Error " + result +
                                      " while copy folder '" +
                                      sourceFolderPath + "' to destination '" +
                                      destFolderPath + "'");
            }
        }

        /// <summary>
        /// AOI-Funktion für verschiedene Dateioperationen
        /// </summary>
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        static extern int SHFileOperation(ref SHFILEOPSTRUCT fileOp);

        /* Konstanten für SHFileOperation (aus ShellAPI.h) */
        private const int FO_COPY = 0x0002;            // Datei oder Ordner kopieren
        private const int FOF_ALLOWUNDO = 0x0040;      // Rückgängigmachen erlauben
        private const int FOF_NOCONFIRMATION = 0x0010; // Keine Nachfrage beim Anwender
        private const int FO_DELETE = 0x0003;          // Datei/Ordner löschen 
        private const int FO_MOVE = 0x0001;            // Verschieben

        /// <summary>
        /// Struktur zur Übergabe an SHFileOperation
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SHFILEOPSTRUCT
        {
            public IntPtr hwnd;
            public int wFunc;
            public string pFrom;
            public string pTo;
            public short fFlags;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fAnyOperationsAborted;
            public IntPtr hNameMappings;
            public string lpszProgressTitle;
        }

        /// <summary>
        /// Verschiebt eine Datei
        /// </summary>
        /// <param name="source">Pfad zur Quelldatei bzw. zum Quellordner</param>
        /// <param name="dest">Pfad zum Ziel</param>
        /// <param name="confirmOverwrites">Gibt an, ob das Überschreiben von Dateien,
        /// die im Zielordner bereits vorhanden sind, vom Anwender bestätigt werden soll</param>
        /// <returns>Gibt true zurück wenn das Verschieben erfolgreich war</returns>
        /// <remarks>
        /// <para>
        /// Beim Verschieben von Ordnern verhält sich SHFileOperation etwas gewöhnungsbedürftig: 
        /// Existiert noch kein Ordner mit dem angegebenen Zielpfad, wird der Quellordner in den 
        /// im Zielpfad angegebenen übergeordneten Ordner verschoben und so benannt, 
        /// wie der letzte Ordner im Zielpfad. Das ist das erwartete Verhalten. 
        /// Existiert jedoch bereits ein Ordner mit dem angegebenen Zielpfad, 
        /// wird der Quellordner so in diesem Ordner verschoben, dass er ein Unterordner wird. 
        /// Existiert z.B. ein Ordner C:\Temp\DemoFolder und Sie verschieben den Ordner 
        /// C:\DemoFolder nach C:\Temp\DemoFolder, wird dieser Ordner in den Ordner 
        /// C:\Temp\DemoFolder\DemoFolder verschoben. 
        /// Existiert C:\Temp\DemoFolder beim Verschieben noch nicht, wird der Ordner 
        /// korrekt in den Ordner C:\Temp\DemoFolder verschoben. 
        /// Deshalb sollten Sie beachten, dass Sie beim Verschieben von Ordnern als Ziel 
        /// immer den Pfad zu dem Ordner angeben sollten, in den der verschobene 
        /// Ordner kopiert werden soll (also den Zielpfad zum Parent-Ordner). 
        /// Leider führt das dazu, dass Sie Ordner beim Verschieben nicht umbenennen können. 
        /// </para>
        /// </remarks>
        public static bool MoveFileOrFolder(string source, string dest,
                                            bool confirmOverwrites)
        {
            // Struktur für die Dateiinformationen erzeugen
            SHFILEOPSTRUCT fileOp = new SHFILEOPSTRUCT();

            // (Unter-)Funktion definieren (ShFileOperation kann auch Dateien und 
            // Ordner löschen und kopieren)
            fileOp.wFunc = FO_MOVE;

            // Quelle und Ziel definieren. Dabei müssen mehrere Datei- oder
            // Ordnerangaben über 0-Zeichen getrennt werden.
            // Am Ende muss ein zusätzliches 0-Zeichen stehen
            fileOp.pFrom = source + "\x0\x0";
            fileOp.pTo = dest + "\x0\x0";

            // Flags setzen, sodass ein Rückgängigmachen möglich ist (was aber
            // beim Verschieben zurzeit scheinbar noch nicht unterstützt wird!)
            // und - je nach Argument confirmOverwrites - keine Nachfrage
            // beim Überschreiben von Ordnern beim Anwender erfolgt
            if (confirmOverwrites)
            {
                fileOp.fFlags = FOF_ALLOWUNDO;
            }
            else
            {
                fileOp.fFlags = FOF_ALLOWUNDO | FOF_NOCONFIRMATION;
            }

            // SHFileOperation unter Übergabe der Struktur aufrufen
            int result = SHFileOperation(ref fileOp);

            // Erfolg oder Misserfolg zurückgeben
            return (result == 0);
        }

        /// <summary>
        /// Verschiebt eine Datei in den Papierkorb
        /// </summary>
        /// <param name="path">Pfad zur Datei</param>
        /// <returns>Gibt true wenn das Verschieben erfolgreich war</returns>
        public static bool MoveToRecycleBin(string path)
        {
            // Struktur für die Dateiinformationen erzeugen
            SHFILEOPSTRUCT fileOp = new SHFILEOPSTRUCT();

            // Quelle definieren. Dabei müssen mehrere Datei- oder
            // Ordnerangaben über 0-Zeichen getrennt werden.
            // Am Ende muss ein zusätzliches 0-Zeichen stehen
            fileOp.pFrom = path + "\x0\x0";

            // Flags setzen, sodass ein Rückgängigmachen möglich ist und
            // keine Nachfrage beim Anwender erfolgt
            fileOp.fFlags = FOF_ALLOWUNDO | FOF_NOCONFIRMATION;

            // (Unter-)Funktion definieren
            fileOp.wFunc = FO_DELETE;

            // ShFileOperation unter Übergabe der Struktur aufrufen
            int result = SHFileOperation(ref fileOp);

            // Erfolg oder Fehler zurückmelden. SHFileOperation liefert 0
            // zurück, wenn kein Fehler aufgetreten ist
            return (result == 0);
        }

        /// <summary>
        /// Überprüft, ob eine Pfadangabe gültig ist
        /// </summary>
        /// <param name="path">Der Pfad</param>
        public static bool IsPathValid(string path)
        {
            try
            {
                // GetFullPath aufrufen um eine Exception 
                // bei einem ungültigen Pfad zu provozieren
                Path.GetFullPath(path);
                return true;
            }
            catch (NotSupportedException)
            {
                // Wird bei ungültigen Pfad-Formaten geworfen,
                // z. B. wenn ein Ordner- oder ein Dateiname
                // Doppelpunkte enthält 
                return false;
            }
            catch (ArgumentException)
            {
                // Wird bei ungültigen Zeichen im Pfad geworfen
                return false;
            }
        }

    }
}