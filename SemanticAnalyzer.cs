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
            
            Definition orDef;

            if (!DecoratedParseTree.TryGetValue(node.GetOrExp(), out orDef))
            {
                //Error would be printed at lower level
            }
            else
            {
                DecoratedParseTree.Add(node, orDef);
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
                PrintWarning(node.GetMinus(), "Only a number can be negated!");
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

        public override void OutAMultiplyExp1(AMultiplyExp1 node)
        {
            Definition exp2Def;
            Definition exp1Def;

            DecoratedParseTree.TryGetValue(node.GetExp1(), out exp1Def);
            // see if it is already in tree like above
            if (!DecoratedParseTree.TryGetValue(node.GetExp2(), out exp2Def))
            {
                //Error would be printed at lower level
            }
            // check to make sure it is infront of a number or double
            else if (!(exp2Def is NumberDefinition) || !(exp2Def is DoubleDefinition))
            {
                PrintWarning(node.GetMult(), "Only a number can be multiplied!");
            }
            
            else if (exp1Def.GetType() != exp2Def.GetType()) 
            {
                PrintWarning(node.GetMult(), "Cannot multiply " + exp1Def.Name + " by " + exp2Def.Name);
            }
            else
            {
                DecoratedParseTree.Add(node, exp2Def);
            }
        }

        public override void OutADivideExp1(ADivideExp1 node)
        {
            Definition exp2Def;
            Definition exp1Def;

            DecoratedParseTree.TryGetValue(node.GetExp1(), out exp1Def);
            // see if it is already in tree like above
            if (!DecoratedParseTree.TryGetValue(node.GetExp2(), out exp2Def))
            {
                //Error would be printed at lower level
            }
            // check to make sure it is infront of a number or double
            else if (!(exp2Def is NumberDefinition) || !(exp2Def is DoubleDefinition))
            {
                PrintWarning(node.GetDiv(), "Only a number can be divided!");
            }

            else if (exp1Def.GetType() != exp2Def.GetType())
            {
                PrintWarning(node.GetDiv(), "Cannot divide " + exp1Def.Name + " by " + exp2Def.Name);
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

        public override void OutAAddExp0(AAddExp0 node)
        {
            Definition exp0Def;
            Definition exp1Def;

            DecoratedParseTree.TryGetValue(node.GetExp0(), out exp0Def);
            // see if it is already in tree like above
            if (!DecoratedParseTree.TryGetValue(node.GetExp1(), out exp1Def))
            {
                //Error would be printed at lower level
            }
            // check to make sure it is infront of a number or double
            else if (!(exp1Def is NumberDefinition) || !(exp1Def is DoubleDefinition))
            {
                PrintWarning(node.GetPlus(), "Only a number can be added!");
            }
            else if (exp0Def is NumberDefinition)
            {
                if (exp1Def is DoubleDefinition)
                {
                    PrintWarning(node.GetPlus(), "Can only add same types!");
                }
            }
            else if (exp0Def is DoubleDefinition)
            {
                if (exp1Def is NumberDefinition)
                {
                    PrintWarning(node.GetPlus(), "Can only add same types!");
                }
            }
            else
            {
                DecoratedParseTree.Add(node, exp1Def);
            }
        }

        public override void OutASubExp0(ASubExp0 node)
        {
            Definition exp0Def;
            Definition exp1Def;

            DecoratedParseTree.TryGetValue(node.GetExp0(), out exp0Def);
            // see if it is already in tree like above
            if (!DecoratedParseTree.TryGetValue(node.GetExp1(), out exp1Def))
            {
                //Error would be printed at lower level
            }
            // check to make sure it is infront of a number or double
            else if (!(exp1Def is NumberDefinition) || !(exp1Def is DoubleDefinition))
            {
                PrintWarning(node.GetMinus(), "Only a number can be subtracted!");
            }
            else if (exp0Def is NumberDefinition)
            {
                if (exp1Def is DoubleDefinition)
                {
                    PrintWarning(node.GetMinus(), "Can only subtract same types!");
                }
            }
            else if (exp0Def is DoubleDefinition)
            {
                if (exp1Def is NumberDefinition)
                {
                    PrintWarning(node.GetMinus(), "Can only subtract same types!");
                }
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

        // --------------------------------------
        // while statement
        // --------------------------------------



        // --------------------------------------
        // else statement
        // --------------------------------------


        // --------------------------------------
        // elif statement
        // --------------------------------------


        // --------------------------------------
        // if statement
        // --------------------------------------


        // --------------------------------------
        // assign statement
        // --------------------------------------
        public override void OutAValueAssignStatement(AValueAssignStatement node)
        {
            Definition idDef;
            Definition expDef;

            if(!LocalSymbolTable.TryGetValue(node.GetId().Text, out idDef))
            {
                PrintWarning(node.GetId(), "Identifier " + node.GetId().Text + " does not exist!");
            }
            else if (!(idDef is VariableDefinition))
            {
                PrintWarning(node.GetId(), "Identifier " + node.GetId().Text + " is not a variable!");
            }
            else if (!DecoratedParseTree.TryGetValue(node.GetOrExp(), out expDef))
            {
                // Error would be printed at lower level
            }
            else if (((VariableDefinition)idDef).Type.Name != expDef.Name)
            {
                PrintWarning(node.GetId(), "Variable type and expression type doesn't match!");
            }
            else
            {
                // dont need to do anything all cases done
            }
        }

        public override void OutAFunctionAssignStatement(AFunctionAssignStatement node)
        {
            Definition functionDef;
            Definition idDef;

            if (!LocalSymbolTable.TryGetValue(node.GetId().Text, out idDef))
            {
                PrintWarning(node.GetId(), "Identifier " + node.GetId().Text + " does not exist!");
            }
            else if (!(idDef is VariableDefinition))
            {
                PrintWarning(node.GetId(), "Identifier " + node.GetId().Text + " is not a variable!");
            }
            else if (!DecoratedParseTree.TryGetValue(node.GetFunctionCallStatement(), out functionDef))
            {
                // Error would be printed at lower level
            }
            else if (((VariableDefinition)idDef).Type.Name != ((FunctionDefinition)functionDef).ReturnType.Name)
            {
                PrintWarning(node.GetId(), "Variable type and function return type don't match!");
            }
            else
            {
                // dont need to do anything all cases done
            }
        }

        // --------------------------------------
        // parameters
        // --------------------------------------


        // --------------------------------------
        // formal parameters
        // --------------------------------------


        // --------------------------------------
        // function call statment
        // --------------------------------------


        // --------------------------------------
        // function declaration statement
        // --------------------------------------
        public override void OutAWithPromiseFunctionDeclarationStatement(AWithPromiseFunctionDeclarationStatement node)
        {
            
        }

        // --------------------------------------
        // main function call
        // --------------------------------------


        // --------------------------------------
        // constant declare statment
        // --------------------------------------


        // --------------------------------------
        // declare statement
        // --------------------------------------
        public override void OutANoAssignDeclareStatement(ANoAssignDeclareStatement node)
        {
            Definition typeDef;
            Definition idDef;

            if (!GlobalSymbolTable.TryGetValue(node.GetType().Text, out typeDef))
            {
                PrintWarning(node.GetType(), "Type " + node.GetType().Text + " does not exist!");
            }
            else if (!(typeDef is TypeDefinition))
            {
                PrintWarning(node.GetType(), "Identifier " + node.GetType().Text + " is not a recognized data type!");
            }
            else if (LocalSymbolTable.TryGetValue(node.GetVarname().Text, out idDef))
            {
                PrintWarning(node.GetVarname(), "Identifier " + node.GetVarname().Text + " is already being used!");
            }
            else
            {
                VariableDefinition newVarDef = new VariableDefinition();
                newVarDef.Name = node.GetVarname().Text;
                newVarDef.Type = (TypeDefinition)typeDef;

                LocalSymbolTable.Add(node.GetVarname().Text, newVarDef);
            }
        }

        public override void OutAAssignDeclareStatement(AAssignDeclareStatement node)
        {
            Definition typeDef;
            Definition idDef;
            Definition expDef;

            DecoratedParseTree.TryGetValue(node.GetOrExp(), out expDef);

            if (!GlobalSymbolTable.TryGetValue(node.GetType().Text, out typeDef))
            {
                PrintWarning(node.GetType(), "Type " + node.GetType().Text + " does not exist!");
            }
            else if (!(typeDef is TypeDefinition))
            {
                PrintWarning(node.GetType(), "Identifier " + node.GetType().Text + " is not a recognized data type!");
            }
            else if (LocalSymbolTable.TryGetValue(node.GetVarname().Text, out idDef))
            {
                PrintWarning(node.GetVarname(), "Identifier " + node.GetVarname().Text + " is already being used!");
            }
            else if (typeDef.Name != expDef.Name)
            {
                PrintWarning(node.GetType(), "Variable type and expression type don't match");
            }
            else
            {
                VariableDefinition newVarDef = new VariableDefinition();
                newVarDef.Name = node.GetVarname().Text;
                newVarDef.Type = (TypeDefinition)typeDef;

                LocalSymbolTable.Add(node.GetVarname().Text, newVarDef);
            }
        }

        public override void OutAAssignFunctionDeclareStatement(AAssignFunctionDeclareStatement node)
        {
            Definition typeDef;
            Definition idDef;
            Definition functionDef;

            DecoratedParseTree.TryGetValue(node.GetFunctionCallStatement(), out functionDef);

            if (!GlobalSymbolTable.TryGetValue(node.GetType().Text, out typeDef))
            {
                PrintWarning(node.GetType(), "Type " + node.GetType().Text + " does not exist!");
            }
            else if (!(typeDef is TypeDefinition))
            {
                PrintWarning(node.GetType(), "Identifier " + node.GetType().Text + " is not a recognized data type!");
            }
            else if (LocalSymbolTable.TryGetValue(node.GetVarname().Text, out idDef))
            {
                PrintWarning(node.GetVarname(), "Identifier " + node.GetVarname().Text + " is already being used!");
            }
            else if (((FunctionDefinition)functionDef).ReturnType.Name != typeDef.Name)
            {
                PrintWarning(node.GetType(), "Variable type and function type don't match");
            }
            else
            {
                VariableDefinition newVarDef = new VariableDefinition();
                newVarDef.Name = node.GetVarname().Text;
                newVarDef.Type = (TypeDefinition)typeDef;

                LocalSymbolTable.Add(node.GetVarname().Text, newVarDef);
            }
        }


        // --------------------------------------
        // 
        // --------------------------------------
    }
}
