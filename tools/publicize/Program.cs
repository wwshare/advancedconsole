using System;
using System.IO;
using Mono.Cecil;

class Program
{
    static int Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: publicize <input.dll> <output.dll>");
            return 1;
        }
        string input = args[0];
        string output = args[1];

        var resolver = new DefaultAssemblyResolver();
        resolver.AddSearchDirectory(Path.GetDirectoryName(Path.GetFullPath(input)));

        var readerParams = new ReaderParameters { AssemblyResolver = resolver };
        var assembly = AssemblyDefinition.ReadAssembly(input, readerParams);

        foreach (var type in assembly.MainModule.GetTypes())
        {
            if (type.IsNested)
            {
                type.IsNestedPublic = true;
            }
            else
            {
                type.IsPublic = true;
                type.IsNotPublic = false;
            }

            foreach (var field in type.Fields)
            {
                if (field.IsAssembly || field.IsFamilyOrAssembly || field.IsFamilyAndAssembly ||
                    field.IsPrivate || field.IsFamily)
                {
                    field.IsPublic = true;
                }
            }

            foreach (var method in type.Methods)
            {
                if (method.IsAssembly || method.IsFamilyOrAssembly || method.IsFamilyAndAssembly ||
                    method.IsPrivate || method.IsFamily)
                {
                    method.IsPublic = true;
                }
            }

            foreach (var prop in type.Properties)
            {
                if (prop.GetMethod != null && !prop.GetMethod.IsPublic) prop.GetMethod.IsPublic = true;
                if (prop.SetMethod != null && !prop.SetMethod.IsPublic) prop.SetMethod.IsPublic = true;
            }
        }

        assembly.Write(output);
        Console.WriteLine("Publicized: " + Path.GetFileName(input) + " -> " + Path.GetFileName(output));
        return 0;
    }
}
