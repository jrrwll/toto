using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

using System;
using System.CodeDom.Compiler;
using System.Reflection;

namespace Toto.Dynam
{
    public static partial class CommuneScript
    {

        // Python
        public struct IPY
        {
            public static object Eval(string statement)
            {
                var code = "def _eval(cmd):\n" +
                    "    return eval(cmd)";

                ScriptEngine engine = Python.CreateEngine();
                ScriptScope scope = engine.CreateScope();
                ScriptSource source = engine.CreateScriptSourceFromString(code);

                source.Execute(scope);

                var eval = scope.GetVariable<Func<object, object>>("_eval");
                return eval(statement);
            }
        }


        // JScript
        public struct JScript
        {
            public static object Eval(string statement)
            {

                string code = @"class Evaluator
                {
                    public function Eval(expr : String) : String 
                    {  
                        return eval(expr); 
                    }
                }";

                //构造JScript的编译驱动代码
                CodeDomProvider provider = CodeDomProvider.CreateProvider("JScript");
                CompilerParameters parameters = new CompilerParameters();
                parameters.GenerateInMemory = true;
                CompilerResults results = provider.CompileAssemblyFromSource(
                    parameters, code);

                // 解析
                Assembly assembly = results.CompiledAssembly;
                Type evaluator_type = assembly.GetType("Evaluator");
                object evaluator = Activator.CreateInstance(evaluator_type);

                return evaluator_type.InvokeMember(
                        "Eval",
                        BindingFlags.InvokeMethod,
                        null,
                        evaluator,
                        new object[] { statement }
                     );
            }
        }


    }

}
