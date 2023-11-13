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
            Console.WriteLine("Line " + t.Line + " Col " + t.Pos + ": " + message);
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
        // idk if I really need to check anything for parenthesis
        public override void OutAParenthesisExp3(AParenthesisExp3 node)
        {
            // Need to do
            Definition operandDef;

            if (!DecoratedParseTree.TryGetValue(node.GetOrExp(), out operandDef))
            {
                //Error would be printed at lower level
            }
        }

        // --------------------------------------
        // Expression 2 (38mins)
        // --------------------------------------
        public override void OutAPassExp2(APassExp2 node)
        {
            Definition exp3Def;

            if (!DecoratedParseTree.TryGetValue(node.GetExp3(), out exp3Def))
            {
                //Error would be printed at lower level
            }
            else
            {
                DecoratedParseTree.Add(node, exp3Def);
            }
        }

        public override void OutANegativeExp2(ANegativeExp2 node)
        {
            Definition exp3Def;

            // see if it is already in tree like above
            if (!DecoratedParseTree.TryGetValue(node.GetExp3(), out exp3Def))
            {
                //Error would be printed at lower level
            }
            // check to make sure it is infront of a number or double
            else if (!(exp3Def is NumberDefinition) || !(exp3Def is DoubleDefinition))
            {
                PrintWarning(node.GetMinus(), "Only a number can be negated");
            }
            else
            {
                DecoratedParseTree.Add(node, exp3Def);
            }

        }

        // --------------------------------------
        // Expression 1
        // --------------------------------------
        public override void OutAPassExp1(APassExp1 node)
        {
            Definition exp2Def;

            if (!DecoratedParseTree.TryGetValue(node.GetExp2(), out exp2Def))
            {
                //Error would be printed at lower level
            }
            else
            {
                DecoratedParseTree.Add(node, exp2Def);
            }
        }



        // --------------------------------------
        // Expression 0
        // --------------------------------------
        public override void OutAPassExp0(APassExp0 node)
        {
            Definition exp1Def;

            if (!DecoratedParseTree.TryGetValue(node.GetExp1(), out exp1Def))
            {
                //Error would be printed at lower level
            }
            else
            {
                DecoratedParseTree.Add(node, exp1Def);
            }
        }



        // --------------------------------------
        // comp_exp_ltgt
        // --------------------------------------
        public override void OutAPassCompExpLtgt(APassCompExpLtgt node)
        {
            Definition exp0Def;

            if (!DecoratedParseTree.TryGetValue(node.GetExp0(), out exp0Def))
            {
                //Error would be printed at lower level
            }
            else
            {
                DecoratedParseTree.Add(node, exp0Def);
            }
        }



        // --------------------------------------
        // comp exp eq
        // --------------------------------------
        public override void OutAPassCompExpEq(APassCompExpEq node)
        {
            Definition compexpltgtDef;

            if (!DecoratedParseTree.TryGetValue(node.GetCompExpLtgt(), out compexpltgtDef))
            {
                //Error would be printed at lower level
            }
            else
            {
                DecoratedParseTree.Add(node, compexpltgtDef);
            }
        }



        // --------------------------------------
        // not exp
        // --------------------------------------
        public override void OutAPassNotExp(APassNotExp node)
        {
            Definition CompExpEqDef;

            if (!DecoratedParseTree.TryGetValue(node.GetCompExpEq(), out CompExpEqDef))
            {
                //Error would be printed at lower level
            }
            else
            {
                DecoratedParseTree.Add(node, CompExpEqDef);
            }
        }



        // --------------------------------------
        //  and exp
        // --------------------------------------
        public override void OutAPassAndExp(APassAndExp node)
        {
            Definition NotExpDef;

            if (!DecoratedParseTree.TryGetValue(node.GetNotExp(), out NotExpDef))
            {
                //Error would be printed at lower level
            }
            else
            {
                DecoratedParseTree.Add(node, NotExpDef);
            }
        }



        // --------------------------------------
        //  or exp
        // --------------------------------------
        public override void OutAPassOrExp(APassOrExp node)
        {
            Definition AndExpDef;

            if (!DecoratedParseTree.TryGetValue(node.GetAndExp(), out AndExpDef))
            {
                //Error would be printed at lower level
            }
            else
            {
                DecoratedParseTree.Add(node, AndExpDef);
            }
        }


    }
}
