//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.CSharp;
using Microsoft.VisualBasic;

namespace CompleX.Scripting
{
    public class ScriptEngine {

        public ScriptEngine(ScriptLanguage language, string script, CompleXScriptContext context) {
            this.Language = language;
            this.Script = script;
            this.Context = context;
        }

        public ScriptLanguage Language { get; protected set; }

        public string Script { get; protected set; }

        public CompleXScriptContext Context { get; protected set; }

        public event EventHandler<ScriptEngineStatusEventArgs> StatusChanged;

        protected virtual void OnStatusChanged(ScriptEngineStatusEventArgs e) {
            if(this.StatusChanged != null) this.StatusChanged(this, e);
        }

        protected virtual void SetStatus(string status) {
            this.OnStatusChanged(new ScriptEngineStatusEventArgs(status));
        }

        public virtual void Execute() {
            var unit = this.GenerateCompileUnit();
            if(unit != null) {
                var type = this.CompileType(unit);
                if(type != null) this.ExecuteScript(type);
            }
        }

        protected virtual CodeCompileUnit GenerateCompileUnit() {
            this.SetStatus("Generating compile unit");

            var unit = new CodeCompileUnit();

            var ns = new CodeNamespace("CompleX.Scripting.Execution");
            unit.Namespaces.Add(ns);

            ns.Imports.Add(new CodeNamespaceImport("System"));
            ns.Imports.Add(new CodeNamespaceImport("System.Windows.Forms"));

            var type = new CodeTypeDeclaration("GeneratedScript") {
                                                                      Attributes = MemberAttributes.Public
                                                                  };
            type.BaseTypes.Add(new CodeTypeReference(typeof(BaseScript)));
            ns.Types.Add(type);

            // Methode überschreiben
            var method = new CodeMemberMethod {
                                                  Name = "Execute",
                                                  Attributes = MemberAttributes.Public | MemberAttributes.Override
                                              };
            method.Statements.Add(new CodeSnippetExpression(this.Script));
            type.Members.Add(method);

            return unit;
        }

        protected virtual Type CompileType(CodeCompileUnit unit) {
            this.SetStatus("Compiling code");
            
            var options = new Dictionary<string, string>() {
                                                               { "CompilerVersion", "v4.0" } 
                                                           };

            // Provider
            
            CodeDomProvider provider = (this.Language == ScriptLanguage.CSharp ? (CodeDomProvider) new CSharpCodeProvider(options) : new VBCodeProvider(options));

            var cp = new CompilerParameters {
                                                GenerateExecutable = false,
                                                OutputAssembly = string.Concat(Guid.NewGuid().ToString(), ".dll"),
                                                GenerateInMemory = false,
                                                TreatWarningsAsErrors = false,
                                                IncludeDebugInformation = false
                                            };

            // Referenzen hinzufügen
            foreach(var assembly in AppDomain.CurrentDomain.GetAssemblies()) {
                //var assemblyShortName = assembly.FullName.Substring(0, assembly.FullName.IndexOf(","));
                if (assembly != null && !String.IsNullOrEmpty(assembly.Location))
                cp.ReferencedAssemblies.Add(assembly.Location);
            }

            // Kompilieren
            var result = provider.CompileAssemblyFromDom(cp, unit);

            // Fehler ausgeben
            if(result.Errors.Count > 0) {
                this.SetStatus("Compilation failed, errors and warning follow:");
                foreach(CompilerError error in result.Errors) {
                    this.SetStatus(error.ErrorText);
                }
            } 
            else if((result.CompiledAssembly != null)) {
                this.SetStatus("Compilation succeeded");
                return result.CompiledAssembly.GetType("CompleX.Scripting.Execution.GeneratedScript");
            }

            return null;
        }

        protected virtual void ExecuteScript(Type type) {
            if(type != null) {
                var instance = (Activator.CreateInstance(type) as BaseScript);
                instance.Initialize(this.Context);

                this.SetStatus("Starting execution");

                try {
                    instance.Execute();
                    this.SetStatus("Execution succeeded");
                }
                catch(Exception e) {
                    this.SetStatus(string.Concat("Execution failed: ", e.ToString()));
                }
            }
        }
    }
}