using CS426.node;
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
    }
}
