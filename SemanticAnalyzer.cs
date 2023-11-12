using CS426.node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CS426.analysis
{
    class SemanticAnalyzer : DepthFirstAdapter
    {
        //Global Symbol Table
        Dictionary<string, Definition> GlobalSymbolTable = new Dictionary<string, Definition>();

        //Local Symbol Table
        Dictionary<string, Definition> LocalSymbolTable = new Dictionary<string, Definition>();

        //Decorated Parse Tree
        Dictionary<Node, Definition> DecoratedParseTree = new Dictionary<Node, Definition>();

        public override void InAProgram(AProgram node)
        {
            Definition intDef = new NumberDefinition();
            intDef.Name = "int";

            Definition doubleDef = new DoubleDefinition();
            doubleDef.Name = "double";

            Definition strDef = new StringDefinition();
            strDef.Name = "string";

            GlobalSymbolTable.Add("int", intDef);
            GlobalSymbolTable.Add("double", doubleDef);
            GlobalSymbolTable.Add("string", strDef);

            Console.WriteLine("Program Entered");

        }

        public void PrintWarning(Token t, String message)
        {
            Console.WriteLine("Line " + t.Line + "Col " + t.Pos + ": " + message);
        }

        // --------------------------------------
        // OPERANDS
        // --------------------------------------
        public override void OutAIntOperand(AIntOperand node)
        {
            Definition intDef = new NumberDefinition();
            intDef.Name = "int";

            DecoratedParseTree.Add(node, intDef);
        }

        public override void OutADoubleOperand(ADoubleOperand node)
        {
            Definition doubleDef = new DoubleDefinition();
            doubleDef.Name = "double";

            DecoratedParseTree.Add(node, doubleDef);
        }

        public override void OutAStringOperand(AStringOperand node)
        {
            Definition stringDef = new StringDefinition();
            stringDef.Name = "string";

            DecoratedParseTree.Add(node, stringDef);
        }

        public override void OutAVariableOperand(AVariableOperand node)
        {
            string varName = node.GetId().Text;
            Definition varDef;

            if(!LocalSymbolTable.TryGetValue(varName, out varDef))
            {
                PrintWarning(node.GetId(), varName + " does not exist!");
            }
            else if (!(varDef is VariableDefinition))
            {
                PrintWarning(node.GetId(), varName + " is not a variable!");
            }
            else
            {
                VariableDefinition v = (VariableDefinition)varDef;
                DecoratedParseTree.Add(node, v.Type);
            }
        }



        // --------------------------------------
        // Expression 3
        // --------------------------------------
        public override void OutAPassExp3(APassExp3 node)
        {
            Definition operandDef;

            if(!DecoratedParseTree.TryGetValue(node.GetOperand(), out operandDef))
            {
                //Error would be printed at lower level
            }
            else
            {
                DecoratedParseTree.Add(node, operandDef);
            }

        }

        public override void OutAParenthesisExp3(AParenthesisExp3 node)
        {
            
        }
    }
}
