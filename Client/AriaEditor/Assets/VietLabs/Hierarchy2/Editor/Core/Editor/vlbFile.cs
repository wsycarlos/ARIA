using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEditor;
using Debug = UnityEngine.Debug;

namespace vietlabs
{
    public static class vlbFile
    {
        static internal byte[] ReadBytes(this string filePath) {
            var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var nBytes = (int)(new FileInfo(filePath).Length);
            return new BinaryReader(fs).ReadBytes(nBytes);
        }
        static internal void WriteBytes(this byte[] bytes, string filePath) {
            var fs = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write);
            var bw = new BinaryWriter(fs);
            bw.Write(bytes);
            bw.Flush();
        }
        static internal void WriteText(this string text, string filePath) {
            var fs = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write);
            var bw = new StreamWriter(fs);
            bw.Write(text);
            bw.Flush();
        }
        static internal string ReadText(this string filePath) {
            var fs = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write);
            var br = new StreamReader(fs);
            return br.ReadToEnd();
        }

        static public string ToAbsolutePath(this string path) {
            return new FileInfo(@path).FullName;
        }
        static public string ToRelativePath(this string path) {
            var fullPath = (new FileInfo(path)).FullName;
            var basePath = (new FileInfo("Assets")).FullName;
            return "Assets" + (fullPath.Replace(basePath, "")).Replace(@"\", "/");
        }
        static public string[] GetPaths(this FileInfo[] fileList)
        {
            return fileList.ToList().Select(file => file.FullName).ToArray();
        }

        static public bool IsFolder(this string path) {
            if (!string.IsNullOrEmpty(path)) {
                return (File.GetAttributes(@path) & FileAttributes.Directory) == FileAttributes.Directory;
            }
            Debug.LogWarning("vlbFile.IsFolder() Error - path should not be null or empty");
            return false;
        }
        static public bool IsFile(this string path) {
            if (!string.IsNullOrEmpty(path)) {
                return (File.GetAttributes(@path) & FileAttributes.Directory) != FileAttributes.Directory;
            }
            Debug.LogWarning("vlbFile.IsFile() Error - path should not be null or empty");
            return false;
        }

        static public string GetName(this string path) {
            return path.IsFolder() ? new DirectoryInfo(@path).Name : new FileInfo(@path).Name;
        }
        static public string GetExtension(this string path) {
            return path.IsFolder() ? new DirectoryInfo(@path).Extension : new FileInfo(@path).Extension;
        }
        static public string GetNameWithoutExtension(this string path) {
            //replace is not safe, what if the path contains a folder with exactly the same extension with the current file/folder ?
            //should replace the last one instead
            return path.GetName().Replace(path.GetExtension(), "");
        }

        static public string CreatePath(this string path) {
            var info = Directory.CreateDirectory(@path);
            AssetDatabase.Refresh();
            return info.FullName;
        }
        static public string ParentFolder(this string path) {
            return path.IsFolder() ? new DirectoryInfo(@path).Parent.FullName : new FileInfo(@path).DirectoryName;
        }

        static public string[] GetFolders(this string path, string pattern = null, bool recursive = false) {
            return Directory.GetDirectories(path, pattern ?? "*", recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
        }
        static public string[] GetFiles(this string path, string pattern = null, bool recursive = false) {
            return Directory.GetFiles(path, pattern ?? "*", recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
        }
    }
}

