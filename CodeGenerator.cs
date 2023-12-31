﻿using CS426.node;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS426.analysis
{
    class CodeGenerator : DepthFirstAdapter
    {
        StreamWriter _output;

        private int labelID = 0;

        private string GetNextLabelId()
        {
            string label = "label" + labelID.ToString();
            labelID++;
            return label;
        }

        public CodeGenerator( String outputFileName )
        {
            _output = new StreamWriter( outputFileName );
        }

        public void Write( string line )
        {
            Console.Write( line );
            _output.Write(line);
        }

        public void WriteLine ( string line )
        {
            Console.WriteLine( line );
            _output.WriteLine( line );
        }

        public override void InAProgram(AProgram node)
        {
            WriteLine(".assembly extern mscorlib {}");
            WriteLine(".assembly funprogram");
            WriteLine("{\n\t.ver 1:0:1:0\n}");
        }

        public override void OutAProgram(AProgram node)
        {
            _output.Close();
            Console.WriteLine("\n\n");
        }

        public override void InAWithoutPromiseFunctionDeclarationStatement(AWithoutPromiseFunctionDeclarationStatement node)
        {
            WriteLine(".method static void " + node.GetFuncname().Text + "() cil managed");
            WriteLine("{\n\t.maxstack 128");
   
            WriteLine("\t// Function Code Goes Here");
        }

        public override void OutAWithoutPromiseFunctionDeclarationStatement(AWithoutPromiseFunctionDeclarationStatement node)
        {
            WriteLine("\tret\n}\n");
        }

        public override void InANoParamMainFunctionCall(ANoParamMainFunctionCall node)
        {
            WriteLine(".method static void main() cil managed");
            WriteLine("{\n\t.maxstack 128");
            WriteLine("\t.entrypoint\n");
            WriteLine("\t// Main Code Goes Here");
        }

        public override void OutANoParamMainFunctionCall(ANoParamMainFunctionCall node)
        {
            WriteLine("\tret\n}\n");
        }

        public override void OutANoAssignDeclareStatement(ANoAssignDeclareStatement node)
        {
            WriteLine("\t// Declaring Variable " + node.GetVarname().ToString());
            Write("\t.locals init(");

            if(node.GetType().Text == "int")
            {
                Write("int32 ");
            } else if ( node.GetType().Text == "double") 
            {
                Write("float32 ");
            } else
            {
                Write(node.GetType().Text + " ");
            }
            WriteLine(node.GetVarname().Text + ")\n");
        }

        public override void OutAIntOperand(AIntOperand node)
        {
            WriteLine("\tldc.i4 " + node.GetInteger().Text);
        }

        public override void OutAStringOperand(AStringOperand node)
        {
            WriteLine("\tldstr " + node.GetString().Text);
        }

        public override void OutADoubleOperand(ADoubleOperand node)
        {
            WriteLine("\tldc.r8 " + node.GetDouble().Text);
        }

        public override void OutAVariableOperand(AVariableOperand node)
        {
            WriteLine("\tldloc " + node.GetId().Text);
        }

        public override void OutAValueAssignStatement(AValueAssignStatement node)
        {
            WriteLine("\tstloc " + node.GetId().Text + "\n");
        }

        public override void OutAAddExp0(AAddExp0 node)
        {
            WriteLine("\tadd");
        }

        public override void OutASubExp0(ASubExp0 node)
        {
            WriteLine("\tsub");
        }

        public override void OutAMultiplyExp1(AMultiplyExp1 node)
        {
            WriteLine("\tmul");
        }

        public override void OutADivideExp1(ADivideExp1 node)
        {
            WriteLine("\tdiv");
        }

        public override void OutANegativeExp2(ANegativeExp2 node)
        {
            WriteLine("\tneg");
        }

        public override void OutAFunctionCallStatement(AFunctionCallStatement node)
        {
            if (node.GetFuncname().Text == "printInt")
            {
                WriteLine("\tcall void [mscorlib]System.Console::Write(int32)");
            }
            else if (node.GetFuncname().Text == "printDouble")
            {
                WriteLine("\tcall void [mscorlib]System.Console::Write(float32)");
            }
            else if (node.GetFuncname().Text == "printString")
            {
                WriteLine("\tcall void [mscorlib]System.Console::Write(string)");
            }
            else if (node.GetFuncname().Text == "printLine")
            {
                WriteLine("\tldstr \"\\n\"");
                WriteLine("\tcall void [mscorlib]System.Console::Write(string)");
            }
            else
            {
                WriteLine("\tcall void " + node.GetFuncname().Text + "()");
            }
        }

        public override void OutANotNotExp(ANotNotExp node)
        {
            WriteLine("\tldc.i4 0");
            WriteLine("\tceq");
        }

        public override void OutAAndAndExp(AAndAndExp node)
        {
            WriteLine("\tand");
        }

        public override void OutAOrOrExp(AOrOrExp node)
        {
            WriteLine("\tor");
        }

        //need to do comparisons now
        public override void CaseAIfStatement(AIfStatement node)
        {
            string labelTrue = GetNextLabelId();
            string labelCont = GetNextLabelId();
    
            InAIfStatement(node);
            if (node.GetIf() != null)
            {
                node.GetIf().Apply(this);
            }
            if (node.GetLeftParenthesis() != null)
            {
                node.GetLeftParenthesis().Apply(this);
            }
            if (node.GetOrExp() != null)
            {
                node.GetOrExp().Apply(this);
            }
            if (node.GetRightParenthesis() != null)
            {
                node.GetRightParenthesis().Apply(this);
            }
            if (node.GetOpenBrace() != null)
            {
                node.GetOpenBrace().Apply(this);
            }
            if (node.GetStatements() != null)
            {
                WriteLine("\tbrtrue " + labelTrue);
                WriteLine("\tbr " + labelCont);
                WriteLine(labelTrue + ":");
                node.GetStatements().Apply(this);
            }
            if (node.GetCloseBrace() != null)
            {
                node.GetCloseBrace().Apply(this);
            }
            WriteLine(labelCont + ":");
            OutAIfStatement(node);           
        }


        public override void CaseAElseStatement(AElseStatement node)
        {
            string labelTrue = GetNextLabelId();
            string labelFalse = GetNextLabelId();
            string labelCont = GetNextLabelId();

            InAElseStatement(node);
            if (node.GetIf() != null)
            {
                node.GetIf().Apply(this);
            }
            if (node.GetLeftParenthesis() != null)
            {
                node.GetLeftParenthesis().Apply(this);
            }
            if (node.GetOrExp() != null)
            {
                node.GetOrExp().Apply(this);
            }
            if (node.GetRightParenthesis() != null)
            {
                node.GetRightParenthesis().Apply(this);
            }
            if (node.GetOne() != null)
            {
                node.GetOne().Apply(this);
            }
            if (node.GetTrue() != null)
            {
                WriteLine("\tbrtrue " + labelTrue);
                WriteLine("\tbr " + labelFalse);
                WriteLine(labelTrue + ":");
                node.GetTrue().Apply(this);
            }
            if (node.GetOnetwo() != null)
            {
                node.GetOnetwo().Apply(this);
            }
            if (node.GetElse() != null)
            {
                node.GetElse().Apply(this);
            }
            if (node.GetOpenBrace() != null)
            {
                node.GetOpenBrace().Apply(this);
            }
            if (node.GetFalse() != null)
            {
                WriteLine("br " + labelCont);
                WriteLine(labelFalse + ":");
                node.GetFalse().Apply(this);
            }
            if (node.GetCloseBrace() != null)
            {
                node.GetCloseBrace().Apply(this);
            }
            WriteLine(labelCont + ":");
            OutAElseStatement(node);
        }

        public override void CaseAWhileStatement(AWhileStatement node)
        {
            string labelTrue = GetNextLabelId();
            string labelFalse = GetNextLabelId();

            InAWhileStatement(node);
            if (node.GetWhile() != null)
            {
                node.GetWhile().Apply(this);
            }
            if (node.GetLeftParenthesis() != null)
            {
                node.GetLeftParenthesis().Apply(this);
            }
            if (node.GetOrExp() != null)
            {
                WriteLine(labelTrue + ":");
                node.GetOrExp().Apply(this);
            }
            if (node.GetRightParenthesis() != null)
            {
                node.GetRightParenthesis().Apply(this);
            }
            if (node.GetOpenBrace() != null)
            {
                node.GetOpenBrace().Apply(this);
            }
            if (node.GetStatements() != null)
            {
                WriteLine("\tbrfalse " + labelFalse);
                node.GetStatements().Apply(this);
                WriteLine("br " + labelTrue);
            }
            if (node.GetCloseBrace() != null)
            {
                node.GetCloseBrace().Apply(this);
            }
            WriteLine(labelFalse + ":");
            OutAWhileStatement(node);
        }

        public override void OutAEqCompExpEq(AEqCompExpEq node)
        {
            string lableTrue = GetNextLabelId();
            string lableFalse = GetNextLabelId();
            WriteLine("\tbeq " + lableTrue);
            WriteLine("\tldc.i4 0");
            WriteLine("\tbr " + lableFalse);
            WriteLine(lableTrue + ":");
            WriteLine("\tldc.i4 1");
            WriteLine(lableFalse + ":");

        }

        public override void OutALtCompExpLtgt(ALtCompExpLtgt node)
        {
            string lableTrue = GetNextLabelId();
            string lableFalse = GetNextLabelId();
            WriteLine("\tblt " + lableTrue);
            WriteLine("\tldc.i4 0");
            WriteLine("\tbr " + lableFalse);
            WriteLine(lableTrue + ":");
            WriteLine("\tldc.i4 1");
            WriteLine(lableFalse + ":");
        }

        public override void OutAGtCompExpLtgt(AGtCompExpLtgt node)
        {
            string lableTrue = GetNextLabelId();
            string lableFalse = GetNextLabelId();
            WriteLine("\tbgt " + lableTrue);
            WriteLine("\tldc.i4 0");
            WriteLine("\tbr " + lableFalse);
            WriteLine(lableTrue + ":");
            WriteLine("\tldc.i4 1");
            WriteLine(lableFalse + ":");
        }

        public override void OutAGteCompExpLtgt(AGteCompExpLtgt node)
        {
            string lableTrue = GetNextLabelId();
            string lableFalse = GetNextLabelId();
            WriteLine("\tbge " + lableTrue);
            WriteLine("\tldc.i4 0");
            WriteLine("\tbr " + lableFalse);
            WriteLine(lableTrue + ":");
            WriteLine("\tldc.i4 1");
            WriteLine(lableFalse + ":");
        }

        public override void OutALteCompExpLtgt(ALteCompExpLtgt node)
        {
            string lableTrue = GetNextLabelId();
            string lableFalse = GetNextLabelId();
            WriteLine("\tble " + lableTrue);
            WriteLine("\tldc.i4 0");
            WriteLine("\tbr " + lableFalse);
            WriteLine(lableTrue + ":");
            WriteLine("\tldc.i4 1");
            WriteLine(lableFalse + ":");
        }

        public override void OutANeqCompExpEq(ANeqCompExpEq node)
        {
            string lableTrue = GetNextLabelId();
            string lableFalse = GetNextLabelId();
            WriteLine("\tbne.un " + lableTrue);
            WriteLine("\tldc.i4 0");
            WriteLine("\tbr " + lableFalse);
            WriteLine(lableTrue + ":");
            WriteLine("\tldc.i4 1");
            WriteLine(lableFalse + ":");
        }

    }
}
