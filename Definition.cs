﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS426.analysis
{
    public abstract class Definition
    {
        public string Name;

        public string toString()
        {
            return Name;
        }
    }

    public abstract class TypeDefinition : Definition { }

    public class NumberDefinition : TypeDefinition { }

    public class StringDefinition : TypeDefinition { }

    public class DoubleDefinition : TypeDefinition { }

    public class BooleanDefinition : TypeDefinition { }

    public class VariableDefinition : Definition 
    { 
        public TypeDefinition Type;
    }

    public class FunctionDefinition : Definition
    {
        public List<VariableDefinition> parameters;
        public TypeDefinition ReturnType;
    }

}
