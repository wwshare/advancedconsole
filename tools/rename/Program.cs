using System;
using System.IO;
using System.Text;

class Program
{
    static int Main(string[] args)
    {
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: rename <file> [oldIdent] [newIdent]");
            return 1;
        }
        string path = args[0];
        string oldIdent = args.Length >= 2 ? args[1] : "Player";
        string newIdent = args.Length >= 3 ? args[2] : "PhotonPlayer";

        string src = File.ReadAllText(path);
        string result = ReplaceIdent(src, oldIdent, newIdent);

        File.WriteAllText(path, result, new UTF8Encoding(false));
        Console.WriteLine("Processed: " + Path.GetFileName(path));
        return 0;
    }

    static string ReplaceIdent(string s, string oldIdent, string newIdent)
    {
        var sb = new StringBuilder(s.Length + 64);
        int i = 0;
        int n = s.Length;

        while (i < n)
        {
            char c = s[i];

            if (c == '/' && i + 1 < n && s[i + 1] == '/')
            {
                sb.Append("//");
                i += 2;
                while (i < n && s[i] != '\n') { sb.Append(s[i]); i++; }
                continue;
            }
            if (c == '/' && i + 1 < n && s[i + 1] == '*')
            {
                sb.Append("/*");
                i += 2;
                while (i + 1 < n && !(s[i] == '*' && s[i + 1] == '/')) { sb.Append(s[i]); i++; }
                if (i + 1 < n) { sb.Append("*/"); i += 2; }
                continue;
            }

            if (c == '$' && i + 1 < n && (s[i + 1] == '"' || s[i + 1] == '@'))
            {
                int j = i + 1;
                if (s[j] == '@') j++;
                if (j < n && s[j] == '"')
                {
                    bool verbatim = s[i + 1] == '@';
                    sb.Append(s, i, j - i + 1);
                    i = j + 1;
                    ProcessInterpolated(s, sb, ref i, oldIdent, newIdent, verbatim);
                    continue;
                }
            }

            if (c == '"')
            {
                sb.Append(c); i++;
                while (i < n)
                {
                    if (s[i] == '\\' && i + 1 < n) { sb.Append(s[i]); sb.Append(s[i + 1]); i += 2; continue; }
                    sb.Append(s[i]);
                    if (s[i] == '"') { i++; break; }
                    i++;
                }
                continue;
            }

            if (c == '\'')
            {
                sb.Append(c); i++;
                while (i < n)
                {
                    if (s[i] == '\\' && i + 1 < n) { sb.Append(s[i]); sb.Append(s[i + 1]); i += 2; continue; }
                    sb.Append(s[i]);
                    if (s[i] == '\'') { i++; break; }
                    i++;
                }
                continue;
            }

            if (IsIdentStart(c))
            {
                int j = i + 1;
                while (j < n && IsIdentPart(s[j])) j++;
                string ident = s.Substring(i, j - i);
                sb.Append(ident == oldIdent ? newIdent : ident);
                i = j;
                continue;
            }

            sb.Append(c);
            i++;
        }

        return sb.ToString();
    }

    static void ProcessInterpolated(string s, StringBuilder sb, ref int i, string oldIdent, string newIdent, bool verbatim)
    {
        int n = s.Length;
        int depth = 0;

        while (i < n)
        {
            char c = s[i];

            if (depth > 0)
            {
                if (c == '{') { sb.Append(c); depth++; i++; }
                else if (c == '}') { sb.Append(c); depth--; i++; }
                else if (c == '"') { sb.Append(c); i++; }
                else if (IsIdentStart(c))
                {
                    int j = i + 1;
                    while (j < n && IsIdentPart(s[j])) j++;
                    string ident = s.Substring(i, j - i);
                    sb.Append(ident == oldIdent ? newIdent : ident);
                    i = j;
                }
                else { sb.Append(c); i++; }
                continue;
            }

            if (verbatim)
            {
                if (c == '"' && i + 1 < n && s[i + 1] == '"') { sb.Append("\"\""); i += 2; continue; }
                if (c == '"') { sb.Append(c); i++; break; }
                if (c == '{' && i + 1 < n && s[i + 1] == '{') { sb.Append("{{"); i += 2; continue; }
                if (c == '{') { sb.Append(c); depth = 1; i++; continue; }
                if (c == '}' && i + 1 < n && s[i + 1] == '}') { sb.Append("}}"); i += 2; continue; }
                sb.Append(c); i++;
            }
            else
            {
                if (c == '\\' && i + 1 < n) { sb.Append(c); sb.Append(s[i + 1]); i += 2; continue; }
                if (c == '"') { sb.Append(c); i++; break; }
                if (c == '{' && i + 1 < n && s[i + 1] == '{') { sb.Append("{{"); i += 2; continue; }
                if (c == '{') { sb.Append(c); depth = 1; i++; continue; }
                if (c == '}' && i + 1 < n && s[i + 1] == '}') { sb.Append("}}"); i += 2; continue; }
                sb.Append(c); i++;
            }
        }
    }

    static bool IsIdentStart(char c) => char.IsLetter(c) || c == '_';
    static bool IsIdentPart(char c) => char.IsLetterOrDigit(c) || c == '_';
}
