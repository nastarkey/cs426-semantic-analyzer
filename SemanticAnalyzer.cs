﻿using CS426.node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
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

        // High scope temporary array fopr formal params
        private List<VariableDefinition> tempFormalDeclarations = new List<VariableDefinition>();
        private List<Definition> tempParams = new List<Definition>();
        private List<VariableDefinition> tempFuncParams = new List<VariableDefinition>();

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


            if (!LocalSymbolTable.TryGetValue(varName, out varDef) && !GlobalSymbolTable.TryGetValue(varName, out varDef))
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

            if (!DecoratedParseTree.TryGetValue(node.GetOperand(), out operandDef))
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
            else if (!(exp3Def is NumberDefinition) && !(exp3Def is DoubleDefinition))
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

            if (!DecoratedParseTree.TryGetValue(node.GetExp1(), out exp1Def))
            {
                //Higher level error
            }
            // see if it is already in tree like above
            else if (!DecoratedParseTree.TryGetValue(node.GetExp2(), out exp2Def))
            {
                //Error would be printed at lower level
            }
            // check to make sure it is infront of a number or double
            else if (!(exp2Def is NumberDefinition) && !(exp2Def is DoubleDefinition))
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

            if (!DecoratedParseTree.TryGetValue(node.GetExp1(), out exp1Def))
            {
                //Higher level error
            }
            // see if it is already in tree like above
            else if (!DecoratedParseTree.TryGetValue(node.GetExp2(), out exp2Def))
            {
                //Error would be printed at lower level
            }
            // check to make sure it is infront of a number or double
            else if (!(exp2Def is NumberDefinition) && !(exp2Def is DoubleDefinition))
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

            if (!DecoratedParseTree.TryGetValue(node.GetExp0(), out exp0Def))
            {
                //Error would be printed at lower level
            }
            // see if it is already in tree like above
            else if (!DecoratedParseTree.TryGetValue(node.GetExp1(), out exp1Def))
            {
                //Error would be printed at lower level
            }
            // check to make sure it is infront of a number or double
            else if (!(exp1Def is NumberDefinition) && !(exp1Def is DoubleDefinition))
            {
                PrintWarning(node.GetPlus(), "Only a number can be added!");
            }
            else if (exp1Def.GetType() != exp0Def.GetType())
            {
                PrintWarning(node.GetPlus(), "Cannot add " + exp1Def.Name + " by " + exp0Def.Name);
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

            if (!DecoratedParseTree.TryGetValue(node.GetExp0(), out exp0Def))
            {
                // Error would be printed at lower level
            }
            // see if it is already in tree like above
            else if (!DecoratedParseTree.TryGetValue(node.GetExp1(), out exp1Def))
            {
                // Error would be printed at lower level
            }
            // check to make sure it is infront of a number or double
            else if (!(exp1Def is NumberDefinition) && !(exp1Def is DoubleDefinition))
            {
                PrintWarning(node.GetMinus(), "Only a number can be subtracted!");
            }
            else if (exp1Def.GetType() != exp0Def.GetType())
            {
                PrintWarning(node.GetMinus(), "Cannot subtract " + exp1Def.Name + " by " + exp0Def.Name);
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

        public override void OutALtCompExpLtgt(ALtCompExpLtgt node)
        {
            int leftDefType;
            int rightDefType;
            Definition leftDef;
            Definition rightDef;

            // make sure only cmp numbers
            if (!DecoratedParseTree.TryGetValue(node.GetLeft(), out leftDef))
            {
                // error would be printed at lower level
            }
            else if (!DecoratedParseTree.TryGetValue(node.GetRight(), out rightDef))
            {
                // error would be printed at lower level
            }
            else
            {
                if (leftDef is NumberDefinition || leftDef is DoubleDefinition)
                {
                    leftDefType = leftDef is NumberDefinition? 1 : 0;
                }
                else
                {
                    PrintWarning(node.GetLt(), "Left operator is not of type int or double.");
                    return;
                }

                if (rightDef is NumberDefinition || rightDef is DoubleDefinition)
                {
                    rightDefType = rightDef is NumberDefinition ? 1 : 0;
                }
                else
                {
                    PrintWarning(node.GetLt(), "Right operator is not of type int or double.");
                    return;
                }

                if ((rightDefType + leftDefType) % 2 != 0) //idk why this works
                {
                    PrintWarning(node.GetLt(), "Cannot compare different types.");
                }
                else
                {
                    BooleanDefinition newDef = new BooleanDefinition();
                    newDef.Name = leftDef.Name;
                    DecoratedParseTree.Add(node, (Definition)newDef);
                }
            }
        }

        public override void OutAGtCompExpLtgt(AGtCompExpLtgt node)
        {
            int leftDefType;
            int rightDefType;
            Definition leftDef;
            Definition rightDef;

            // make sure only cmp numbers
            if (!DecoratedParseTree.TryGetValue(node.GetLeft(), out leftDef))
            {
                // error would be printed at lower level
            }
            else if (!DecoratedParseTree.TryGetValue(node.GetRight(), out rightDef))
            {
                // error would be printed at lower level
            }
            else
            {
                if (leftDef is NumberDefinition || leftDef is DoubleDefinition)
                {
                    leftDefType = leftDef is NumberDefinition ? 1 : 0;
                }
                else
                {
                    PrintWarning(node.GetGt(), "Left operator is not of type int or double.");
                    return;
                }

                if (rightDef is NumberDefinition || rightDef is DoubleDefinition)
                {
                    rightDefType = rightDef is NumberDefinition ? 1 : 0;
                }
                else
                {
                    PrintWarning(node.GetGt(), "Right operator is not of type int or double.");
                    return;
                }

                if ((rightDefType + leftDefType) % 2 != 0) //idk why this works
                {
                    PrintWarning(node.GetGt(), "Cannot compare different types.");
                }
                else
                {
                    BooleanDefinition newDef = new BooleanDefinition();
                    newDef.Name = leftDef.Name;
                    DecoratedParseTree.Add(node, (Definition)newDef);
                }
            }
        }

        public override void OutALteCompExpLtgt(ALteCompExpLtgt node)
        {
            int leftDefType;
            int rightDefType;
            Definition leftDef;
            Definition rightDef;

            // make sure only cmp numbers
            if (!DecoratedParseTree.TryGetValue(node.GetLeft(), out leftDef))
            {
                // error would be printed at lower level
            }
            else if (!DecoratedParseTree.TryGetValue(node.GetRight(), out rightDef))
            {
                // error would be printed at lower level
            }
            else
            {
                if (leftDef is NumberDefinition || leftDef is DoubleDefinition)
                {
                    leftDefType = leftDef is NumberDefinition ? 1 : 0;
                }
                else
                {
                    PrintWarning(node.GetLte(), "Left operator is not of type int or double.");
                    return;
                }

                if (rightDef is NumberDefinition || rightDef is DoubleDefinition)
                {
                    rightDefType = rightDef is NumberDefinition ? 1 : 0;
                }
                else
                {
                    PrintWarning(node.GetLte(), "Right operator is not of type int or double.");
                    return;
                }

                if ((rightDefType + leftDefType) % 2 != 0) //idk why this works
                {
                    PrintWarning(node.GetLte(), "Cannot compare different types.");
                }
                else
                {
                    BooleanDefinition newDef = new BooleanDefinition();
                    newDef.Name = leftDef.Name;
                    DecoratedParseTree.Add(node, (Definition)newDef);
                }
            }
        }

        public override void OutAGteCompExpLtgt(AGteCompExpLtgt node)
        {
            int leftDefType;
            int rightDefType;
            Definition leftDef;
            Definition rightDef;

            // make sure only cmp numbers
            if (!DecoratedParseTree.TryGetValue(node.GetLeft(), out leftDef))
            {
                // error would be printed at lower level
            }
            else if (!DecoratedParseTree.TryGetValue(node.GetRight(), out rightDef))
            {
                // error would be printed at lower level
            }
            else
            {
                if (leftDef is NumberDefinition || leftDef is DoubleDefinition)
                {
                    leftDefType = leftDef is NumberDefinition ? 1 : 0;
                }
                else
                {
                    PrintWarning(node.GetGte(), "Left operator is not of type int or double.");
                    return;
                }

                if (rightDef is NumberDefinition || rightDef is DoubleDefinition)
                {
                    rightDefType = rightDef is NumberDefinition ? 1 : 0;
                }
                else
                {
                    PrintWarning(node.GetGte(), "Right operator is not of type int or double.");
                    return;
                }

                if ((rightDefType + leftDefType) % 2 != 0) //idk why this works
                {
                    PrintWarning(node.GetGte(), "Cannot compare different types.");
                }
                else
                {
                    BooleanDefinition newDef = new BooleanDefinition();
                    newDef.Name = leftDef.Name;
                    DecoratedParseTree.Add(node, (Definition)newDef);
                }
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

        public override void OutAEqCompExpEq(AEqCompExpEq node)
        {
            int leftDefType;
            int rightDefType;
            Definition leftDef;
            Definition rightDef;

            // make sure only cmp numbers
            if (!DecoratedParseTree.TryGetValue(node.GetLeft(), out leftDef))
            {
                // error would be printed at lower level
            }
            else if (!DecoratedParseTree.TryGetValue(node.GetRight(), out rightDef))
            {
                // error would be printed at lower level
            }
            else
            {
                if (leftDef is NumberDefinition || leftDef is DoubleDefinition)
                {
                    leftDefType = leftDef is NumberDefinition ? 1 : 0;
                }
                else
                {
                    PrintWarning(node.GetEq(), "Left operator is not of type int or double.");
                    return;
                }

                if (rightDef is NumberDefinition || rightDef is DoubleDefinition)
                {
                    rightDefType = rightDef is NumberDefinition ? 1 : 0;
                }
                else
                {
                    PrintWarning(node.GetEq(), "Right operator is not of type int or double.");
                    return;
                }

                if ((rightDefType + leftDefType) % 2 != 0) //idk why this works
                {
                    PrintWarning(node.GetEq(), "Cannot compare different types.");
                }
                else
                {
                    BooleanDefinition newDef = new BooleanDefinition();
                    newDef.Name = leftDef.Name;
                    DecoratedParseTree.Add(node, (Definition)newDef);
                }
            }
        }

        public override void OutANeqCompExpEq(ANeqCompExpEq node)
        {
            int leftDefType;
            int rightDefType;
            Definition leftDef;
            Definition rightDef;

            // make sure only cmp numbers
            if (!DecoratedParseTree.TryGetValue(node.GetLeft(), out leftDef))
            {
                // error would be printed at lower level
            }
            else if (!DecoratedParseTree.TryGetValue(node.GetRight(), out rightDef))
            {
                // error would be printed at lower level
            }
            else
            {
                if (leftDef is NumberDefinition || leftDef is DoubleDefinition)
                {
                    leftDefType = leftDef is NumberDefinition ? 1 : 0;
                }
                else
                {
                    PrintWarning(node.GetNeq(), "Left operator is not of type int or double.");
                    return;
                }

                if (rightDef is NumberDefinition || rightDef is DoubleDefinition)
                {
                    rightDefType = rightDef is NumberDefinition ? 1 : 0;
                }
                else
                {
                    PrintWarning(node.GetNeq(), "Right operator is not of type int or double.");
                    return;
                }

                if ((rightDefType + leftDefType) % 2 != 0) //idk why this works
                {
                    PrintWarning(node.GetNeq(), "Cannot compare different types.");
                }
                else
                {
                    BooleanDefinition newDef = new BooleanDefinition();
                    newDef.Name = leftDef.Name;
                    DecoratedParseTree.Add(node, (Definition)newDef);
                }
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

        public override void OutANotNotExp(ANotNotExp node)
        {
            Definition compDef;
            Definition notDef;

            if (!DecoratedParseTree.TryGetValue(node.GetCompExpEq(), out compDef))
            {
                // handled lower level
            }
            else if (!(compDef is BooleanDefinition))
            {
                PrintWarning(node.GetNot(), "Can only invert boolean types");
            }
            else
            {
                DecoratedParseTree.Add(node, compDef);
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

        public override void OutAAndAndExp(AAndAndExp node)
        {
            Definition andDef;
            Definition notDef;

            if (!DecoratedParseTree.TryGetValue(node.GetAndExp(), out andDef))
            {
                // handled lower level
            }
            else if (!DecoratedParseTree.TryGetValue(node.GetNotExp(), out notDef))
            {
                // handled lower level
            }
            else if (!(notDef is BooleanDefinition) && !(andDef is DoubleDefinition))
            {
                PrintWarning(node.GetAnd(), "Can only union boolean types");
            }
            else
            {
                DecoratedParseTree.Add(node, andDef);
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

        public override void OutAOrOrExp(AOrOrExp node)
        {
            Definition andDef;
            Definition orDef;

            if (!DecoratedParseTree.TryGetValue(node.GetAndExp(), out andDef))
            {
                // handled lower level
            }
            else if (!DecoratedParseTree.TryGetValue(node.GetOrExp(), out orDef))
            {
                // handled lower level
            }
            else if (!(orDef is BooleanDefinition) && !(andDef is DoubleDefinition))
            {
                PrintWarning(node.GetOr(), "Can only or boolean types");
            }
            else
            {
                DecoratedParseTree.Add(node, andDef);
            }
        }

        // --------------------------------------
        // while statement
        // --------------------------------------
        public override void OutAWhileStatement(AWhileStatement node)
        {
            Definition orExp;

            if (!DecoratedParseTree.TryGetValue(node.GetOrExp(), out orExp))
            {
                // handled earlier
            }
            else if ( !(orExp is BooleanDefinition))
            {
                PrintWarning(node.GetWhile(), "Expression must be boolean for a while loop");
            }
        }


        // --------------------------------------
        // else statement
        // --------------------------------------
        public override void OutAElseStatement(AElseStatement node)
        {
            Definition orExp;

            if (!DecoratedParseTree.TryGetValue(node.GetOrExp(), out orExp))
            {
                // handled earlier
            }
            else if ( !(orExp is BooleanDefinition))
            {
                PrintWarning(node.GetElse(), "Expression must be boolean for a while loop");
            }
        }

        // --------------------------------------
        // elif statement
        // --------------------------------------
        //public override void OutAElifStatement(AElifStatement node)
        //{
        //    // depreciated
        //}

        // --------------------------------------
        // if statement
        // --------------------------------------
        public override void OutAIfStatement(AIfStatement node)
        {
            Definition orExp;

            if (!DecoratedParseTree.TryGetValue(node.GetOrExp(), out orExp))
            {
                // handled earlier
            }
            else if (!(orExp is BooleanDefinition))
            {
                PrintWarning(node.GetIf(), "Expression must be boolean for a while loop");
            }
        }

        // --------------------------------------
        // assign statement
        // --------------------------------------
        public override void OutAValueAssignStatement(AValueAssignStatement node)
        {
            Definition idDef;
            Definition expDef;
            Definition constDef;

            if (GlobalSymbolTable.TryGetValue(node.GetId().Text, out constDef))
            {
                PrintWarning(node.GetId(), "Cannot assign value to a constant");
            }
            else if (!LocalSymbolTable.TryGetValue(node.GetId().Text, out idDef))
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
        public override void OutASingleParamParameters(ASingleParamParameters node)
        {
            Definition tempDef;

            if (DecoratedParseTree.TryGetValue(node.GetOrExp(), out tempDef))
            {
                tempParams.Add(tempDef);
            }
            else
            {
                // Nothing?
            }
        }

        public override void OutAMultipleParamsParameters(AMultipleParamsParameters node)
        {
            Definition tempDef;

            if (DecoratedParseTree.TryGetValue(node.GetOrExp(), out tempDef))
            {
                tempParams.Add(tempDef);
                
            }
        }

        // --------------------------------------
        // formal parameters
        // --------------------------------------
        public override void InAMultipleParamsFormalParameters(AMultipleParamsFormalParameters node)
        {
            Definition newVarExpression = new VariableDefinition();
            if (GlobalSymbolTable.TryGetValue(node.GetParam().Text, out newVarExpression))
            {
                PrintWarning(node.GetParam(), node.GetParam().Text + "Has already been declared at a higher scope.");
            }
            else
            {
                newVarExpression = new VariableDefinition();
                Definition newTypeExpression;

                for (int i = 0; i < tempFormalDeclarations.Count; i++)
                {
                    if (tempFormalDeclarations[i].Name == node.GetParam().Text)
                    {
                        PrintWarning(node.GetParam(), node.GetParam().Text + " has already been declared as a parameter.");
                        return;
                    }
                }


                if (!GlobalSymbolTable.TryGetValue(node.GetType().Text, out newTypeExpression))
                {
                    PrintWarning(node.GetType(), "Type " + node.GetType().Text + " does not exist!");
                }
                else if (!(newTypeExpression is TypeDefinition))
                {
                    PrintWarning(node.GetType(), "Identifier " + node.GetType().Text + " is not a recognized data type!");
                }
                else
                {
                    newVarExpression = new VariableDefinition();
                    newVarExpression.Name = node.GetParam().Text;
                    ((VariableDefinition)newVarExpression).Type = (TypeDefinition)newTypeExpression;
                    tempFormalDeclarations.Add((VariableDefinition) newVarExpression);
                }
            }
        }

        public override void OutASingleParamFormalParameters(ASingleParamFormalParameters node)
        {
            Definition newVarExpression;
            if (GlobalSymbolTable.TryGetValue(node.GetParam().Text, out newVarExpression))
            {
                PrintWarning(node.GetParam(), node.GetParam().Text + "Has already been declared at a higher scope.");
            }
            else
            {
                newVarExpression = new VariableDefinition();
                Definition newTypeExpression;

                for (int i = 0; i < tempFormalDeclarations.Count; i++)
                {
                    // Console.WriteLine(tempFormalDeclarations[i].Name + " " + node.GetParam().Text);
                    if (tempFormalDeclarations[i].Name == node.GetParam().Text)
                    {
                        PrintWarning(node.GetParam(), node.GetParam().Text + " has already been declared as a parameter.");
                        return;
                    }
                }

                if (!GlobalSymbolTable.TryGetValue(node.GetType().Text, out newTypeExpression))
                {
                    PrintWarning(node.GetType(), "Type " + node.GetType().Text + " does not exist!");
                }
                else if (!(newTypeExpression is TypeDefinition))
                {
                    PrintWarning(node.GetType(), "Identifier " + node.GetType().Text + " is not a recognized data type!");
                }
                else
                {
                    newVarExpression = new VariableDefinition();
                    newVarExpression.Name = node.GetParam().Text;
                    ((VariableDefinition) newVarExpression).Type = (TypeDefinition)newTypeExpression;
                    tempFormalDeclarations.Add((VariableDefinition)newVarExpression);
                }

                foreach (VariableDefinition variable in tempFormalDeclarations)
                {
                    Definition tempDef = variable;
                    string id = variable.Name;
                    LocalSymbolTable[id] = variable;
                }
            }
        }

        // --------------------------------------
        // function call statment
        // --------------------------------------

        public override void InAFunctionCallStatement(AFunctionCallStatement node)
        {
            // store function param list temporarily for param checks
            Definition funcDef;
            if (GlobalSymbolTable.TryGetValue(node.GetFuncname().Text, out funcDef))
            {
                tempFuncParams = ((FunctionDefinition)funcDef).parameters;
                tempFuncParams.Reverse();
            }
        }

        public override void OutAFunctionCallStatement(AFunctionCallStatement node)
        {
            Definition idDef;
            Definition tempDef;

            if (!GlobalSymbolTable.TryGetValue(node.GetFuncname().Text, out idDef))
            {
                PrintWarning(node.GetFuncname(), "Identifier " + node.GetFuncname().Text + " does not exist");
            }
            else if (!(idDef is FunctionDefinition)) 
            {
                PrintWarning(node.GetFuncname(), "Identifier " + node.GetFuncname().Text + " is not a function");
            }
            else
            {
                //make sure there is the same number of params
                if (tempParams.Count != tempFuncParams.Count)
                {
                    PrintWarning(node.GetFuncname(), "Expected " + tempFuncParams.Count.ToString() + " params, but was given " + tempParams.Count.ToString());
                }
                else
                {
                    for (int i = 0; i < tempParams.Count; i++)
                    {
                        if (!LocalSymbolTable.TryGetValue(tempParams[i].Name, out tempDef) && !GlobalSymbolTable.TryGetValue(tempParams[i].Name, out tempDef))
                        {
                            PrintWarning(node.GetFuncname(), "Parameter " + tempParams[i].Name + " not defined!");

                        }
                        else if (tempParams[i].Name != tempFuncParams[i].Type.Name)
                        {
                            PrintWarning(node.GetFuncname(), "Parameter (" + i.ToString() + ") " + tempParams[i].Name + " is an incorrect type, should be " + tempFuncParams[i].Type.Name + "!");
                        }
                    }
                }
                
            }
        }

        // --------------------------------------
        // function declaration statement
        // --------------------------------------
        public override void InAWithoutPromiseFunctionDeclarationStatement(AWithoutPromiseFunctionDeclarationStatement node)
        {
            Definition idDef;

            if (GlobalSymbolTable.TryGetValue(node.GetFuncname().Text, out idDef))
            {
                PrintWarning(node.GetFuncname(), "Identifier " + node.GetFuncname().Text + " already exists!");
            }
            else
            {


                tempFormalDeclarations = new List<VariableDefinition>();
                LocalSymbolTable = new Dictionary<string, Definition>();

                // Register the new function definition ion the global table
                FunctionDefinition newFuncDef = new FunctionDefinition();

                newFuncDef.Name = node.GetFuncname().Text;
                newFuncDef.parameters = tempFormalDeclarations;


                GlobalSymbolTable.Add(node.GetFuncname().Text, newFuncDef);
            }
        }

        public override void OutAWithoutPromiseFunctionDeclarationStatement(AWithoutPromiseFunctionDeclarationStatement node)
        {
            LocalSymbolTable = new Dictionary<string, Definition>();
            Definition newFuncDef;

            if (GlobalSymbolTable.TryGetValue(node.GetFuncname().Text, out newFuncDef))
            {
                ((FunctionDefinition)newFuncDef).parameters = tempFormalDeclarations;
                GlobalSymbolTable[newFuncDef.Name] = newFuncDef;
            }

        }

        public override void InAWithPromiseFunctionDeclarationStatement(AWithPromiseFunctionDeclarationStatement node)
        {
            Definition idDef;
            Definition typeDef;

            if (GlobalSymbolTable.TryGetValue(node.GetFuncname().Text, out idDef))
            {
                PrintWarning(node.GetFuncname(), "Identifier " + node.GetFuncname().Text + " already exists!");
            } 
            else if ( !GlobalSymbolTable.TryGetValue(node.GetType().Text, out typeDef))
            {
                PrintWarning(node.GetType(), "Type " + node.GetType().Text + " is not a valid type!");
            }
            else
            {


                tempFormalDeclarations = new List<VariableDefinition>();
                LocalSymbolTable = new Dictionary<string, Definition>();

                // Register the new function definition ion the global table
                FunctionDefinition newFuncDef = new FunctionDefinition();

                newFuncDef.Name = node.GetFuncname().Text;
                newFuncDef.parameters = tempFormalDeclarations;
                newFuncDef.ReturnType = (TypeDefinition)typeDef;

                GlobalSymbolTable.Add(node.GetFuncname().Text, newFuncDef);
            }
        }

        public override void OutAWithPromiseFunctionDeclarationStatement(AWithPromiseFunctionDeclarationStatement node)
        {
            LocalSymbolTable = new Dictionary<string, Definition>();
            Definition newFuncDef;

            if (GlobalSymbolTable.TryGetValue(node.GetFuncname().Text, out newFuncDef))
            {
                ((FunctionDefinition)newFuncDef).parameters = tempFormalDeclarations;
                GlobalSymbolTable[newFuncDef.Name] = newFuncDef;
            }
        }

        // --------------------------------------
        // main function call
        // --------------------------------------


        // --------------------------------------
        // constant declare statment
        // --------------------------------------
        public override void OutAConstConstantDeclareStatement(AConstConstantDeclareStatement node)
        {
            Definition typeDef;
            Definition idDef;
            Definition constDef;
            Definition valDef;

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
            else if (GlobalSymbolTable.TryGetValue(node.GetVarname().Text, out constDef))
            {
                PrintWarning(node.GetVarname(), "Identifier " + node.GetVarname().Text + " is already being used!");
            }
            else if (!DecoratedParseTree.TryGetValue(node.GetOrExp(), out valDef))
            {
                //Higher order error
            }
            else if (valDef.Name != typeDef.Name)
            {
                PrintWarning(node.GetType(), "Variable type " + typeDef.Name + " and expression type " + valDef.Name + " don't match.");
            }
            else
            {
                VariableDefinition newVarDef = new VariableDefinition();
                newVarDef.Name = node.GetVarname().Text;
                newVarDef.Type = (TypeDefinition)typeDef;

                GlobalSymbolTable.Add(node.GetVarname().Text, newVarDef);
            }
        }

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
