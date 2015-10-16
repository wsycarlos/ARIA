using System;
using System.CodeDom;
using System.Collections.Generic;
using CodeGeneration;
using UnityEngine;

namespace Game.Template.Editor
{
    /// <summary>
    /// 功能概述：
    /// </summary>
    public class TemplateCodeGenerator
    {
        private static string _writerPath = "/ZGame/CorePackage/Script/Editor/Template/Writer/";
        private static string _readerPath = "/../../AriaExtends/AriaExtends/Game/Template/";
        private static string _dictPth = "/../../AriaExtends/AriaExtends/Game/Template/Dictionary/";

        public static void WriteCodeGenerator(List<FiledPropObject> list, string realName, string pre, string TemplateVOType)
        {
            string filename = Application.dataPath + _writerPath + realName;
            ClassGenerator generator = new ClassGenerator("Game.Template.Editor", realName);
            generator.AddBaseType("ITemplaterWriter");
            generator.AddImport("System.Collections.Generic");
            List<CodeStatement> CodeStatementList = new List<CodeStatement>();

            CodeParameterDeclarationExpression par = new CodeParameterDeclarationExpression(typeof(System.Object), "data");
            List<CodeParameterDeclarationExpression> pList = new List<CodeParameterDeclarationExpression>();
            pList.Add(par);

            //List
            CodeStatementList.Add(
                new CodeVariableDeclarationStatement("List<" + TemplateVOType + ">", "list",
                    new CodeVariableReferenceExpression("data as List<" + TemplateVOType + ">")));

            //ByteArray
            CodeStatementList.Add(
                new CodeVariableDeclarationStatement(typeof(ByteArray), "ba",
                    new CodeObjectCreateExpression(typeof(ByteArray))));

            //length
            CodeStatementList.Add(
                new CodeVariableDeclarationStatement(typeof(int), "length",
                    new CodeVariableReferenceExpression("list.Count")));

            //写入长度
            CodeStatementList.Add(
                new CodeExpressionStatement(
                    new CodeMethodInvokeExpression(
                        new CodeTypeReferenceExpression("ba"),
                        "WriteInt",
                        new CodeVariableReferenceExpression("length"))));

            //for
            CodeIterationStatement forExp = new CodeIterationStatement();
            // 初始化  
            forExp.InitStatement = new CodeVariableDeclarationStatement(typeof(int), "i",
                new CodePrimitiveExpression(0));
            // 递增条件  
            forExp.IncrementStatement = new CodeAssignStatement(
                new CodeVariableReferenceExpression("i"),
                new CodeBinaryOperatorExpression(
                    new CodeVariableReferenceExpression("i"),
                    CodeBinaryOperatorType.Add,
                    new CodePrimitiveExpression(1)));
            // 测试表达式  
            forExp.TestExpression = new CodeBinaryOperatorExpression(
                new CodeVariableReferenceExpression("i"),
                CodeBinaryOperatorType.LessThan,
                new CodeVariableReferenceExpression("length"));

            for (int i = 0; i < list.Count; i++)
            {
                forExp.Statements.Add(
                    new CodeExpressionStatement(
                        new CodeMethodInvokeExpression(
                            new CodeTypeReferenceExpression("ba"),
                            GetByteArrayMethodName(list[i].type, pre),
                            new CodeVariableReferenceExpression("list[i]." + list[i].name))));
            }


            CodeStatementList.Add(forExp);

            //return
            CodeStatementList.Add(
                new CodeMethodReturnStatement(
                    new CodeVariableReferenceExpression("ba")));

            generator.AddMethod("GenerateByteArray", pList, CodeStatementList, typeof(ByteArray), MemberAttributes.Public | MemberAttributes.Final);
            generator.Generate(filename + ".cs");
        }

        public static void ReadCodeGenerator(List<FiledPropObject> list, string realName, string pre, string TemplateVOType)
        {
            string filename = Application.dataPath + _readerPath + pre + "/" + realName;
            ClassGenerator generator = new ClassGenerator("Game.Template", realName);
            generator.AddBaseType("ITemplateReader");
            //generator.AddImport("System.Collections.Generic");
            List<CodeStatement> CodeStatementList = new List<CodeStatement>();

            CodeParameterDeclarationExpression par = new CodeParameterDeclarationExpression(typeof(byte[]), "bytes");
            List<CodeParameterDeclarationExpression> pList = new List<CodeParameterDeclarationExpression>();
            pList.Add(par);

            CodeStatementList.Add(
                new CodeVariableDeclarationStatement(typeof(ByteArray), "data",
                    new CodeObjectCreateExpression(typeof(ByteArray),
                        new CodeVariableReferenceExpression("bytes"))));

            CodeStatementList.Add(
                new CodeVariableDeclarationStatement(typeof(List<object>), "list",
                    new CodeObjectCreateExpression(typeof(List<object>))));

            //length
            CodeStatementList.Add(
                new CodeVariableDeclarationStatement(typeof(int), "length",
                    new CodeVariableReferenceExpression("data.ReadInt()")));

            //for
            CodeIterationStatement forExp = new CodeIterationStatement();
            // 初始化  
            forExp.InitStatement = new CodeVariableDeclarationStatement(typeof(int), "i",
                new CodePrimitiveExpression(0));
            // 递增条件  
            forExp.IncrementStatement = new CodeAssignStatement(
                new CodeVariableReferenceExpression("i"),
                new CodeBinaryOperatorExpression(
                    new CodeVariableReferenceExpression("i"),
                    CodeBinaryOperatorType.Add,
                    new CodePrimitiveExpression(1)));
            // 测试表达式  
            forExp.TestExpression = new CodeBinaryOperatorExpression(
                new CodeVariableReferenceExpression("i"),
                CodeBinaryOperatorType.LessThan,
                new CodeVariableReferenceExpression("length"));
            forExp.Statements.Add(
                     new CodeVariableDeclarationStatement(TemplateVOType, "vo",
                         new CodeObjectCreateExpression(TemplateVOType)));

            for (int i = 0; i < list.Count; i++)
            {
                forExp.Statements.Add(
                    new CodeAssignStatement(
                        new CodeVariableReferenceExpression(
                            "vo." + list[i].name),
                            new CodeMethodInvokeExpression(
                                new CodeTypeReferenceExpression("data"),
                                GetByteArrayMethodName(list[i].type, pre))));
            }

            forExp.Statements.Add(new CodeVariableReferenceExpression("list.Add(vo)"));


            CodeStatementList.Add(forExp);

            //return
            CodeStatementList.Add(
                new CodeMethodReturnStatement(
                    new CodeVariableReferenceExpression("list")));

            generator.AddMethod("GenerateByteArray", pList, CodeStatementList, typeof(List<object>), MemberAttributes.Public | MemberAttributes.Final);
            generator.Generate(filename + ".cs");
        }

        public static void DictionaryGenerator(string realName, Type type, Type subType, Type dataType)
        {
            string filename = Application.dataPath + _dictPth + realName;
            ClassGenerator generator = new ClassGenerator("Game.Template", realName + "Dictionary");
            generator.AddBaseType("ITemplateDictionary");
            generator.AddImport("UnityEngine");
            //generator.AddImport("System.Collections.Generic");

            List<CodeStatement> CodeStatementList = new List<CodeStatement>();

            generator.AddProperty("Item List", type, null);
            generator.AddProperty("Item Dic", dataType, null);

            CodeParameterDeclarationExpression par = new CodeParameterDeclarationExpression(typeof(List<object>), "list");
            List<CodeParameterDeclarationExpression> pList = new List<CodeParameterDeclarationExpression>();
            pList.Add(par);
            CodeStatementList.Add(new CodeAssignStatement(
                new CodeVariableReferenceExpression("_itemlist"),
                new CodeObjectCreateExpression(type)));

            CodeStatementList.Add(
                new CodeVariableDeclarationStatement(typeof(int), "length",
                    new CodeVariableReferenceExpression("list.Count")));

            //for
            CodeIterationStatement forExp = new CodeIterationStatement();
            // 初始化  
            forExp.InitStatement = new CodeVariableDeclarationStatement(typeof(int), "i",
                new CodePrimitiveExpression(0));
            // 递增条件  
            forExp.IncrementStatement = new CodeAssignStatement(
                new CodeVariableReferenceExpression("i"),
                new CodeBinaryOperatorExpression(
                    new CodeVariableReferenceExpression("i"),
                    CodeBinaryOperatorType.Add,
                    new CodePrimitiveExpression(1)));
            // 测试表达式  
            forExp.TestExpression = new CodeBinaryOperatorExpression(
                new CodeVariableReferenceExpression("i"),
                CodeBinaryOperatorType.LessThan,
                new CodeVariableReferenceExpression("length"));
            forExp.Statements.Add(
                     new CodeExpressionStatement(
                        new CodeMethodInvokeExpression(
                            new CodeTypeReferenceExpression("_itemlist"), "Add",
                            new CodeVariableReferenceExpression("list[i] as " + subType))));

            CodeStatementList.Add(forExp);

            CodeStatementList.Add(
                new CodeExpressionStatement(
                        new CodeMethodInvokeExpression(
                            new CodeThisReferenceExpression(), "InitDictionary")));

            generator.AddMethod("Init", pList, CodeStatementList, null, MemberAttributes.Public | MemberAttributes.Final);

            CodeStatementList.Clear();

            //for
            forExp = new CodeIterationStatement();
            // 初始化  
            forExp.InitStatement = new CodeVariableDeclarationStatement(typeof(int), "i",
                new CodePrimitiveExpression(0));
            // 递增条件  
            forExp.IncrementStatement = new CodeAssignStatement(
                new CodeVariableReferenceExpression("i"),
                new CodeBinaryOperatorExpression(
                    new CodeVariableReferenceExpression("i"),
                    CodeBinaryOperatorType.Add,
                    new CodePrimitiveExpression(1)));
            // 测试表达式  
            forExp.TestExpression = new CodeBinaryOperatorExpression(
                new CodeVariableReferenceExpression("i"),
                CodeBinaryOperatorType.LessThan,
                new CodeVariableReferenceExpression("length"));

            CodeTryCatchFinallyStatement myTrycafly = new CodeTryCatchFinallyStatement();
            // try  
            myTrycafly.TryStatements.Add(new CodeExpressionStatement(
                        new CodeMethodInvokeExpression(
                            new CodeTypeReferenceExpression("_itemdic"), "Add",
                            new CodeVariableReferenceExpression("_itemlist[i].id"),
                            new CodeVariableReferenceExpression("_itemlist[i]"))));
            // catch  
            myTrycafly.CatchClauses.Add(new CodeCatchClause(
                "ex", new CodeTypeReference(typeof(Exception)),
                new CodeExpressionStatement(
                    new CodeMethodInvokeExpression(
                        new CodeTypeReferenceExpression("Debug"),
                        "LogWarning",
                        new CodeVariableReferenceExpression("ex.Message")))));
            forExp.Statements.Add(myTrycafly);

            CodeStatementList.Add(
                new CodeVariableDeclarationStatement(typeof(int), "length",
                    new CodeVariableReferenceExpression("_itemlist.Count")));

            par = new CodeParameterDeclarationExpression(dataType, "_itemdic");
            pList = new List<CodeParameterDeclarationExpression>();
            pList.Add(par);
            CodeStatementList.Add(new CodeAssignStatement(
                new CodeVariableReferenceExpression("_itemdic"),
                new CodeObjectCreateExpression(dataType)));

            CodeStatementList.Add(forExp);

            generator.AddMethod("InitDictionary", null, CodeStatementList, null, MemberAttributes.Private);
            generator.Generate(filename + "Dictionary.cs");
            Debug.Log("Code generator completed : " + realName + "Dictionary.cs");
        }

        private static string GetByteArrayMethodName(string type, string pre)
        {
            string result = "";
            switch (type)
            {
                case "String":
                    result = pre + "UTF";
                    break;
                case "Int32":
                    result = pre + "Int";
                    break;
                case "Double":
                    result = pre + "Double";
                    break;
                case "Single":
                    result = pre + "Float";
                    break;
                case "Int64":
                    result = pre + "Long";
                    break;
                case "Int16":
                    result = pre + "Short";
                    break;
                case "Boolean":
                    result = pre + "Bool";
                    break;
                case "UInt16":
                    result = pre + "UShort";
                    break;
                case "UInt32":
                    result = pre + "UInt";
                    break;
            }

            return result;
        }
    }

    public class FiledPropObject
    {
        public string type;
        public string name;
        public int offset;
    }
}
