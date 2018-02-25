using System;
using System.IO;
using System.Linq;
using System.Reflection;
using VanillaRuleGenerator.Helpers;

namespace VanillaRuleGenerator
{
    public class ModRuleGenerator
    {
        private Type _ruleGeneratorType;
        private MethodInfo _initRNG;
        private MethodInfo _createRules;
        private MethodInfo _getHTMLManual;
        private MethodInfo _getTextFiles;
        private MethodInfo _getBinaryFiles;
        private string _htmlFileName;

        private int _seed;
        private object _ruleGenerator;


        private void InitializeRules(int seed)
        {
            if (_ruleGenerator == null)
            {
                _ruleGenerator = Activator.CreateInstance(_ruleGeneratorType);
                _seed = seed;
                _initRNG.Invoke(_ruleGenerator, new object[] {_seed, typeof(MonoRandom)});
                _createRules.Invoke(_ruleGenerator, null);
            }
            else if (_seed != seed)
            {
                _seed = seed;
                _initRNG.Invoke(_ruleGenerator, new object[] { _seed, typeof(MonoRandom) });
                _createRules.Invoke(_ruleGenerator, null);
            }
        }

        public string GetHTML(int seed)
        {
			return GetHTML(seed, out string notUsed);
		}

        public string GetHTML(int seed, out string filename)
        {
            InitializeRules(seed);
            var htmlFilename="";
            object[] args = {htmlFilename};
            var html = (string) _getHTMLManual.Invoke(_ruleGenerator, args);
            filename = (string) args[0];
            _htmlFileName = filename;
            return html;
        }

        public string GetHTMLFileName()
        {
            if (_htmlFileName == null)
                GetHTML(_seed, out _htmlFileName);
            return _htmlFileName;
        }

        public string[] GetTextFiles(out string[] filenames)
        {
            var textFileNames = new string[0];
            var textFileArgs = new object[] { textFileNames };
            var textFiles = (string[])_getTextFiles.Invoke(_ruleGenerator, textFileArgs);
            filenames = (string[])textFileArgs[0];
            return textFiles;
        }

        public byte[][] GetBinaryFiles(out string[] filenames)
        {
            var binaryFileNames = new string[0];
            var binaryFileArgs = new object[] { binaryFileNames };
            var binaryFiles = (byte[][])_getBinaryFiles.Invoke(_ruleGenerator, binaryFileArgs);
            filenames = (string[])binaryFileArgs[0];
            return binaryFiles;
        }

        public void WriteHTMLFile(int seed, string path)
        {
			var html = GetHTML(seed, out string htmlFileName);
			File.WriteAllText(Path.Combine(path, htmlFileName), html);
        }

        public void WriteTextFiles(string path)
        {
			var textFiles = GetTextFiles(out string[] textFileNames);
			for (var i = 0; i < textFileNames.Length; i++)
            {
                var filePath = Path.GetDirectoryName(textFileNames[i]);
                if (!string.IsNullOrEmpty(filePath) && !Directory.Exists(Path.Combine(path, filePath)))
                    Directory.CreateDirectory(Path.Combine(path, filePath));
                File.WriteAllText(Path.Combine(path, textFileNames[i]), textFiles[i]);
            }
        }

        public void WriteBinaryFiles(string path)
        {
			var binaryFiles = GetBinaryFiles(out string[] binaryFileNames);
			for (var i = 0; i < binaryFileNames.Length; i++)
            {
                var filePath = Path.GetDirectoryName(binaryFileNames[i]);
                if (!string.IsNullOrEmpty(filePath) && !Directory.Exists(Path.Combine(path, filePath)))
                    Directory.CreateDirectory(Path.Combine(path, filePath));
                File.WriteAllBytes(Path.Combine(path, binaryFileNames[i]), binaryFiles[i]);
            }
        }

        public void WriteAllFiles(int seed, string path)
        {
            WriteHTMLFile(seed, path);
            WriteTextFiles(path);
            WriteBinaryFiles(path);
        }

        public static ModRuleGenerator GetRuleGenerator(Assembly assembly)
        {
            var ruleGeneratorType = assembly.GetSafeTypes().FirstOrDefault(t => t.FullName != null && t.FullName.EndsWith("AbstractRuleGenerator"));
            if (ruleGeneratorType == null) return null;
            var implementsType = assembly.GetSafeTypes().Where(p => ruleGeneratorType.IsAssignableFrom(p)).FirstOrDefault(t => t != ruleGeneratorType) ?? ruleGeneratorType;

            var initRNGMethod = ruleGeneratorType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(m => m.Name.Equals("InitializeRNG") &&
                                     m.ReturnType == typeof(void) &&
                                     m.GetParameters().Length == 2 &&
                                     m.GetParameters()[0].ParameterType == typeof(int) &&
                                     m.GetParameters()[1].ParameterType == typeof(Type));

            var getHTMLMethod = implementsType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(m => m.Name.Equals("GetHTMLManual") &&
                                     m.ReturnType == typeof(string) &&
                                     m.GetParameters().Length == 1);

            var createRulesMethod = implementsType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(m => m.Name.Equals("CreateRules") &&
                                     m.ReturnType == typeof(void) &&
                                     m.GetParameters().Length == 0);

            var getTextFilesMethod = implementsType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(m => m.Name.Equals("GetTextFiles") &&
                                     m.ReturnType == typeof(string[]) &&
                                     m.GetParameters().Length == 1 &&
                                     m.GetParameters()[0].IsOut);


            var getBinaryFilesMethod = implementsType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(m => m.Name.Equals("GetBinaryFiles") &&
                                     m.ReturnType == typeof(byte[][]) &&
                                     m.GetParameters().Length == 1 &&
                                     m.GetParameters()[0].IsOut);

            if (initRNGMethod == null || getHTMLMethod == null || createRulesMethod == null || getTextFilesMethod == null || getBinaryFilesMethod == null)
                return null;

            var ruleGen = new ModRuleGenerator
            {
                _ruleGeneratorType = implementsType,
                _createRules = createRulesMethod,
                _initRNG = initRNGMethod,
                _getHTMLManual = getHTMLMethod,
                _getTextFiles = getTextFilesMethod,
                _getBinaryFiles = getBinaryFilesMethod
            };
            return ruleGen;
        }
    }
}